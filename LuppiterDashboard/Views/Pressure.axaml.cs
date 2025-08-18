using System.Collections.ObjectModel;
using Avalonia.Controls;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Drawing.Geometries;

namespace LuppiterDashboard;

public partial class Pressure : UserControl
{
    public ISeries[] Series { get; set; } =
    {
        new LineSeries<float>
        {
            Values = new ObservableCollection<float>(),
            Fill = null,
            GeometrySize = 20,
        }
    };
    
    public string Title { get; set; } = "Pressure";
    public Pressure()
    {
        InitializeComponent();
        DataContext = this;
    }
    
    public void AddData(float value)
    {
        if (Series[0] is LineSeries<float> lineSeries)
        {
            if (lineSeries.Values is ObservableCollection<float> values)
            {
                values.Add(value);
            }
        }
    }
}