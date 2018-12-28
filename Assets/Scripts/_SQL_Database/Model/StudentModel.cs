using SQLite4Unity3d;

[System.Serializable]
public class StudentModel {

	[PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string DeviceId { get; set; }
    public int SectionId { get; set; }
    public string Givenname { get; set; }
    public string Middlename { get; set; }
    public string Lastname { get; set; }
    public string Nickname { get; set; }

    public override string ToString()
    {
        return string.Format("[Student: Id={0}, Name={2}", Id, Givenname);
    }
}
