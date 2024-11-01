using RtspListener.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddSingleton<IRtspService, RtspService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/start", async (string url, IRtspService _rtspService, CancellationToken cancellationToken) =>
{
    _rtspService.StartCapture(url);
});

app.MapPost("/stop", async (string url, IRtspService _rtspService, CancellationToken cancellationToken) =>
{
    _rtspService.StopCapture();
});

app.Run();