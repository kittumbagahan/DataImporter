using SQLite4Unity3d;

[System.Serializable]
public class SectionModel{

    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string DeviceId { get; set; }
    public string Description { get; set; }
    public override string ToString()
    {
        return string.Format("[Section: Id={0}, DeviceId={1}, Description={2}", Id, DeviceId, Description);
    }
}
