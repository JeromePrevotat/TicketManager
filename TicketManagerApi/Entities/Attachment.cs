namespace TicketManagerApi.Entities;

public class Attachment
{
  public int Id { get; set; }
  public required int TicketId { get; set; }
  public required string FileName { get; set; }
  public required string FilePath { get; set; }
  public required DateTime UploadedAt { get; set; }
}

