# ImageProcessor
## Description
This solution consists of two applications:
1. RtspListener - This listens to an rtsp stream and captures frames from the streams then sends each captured frame to the second application using an established signalr hubconnection.
2. SignalRServer - This provides a hubconnection through which the first application sends the captured frames. It then proceeds to save each captured frame to the local file system.

## How to run

### Running App 1 (RtspListener):

Run the aspnet application.
Pass the rtsp url as an argument to the `/fetch` endpoint.


### Running App 2 (SignalRServer):

Ensure the web application is running.
Images will be saved in the ReceivedImages folder in the Videos directory.
