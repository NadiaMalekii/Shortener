namespace Shortener.Grains;

public sealed class UrlShortenerGrain(
    [PersistentState(stateName: "url", storageName: "urls")] IPersistentState<UrlDetails> state)
    : Grain, IUrlShortenerGrain
{
    public Task<string?> GetLongUrl()
    {
        if (string.IsNullOrEmpty(state.State.LongUrl))
            return Task.FromResult<string?>(null);

        state.State.ClickCount++;
        _ = state.WriteStateAsync(); 

        return Task.FromResult<string?>(state.State.LongUrl);
    }

    public Task<long> GetClickCount() => Task.FromResult(state.State.ClickCount);

    public async Task SetLongUrl(string longUrl)
    {
        state.State = new()
        {
            ShortCode = this.GetPrimaryKeyString(),
            LongUrl = longUrl,
            ClickCount = 0
        };

        await state.WriteStateAsync();
    }
}