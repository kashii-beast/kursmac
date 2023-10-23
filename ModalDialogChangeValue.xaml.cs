using System.Windows;

namespace Burgers;

/// <summary>
/// Логика взаимодействия для ModalDialog.xaml
/// </summary>
public partial class ModalDialogChangeValue : Window
{
	public McFlurry.ETopping Topping => (McFlurry.ETopping)ToppingComboBox.SelectedIndex;

	public ModalDialogChangeValue()
	{
		InitializeComponent();
	}

	public new bool? Show()
	{
		return ShowDialog();
	}

	private void OKButton_Click(object sender, RoutedEventArgs e)
	{
		if (ToppingComboBox.SelectedIndex == -1)
		{
			MessageBox.Show(this, "Fill missing fields first", "Missing fields", MessageBoxButton.OK, MessageBoxImage.Warning);
			return;
		}

		DialogResult = true;
	}

	private void CancelButton_Click(object sender, RoutedEventArgs e)
	{
		DialogResult = false;
	}

	private void ToppingComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
	{
		ToppingPlaceholder.Visibility = Visibility.Hidden;
	}
}