namespace KanbanGame.Shared;
using System.ComponentModel.DataAnnotations;

public class Role
{
    public int TrainingDaysLeft
    {
        get => GetTrainingDaysLeft();
    }

    public bool? IsHighLevelAnalyzer { get; set; } = false;
    public int HLAnalyzerTrainingDaysLeft { get; set; } = 5;
    public bool? IsAnalyzer { get; set; } = false;
    public int AnalyzerTrainingDaysLeft { get; set; } = 5;
    public bool? IsDeveloper { get; set; } = false;
    public int DeveloperTrainingDaysLeft { get; set; } = 5;
    public bool? IsTester { get; set; } = false;
    public int TesterTrainingDaysLeft { get; set; } = 5;

    public EmployeeSeniority Seniority { get; set; } = EmployeeSeniority.Junior;
    public EmployeeStatus Status { get; set; } = EmployeeStatus.Working;
    public EmployeeRole CurrentRole { get; set; }
    public Team.TeamName Team { get; set; }
    public string AvatarPath { get; set; } = "Avatars/Default.png";

    private int GetTrainingDaysLeft()
    {
        switch (CurrentRole)
        {
            case EmployeeRole.HighLevelAnalyzer1:
                return HLAnalyzerTrainingDaysLeft;
            case EmployeeRole.HighLevelAnalyzer2:
                return HLAnalyzerTrainingDaysLeft;
            case EmployeeRole.Analyzer:
                return AnalyzerTrainingDaysLeft;
            case EmployeeRole.Developer:
                return DeveloperTrainingDaysLeft;
            case EmployeeRole.Tester:
                return TesterTrainingDaysLeft;
            default:
                return 0;
        }
    }

    public EmployeeRole ColumnToRole(string column)
    {
        switch (column.ToLower())
        {
            case "backlog":
                return CurrentRole;
            case "analysisdoing":
                if (IsAnalyzer is not null)
                {
                    if (!IsAnalyzer.Value)
                        Status = EmployeeStatus.Learning;
                    else
                        Status = EmployeeStatus.Working;
                    return EmployeeRole.Analyzer;
                }
                else
                    return CurrentRole;
            case "developmentwaiting":
            case "developmentdoing":
                if (IsDeveloper is not null)
                {
                    if (!IsDeveloper.Value)
                        Status = EmployeeStatus.Learning;
                    else
                        Status = EmployeeStatus.Working;
                    return EmployeeRole.Developer;
                }
                else
                    return CurrentRole;
            case "testwaiting":
            case "testdoing":
                if (IsTester is not null)
                {
                    if (!IsTester.Value)
                        Status = EmployeeStatus.Learning;
                    else
                        Status = EmployeeStatus.Working;
                    return EmployeeRole.Tester;
                }
                else
                    return CurrentRole;
            case "doing1":
                if (IsHighLevelAnalyzer is not null)
                {
                    if (!IsHighLevelAnalyzer.Value)
                        Status = EmployeeStatus.Learning;
                    else
                        Status = EmployeeStatus.Working;
                    return EmployeeRole.HighLevelAnalyzer1;
                }
                else
                    return CurrentRole;
            case "doing2":
                if (IsHighLevelAnalyzer is not null)
                {
                    if (!IsHighLevelAnalyzer.Value)
                        Status = EmployeeStatus.Learning;
                    else
                        Status = EmployeeStatus.Working;
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
