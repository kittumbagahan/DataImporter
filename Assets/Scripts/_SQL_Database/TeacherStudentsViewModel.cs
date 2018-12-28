using SQLite4Unity3d;
public class TeacherStudentsViewModel {

    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int DeviceId { get; set; }
    public int SectionId { get; set; } //section Id can be selection from StudentId but the plugin has a bug in using function inside its LINQ
    public int StudentId { get; set; }
   

    public override string ToString()
    {
        return string.Format("[StudentActivityModel: Id={0}, DeviceId={1}, SectionId={2}, StudentId={3}", Id, DeviceId, SectionId, StudentId);
    }
}
