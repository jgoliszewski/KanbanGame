namespace KanbanGame.Shared;
using System.ComponentModel.DataAnnotations;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public EmployeeSeniority Seniority { get; set; } = EmployeeSeniority.Junior;
    public EmployeeStatus Status { get; set; } = EmployeeStatus.NotWorking;
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
        Analyzer,
        Developer,
        Tester
    }
    static Role ColumnToRole(string column)
    {
        switch (column.ToLower())
        {
            case "backlog":
            case "analysisdoing":
                return Role.Analyzer;
            case "developmentwaiting":
            case "developmentdoing":
                return Role.Developer;
            case "testwaiting":
            case "testdoing":
            case "delivered":
                return Role.Tester;
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
