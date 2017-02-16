using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using winForms = System.Windows.Forms;
namespace KabelTest
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        char[] ArrayFromFile_1;
        char[] ArrayFromFile_2;
        string String_ArrayFromFile_1;
        string String_ArrayFromFile_2;

        String reciveString;
        private char[] recive_arr;

        public MainWindow()
        {
            InitializeComponent();
            Console_comparingWindow.AppendText("\n");
            setStacPanels();

            if ((Properties.Settings.Default.PathSaveFolder == null) || (Properties.Settings.Default.PathSaveFolder == ""))
            {
                winForms.FolderBrowserDialog FBD = new winForms.FolderBrowserDialog();                
                FBD.ShowDialog();
                Properties.Settings.Default.PathSaveFolder = FBD.SelectedPath;               
            }
            try
            {
                if((Properties.Settings.Default.COMport == null) || (Properties.Settings.Default.COMport == ""))
                {
                    MessageBox.Show("Для связи с прибором выберите порт.\nПункт Меню: Окно -> Порт");
                    Console_comparingWindow.AppendText("Для связи с прибором выберите порт \nПункт Меню: Окно -> Порт");
                }

                SerialPort_class.ConnectingSerialPort(Properties.Settings.Default.COMport);
            }
            catch { }
            UpdateListBox();
        }
        
        private void setStacPanels()
        {

            var stacPanelsList = GetStackPanels_ComparingTableGrid();
            int lable_counter = 0;
            byte pass_1 = 1;
            byte pass_2 = 1;
            foreach (var item in stacPanelsList)
            {
                for (; lable_counter < 41; lable_counter++)
                {
                    Label compGridTable_lable = new Label();        //-----------------------cereate top line of frame
                    compGridTable_lable.MinWidth = 30;

                    compGridTable_lable.BorderBrush = Brushes.RoyalBlue;
                    compGridTable_lable.BorderThickness = new Thickness(1, 1, 1, 1);
                    compGridTable_lable.HorizontalContentAlignment = HorizontalAlignment.Center;
                    compGridTable_lable.VerticalContentAlignment = VerticalAlignment.Center;
                    compGridTable_lable.Background = Brushes.Goldenrod;
                    if ((lable_counter % 20) == 0) compGridTable_lable.Content = "  ";
                    else compGridTable_lable.Content = lable_counter % 20;
                    compGridTable_lable.FontWeight = FontWeights.Bold;
                    item.Children.Add(compGridTable_lable);
                }


                if (lable_counter == 41)
                {
                    lable_counter++;
                    continue;
                }
                if (lable_counter == 42) lable_counter--;


                for (int i = 0; (i < 41) && (lable_counter < 820); i++)     //-----------------------cereate pass 1
                {
                    Label compGridTable_lable = new Label();
                    compGridTable_lable.MinWidth = 30;

                    compGridTable_lable.BorderBrush = Brushes.RoyalBlue;
                    compGridTable_lable.BorderThickness = new Thickness(1, 1, 1, 1);
                    compGridTable_lable.HorizontalContentAlignment = HorizontalAlignment.Center;
                    compGridTable_lable.VerticalContentAlignment = VerticalAlignment.Center;
                    if ((i == 0) || (i == 20) || (i == 40))
                    {
                        compGridTable_lable.Background = Brushes.Goldenrod;
                        compGridTable_lable.Content = pass_1;
                    }
                    else
                    {
                        compGridTable_lable.Background = Brushes.CornflowerBlue;
                        compGridTable_lable.Content = i % 20;
                    }
                    compGridTable_lable.FontWeight = FontWeights.Bold;
                    item.Children.Add(compGridTable_lable);
                    lable_counter++;
                }
                pass_1++;

                if (lable_counter < 820) continue;
                if (lable_counter == 820)
                {
                    lable_counter++;
                    continue;
                }
                if (lable_counter == 821) lable_counter--;


                for (; lable_counter < 861; lable_counter++)
                {
                    Label compGridTable_lable = new Label();        //-----------------------cereate center line of frame
                    compGridTable_lable.MinWidth = 30;

                    compGridTable_lable.BorderBrush = Brushes.RoyalBlue;
                    compGridTable_lable.BorderThickness = new Thickness(1, 1, 1, 1);
                    compGridTable_lable.HorizontalContentAlignment = HorizontalAlignment.Center;
                    compGridTable_lable.VerticalContentAlignment = VerticalAlignment.Center;
                    compGridTable_lable.Background = Brushes.Goldenrod;
                    if ((lable_counter % 20) == 0) compGridTable_lable.Content = "  ";
                    else compGridTable_lable.Content = lable_counter % 20;
                    compGridTable_lable.FontWeight = FontWeights.Bold;
                    item.Children.Add(compGridTable_lable);
                }


                if (lable_counter == 861)
                {
                    lable_counter++;
                    continue;
                }
                if (lable_counter == 862) lable_counter--;


                for (int i = 0; (i < 41) && (lable_counter < 1640); i++)     //-----------------------cereate pass 2
                {
                    Label compGridTable_lable = new Label();
                    compGridTable_lable.MinWidth = 30;

                    compGridTable_lable.BorderBrush = Brushes.RoyalBlue;
                    compGridTable_lable.BorderThickness = new Thickness(1, 1, 1, 1);
                    compGridTable_lable.HorizontalContentAlignment = HorizontalAlignment.Center;
                    compGridTable_lable.VerticalContentAlignment = VerticalAlignment.Center;
                    if ((i == 0) || (i == 20) || (i == 40))
                    {
                        compGridTable_lable.Background = Brushes.Goldenrod;
                        compGridTable_lable.Content = pass_2;
                    }
                    else
                    {
                        compGridTable_lable.Background = Brushes.CornflowerBlue;
                        compGridTable_lable.Content = i % 20;
                    }
                    compGridTable_lable.FontWeight = FontWeights.Bold;
                    item.Children.Add(compGridTable_lable);
                    lable_counter++;
                }
                pass_2++;

                if (lable_counter < 1640) continue;
                if (lable_counter == 1640)
                {
                    lable_counter++;
                    continue;
                }
                if (lable_counter == 1641) lable_counter--;


                for (; lable_counter < 1681; lable_counter++)
                {
                    Label compGridTable_lable = new Label();        //-----------------------cereate bottom line of frame
                    compGridTable_lable.MinWidth = 30;

                    compGridTable_lable.BorderBrush = Brushes.RoyalBlue;
                    compGridTable_lable.BorderThickness = new Thickness(1, 1, 1, 1);
                    compGridTable_lable.HorizontalContentAlignment = HorizontalAlignment.Center;
                    compGridTable_lable.VerticalContentAlignment = VerticalAlignment.Center;
                    compGridTable_lable.Background = Brushes.Goldenrod;
                    if ((lable_counter % 20) == 0) compGridTable_lable.Content = "  ";
                    else compGridTable_lable.Content = lable_counter % 20;
                    compGridTable_lable.FontWeight = FontWeights.Bold;
                    item.Children.Add(compGridTable_lable);
                }
            }
        }

        private List<StackPanel> GetStackPanels_ComparingTableGrid()
        {
            var result = new List<StackPanel>();
            foreach (var child in ComparingTableGrid.Children)
            {
                var stackPanel = child as StackPanel;
                if (stackPanel == null) continue;

                result.Add(stackPanel);
            }

            return result;
        }

        private List<Label> GetLabeles_ComparingTableGrid()
        {
            var result = new List<Label>();
            foreach (var stacPanel in GetStackPanels_ComparingTableGrid())
            {
                foreach (var labels in stacPanel.Children)
                {
                    var label = labels as Label;
                    if ((label == null) || (label.Background == Brushes.Goldenrod)) continue;
                    result.Add(label);
                }
            }
            return result;
        }

        private void ClearLabeles_ComparingTableGrid()
        {
            foreach (var item in GetLabeles_ComparingTableGrid())
            {
                item.Content = " ";
                item.Background = Brushes.CornflowerBlue;
                item.BorderBrush = Brushes.RoyalBlue;
                item.ToolTip = null;
                item.MouseLeftButtonDown -= MouseleftButtonDownEvent;
            }
        }

        private void ComparingButton_Click(object sender, RoutedEventArgs e)
        {
            Comparing();
        }

        private void ClearLabeles_Click(object sender, RoutedEventArgs e)
        {
            CanvasGrid.Children.Clear();
            ClearLabeles_ComparingTableGrid();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            GenerateTopolpgy generateTopology_window = new GenerateTopolpgy();
            generateTopology_window.Owner = this;
            generateTopology_window.Show();
        }

        private void PortItem_Click(object sender, RoutedEventArgs e)
        {
            Window1 ComPortOptions_window = new Window1();
            ComPortOptions_window.Owner = this;
            ComPortOptions_window.Show();
        }

        private void UpdateListBox()
        {           
            try
            {
                listBox.Items.Clear();
                foreach (var item in Directory.GetFiles(Properties.Settings.Default.PathSaveFolder + "\\"))
                {
                    if (System.IO.Path.GetExtension(item) == ".kts")
                    {
                        listBox.Items.Add(System.IO.Path.GetFileNameWithoutExtension(item));
                    }
                }
                listBox.Focus();
                listBox.SelectedIndex = 0;
            }
            catch
            {
                Console_comparingWindow.AppendText("Не удалось найти папку: " + Properties.Settings.Default.PathSaveFolder + "\n");
            }
        }

        private void listBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SetSchemeCompWin();
        }

        private void SchemeCompWin_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (SchemeCompWin.Stretch == Stretch.Uniform)
            {
                SchemeCompWin.Stretch = Stretch.None;
            }
            else if (SchemeCompWin.Stretch == Stretch.None)
            {
                SchemeCompWin.Stretch = Stretch.Fill;
            }
            else if (SchemeCompWin.Stretch == Stretch.Fill)
            {
                SchemeCompWin.Stretch = Stretch.Uniform;
            }
        }

        private void RefreshList_Click(object sender, RoutedEventArgs e)
        {            
            UpdateListBox();
        }

        private void listBox_KeyDown(object sender, KeyEventArgs e)
        {           
            Comparing();
        }

        private void SetSchemeCompWin()
        {
            SchemeCompWin.Source = null;
            try
            {
                var bmi = new BitmapImage();
                bmi.BeginInit();
                bmi.CacheOption = BitmapCacheOption.OnLoad;
                bmi.UriSource = new Uri(Properties.Settings.Default.PathSaveFolder + "\\" + listBox.SelectedItem.ToString() + ".jpg", UriKind.Absolute);
                bmi.EndInit();
                SchemeCompWin.Source = bmi;

            }
            catch
            { }
            try
            {
                var bmi = new BitmapImage();
                bmi.BeginInit();
                bmi.CacheOption = BitmapCacheOption.OnLoad;
                bmi.UriSource = new Uri(Properties.Settings.Default.PathSaveFolder + "\\" + listBox.SelectedItem.ToString() + ".png", UriKind.Absolute);
                bmi.EndInit();
                SchemeCompWin.Source = bmi;

            }
            catch
            { }
            try
            {
                var bmi = new BitmapImage();
                bmi.BeginInit();
                bmi.CacheOption = BitmapCacheOption.OnLoad;
                bmi.UriSource = new Uri(Properties.Settings.Default.PathSaveFolder + "\\" + listBox.SelectedItem.ToString() + ".jpeg", UriKind.Absolute);
                bmi.EndInit();
                SchemeCompWin.Source = bmi;
            }
            catch
            { }
            try
            {
                var bmi = new BitmapImage();
                bmi.BeginInit();
                bmi.CacheOption = BitmapCacheOption.OnLoad;
                bmi.UriSource = new Uri(Properties.Settings.Default.PathSaveFolder + "\\" + listBox.SelectedItem.ToString() + ".gif", UriKind.Absolute);
                bmi.EndInit();
                SchemeCompWin.Source = bmi;
            }
            catch
            { }
            try
            {
                var bmi = new BitmapImage();
                bmi.BeginInit();
                bmi.CacheOption = BitmapCacheOption.OnLoad;
                bmi.UriSource = new Uri(Properties.Settings.Default.PathSaveFolder + "\\" + listBox.SelectedItem.ToString() + ".bmp", UriKind.Absolute);
                bmi.EndInit();
                SchemeCompWin.Source = bmi;
            }
            catch
            { }
            try
            {
                var bmi = new BitmapImage();
                bmi.BeginInit();
                bmi.CacheOption = BitmapCacheOption.OnLoad;
                bmi.UriSource = new Uri(Properties.Settings.Default.PathSaveFolder + "\\" + listBox.SelectedItem.ToString() + ".tif", UriKind.Absolute);
                bmi.EndInit();
                SchemeCompWin.Source = bmi;
            }
            catch
            { }
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string[] OpenFile_strings = new string[Properties.Settings.Default.Stings_in_saved_file];
            OpenFile_strings = File.ReadAllLines(Properties.Settings.Default.PathSaveFolder + "\\" + listBox.SelectedItem.ToString() + ".kts");
            String_ArrayFromFile_1 = OpenFile_strings[4];
            String_ArrayFromFile_2 = OpenFile_strings[5];
            ArrayFromFile_1 = OpenFile_strings[4].ToCharArray();
            ArrayFromFile_2 = OpenFile_strings[5].ToCharArray();
            SetSchemeCompWin();
        }

        private void Comparing()
        {
            CanvasGrid.Children.Clear();

            Console_comparingWindow.Text = "Результат проверки: " + listBox.SelectedItem + ".  ";
            byte first_or_second = 0;
            ClearLabeles_ComparingTableGrid();
            try
            {
                SerialPort_class._serialPort.Write("1");
                reciveString = SerialPort_class._serialPort.ReadLine();
                recive_arr = reciveString.ToCharArray();
                if (reciveString.Equals(String_ArrayFromFile_1))
                {
                    ComparingButton.Background = Brushes.CadetBlue;
                    ComparingButton.Content = "ОК (X1 - X2)";
                    first_or_second = 1;
                }
                else if (reciveString.Equals(String_ArrayFromFile_2))
                {
                    ComparingButton.Background = Brushes.CadetBlue;
                    ComparingButton.Content = "ОК (X2 - X1)";
                    first_or_second = 2;
                }
                else
                {
                    ComparingButton.Background = Brushes.Red;
                    ComparingButton.Content = "Несоответствие!";
                }

                if (first_or_second == 1) 
                {
                    int i = 0;
                    foreach (var item in GetLabeles_ComparingTableGrid())
                    {
                        if (recive_arr[i] == ' ') i++;
                       
                        item.Content = recive_arr[i];
                        
                        
                            if (ArrayFromFile_1[i] == recive_arr[i])
                            {
                                if (recive_arr[i] == '1') item.Background = Brushes.LimeGreen;
                                else if (recive_arr[i] == '*') item.Background = Brushes.SlateBlue;
                            }                            
                            i++;
                        }
                    }
                else if (first_or_second == 2)
                {
                    int i = 0;
                    foreach (var item in GetLabeles_ComparingTableGrid())
                    {
                        if (recive_arr[i] == ' ') i++;
                        item.Content = recive_arr[i];                       

                        if (ArrayFromFile_2[i] == recive_arr[i])
                        {
                            if (recive_arr[i] == '1') item.Background = Brushes.LimeGreen;
                            else if (recive_arr[i] == '*') item.Background = Brushes.SlateBlue;
                        }
                        i++;
                    }
                }
               
                else if (first_or_second == 0) ComparingResultFalse();
            }
            catch { }
        }

        private void ComparingResultFalse()
        {
            int greenlabeles_1 = 0;
            int greenlabeles_2 = 0;
            int i = 0;
            var StackPanelList = new List<StackPanel>();           
            foreach (var element in recive_arr)
            {
                if ((element == '1') && (ArrayFromFile_1[i] == element))
                {
                    greenlabeles_1++;
                }
                i++;
            }            
            i = 0;
            foreach (var element in recive_arr)
            {
                if ((element == '1') && (ArrayFromFile_2[i] == element))
                {
                    greenlabeles_2++;
                }
                i++;
            }            

            if(greenlabeles_1 >= greenlabeles_2)
            {
                Console_comparingWindow.AppendText("При установке X1 - X2:" + "\n\n");
                i = 0;
                foreach (var item in GetLabeles_ComparingTableGrid())
                {
                    if (recive_arr[i] == ' ') i++;
                    item.Content = recive_arr[i];

                    if (ArrayFromFile_1[i] == recive_arr[i])
                    {
                        if (recive_arr[i] == '1') item.Background = Brushes.LimeGreen;
                        else if (recive_arr[i] == '*') item.Background = Brushes.SlateBlue;
                    }
                    else
                    {                        
                        item.Background = Brushes.DarkRed;
                        var stackpanel = item.Parent as StackPanel;
                        StackPanelList.Add(stackpanel);                        
                    }
                    i++;
                }                
            }

            else
            {
                Console_comparingWindow.AppendText("При установке X2 - X1: " + "\n\n");
                i = 0;
                foreach (var item in GetLabeles_ComparingTableGrid())
                {
                    if (recive_arr[i] == ' ') i++;
                    item.Content = recive_arr[i];                  

                    if (ArrayFromFile_2[i] == recive_arr[i])
                    {
                        if (recive_arr[i] == '1') item.Background = Brushes.LimeGreen;
                        else if (recive_arr[i] == '*') item.Background = Brushes.SlateBlue;
                    }
                    else
                    {
                        item.Background = Brushes.DarkRed;
                        var stackpanel = item.Parent as StackPanel;
                        StackPanelList.Add(stackpanel);
                    }
                    i++;
                }                
            }

            if(StackPanelList != null)
            report_to_Console_comparingWindow(StackPanelList);
        }

        private void report_to_Console_comparingWindow(List<StackPanel> stackPanelList)
        {
            string Xn_SourcePin = "";
            byte SourcePin = 0;
            string Xn_ErrorPin = "";
            byte ErrorPin = 0;
            string Connect_o_notConnect = "";
            var ReportStringList = new List<String>();


            foreach (var stackPanel in stackPanelList)
            {
                if (stackPanel == null) continue;

                byte counterForeach = 0;
                foreach (var item in stackPanel.Children)
                {
                    var lable = item as Label;
                    if (lable == null) continue;
                    if (lable.Content.ToString() == "*")
                    {
                        SourcePin = counterForeach;
                        Xn_SourcePin = " X1 ";
                        if (SourcePin > 19)
                        {
                            SourcePin -= 20;
                            Xn_SourcePin = " X2 ";
                        }                        
                    }
                    counterForeach++;
                }               

                counterForeach = 0;
                foreach (var item in stackPanel.Children)
                {
                    var lable = item as Label;
                    if (lable == null) continue;
                    if (lable.Background == Brushes.DarkRed)
                    {
                        if (lable.Content.ToString() == "1") Connect_o_notConnect = " присоединён ";
                        else if (lable.Content.ToString() == "0") Connect_o_notConnect = " не присоединён ";

                        ErrorPin = counterForeach;
                        Xn_ErrorPin = " X1.";
                        if(ErrorPin > 20)
                        {
                            ErrorPin -= 20;
                            Xn_ErrorPin = " X2.";
                        }

                        string s = "Контакт  " + SourcePin + "  разъёма " + Xn_SourcePin + Connect_o_notConnect + " к контакту  " + ErrorPin + "  разъёма " + Xn_ErrorPin;                       
                        lable.ToolTip = s;
                        lable.MouseLeftButtonDown -= MouseleftButtonDownEvent;
                        lable.MouseLeftButtonDown += MouseleftButtonDownEvent;                                                                    
                        ReportStringList.Add(s);
                    }
                    counterForeach++;
                }                
            }
            
            IEnumerable<string> distinctReportStringList = ReportStringList.Distinct();
            foreach (var s in distinctReportStringList)
            {
                Console_comparingWindow.AppendText(s + "\n");
            }

        }

        private void MouseleftButtonDownEvent(object sender, MouseButtonEventArgs e)
        {           
            var myLine1 = new Line();
            var position1 = Mouse.GetPosition(CanvasGrid);
            myLine1.Stroke = System.Windows.Media.Brushes.White;
            myLine1.X1 = position1.X;
            myLine1.X2 = position1.X;
            myLine1.Y1 = 0;
            myLine1.Y2 = CanvasGrid.ActualHeight;            
            myLine1.StrokeThickness = 1;
            CanvasGrid.Children.Add(myLine1);

            var myLine2 = new Line();
            var position2 = Mouse.GetPosition(CanvasGrid);
            myLine2.Stroke = System.Windows.Media.Brushes.White;
            myLine2.X1 = 0;
            myLine2.X2 = CanvasGrid.ActualWidth;
            myLine2.Y1 = position2.Y;
            myLine2.Y2 = position2.Y;
            myLine2.StrokeThickness = 1;
            CanvasGrid.Children.Add(myLine2);

            var lable = sender as Label;
            MessageBox.Show(lable.ToolTip.ToString());                      
        }

        private void listBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Comparing();
        }

        private void Console_comparingWindow_TextChanged(object sender, TextChangedEventArgs e)
        {
            Console_comparingWindow.ScrollToEnd();
        }
        
        private void ComparingTableGrid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var myLine1 = new Line();
            var position1 = Mouse.GetPosition(CanvasGrid);
            myLine1.Stroke = System.Windows.Media.Brushes.White;
            myLine1.X1 = position1.X;
            myLine1.X2 = position1.X;
            myLine1.Y1 = 0;
            myLine1.Y2 = CanvasGrid.ActualHeight;
            myLine1.StrokeThickness = 1;
            CanvasGrid.Children.Add(myLine1);

            var myLine2 = new Line();
            var position2 = Mouse.GetPosition(CanvasGrid);
            myLine2.Stroke = System.Windows.Media.Brushes.White;
            myLine2.X1 = 0;
            myLine2.X2 = CanvasGrid.ActualWidth;
            myLine2.Y1 = position2.Y;
            myLine2.Y2 = position2.Y;
            myLine2.StrokeThickness = 1;
            CanvasGrid.Children.Add(myLine2);
        }

        private void ComparingTableGrid_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {                       
                CanvasGrid.Children.Clear();            
        }
        
    }
}
