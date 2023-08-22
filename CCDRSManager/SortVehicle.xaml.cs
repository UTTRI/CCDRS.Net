using System.Windows;

namespace CCDRSManager;

/// <summary>
/// Interaction logic for SortVehicle.xaml
/// </summary>
public partial class SortVehicle : Window
{
    public SortVehicle()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Method to move the selected vehicle down the list.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void MoveVehicleDown(object sender, RoutedEventArgs e)
    {
        int VehicleIndex = SortVehicleList.SelectedIndex;
        if (DataContext is CCDRSManagerViewModel viewModel)
        {
            viewModel.MoveTechnologyDown(VehicleIndex);
        }
    }

    /// <summary>
    /// Move the selected vehicle up the list.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void MoveVehicleUp(object sender, RoutedEventArgs e)
    {
        int VehicleIndex = SortVehicleList.SelectedIndex;
        if (DataContext is CCDRSManagerViewModel viewModel)
        {
            viewModel.MoveTechnologyUp(VehicleIndex);
        }
    }

    /// <summary>
    /// Save the updated vehicle order to the datbase.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SaveSortedVehicleData(object sender, RoutedEventArgs e)
    {
        if (DataContext is CCDRSManagerViewModel viewModel)
        {
            viewModel.SaveVehicleOrder();
        }
    }
}
