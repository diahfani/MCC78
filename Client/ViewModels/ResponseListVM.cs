namespace Client.ViewModels
{
    public class ResponseListVM<TEntity>
    {
        public string StatusCode { get; set; }
        public string Message { get; set; }
        public List<TEntity>? Data { get; set; }

    }
}
