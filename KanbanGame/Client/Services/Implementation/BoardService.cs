using KanbanGame.Shared;
using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http.Json;

namespace KanbanGame.Client.Services;

public class BoardService : IBoardService
{
    private int lastId;
    private readonly IKanbanTaskService _kanbanTaskService;
    private readonly IEmployeeService _employeeService;
    private readonly IFeatureService _featureService;
    private readonly HttpClient _http;

    public BoardService(
        HttpClient http,
        IKanbanTaskService kanbanTaskService,
        IEmployeeService employeeService,
        IFeatureService featureService
    )
    {
        _http = http;
        _kanbanTaskService = kanbanTaskService;
        _employeeService = employeeService;
        _featureService = featureService;
        ColumnMaxCount = new Dictionary<string, int>
        {
            { "Backlog", 9 },
            { "AnalysisDoing", 9 },
            { "DevelopmentDoing", 9 },
            { "TestDoing", 9 },
            { "Doing1", 9 },
            { "Doing2", 9 },
        }; // For SF Initialization
    }

    public List<Card> Cards { get; set; }
    public Dictionary<string, int> ColumnMaxCount { get; set; }
    private Dictionary<string, int> ColumnCount;
    private Dictionary<Employee, KanbanTask?> DevBoardBuild;
    private Dictionary<Employee, Feature?> FeatureBoardBuild;
    private List<KanbanTask> TempTasks;
    private List<Feature> TempFeatures;

    public async Task GetCardsByTeamId(int teamId)
    {
        InitProperties();

        await _kanbanTaskService.GetKanbanTasksByTeamId(teamId);
        await _employeeService.GetEmployeesByTeamId(teamId);
        if (teamId == (int)Team.TeamName.HighLevelAnalysis)
        {
            await _featureService.GetFeatures();
            await BuildFeatureBoard();
        }
        else
        {
            await BuildDevBoard();
        }
    }

    private async Task BuildDevBoard()
    {
        foreach (var e in _employeeService.Employees)
        {
            DevBoardBuild.Add(e, null);
            ColumnMaxCount[e.SF_Column] += 2;
        }

        foreach (var t in _kanbanTaskService.KanbanTasks)
        {
            if (t.Assignee is not null)
            {
                t.SF_Column = t.Assignee.SF_Column;
                var key = DevBoardBuild.Where(p => p.Key.Id == t.Assignee.Id).FirstOrDefault().Key;
                DevBoardBuild[key] = t;
            }
            else
            {
                TempTasks.Add(t);
            }
        }

        foreach (var (e, t) in DevBoardBuild)
        {
            Cards.Add(EmployeeToCard(e, ColumnCount[e.SF_Column]));
            ColumnCount[e.SF_Column] += 2;
            if (t is not null)
            {
                Cards.Add(TaskToCard(t, ColumnCount[e.SF_Column]));
                ColumnCount[e.SF_Column] += 2;
            }
        }
        foreach (var t in TempTasks)
        {
            var card = TaskToCard(t, ColumnCount[t.SF_Column]);
            var cardAbove = FindCardAbove(card);
            if (cardAbove is not null && cardAbove.Employee is not null)
            {
                card.KanbanTask.Assignee = cardAbove.Employee;
            }
            Cards.Add(card);
            ColumnCount[t.SF_Column] += 2;
        }
    }

    private async Task BuildFeatureBoard()
    {
        foreach (var e in _employeeService.Employees)
        {
            FeatureBoardBuild.Add(e, null);
            ColumnMaxCount[e.SF_Column] += 2;
        }

        foreach (var f in _featureService.Features)
        {
            if (f.Assignee is not null)
            {
                f.SF_Column = f.Assignee.SF_Column;
                var key = FeatureBoardBuild
                    .Where(p => p.Key.Id == f.Assignee.Id)
                    .FirstOrDefault()
                    .Key;
                FeatureBoardBuild[key] = f;
            }
            else
            {
                TempFeatures.Add(f);
            }
        }

        foreach (var (e, f) in FeatureBoardBuild)
        {
            Cards.Add(EmployeeToCard(e, ColumnCount[e.SF_Column]));
            ColumnCount[e.SF_Column] += 2;
            if (f is not null)
            {
                Cards.Add(FeatureToCard(f, ColumnCount[e.SF_Column]));
                ColumnCount[e.SF_Column] += 2;
            }
        }
        
        foreach (var f in TempFeatures)
        {
            var card = FeatureToCard(f, ColumnCount[f.SF_Column]);
            var cardAbove = FindCardAbove(card);
            if (cardAbove is not null && cardAbove.Employee is not null)
            {
                card.Feature.Assignee = cardAbove.Employee;
            }
            Cards.Add(card);
            ColumnCount[f.SF_Column] += 2;
        }

        foreach (var t in _kanbanTaskService.KanbanTasks)
        {
            Cards.Add(TaskToCard(t, ColumnCount[t.SF_Column]));
            ColumnCount[t.SF_Column] += 2;
        }
        //todo: add task to delivered
    }

