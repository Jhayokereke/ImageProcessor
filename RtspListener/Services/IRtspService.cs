
namespace RtspListener.Services
{
    public interface IRtspService
    {
        void StartCapture(string rtspUrl);
        void StopCapture();
    }
}