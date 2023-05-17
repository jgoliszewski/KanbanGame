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

    public void ColumnToRole(string column)
    {
        switch (column.ToLower())
        {
            case "analysisdoing":
                ChangeRole(EmployeeRole.Analyzer);
                break;

            case "developmentdoing":
                ChangeRole(EmployeeRole.Developer);
                break;

            case "testdoing":
                ChangeRole(EmployeeRole.Tester);
                break;

            case "doing1":
                ChangeRole(EmployeeRole.HighLevelAnalyzer1);
                break;

            case "doing2":
                ChangeRole(EmployeeRole.HighLevelAnalyzer2);
                break;
        }
    }

    //todo: refactor this monster
    private void ChangeRole(EmployeeRole role)
    {
        switch (role)
        {
            case EmployeeRole.Analyzer:
                if (IsBlocked)
                    break;
                if (PreviousRole == EmployeeRole.Analyzer && Team == PreviousTeam)
                {
                    Status = EmployeeStatus.Working;
                    TransitioningDaysLeft = 0;
                    LearningDaysLeft = 0;
                    CurrentRole = EmployeeRole.Analyzer;
                    break;
                }
                else if (IsAnalyzer.HasValue)
                {
                    if (IsAnalyzer.Value)
                    {
                        LearningDaysLeft = 0;
                        if (TransitioningDaysLeft < 2)
                        {
                            TransitioningDaysLeft = 2;
                            Status = EmployeeStatus.Transitioning;
                        }
                    }
                    else
                    {
                        if (TransitioningDaysLeft > 3)
                        {
                            LearningDaysLeft = 5;
                        }
                        else
                        {
                            LearningDaysLeft = 5;
                            TransitioningDaysLeft = 0;
                            Status = EmployeeStatus.Learning;
                        }
                    }
                    CurrentRole = EmployeeRole.Analyzer;
                }
                break;

            case EmployeeRole.Developer:
                if (IsBlocked)
                    break;
                if (PreviousRole == EmployeeRole.Developer && Team == PreviousTeam)
                {
                    Status = EmployeeStatus.Working;
                    TransitioningDaysLeft = 0;
                    LearningDaysLeft = 0;
                    CurrentRole = EmployeeRole.Developer;
                    break;
                }
                else if (IsDeveloper.HasValue)
                {
                    if (IsDeveloper.Value)
                    {
                        LearningDaysLeft = 0;
                        if (TransitioningDaysLeft < 2)
                        {
                            TransitioningDaysLeft = 2;
                            Status = EmployeeStatus.Transitioning;
                        }
                    }
                    else
                    {
                        if (TransitioningDaysLeft > 3)
                        {
                            LearningDaysLeft = 5;
                        }
                        else
                        {
                            LearningDaysLeft = 5;
                            TransitioningDaysLeft = 0;
                            Status = EmployeeStatus.Learning;
                        }
                    }
                    CurrentRole = EmployeeRole.Developer;
                }
                break;

            case EmployeeRole.Tester:
                if (IsBlocked)
                    break;
                if (PreviousRole == EmployeeRole.Tester && Team == PreviousTeam)
                {
                    Status = EmployeeStatus.Working;
                    TransitioningDaysLeft = 0;
                    LearningDaysLeft = 0;
                    CurrentRole = EmployeeRole.Tester;
                    break;
                }
                else if (IsTester.HasValue)
                {
                    if (IsTester.Value)
                    {
                        LearningDaysLeft = 0;
                        if (TransitioningDaysLeft < 2)
                        {
                            TransitioningDaysLeft = 2;
                            Status = EmployeeStatus.Transitioning;
                        }
                    }
                    else
                    {
                        if (TransitioningDaysLeft > 3)
                        {
                            LearningDaysLeft = 5;
                        }
                        else
                        {
                            LearningDaysLeft = 5;
                            TransitioningDaysLeft = 0;
                            Status = EmployeeStatus.Learning;
                        }
                    }
                    CurrentRole = EmployeeRole.Tester;
                }
                break;

            case EmployeeRole.HighLevelAnalyzer1:
                if (IsBlocked)
                    break;
                if (PreviousRole == EmployeeRole.HighLevelAnalyzer1 && Team == PreviousTeam)
                {
                    Status = EmployeeStatus.Working;
                    TransitioningDaysLeft = 0;
                    LearningDaysLeft = 0;
                    CurrentRole = EmployeeRole.HighLevelAnalyzer1;
                    break;
                }
                else if (IsHighLevelAnalyzer.HasValue)
                {
                    if (IsHighLevelAnalyzer.Value)
                    {
                        LearningDaysLeft = 0;
                        if (TransitioningDaysLeft < 2)
                        {
                            TransitioningDaysLeft = 2;
                            Status = EmployeeStatus.Transitioning;
                        }
                    }
                    else
                    {
                        if (TransitioningDaysLeft > 3)
                        {
                            LearningDaysLeft = 5;
                        }
                        else
                        {
                            LearningDaysLeft = 5;
                            TransitioningDaysLeft = 0;
                            Status = EmployeeStatus.Learning;
                        }
                    }
                    CurrentRole = EmployeeRole.HighLevelAnalyzer1;
                }
                break;

            case EmployeeRole.HighLevelAnalyzer2:
                if (IsBlocked)
                    break;
                if (PreviousRole == EmployeeRole.HighLevelAnalyzer2 && Team == PreviousTeam)
                {
                    Status = EmployeeStatus.Working;
                    TransitioningDaysLeft = 0;
                    LearningDaysLeft = 0;
                    CurrentRole = EmployeeRole.HighLevelAnalyzer2;
                    break;
                }
                else if (IsHighLevelAnalyzer.HasValue)
                {
                    if (IsHighLevelAnalyzer.Value)
                    {
                        LearningDaysLeft = 0;
                        if (TransitioningDaysLeft < 2)
                        {
                            TransitioningDaysLeft = 2;
                            Status = EmployeeStatus.Transitioning;
                        }
                    }
                    else
                    {
                        if (TransitioningDaysLeft > 3)
                        {
                            LearningDaysLeft = 5;
                        }
                        else
                        {
                            LearningDaysLeft = 5;
                            TransitioningDaysLeft = 0;
                            Status = EmployeeStatus.Learning;
                        }
                    }
                    CurrentRole = EmployeeRole.HighLevelAnalyzer2;
                }
                break;
        }
    }

    public void ChangeTeam(Team.TeamName team)
    {
        Team = team;
        TransitioningDaysLeft = 6;
        Status = EmployeeStatus.Transitioning;
        if (Team == PreviousTeam)
        {
            CurrentRole = PreviousRole;
            TransitioningDaysLeft = 0;
            LearningDaysLeft = 0;
            Status = EmployeeStatus.Working;
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
