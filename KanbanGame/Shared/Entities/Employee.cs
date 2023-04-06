namespace KanbanGame.Shared;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public EmployeeSeniority Seniority { get; set; } = EmployeeSeniority.Junior;
    public EmployeeStatus Status { get; set; } = EmployeeStatus.NotWorking;
    public string AvatarPath { get; set; } = "Avatars/Default.png";

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
