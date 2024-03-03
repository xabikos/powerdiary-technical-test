using System.Text.Json.Serialization;

using Microsoft.EntityFrameworkCore;

using NextjsStaticHosting.AspNetCore;

using PowerDiary.Configuration;
using PowerDiary.Persistence;
using PowerDiary.Services;

// In a real world application we should initialize a logger and include the following code in a try-catch block
// to catch any exceptions that occur during the startup process

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<RouteOptions>(options =>
{
    options.ConstraintMap.Add("EventsGranularity", typeof(CustomRouteConstraint));
});
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

// Step 1: Add Next.js hosting support
builder.Services.Configure<NextjsStaticHostingOptions>(builder.Configuration.GetSection("NextjsStaticHosting"));
builder.Services.AddNextjsStaticHosting();

builder.Services.AddDbContext<PowerDiaryDbContext>();

builder.Services.AddScoped<IDataStore, PowerDiaryDbContext>();
builder.Services.AddTransient<IChatEventsService, ChatEventsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

app.MapControllers();

// Step 2: Register dynamic endpoints to serve the correct HTML files at the right request paths.
// Endpoints are created dynamically based on HTML files found under the specified RootPath during startup.
// Endpoints are currently NOT refreshed if the files later change on disk.
app.MapNextjsStaticHtmls();

// Step 3: Serve other required files (e.g. js, css files in the exported next.js app).
app.UseNextjsStaticHosting();

// This is a simple way to ensure the database is created and migrated
// In production application this should be done in a separate setup step
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<PowerDiaryDbContext>();
    context.Database.Migrate();
}

app.Run();
