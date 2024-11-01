using Microsoft.AspNetCore.SignalR;

namespace SignalRServer.Hubs
{
    public class ImageHub : Hub
    {
        public async Task ReceiveImage(byte[] image)
        {
            var directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos), "ReceivedImages");

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var imagePath = Path.Combine(directoryPath, Guid.NewGuid().ToString() + ".jpg");
            await File.WriteAllBytesAsync(imagePath, image);
        }
    }
}
