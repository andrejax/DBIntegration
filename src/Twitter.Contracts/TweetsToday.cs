namespace Twitter.Contracts;

public class TweetsToday
{
    public int AuthorsCount { get; set; }
    public int TweetsCount { get; set; }
    public int RetweetsCount { get; set; }
    public DateTime Date { get; set; }
    public string Query { get; set; }
}

