namespace KanbanGame.Shared;
using System.ComponentModel.DataAnnotations;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public EmployeeSeniority Seniority { get; set; } = EmployeeSeniority.Junior;
    public EmployeeStatus Status { get; set; } = EmployeeStatus.Working;
    public Role CurrentRole { get; set; }
    public Team.TeamName Team { get; set; }
    public string AvatarPath { get; set; } = "Avatars/Default.png";
    public string SF_Column
    {
        get => RoleToColumn(CurrentRole);
        set => CurrentRole = ColumnToRole(value);
    } // for Syncfunction D&D

    //todod: extract Role to separate classes with interface
    public enum Role
    {
        HighLevelAnalyzer1,
        HighLevelAnalyzer2,
        Analyzer,
        Developer,
        Tester
    }

    public Role ColumnToRole(string column)
    {
        switch (column.ToLower())
        {
            case "backlog":
                if(this.Team == Shared.Team.TeamName.HighLevelAnalysis)
                    return Role.HighLevelAnalyzer1;
                else
                    return Role.Analyzer;
            case "analysisdoing":
                return Role.Analyzer;
            case "developmentwaiting":
            case "developmentdoing":
                return Role.Developer;
            case "testwaiting":
            case "testdoing":
            case "delivered":
                return Role.Tester;
            case "doing1":
                return Role.HighLevelAnalyzer1;
            case "doing2":
                return Role.HighLevelAnalyzer2;
            default:
                return Role.Developer;
        }
    }

    static String RoleToColumn(Role role)
    {
        switch (role)
        {
            case Role.Analyzer:
                return "AnalysisDoing";
            case Role.Developer:
                return "DevelopmentDoing";
            case Role.Tester:
                return "TestDoing";
            case Role.HighLevelAnalyzer1:
                return "Doing1";
            case Role.HighLevelAnalyzer2:
                return "Doing2";
            default:
                return "DevelopmentDoing";
        }
    }

    //todod: extract seniority to separate class
    public enum EmployeeSeniority
    {
        Junior,
        Mid,
        Senior
    }

    //todo: extract status to separate class
    public enum EmployeeStatus
    {
        Working,
        Learning,
        NotWorking
    }
    //todo: add role interface and classes and extract to seperate ...
}
