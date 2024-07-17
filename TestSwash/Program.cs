using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Routing.Constraints;
using TestSwash;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.Configure<RouteOptions>(options => options.SetParameterPolicy<RegexInlineRouteConstraint>("regex"));
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
var testApi = app.MapGroup("/test");
testApi.MapGet("/swash", TestEndPoint.Swash);

app.Run();

[JsonSerializable(typeof(What))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}
