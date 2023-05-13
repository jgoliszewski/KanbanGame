namespace KanbanGame.Shared;

public class Session
{
    public int Day { get; set; } = 1;
    public int Week
    {
        get => Day / 7;
    }
    public DayOfTheWeek Weekday
    {
        get => (DayOfTheWeek)(Day % 7 - 1);
    }
    public bool IsFeatureBoardUnlocked { get; set; } = false;

    public enum DayOfTheWeek
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }
}
