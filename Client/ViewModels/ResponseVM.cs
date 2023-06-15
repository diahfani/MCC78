namespace Client.ViewModels;

public class ResponseVM<TEntity>
{
    public int Code { get; set; }
    public string StatusCode { get; set; }
    public string Message { get; set; }
    public TEntity? Data { get; set; }

}
