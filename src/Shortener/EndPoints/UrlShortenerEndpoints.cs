using Microsoft.AspNetCore.Mvc;
using Shortener.Grains;
using Shortener.Services;

namespace Shortener.Endpoints
{
    public static class UrlShortenerEndpoints
    {
        private const string ShortenBadRequest = "The URL is required and needs to be well formed";
        public record ShortenRequest(string Url);
        public record ShortenResponse(string ShortUrl, string Code);
        public record StatsResponse(string Code, long Clicks);

        public static void MapUrlShortenerEndpoints(this WebApplication app)
        {
            app.MapPost("/shorten", ShortenUrl)
                .WithName("ShortenUrl")
                .WithSummary("Shorten a URL")
                .Accepts<ShortenRequest>("application/json")
                .Produces<ShortenResponse>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest);

            app.MapGet("/{short_code:required}", RedirectToLongUrl)
                .WithName("RedirectToLongUrl")
                .Produces(StatusCodes.Status301MovedPermanently)
                .Produces(StatusCodes.Status404NotFound);

            app.MapGet("/{short_code}/stats", GetStats)
                .WithName("GetUrlStats")
                .Produces<StatsResponse>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound);
        }

        private static async Task<IResult> ShortenUrl(
            ShortenRequest request,
            IGrainFactory grains,
            IConfiguration configuration)
        {
            if (string.IsNullOrWhiteSpace(request.Url) ||
                !Uri.IsWellFormedUriString(request.Url, UriKind.Absolute))
            {
                return Results.BadRequest(ShortenBadRequest);
            }

            var shortCode = CodeGenerator.Generate(request.Url);
            var grain = grains.GetGrain<IUrlShortenerGrain>(shortCode);
            await grain.SetLongUrl(request.Url);

            var resultBuilder = new UriBuilder(configuration["BaseUrl"] ?? "https://localhost:5001")
            {
                Path = $"/{shortCode}"
            };

            return Results.Ok(new ShortenResponse(resultBuilder.Uri.ToString(), shortCode));
        }

        private static async Task<IResult> RedirectToLongUrl(
            IGrainFactory grains,
            [FromRoute(Name = "short_code")] string shortCode)
        {
            var grain = grains.GetGrain<IUrlShortenerGrain>(shortCode);
            var url = await grain.GetLongUrl();

            return url is null
                ? Results.NotFound("Short code not found")
                : Results.Redirect(url, permanent: true);
        }

        private static async Task<IResult> GetStats(
            IGrainFactory grains,
            [FromRoute(Name = "short_code")] string shortCode)
        {
            var grain = grains.GetGrain<IUrlShortenerGrain>(shortCode);
            var url = await grain.GetLongUrl();

            if (url is null)
                return Results.NotFound("Short code not found");

            var clicks = await grain.GetClickCount();
            return Results.Ok(new StatsResponse(shortCode, clicks));
        }
    }
}
