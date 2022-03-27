namespace NS.BLOGMGMT.Model;

public class CommentModel
{
  public long CommentId { get; set; }
  public long? BlogId { get; set; }
  public string? CommentContent { get; set; }
  public string? Name { get; set; }
}
