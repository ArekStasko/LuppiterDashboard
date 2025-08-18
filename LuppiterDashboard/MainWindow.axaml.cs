using System.Collections.ObjectModel;
using System.Runtime.InteropServices.JavaScript;
using Avalonia.Controls;
using Avalonia.Threading;
using LuppiterDashboard.Services.Services;

namespace LuppiterDashboard;

public partial class MainWindow : Window
{
    private readonly CommunicationService _communicationService;
    public MainWindow()
    {
        InitializeComponent();
        DataContext = this;

        _communicationService = new CommunicationService("/dev/ttyUSB0", 115200);
        _communicationService.OnDataReceived += (data) =>
        {
            Dispatcher.UIThread.Post(() =>
            {
                if (data.Contains("DATA"))
                {
                    var elements = data.Split('|');
                    TemperatureControl.AddData(float.Parse(elements[1].Trim()));
                    AltitudeControl.AddData(float.Parse(elements[2].Trim()));
                    PressureControl.AddData(float.Parse(elements[3].Trim()));
                }
            });
        };
        
        _communicationService.Start();

    }
}