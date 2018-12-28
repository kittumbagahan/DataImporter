using SQLite4Unity3d;

public class SubjectModel {

    [PrimaryKey, AutoIncrement, Unique]
	public int ID { get; set; }
    public string Name { get; set; }
}
