using System.Text.Json.Serialization;

using Microsoft.EntityFrameworkCore;

using PowerDiary.Persistence;
using PowerDiary.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// This is a simple way to ensure the database is created and migrated
// In production application this should be done in a separate setup step
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<PowerDiaryDbContext>();
    context.Database.Migrate();
}

app.Run();
