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
                    if (!IsAnalyzer.Value)
                    {
                        Status = Status == EmployeeStatus.Transitioning ? Status : EmployeeStatus.Learning;
                        LearningDaysLeft = 4;
                    }
                    else 
                    {
                        TransitionEmployee();
                    }
                    CurrentRole = EmployeeRole.Analyzer;
                }
                break;

            case "developmentdoing":
                if (IsDeveloper.HasValue)
                {
                    if (!IsDeveloper.Value)
                    {
                        Status = Status == EmployeeStatus.Transitioning ? Status : EmployeeStatus.Learning;
                        LearningDaysLeft = 4;
                    }
                    else 
                    {
                        TransitionEmployee();
                    }
                    CurrentRole = EmployeeRole.Developer;
                }
                break;
            
            case "testdoing":
                if (IsTester.HasValue)
                {
                    if (!IsTester.Value)
                    {
                        Status = Status == EmployeeStatus.Transitioning ? Status : EmployeeStatus.Learning;
                        LearningDaysLeft = 4;
                    }
                    else 
                    {
                        TransitionEmployee();
                    }
                    CurrentRole = EmployeeRole.Tester;
                }
                break;

            case "doing1":
                if (IsHighLevelAnalyzer.HasValue)
                {
                    if (!IsHighLevelAnalyzer.Value)
                    {
                        Status = Status == EmployeeStatus.Transitioning ? Status : EmployeeStatus.Learning;
                        LearningDaysLeft = 4;
                    }
                    else 
                    {
                        TransitionEmployee();
                }
                    CurrentRole = EmployeeRole.HighLevelAnalyzer1;
                }
                break;

            case "doing2":
                if (IsHighLevelAnalyzer.HasValue)
                {
                    if (!IsHighLevelAnalyzer.Value)
                    {
                        Status = Status == EmployeeStatus.Transitioning ? Status : EmployeeStatus.Learning;
                        LearningDaysLeft = 4;
                    }
                    else 
                    {
                        TransitionEmployee();
                    }
                    CurrentRole = EmployeeRole.HighLevelAnalyzer2;
                }
                break;

        }   
    }

    private void TransitionEmployee()
    {
        if (CurrentRole != PreviousRole)
        {
            Status = EmployeeStatus.Transitioning;
            TransitioningDaysLeft = 3;
        }
        if (Team != PreviousTeam)
        {
            Status = EmployeeStatus.Transitioning;
            TransitioningDaysLeft = 6;
        }
        else 
        {
            Status = EmployeeStatus.Working;
            TransitioningDaysLeft = 0;
            LearningDaysLeft = 0;
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
