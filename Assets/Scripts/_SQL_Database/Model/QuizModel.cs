using SQLite4Unity3d;

public class QuizModel 
{
    [PrimaryKey, Unique]
    public string ID { get; set; }
    public string Subject { get; set; }
    public string Title { get; set; }
    public int Items { get; set; }
}

public class QuizItemModel
{
    [PrimaryKey, Unique]
    public string ID { get; set; }
    public string QuizID { get; set; }
}

public class QuizItemChoicesModel
{
    [PrimaryKey, Unique]
    public string ID { get; set; }
    public string QuizItemID { get; set; }
    public string Answer { get; set; }
    public bool IsCorrect { get; set; }
}
