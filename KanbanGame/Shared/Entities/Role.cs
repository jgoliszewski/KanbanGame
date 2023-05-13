namespace KanbanGame.Shared;
using System.ComponentModel.DataAnnotations;

public class Role
{
    public int LearningDaysLeft { get; set; } = 0;
    public int TransitioningDaysLeft { get; set; } = 0;

    public bool? IsHighLevelAnalyzer { get; set; } = false;
    public bool? IsAnalyzer { get; set; } = false;
    public bool? IsDeveloper { get; set; } = false;
    public bool? IsTester { get; set; } = false;

    public EmployeeSeniority Seniority { get; set; } = EmployeeSeniority.Junior;
    public EmployeeStatus Status { get; set; } = EmployeeStatus.Working;
    public EmployeeRole CurrentRole { get; set; }
    public EmployeeRole PreviousRole { get; set; }
    public Team.TeamName Team { get; set; }
    public string AvatarPath { get; set; } = "Avatars/Default.png";

    //todo: refactor this monster
    public EmployeeRole ColumnToRole(string column)
    {
        switch (column.ToLower())
        {
            case "backlog":
                return CurrentRole;
            case "analysisdoing":
                if (IsAnalyzer is not null && CurrentRole != EmployeeRole.Analyzer)
                {
                    if (!IsAnalyzer.Value)
                    {
                        Status = EmployeeStatus.Learning;
                        LearningDaysLeft = 5;
                    }
                    else if (PreviousRole != EmployeeRole.Analyzer)
                    {
                        Status = EmployeeStatus.Transitioning;
                        TransitioningDaysLeft = 2;
                    }
                    else
                    {
                        LearningDaysLeft = 0;
                        TransitioningDaysLeft = 0;
                        Status = EmployeeStatus.Working;
                    }
                    return EmployeeRole.Analyzer;
                }
                else
                    return CurrentRole;
            case "developmentwaiting":
            case "developmentdoing":
                if (IsDeveloper is not null && CurrentRole != EmployeeRole.Developer)
                {
                    if (!IsDeveloper.Value)
                    {
                        Status = EmployeeStatus.Learning;
                        LearningDaysLeft = 5;
                    }
                    else if (PreviousRole != EmployeeRole.Developer)
                    {
                        Status = EmployeeStatus.Transitioning;
                        TransitioningDaysLeft = 2;
                    }
                    else
                    {
                        LearningDaysLeft = 0;
                        TransitioningDaysLeft = 0;
                        Status = EmployeeStatus.Working;
                    }
                    return EmployeeRole.Developer;
                }
                else
                    return CurrentRole;
            case "testwaiting":
            case "testdoing":
                if (IsTester is not null && CurrentRole != EmployeeRole.Tester)
                {
                    if (!IsTester.Value)
                    {
                        Status = EmployeeStatus.Learning;
                        LearningDaysLeft = 5;
                    }
                    else if (PreviousRole != EmployeeRole.Tester)
                    {
                        Status = EmployeeStatus.Transitioning;
                        TransitioningDaysLeft = 2;
                    }
                    else
                    {
                        LearningDaysLeft = 0;
                        TransitioningDaysLeft = 0;
                        Status = EmployeeStatus.Working;
                    }
                    return EmployeeRole.Tester;
                }
                else
                    return CurrentRole;
            case "doing1":
                if (
                    IsHighLevelAnalyzer is not null
                    && CurrentRole != EmployeeRole.HighLevelAnalyzer1
                )
                {
                    if (!IsHighLevelAnalyzer.Value)
                    {
                        Status = EmployeeStatus.Learning;
                        LearningDaysLeft = 5;
                    }
                    else if (PreviousRole != EmployeeRole.HighLevelAnalyzer1)
                    {
                        Status = EmployeeStatus.Transitioning;
                        TransitioningDaysLeft = 2;
                    }
                    else
                    {
                        LearningDaysLeft = 0;
                        TransitioningDaysLeft = 0;
                        Status = EmployeeStatus.Working;
                    }
                    return EmployeeRole.HighLevelAnalyzer1;
                }
                else
                    return CurrentRole;
            case "doing2waiting":
            case "doing2":
                if (
                    IsHighLevelAnalyzer is not null
                    && CurrentRole != EmployeeRole.HighLevelAnalyzer2
                )
                {
                    if (!IsHighLevelAnalyzer.Value)
                    {
                        Status = EmployeeStatus.Learning;
                        LearningDaysLeft = 5;
                    }
                    else if (PreviousRole != EmployeeRole.HighLevelAnalyzer2)
                    {
                        Status = EmployeeStatus.Transitioning;
                        TransitioningDaysLeft = 2;
                    }
                    else
                    {
                        LearningDaysLeft = 0;
                        TransitioningDaysLeft = 0;
                        Status = EmployeeStatus.Working;
                    }
                    return EmployeeRole.HighLevelAnalyzer2;
                }
                else
                    return CurrentRole;
            default:
                return CurrentRole;
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
