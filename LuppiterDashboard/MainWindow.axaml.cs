using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Threading;
using LuppiterDashboard.Services.Services;

namespace LuppiterDashboard;

public partial class Temperature : Window
{
    private readonly CommunicationService _communicationService;
    public ObservableCollection<string> Data { get; } = new();
    public Temperature()
    {
        InitializeComponent();
        DataContext = this;

        _communicationService = new CommunicationService("/dev/ttyUSB0", 115200);
        _communicationService.OnDataReceived += (data) =>
        {
            Dispatcher.UIThread.Post(() =>
            {
                if (data.Contains("DATA"))
                    Data.Add(data);
            });
        };
        
        _communicationService.Start();
    }
}