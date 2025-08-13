using System.IO.Ports;

namespace LuppiterDashboard.Services.Services;

public class CommunicationService
{
    private SerialPort _serialPort;
    public event Action<string> OnDataReceived;

    public void Start() => _serialPort.Open();
    
    public CommunicationService(string comPort, int baudRate)
    {
        _serialPort = new SerialPort(comPort, baudRate);
        _serialPort.DataReceived += SerialPortDataReceived;
    }

    private void SerialPortDataReceived(object sender, SerialDataReceivedEventArgs e)
       => OnDataReceived?.Invoke(_serialPort.ReadLine());
}