using SQLite4Unity3d;

public class NumberOfStudentsModel {

   [PrimaryKey, AutoIncrement]
   public int Id { get; set; }
   public int MaxStudent { get; set; }
  

   public override string ToString()
   {
      return string.Format ("[NumberOfStudentsModel: Id={0}, MaxStudent={1}", Id, MaxStudent);
   }
}
