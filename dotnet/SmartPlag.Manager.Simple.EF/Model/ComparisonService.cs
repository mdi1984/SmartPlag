namespace SmartPlag.Manager.Simple.EF.Model
{
  public class ComparisonService : PlagEntity
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public string BaseUrl { get; set; }
    public string RequestPath { get; set; }

  }
}