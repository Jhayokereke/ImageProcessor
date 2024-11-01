using Microsoft.AspNet.SignalR;
using SignalRServer.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseRouting();
app.MapHub<ImageHub>("/image-hub");


app.Run();
