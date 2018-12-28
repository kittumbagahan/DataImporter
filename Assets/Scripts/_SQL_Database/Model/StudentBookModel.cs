using SQLite4Unity3d;

[System.Serializable]
public class StudentBookModel {

	[PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string DeviceId { get; set; }
    public int SectionId { get; set; }
    public int StudentId { get; set; }
    public int BookId { get; set; }
    public int ReadCount { get; set; }
    public int ReadToMeCount { get; set; }
    public int AutoReadCount { get; set; }

    public override string ToString()
    {
        return string.Format("[StudentBookModel: Id={0}, SectionId={1}, StudentId={2}, AutoReadCount={3}, ReadToMeCount={4}, ReadCount={5}", Id, SectionId, StudentId, AutoReadCount, ReadToMeCount, ReadCount);
    }
}
