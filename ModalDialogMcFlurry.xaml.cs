using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Burgers;

/// <summary>
/// Логика взаимодействия для ModalDialog.xaml
/// </summary>
public partial class ModalDialogMcFlurry : Window
{
	public int Price => int.Parse(PriceInput.Text);

	public McFlurry.ETopping Topping => (McFlurry.ETopping)ToppingComboBox.SelectedIndex;

	[GeneratedRegex("[0-9]+")]
	private static partial Regex NumberRegex();

	public ModalDialogMcFlurry()
	{
		InitializeComponent();
	}

	public new bool? Show()
	{
		return ShowDialog();
	}

	private void OKButton_Click(object sender, RoutedEventArgs e)
	{
		if (string.IsNullOrWhiteSpace(PriceInput.Text) && int.Parse(PriceInput.Text) < 0 || ToppingComboBox.SelectedIndex == -1)
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

	private void PriceInput_GotFocus(object sender, RoutedEventArgs e)
	{
		PricePlaceholder.Visibility = Visibility.Hidden;
	}

	private void PriceInput_LostFocus(object sender, RoutedEventArgs e)
	{
		if (string.IsNullOrWhiteSpace(PriceInput.Text))
			PricePlaceholder.Visibility = Visibility.Visible;
	}

	private void PriceInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
	{
		bool is_match = NumberRegex().IsMatch(e.Text);
		if (is_match)
		{
			int result = int.Parse(e.Text);
			e.Handled = !(is_match && result < 1000000);
			return;
		}

		e.Handled = true;
	}

	private void ToppingComboBox_SelectionChanged(object sender, RoutedEventArgs e)
	{
		ToppingPlaceholder.Visibility = Visibility.Hidden;
	}
}
