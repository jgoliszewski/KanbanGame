namespace KanbanGame.Shared;
using System.ComponentModel.DataAnnotations;

public class Role
{
    public int LearningDaysLeft { get; set; } = 0;
    public int TransitioningDaysLeft { get; set; } = 0;
    public bool IsBlocked { get; set; } = false;

    public bool? IsHighLevelAnalyzer { get; set; } = false;
    public bool? IsAnalyzer { get; set; } = false;
    public bool? IsDeveloper { get; set; } = false;
    public bool? IsTester { get; set; } = false;

    public EmployeeSeniority Seniority { get; set; } = EmployeeSeniority.Junior;
    public EmployeeStatus Status { get; set; } = EmployeeStatus.Working;
    public EmployeeRole CurrentRole { get; set; }
    public EmployeeRole PreviousRole { get; set; }
    public Team.TeamName Team { get; set; }
    public Team.TeamName PreviousTeam { get; set; }
    public string AvatarPath { get; set; } = "Avatars/Default.png";

    //todo: refactor this monster
    public void ColumnToRole(string column)
    {
        switch (column.ToLower())
        {
            case "analysisdoing":
                if (IsAnalyzer.HasValue)
                {
                    CurrentRole = EmployeeRole.Analyzer;
                }
                break;

            case "developmentdoing":
                if (IsDeveloper.HasValue)
                {
                    CurrentRole = EmployeeRole.Developer;
                }
                break;

            case "testdoing":
                if (IsTester.HasValue)
                {
                    CurrentRole = EmployeeRole.Tester;
                }
                break;

            case "doing1":
                if (IsHighLevelAnalyzer.HasValue)
                {
                    CurrentRole = EmployeeRole.HighLevelAnalyzer1;
                }
                break;

            case "doing2":
                if (IsHighLevelAnalyzer.HasValue)
                {
                    CurrentRole = EmployeeRole.HighLevelAnalyzer2;
                }
                break;
        }
    }

    public void ChangeTeam(Team.TeamName team)
    {
        Team = team;

        if (Team == PreviousTeam)
        {
            CurrentRole = PreviousRole;
        }
        else if (Team == KanbanGame.Shared.Team.TeamName.HighLevelAnalysis)
        {
            CurrentRole = EmployeeRole.HighLevelAnalyzer1;
        }
        else if (PreviousTeam == KanbanGame.Shared.Team.TeamName.HighLevelAnalysis)
        {
            if (IsDeveloper.HasValue)
                CurrentRole = EmployeeRole.Developer;
            else if (IsAnalyzer.HasValue)
                CurrentRole = EmployeeRole.Analyzer;
            else if (IsTester.HasValue)
                CurrentRole = EmployeeRole.Tester;
        }
        else
        {
            CurrentRole = PreviousRole;
        }
    }

    public static String RoleToColumn(EmployeeRole role)
    {
        switch (role)
        {
            case EmployeeRole.Analyzer:
                return "AnalysisDoing";
            case EmployeeRole.Developer:
                return "DevelopmentDoing";
            case EmployeeRole.Tester:
                return "TestDoing";
            case EmployeeRole.HighLevelAnalyzer1:
                return "Doing1";
            case EmployeeRole.HighLevelAnalyzer2:
                return "Doing2";
            default:
                return "DevelopmentDoing";
        }
    }

    public enum EmployeeSeniority
    {
        Junior,
        Mid,
        Senior
    }

    public enum EmployeeStatus
    {
        Working,
        Learning,
        Transitioning,
        NotWorking
    }

    public enum EmployeeRole
    {
        HighLevelAnalyzer1,
        HighLevelAnalyzer2,
        Analyzer,
        Developer,
        Tester
    }
}
