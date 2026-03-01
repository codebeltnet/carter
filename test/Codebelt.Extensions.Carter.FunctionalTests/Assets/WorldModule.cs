using System.Linq;
using System.Threading;
using Carter;
using Carter.Response;
using Cuemon.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Codebelt.Extensions.Carter.Assets;

internal sealed class WorldModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/world/statistical-regions", (HttpResponse res, CancellationToken ct) =>
            res.Negotiate(World.StatisticalRegions
                .Where(r => r.Kind == StatisticalRegionKind.World)
                .Select(r => new StatisticalRegionModel { Code = r.Code, Name = r.Name, Kind = r.Kind }), ct));
    }
}

internal sealed class StatisticalRegionModel
{
    public string Code { get; init; }
    public string Name { get; init; }
    public StatisticalRegionKind Kind { get; init; }
}