    private Card TaskToCard(KanbanTask kanbanTask, int rankId)
    {
        var card = new Card()
        {
            Id = lastId++,
            RankId = rankId,
            KanbanTask = kanbanTask,
            Column = kanbanTask.SF_Column
        };
        return card;
    }

    private Card FeatureToCard(Feature feature, int rankId)
    {
        var card = new Card()
        {
            Id = lastId++,
            RankId = rankId,
            Feature = feature,
            Column = feature.SF_Column
        };
        return card;
    }

    private Card EmployeeToCard(Employee employee, int rankId)
    {
        var card = new Card()
        {
            Id = lastId++,
            RankId = rankId,
            Employee = employee,
            Column = employee.SF_Column
        };
        return card;
    }

    public async Task SimulateBoard()
    {
        foreach (var card in Cards)
        {
            if (card.KanbanTask is not null && card.KanbanTask.Assignee is not null)
            {
                card.KanbanTask.NextTaskStatus();
                card.KanbanTask.Assignee = null;

                await _kanbanTaskService.UpdateKanbanTask(card.KanbanTask.Id, card.KanbanTask);
            }
            if (card.Feature is not null && card.Feature.Assignee is not null)
            {
                card.Feature.NextFeatureStatus();
                card.Feature.Assignee = null;
                if(card.Feature.Status == Feature.FeatureStatus.Delivered)
                {
                    UnpackFeature(card.Feature);
                }

                await _featureService.UpdateFeature(card.Feature.Id, card.Feature);
            }
        }
    }
    private async Task UnpackFeature(Feature feature)
    {
        foreach(var t in feature.KanbanTasks)
        {
            t.Team = Team.TeamName.HighLevelAnalysis;
            t.Status = KanbanTask.TaskStatus.ReadyForDevelopment;
            _kanbanTaskService.CreateKanbanTask(t);
        }
    }

    public async Task UpdateCard(int cardId, Card card)
    {
        if (card.Employee is null && card.KanbanTask is not null)
        {
            await _kanbanTaskService.UpdateKanbanTask(card.KanbanTask.Id, card.KanbanTask);
        }
        else if (card.KanbanTask is null && card.Employee is not null)
        {
            await _employeeService.UpdateEmployee(card.Employee.Id, card.Employee);
        }
        else if (card.Feature is not null)
        {
            await _featureService.UpdateFeature(card.Feature.Id, card.Feature);
        }
    }

    public async Task UpdateCardLocal(int cardId, Card card)
    {
        var dbCard = Cards.Where(c => c.Id == cardId).FirstOrDefault();
        if (dbCard is not null)
        {
            dbCard.Column = card.Column;
            dbCard.RankId = card.RankId;
            dbCard.Employee = card.Employee;
            dbCard.KanbanTask = card.KanbanTask;
            dbCard.Feature = card.Feature;
        }
    }

    public async Task UpdateColumn(string column)
    {
        foreach (var card in Cards.Where(c => c.Column == column))
        {
            if (card.KanbanTask is not null)
            {
                var cardAbove = FindCardAbove(card);

                if (cardAbove is not null && cardAbove.Employee is not null)
                {
                    card.KanbanTask.Assignee = cardAbove.Employee;
                    card.KanbanTask.SF_Column = cardAbove.Employee.SF_Column;
                }
                else
                {
                    card.KanbanTask.Assignee = null;
                }
            }
            else if (card.Feature is not null)
            {
                var cardAbove = FindCardAbove(card);

                if (cardAbove is not null && cardAbove.Employee is not null)
                {
                    card.Feature.Assignee = cardAbove.Employee;
                    card.Feature.SF_Column = cardAbove.Employee.SF_Column;
                }
                else
                {
                    card.Feature.Assignee = null;
                }
            }
            await UpdateCard(card.Id, card);
        }
    }

    private Card? FindCardAbove(Card card)
    {
        var column = Cards.Where(
            c => c.Team == card.Team && c.Column == card.Column && c.RankId < card.RankId
        );
        if (column.ToList().Count > 0)
        {
            var cardAbove = column.MaxBy(c => c.RankId);
            return cardAbove;
        }
        return null;
    }

    private void InitProperties()
    {
        lastId = 0;
        Cards = new List<Card>();
        ColumnMaxCount = new Dictionary<string, int>
        {
            { "Backlog", 6 },
            { "AnalysisDoing", 0 },
            { "DevelopmentDoing", 0 },
            { "TestDoing", 0 },
            { "Doing1", 0 },
            { "Doing2", 0 }
        };
        ColumnCount = new Dictionary<string, int>
        {
            { "Backlog", 1 },
            { "AnalysisDoing", 1 },
            { "DevelopmentWaiting", 1 },
            { "DevelopmentDoing", 1 },
            { "TestWaiting", 1 },
            { "TestDoing", 1 },
            { "Delivered", 1 },
            { "Doing1", 1 },
            { "Doing2", 1 },
            { "ReadyForDevelopment", 1}
        };
        DevBoardBuild = new Dictionary<Employee, KanbanTask?>();
        FeatureBoardBuild = new Dictionary<Employee, Feature?>();
        TempTasks = new List<KanbanTask>();
        TempFeatures = new List<Feature>();
    }
}
