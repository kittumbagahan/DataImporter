using SQLite4Unity3d;

public class TeacherDeviceModel {

   [PrimaryKey, AutoIncrement]
   public int Id { get; set; }
   public string DeviceId { get; set; }

   public override string ToString()
   {
      return string.Format ("[TeacherDeviceModel: Id={0}, DeviceId={1}", Id, DeviceId);
   }
}
