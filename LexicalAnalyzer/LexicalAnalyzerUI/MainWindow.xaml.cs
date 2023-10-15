using LexicalAnalyzer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LexicalAnalyzerUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string ValidTextMessage = "Текст не содержит ошибок.";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.OutputTextBlock.Text = string.Empty;
            try
            {
                var inputText = this.InputTextBox.Text.Trim();
                new LexicalAnalyzer.LexicalAnalyzer().ValidateText(inputText);
            }
            catch (InvalidTokenException ex)
            {
                this.OutputTextBlock.Text = string.Join(' ', ex.Message, $"Строка {ex.Row} - {ex.Column} символ.");
                this.SelectSymbol(ex.Row, ex.Column);
                return;
            }
            this.OutputTextBlock.Text = ValidTextMessage;
        }

        private void SelectSymbol(int row, int col)
        {
            if (row == 0)
            {
                this.InputTextBox.Select(col - 1, 1);
                return;
            }

            for (int i = 0, symbolIndex = 0, rowIndex = 0; i < this.InputTextBox.Text.Length; i++, symbolIndex++)
            {
                if (this.InputTextBox.Text[i] == '\r')
                {
                    i++;
                    rowIndex++;
                    symbolIndex = 0;
                }
                else if (symbolIndex + 1 == col && rowIndex + 1 == row)
                {
                    this.InputTextBox.Focus();
                    this.InputTextBox.Select(i, 1);
                    return;
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.InputTextBox.Focus();
        }
    }
}
