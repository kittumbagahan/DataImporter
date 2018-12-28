using SQLite4Unity3d;

[System.Serializable]
public class BookModel {

	[PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Description { get; set; }

    public override string ToString()
    {
        return string.Format("[Book: Id={0}, Desc={1}", Id, Description);
    }

}
