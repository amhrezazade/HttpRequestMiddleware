using HRMServer;
using HRMServer.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddSingleton<RequestResponseHandlerService>();
builder.Services.AddControllers();
 
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseMiddleware<ReequestSenderMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<ApplicationHub>("/appHub");

app.Run();
