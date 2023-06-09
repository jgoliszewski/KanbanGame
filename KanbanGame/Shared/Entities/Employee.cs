namespace KanbanGame.Shared;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Productivity { get; set; } = 0.2;
    public Role Roles { get; set; }
    public string AvatarPath { get; set; } = "Avatars/Default.png";
    public string SF_Column
    {
        get => Role.RoleToColumn(Roles.CurrentRole);
        set => Roles.ColumnToRole(value);
    } // for Syncfunction D&D
}
