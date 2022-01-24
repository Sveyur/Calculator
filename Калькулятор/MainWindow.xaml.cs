using System;
using System.Collections.Generic;
using System.Data;
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
using System.Threading;

namespace Калькулятор
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Переменная, указывающая на то, что было выполнено вычисление
        bool envir = false;
        public MainWindow()
        {
            InitializeComponent();
            //Курсор
            text.Focus();
            //Иницилизация кнопок
            foreach (UIElement item in grid.Children)
            {
                if (item is Button)
                {
                    ((Button)item).Click += ButtonClic;
                }
            }
        }
        //Метод обработки нажатий на кнопки
        private void ButtonClic(object sender, RoutedEventArgs e)
        {
            string textButton = ((Button)e.OriginalSource).Content.ToString();
            switch (textButton)
            {
                case "CE":
                    if (text.Text.Length == 0)
                        break;
                    text.Text = text.Text.Remove(text.Text.Length - 1);
                    break;

                case "=":
                    if (text.Text == null)
                        break;
                    envir = true;
                    try
                    {
                        string math = new DataTable().Compute(text.Text, null).ToString();
                        text.Text = math;
                    }
                    catch
                    {
                        text.Text = string.Empty;
                    }
                    break;

                case "C":
                    text.Clear();
                    break;

                default:
                    //Стирает предыдущее вычисление
                    if (envir == true && textButton != "*" && textButton != "/" && textButton != "+" && textButton != "-")
                    {
                        text.Text = textButton;
                    }
                    else
                    {
                        text.Text += textButton;
                    }
                    envir = false;
                    break;
            }
        }

        //Клавиша Enter, OemPlus, Delete c клавиатуры
        private void KeyEvent(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.OemPlus)
            {
                envir = true;
                if (text.Text != null)
                    try
                    {
                        string math = new DataTable().Compute(text.Text, null).ToString();
                        text.Text = math;
                    }
                    catch
                    {
                        text.Text = string.Empty;
                    }
            }
            if (e.Key == Key.Delete)
            {
                text.Clear();
            }
        }

        //Разрешение на ввод с клавиатуры только определенных знаков и стирание предыдущего вычисления
        private void textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "".IndexOf(e.Text) < 0;

            string znach_vvoda = e.Text;
            if (znach_vvoda == ".")
                znach_vvoda = ",";

            List<string> dopustimie_znach = new List<string>()
            { "0", "1", "2","3", "4", "5", "6", "7", "8", "9", "+", "-", "*", "/"};

            if (dopustimie_znach.Contains(znach_vvoda))
            {
                if (envir == true && znach_vvoda != "*" && znach_vvoda != "/" && znach_vvoda != "+" && znach_vvoda != "-")
                {
                    text.Text = znach_vvoda;
                }
                else
                {
                    text.Text += znach_vvoda;
                }
                envir = false;
            }
        }


    }
}
