using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace Burgers;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
	private List<IForKurs> Elements { get; } = new();

	public MainWindow()
	{
		InitializeComponent();
	}

	private static List<Func<string>> _getDelegateList(IForKurs obj)
	{
		return obj.GenerateDelegateList();
	}

	private void buttonAdd_Click(object sender, EventArgs e)
	{
		switch (ClassComboBox.SelectedIndex)
		{
			//0 - BigMac
			case 0:
			{
				ModalDialog dialog = new();
				if (dialog.Show() is true)
				{
					Elements.Add(new BigMac
					{
						Price = dialog.Price,
						CutletAmount = dialog.CutletAmount
					});
					ListBox.Items.Add(Elements[^1]);
				}
			}
			break;
			//1 - CheeseBurger
			case 1:
			{
				ModalDialog dialog = new(bacon_check: true);
				if (dialog.Show() is true)
				{
					Elements.Add(new CheeseBurger
					{
						Price = dialog.Price,
						CutletAmount = dialog.CutletAmount,
						PutBacon = dialog.PutBacon!.Value
					});
					ListBox.Items.Add(Elements[^1]);
				}
			}
			break;
			//2 - DoubleCheese
			case 2:
			{
				ModalDialog dialog = new(bacon_check: true);
				if (dialog.Show() is true)
				{
					Elements.Add(new DoubleCheese
					{
						Price = dialog.Price,
						CutletAmount = dialog.CutletAmount,
						PutBacon = dialog.PutBacon!.Value
					});
					ListBox.Items.Add(Elements[^1]);
				}
			}
			break;
			//3 - BigTasty
			case 3:
			{
				ModalDialog dialog = new();
				if (dialog.Show() is true)
				{
					Elements.Add(new BigTasty
					{
						Price = dialog.Price,
						CutletAmount = dialog.CutletAmount
					});
					ListBox.Items.Add(Elements[^1]);
				}
			}
			break;
			//4 - McFlurry
			case 4:
			{
				ModalDialogMcFlurry dialog = new();
				if (dialog.Show() is true)
				{
					Elements.Add(new McFlurry(dialog.Topping)
					{
						Price = dialog.Price,
					});
					ListBox.Items.Add(Elements[^1]);
				}
			}
			break;
			//5 - HappyMeal
			case 5:
			{
				ModalDialog dialog = new(cutlet_amount_input: false);
				if (dialog.Show() is true)
				{
					Elements.Add(new HappyMeal
					{
						Price = dialog.Price,
					});
					ListBox.Items.Add(Elements[^1]);
				}
			}
			break;
		}
	}

	private void buttonDel_Click(object sender, EventArgs e)
	{
		int index = ListBox.SelectedIndex;
		if (index == -1) return;

		Elements.RemoveAt(index);
		ListBox.Items.RemoveAt(index);
	}

	private void buttonMethod_Click(object sender, EventArgs e)
	{
		int index = ListBox.SelectedIndex;
		if (index == -1) return;

		var delegates = _getDelegateList(Elements[index]);
		ModalDialogMethodInvoke dialog = new(delegates);
		if (dialog.ShowDialog() is not true) return;

		TextBox.AppendText(dialog.Result + Environment.NewLine);
		ListBox.Items[index] = Elements[index].ToString() ?? " ";
	}

	//Сериализация
	private void buttonSave_Click(object sender, EventArgs e)
	{
		SaveFileDialog dialog = new()
		{
			FileName = "Classes",
			DefaultExt = ".json",
			Filter = @"json (*.json)|*.json"
		};

		if (dialog.ShowDialog() is not true) return;

		var file_path = dialog.FileName;

		JsonSerializerOptions json_serializer_options = new()
		{
			WriteIndented = true,

		};

		JsonWriterOptions json_writer_options = new()
		{
			Indented = true,
		};

		using Utf8JsonWriter json_writer = new(File.Open(file_path, FileMode.Create), json_writer_options);
		json_writer.WriteStartArray();

		foreach (var elem in Elements)
		{
			json_writer.WriteStartObject();

			json_writer.WriteString("type", elem.GetType().FullName);

			var bytes = JsonSerializer.Serialize(elem, elem.GetType(), json_serializer_options);
			json_writer.WritePropertyName("object");
			json_writer.WriteRawValue(bytes);

			json_writer.WriteEndObject();
		}

		json_writer.WriteEndArray();
	}

	//Десериализация
	private void buttonLoad_Click(object sender, EventArgs e)
	{
		OpenFileDialog file_dialog = new()
		{
			Filter = "json (*.json)|*.json"
		};

		if (file_dialog.ShowDialog() is not true) return;

		var file_path = file_dialog.FileName;

		using var file_stream = File.Open(file_path, FileMode.Open);
		byte[] buffer = new byte[file_stream.Length];
		int bytes_to_read = (int)file_stream.Length;
		int bytes_read = 0;

		do
		{
			int bytes = file_stream.Read(buffer, bytes_read, bytes_to_read);
			if (bytes == 0) break;

			bytes_read += bytes;
			bytes_to_read -= bytes;

		} while (bytes_to_read > 0);

		var json_reader = new Utf8JsonReader(buffer);

		Elements.Clear();
		ListBox.Items.Clear();

		string? type_string = null;
		while (json_reader.Read())
		{
			switch (json_reader.TokenType)
			{
				case JsonTokenType.PropertyName when json_reader.ValueTextEquals("type"):
				{
					json_reader.Read();
					type_string = json_reader.GetString();
				}
				break;

				case JsonTokenType.PropertyName when json_reader.ValueTextEquals("object") && !string.IsNullOrEmpty(type_string):
				{
					json_reader.Read();
					var type = Type.GetType(type_string);
					if (JsonSerializer.Deserialize(ref json_reader, type!) is IForKurs obj)
					{
						Elements.Add(obj);
						ListBox.Items.Add(obj.ToString());
					}
				}
				break;
			}
		}
	}

	private void RefillIcecreamButton_Click(object sender, RoutedEventArgs e)
	{
		int index = ListBox.SelectedIndex;
		if (index == -1 || Elements[index] is not McFlurry repr) return;

		ModalDialogChangeValue dialog = new();
		if (dialog.ShowDialog() != true) return;

		TextBox.AppendText(repr.RefillIcecream(dialog.Topping) + Environment.NewLine);
		ListBox.Items[index] = Elements[index].ToString() ?? " ";
	}

	private void SingBigMacSongButton_Click(object sender, RoutedEventArgs e)
	{
		TextBox.AppendText(BigMac.Sing() + Environment.NewLine);
	}
}
