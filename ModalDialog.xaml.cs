using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Burgers;

/// <summary>
/// Логика взаимодействия для ModalDialog.xaml
/// </summary>
public partial class ModalDialog : Window
{
	public int Price => int.Parse(PriceInput.Text);

	public int CutletAmount => int.Parse(CutletAmountInput.Text);

	public bool? PutBacon => PutBaconCheckBox.IsEnabled ? PutBaconCheckBox.IsChecked : null;

	[GeneratedRegex("[0-9]+")]
	private static partial Regex NumberRegex();

	public ModalDialog(bool cutlet_amount_input = true, bool bacon_check = false)
	{
		InitializeComponent();

		CutletAmountInput.IsEnabled = cutlet_amount_input;
		PutBaconCheckBox.IsEnabled = bacon_check;
	}

	public new bool? Show()
	{
		return ShowDialog();
	}

	private void OKButton_Click(object sender, RoutedEventArgs e)
	{
		if (string.IsNullOrWhiteSpace(PriceInput.Text) || int.Parse(PriceInput.Text) < 0 ||
			((string.IsNullOrWhiteSpace(CutletAmountInput.Text) || int.Parse(CutletAmountInput.Text) < 0) && CutletAmountInput.IsEnabled))
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

	private void CutletAmountInput_GotFocus(object sender, RoutedEventArgs e)
	{
		CutletAmountPlaceholder.Visibility = Visibility.Hidden;
	}

	private void CutletAmountInput_LostFocus(object sender, RoutedEventArgs e)
	{
		if (string.IsNullOrWhiteSpace(CutletAmountInput.Text))
			CutletAmountPlaceholder.Visibility = Visibility.Visible;
	}

	private void NumberInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
}