namespace Shortener.Grains;

public interface IUrlShortenerGrain : IGrainWithStringKey
{
    Task SetLongUrl(string longUrl);
    Task<string?> GetLongUrl();
    Task<long> GetClickCount();
}
