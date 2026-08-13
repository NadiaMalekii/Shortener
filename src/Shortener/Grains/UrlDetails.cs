namespace Shortener.Grains;


[GenerateSerializer, Alias(nameof(UrlDetails))]
public sealed record class UrlDetails
{
    [Id(0)]
    public string LongUrl { get; set; } = "";

    [Id(1)]
    public string ShortCode { get; set; } = "";

    [Id(2)]
    public long ClickCount { get; set; }
}