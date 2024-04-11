namespace upd8.Infrastructure.Interfaces
{
    public interface IClientFactory
    {
        Task<T> Get<T>(string url);
        Task<T> Update<T>(string url, StringContent content);
        Task<string> Delete(string url);
        Task<T> Post<T>(string url, StringContent content);
    }
}
