using InMemCacheMinimalApi.Api;
using InMemCacheMinimalApi.BusinessLogic;
using InMemCacheMinimalApi.Cache;

var builder = WebApplication.CreateBuilder(args);

// General Services for caching
CachedDataConfiguration.AddServices(builder.Services);

// Services for generating the data
builder.Services.AddSingleton<ICachedDataEntryProducer, ListOfAuthorsProducer>();
builder.Services.AddSingleton<ICachedDataEntryProducer, ListOfProjectsProducer>();

// Services for the APIs
builder.Services.AddScoped<ISummaryService, SummaryService>();
builder.Services.AddScoped<ICacheInfoService, CacheInfoService>();

var app = builder.Build();

app.MapGet("/summary/{objectType}", (string objectType, ISummaryService service) => service.Get(objectType));
app.MapGet("/cacheInfo/{objectType}", (string objectType, ICacheInfoService service) => service.Get(objectType));
app.MapPost("/triggerCacheUpdate/{objectType}", (string objectType, ICacheInfoService service) => service.TriggerUpdate(objectType));

app.Run();
