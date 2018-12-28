using SQLite4Unity3d;

[System.Serializable]
public class ActivityModel {

    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int BookId { get; set; }
    public string Description { get; set; }
    public string Module { get; set; }
    public int Set { get; set; }

   public override string ToString()
   {
      return string.Format ("[ActivityModel: Id={0}, BookId={1}, Description={2}, Module={3}, Set={4}", 
         Id, BookId, Description, Module, Set);
   }
}
