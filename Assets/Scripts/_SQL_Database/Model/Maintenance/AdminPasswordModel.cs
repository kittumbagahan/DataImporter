using SQLite4Unity3d;

public class AdminPasswordModel {

   [PrimaryKey, AutoIncrement]
   public int Id { get; set; }
   public string Password { get; set; }
   

   public override string ToString()
   {
      return string.Format ("[AdminPasswordModel: Id={0}, Password={1}", Id, Password);
   }
}
