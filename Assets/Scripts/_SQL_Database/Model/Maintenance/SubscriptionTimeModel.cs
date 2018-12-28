using SQLite4Unity3d;

public class SubscriptionTimeModel
{

   [PrimaryKey, AutoIncrement]
   public int Id { get; set; }
   public int SettedTime { get; set; }
   public int Timer { get; set; }


   public override string ToString()
   {
      return string.Format ("[SubcriptionTimeModel: Id={0}, SettedTime={1}, Timer={2}", Id, SettedTime, Timer);
   }
}
