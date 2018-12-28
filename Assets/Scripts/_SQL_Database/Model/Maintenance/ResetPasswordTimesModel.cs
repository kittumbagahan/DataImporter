using SQLite4Unity3d;

public class ResetPasswordTimesModel {

   [PrimaryKey, AutoIncrement]
   public int Id { get; set; }
   public int MaxReset { get; set; }
   public int ResetCount { get; set; }


   public override string ToString()
   {
      return string.Format ("[ResetPasswordTimesModel: Id={0}, MaxReset={1}, ResetCount={2}", Id, MaxReset, ResetCount);
   }
}
