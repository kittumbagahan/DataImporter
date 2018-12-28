using SQLite4Unity3d;

[System.Serializable]
public class StudentActivityModel  {

    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string DeviceId { get; set; }
    public int SectionId { get; set; } //section Id can be selection from StudentId but the plugin has a bug in using function inside its LINQ
    public int StudentId { get; set; }
    public int BookId { get; set; }
    public int ActivityId { get; set; }
    public string Grade { get; set; }
    public int PlayCount { get; set; }

    public override string ToString()
    {
        return string.Format("[StudentActivityModel: Id={0}, SectionId={1}, StudentId={2}, ActivityId={3} Grade={4}, PlayCount={5}", Id, SectionId, StudentId, ActivityId, Grade, PlayCount);
    }


}
