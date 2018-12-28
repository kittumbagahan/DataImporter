using SQLite4Unity3d;

public class ResetPasswordModel {


    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string SystemPasscode { get; set; }
    public bool Used { get; set; }
   

    public override string ToString()
    {
        return string.Format("[ResetPasswordModel: Id={0}, SystemPasscode={1}, Used={2}",
           Id, SystemPasscode, Used);
    }
}
