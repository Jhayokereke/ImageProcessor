using AForge.Video;
using AForge.Video.DirectShow;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using System.Drawing;
using System.Drawing.Imaging;

namespace RtspListener.Services
{
    public class RtspService : IRtspService
    {
        private readonly HubConnection _hubConnection;
        private readonly string _signalRUrl;
        private VideoCaptureDevice _videoSource;

        public RtspService(IConfiguration configuration)
        {
            _signalRUrl = configuration.GetValue<string>("SignalRUrl")!;
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(_signalRUrl)
                .WithAutomaticReconnect()
                .Build();
        }

        public void StartCapture(string rtspUrl)
        {
            _videoSource = new VideoCaptureDevice(rtspUrl);
            _videoSource.NewFrame += new NewFrameEventHandler(VideoSource_NewFrame);
            _videoSource.Start();
        }

        private async void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            using (var bitmap = (Bitmap)eventArgs.Frame.Clone())
            {
                using (var ms = new MemoryStream())
                {
                    bitmap.Save(ms, ImageFormat.Jpeg);
                    var imageBytes = ms.ToArray();
                    await _hubConnection.SendAsync("ReceiveImage", imageBytes);
                }
            }
        }

        public void StopCapture()
        {
            if (_videoSource != null && _videoSource.IsRunning)
            {
                _videoSource.SignalToStop();
                _videoSource = null;
            }
        }
    }
}