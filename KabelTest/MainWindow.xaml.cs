using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
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
        string ShortStringFromFile_2;
        string ShortStringFromFile_3;
        string MorF_X1;
        string MorF_X2;

        string QuickComparingString;
        private char[] QuickComparingArr;

        string reciveString;
        private char[] recive_arr;

        public delegate void StringToReverseDelegate(string s);
        public event StringToReverseDelegate StringToReverse;

        public delegate void WriteToEepromDelegate(string s);
        public event WriteToEepromDelegate sequenceWriteToEeprom_event;

        GenerateTopolpgy generateTopology_window = new GenerateTopolpgy();
        

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
                Properties.Settings.Default.Save();
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
            SerialPort_class._serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);




            
            //generateTopology_window.Owner = this;
            this.StringToReverse -= generateTopology_window.Reverse;
            this.StringToReverse += generateTopology_window.Reverse;

            this.sequenceWriteToEeprom_event -= generateTopology_window.sequenceWriteToEeprom;
            if (this.sequenceWriteToEeprom_event == null)
            {
                this.sequenceWriteToEeprom_event += generateTopology_window.sequenceWriteToEeprom;
            }           
        }
       
        private void setStacPanels()
        {
            UInt16 lable_min_Width;            
            var resolution = SystemParameters.WorkArea.Width;
            Console.WriteLine(resolution);
            if(resolution <= 1920) lable_min_Width = 30;
            else lable_min_Width = 40;

            var stacPanelsList = GetStackPanels_ComparingTableGrid();
            int lable_counter = 0;
            byte pass_1 = 1;
            byte pass_2 = 1;
            foreach (var item in stacPanelsList)
            {
                for (; lable_counter < 41; lable_counter++)
                {
                    Label compGridTable_lable = new Label();        //-----------------------cereate top line of frame
                    compGridTable_lable.MinWidth = lable_min_Width;

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
                    compGridTable_lable.MinWidth = lable_min_Width;

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
                    compGridTable_lable.MinWidth = lable_min_Width;

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
                    compGridTable_lable.MinWidth = lable_min_Width;

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
                    compGridTable_lable.MinWidth = lable_min_Width;

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
            StartComparing();
        }       

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {                       
            generateTopology_window.Show();
        }

        private void PortItem_Click(object sender, RoutedEventArgs e)
        {
            Window1 ComPortOptions_window = new Window1();
            ComPortOptions_window.Owner = this;
            //ComPortOptions_window.Show();
            ComPortOptions_window.ShowDialog();
        }

        private void UpdateListBox()
        {
            
            try
             {                
                SchemeCompWin.Source = null;                                
                listBox.SelectionChanged -= listBox_SelectionChanged;
                listBox.Items.Clear();
                foreach (var item in Directory.GetFiles(Properties.Settings.Default.PathSaveFolder + "\\"))
                {
                    if (System.IO.Path.GetExtension(item) == ".kts")
                    {
                        listBox.Items.Add(System.IO.Path.GetFileNameWithoutExtension(item));
                    }
                }
            listBox.SelectionChanged += listBox_SelectionChanged;
            listBox.Focus();
            listBox.SelectedIndex = 0;
            SetSchemeCompWin();            
            }
            catch
            {
                winForms.FolderBrowserDialog FBD = new winForms.FolderBrowserDialog();
                FBD.ShowDialog();
                if (FBD.SelectedPath != "")
                {
                    Properties.Settings.Default.PathSaveFolder = FBD.SelectedPath;
                    Properties.Settings.Default.Save();
                }
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
            StartComparing();
        }

        private void SetSchemeCompWin()
        {            
            byte picture_absence_counter = 0;
            SchemeCompWin.Source = null;
            try
            {
                var bmi = new BitmapImage();
                bmi.BeginInit();
                bmi.CacheOption = BitmapCacheOption.OnLoad;
                bmi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                bmi.UriSource = new Uri(Properties.Settings.Default.PathSaveFolder + "\\" + listBox.SelectedItem.ToString() + ".jpg", UriKind.Absolute);
                bmi.EndInit();
                SchemeCompWin.Source = bmi;

            }
            catch
            { picture_absence_counter++; }
            try
            {
                var bmi = new BitmapImage();
                bmi.BeginInit();
                bmi.CacheOption = BitmapCacheOption.OnLoad;
                bmi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                bmi.UriSource = new Uri(Properties.Settings.Default.PathSaveFolder + "\\" + listBox.SelectedItem.ToString() + ".png", UriKind.Absolute);
                bmi.EndInit();
                SchemeCompWin.Source = bmi;

            }
            catch
            { picture_absence_counter++; }
            try
            {
                var bmi = new BitmapImage();
                bmi.BeginInit();
                bmi.CacheOption = BitmapCacheOption.OnLoad;
                bmi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                bmi.UriSource = new Uri(Properties.Settings.Default.PathSaveFolder + "\\" + listBox.SelectedItem.ToString() + ".jpeg", UriKind.Absolute);
                bmi.EndInit();
                SchemeCompWin.Source = bmi;
            }
            catch
            { picture_absence_counter++; }
            try
            {
                var bmi = new BitmapImage();
                bmi.BeginInit();
                bmi.CacheOption = BitmapCacheOption.OnLoad;
                bmi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                bmi.UriSource = new Uri(Properties.Settings.Default.PathSaveFolder + "\\" + listBox.SelectedItem.ToString() + ".gif", UriKind.Absolute);
                bmi.EndInit();
                SchemeCompWin.Source = bmi;
            }
            catch
            { picture_absence_counter++; }
            try
            {
                var bmi = new BitmapImage();
                bmi.BeginInit();
                bmi.CacheOption = BitmapCacheOption.OnLoad;
                bmi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                bmi.UriSource = new Uri(Properties.Settings.Default.PathSaveFolder + "\\" + listBox.SelectedItem.ToString() + ".bmp", UriKind.Absolute);
                bmi.EndInit();
                SchemeCompWin.Source = bmi;
            }
            catch
            { picture_absence_counter++; }
            try
            {
                var bmi = new BitmapImage();
                bmi.BeginInit();
                bmi.CacheOption = BitmapCacheOption.OnLoad;
                bmi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                bmi.UriSource = new Uri(Properties.Settings.Default.PathSaveFolder + "\\" + listBox.SelectedItem.ToString() + ".tif", UriKind.Absolute);
                bmi.EndInit();
                SchemeCompWin.Source = bmi;
            }
            catch
            { picture_absence_counter++; }
            if (picture_absence_counter >= 6)
            {
                Create_PictureScheme();
            }
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SchemeStackPanel.Visibility = Visibility.Hidden;
            
            string[] OpenFile_strings = new string[Properties.Settings.Default.Stings_in_saved_file];
            OpenFile_strings = File.ReadAllLines(Properties.Settings.Default.PathSaveFolder + "\\" + listBox.SelectedItem.ToString() + ".kts");

            ShortStringFromFile_2 = OpenFile_strings[2];
            ShortStringFromFile_3 = OpenFile_strings[3];            

            String_ArrayFromFile_1 = OpenFile_strings[4];
            String_ArrayFromFile_2 = OpenFile_strings[5];
            ArrayFromFile_1 = OpenFile_strings[4].ToCharArray();
            ArrayFromFile_2 = OpenFile_strings[5].ToCharArray();
            try
            {
                MorF_X1 = OpenFile_strings[6];
                MorF_X2 = OpenFile_strings[7];
            }
            catch { }
            SetSchemeCompWin();
        }

        private void StartComparing()
        {
            if (SerialPort_class._serialPort.IsOpen)
            {
                SerialPort_class._serialPort.DiscardInBuffer();
                SerialPort_class._serialPort.DiscardOutBuffer();
                ComparingButton.Content = "Опрос";
                SerialPort_class.setOperationChoiceByte(SerialPort_class.op_Comparing);
                SerialPort_class.TransmitByte(SerialPort_class.StandartComparing);
            }
            else
            {
                Window1 ComPortOptions_window = new Window1();
                ComPortOptions_window.Owner = this;
                //ComPortOptions_window.Show();                
                ComPortOptions_window.ShowDialog();
            }
        }

        private void Comparing(string s_recive)
        {
            reciveString = s_recive;
            CanvasGrid.Children.Clear();            
            Console_comparingWindow.Text = "Результат проверки: " + listBox.SelectedItem + " -> ";
            byte first_or_second = 0;
            ClearLabeles_ComparingTableGrid();
            try
            {                
                
                recive_arr = reciveString.ToCharArray();
                if (reciveString.Equals(String_ArrayFromFile_1))
                {
                    ComparingButton.Background = Brushes.CadetBlue;
                    ComparingButton.Content = "ОК (X1 - X2)";
                    Console_comparingWindow.AppendText("  ОК (X1 - X2)");
                    first_or_second = 1;
                }
                else if (reciveString.Equals(String_ArrayFromFile_2))
                {
                    ComparingButton.Background = Brushes.CadetBlue;
                    ComparingButton.Content = "ОК (X2 - X1)";
                    Console_comparingWindow.AppendText("  ОК (X2 - X1)");
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
                                else if (recive_arr[i] == 'U') item.Background = Brushes.SlateBlue;                           
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
                            else if (recive_arr[i] == 'U') item.Background = Brushes.SlateBlue;                           
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
                        else if (recive_arr[i] == 'U') item.Background = Brushes.SlateBlue;                       
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
                        else if (recive_arr[i] == 'U') item.Background = Brushes.SlateBlue;                       
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
                    if (lable.Content.ToString() == "U")
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
                        else if (lable.Content.ToString() == "0") Connect_o_notConnect = " НЕ  присоединён ";

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
            StartComparing();
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

        
        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {

                SerialPort sp = (SerialPort)sender;
                string indata = sp.ReadLine();


                Action action = () =>
                {
                    switch (SerialPort_class.getOperationChoiceByte())

                    {
                        case SerialPort_class.op_Comparing:
                            Comparing(indata);                 // Сравнение с топологией взятой из файла
                            break;
                        case SerialPort_class.op_SetQuickComparingString:
                            SetQuickComparingString(indata);   // Запись топологии в озу
                            break;
                        case SerialPort_class.op_QuickComparingMethod:
                            QuickComparingMethod(indata);      // Сравнение с топологией хранящейся в озу
                            break;
                        case SerialPort_class.op_StringToReverse:
                            StringToReverse(indata);           // Восстановление топологии для сохранения в файл
                            break;
                        case SerialPort_class.op_StringToReverseFromEeprom:
                            StringToReverse(indata);           // Восстановление топологии для сохранения в файл из памяти микроконтроллера
                            break;
                        case SerialPort_class.op_arrayToEeprom:
                            sequenceWriteToEeprom_event(indata);           // 
                            break;
                        default:
                            break;
                    }

                };
                Dispatcher.Invoke(action);

            }
            catch { }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            CanvasGrid.Children.Clear();
            ClearLabeles_ComparingTableGrid();
            Console_comparingWindow.Text = "Результат проверки: ";
        }

        private void QuickComparingButton_Click(object sender, RoutedEventArgs e)
        {            
            if (SerialPort_class._serialPort.IsOpen)
            {               
                QuickComparingButton.Content = "Опрос";
                SerialPort_class.setOperationChoiceByte(SerialPort_class.op_QuickComparingMethod);
                SerialPort_class.TransmitByte(SerialPort_class.StandartComparing);
            }
            else
            {
                Window1 ComPortOptions_window = new Window1();
                ComPortOptions_window.Owner = this;
                //ComPortOptions_window.Show();
                ComPortOptions_window.ShowDialog();
            }
        }

        private void QuickComparing_Click(object sender, RoutedEventArgs e)
        {
            if (QuickComparing.IsChecked == false)
            {
                ClearLabeles_ComparingTableGrid();
                QuickComparing.IsChecked = true;
                listBox.IsEnabled = false;
                RefreshList.IsEnabled = false;
                ComparingButton.IsEnabled = false;
                ComparingButton.Content = "";
                QuickComparingButton.IsEnabled = true;
                SchemeCompWin.IsEnabled = false;
                SchemeCompWin.Source = null;
                QuickComparing.Header = "Выключить быструю проверку";
                SerialPort_class.setOperationChoiceByte(SerialPort_class.op_SetQuickComparingString);
                Console_comparingWindow.Text = "Результат проверки: ";
                                
                if (SerialPort_class._serialPort.IsOpen)
                {                   
                    SerialPort_class.TransmitByte(SerialPort_class.StandartComparing);
                }
                else
                {
                    Window1 ComPortOptions_window = new Window1();
                    ComPortOptions_window.Owner = this;
                    //ComPortOptions_window.Show();
                    ComPortOptions_window.ShowDialog();
                }
            }
            else if (QuickComparing.IsChecked == true)
            {
                QuickComparing.IsChecked = false;
                SchemeCompWin.IsEnabled = true;
                listBox.IsEnabled = true;
                RefreshList.IsEnabled = true;
                ComparingButton.IsEnabled = true;
                ComparingButton.Content = "Проверка";
                QuickComparingButton.IsEnabled = false;                                
                QuickComparing.Header = "Быстрая проверка";
                QuickComparingButton.Content = "Быстрая проверка";
                SetSchemeCompWin();
                SerialPort_class.setOperationChoiceByte(0);
                Console_comparingWindow.Text = "Результат проверки: ";
            }
        }

        private void SetQuickComparingString(string s)
        {
            QuickComparingString = s;
            QuickComparingArr = QuickComparingString.ToCharArray();
            SerialPort_class.setOperationChoiceByte(SerialPort_class.op_QuickComparingMethod);
        }

        private void QuickComparingMethod(string s_recive)
        {
            reciveString = s_recive;
            CanvasGrid.Children.Clear();
            Console_comparingWindow.Text = "Результат проверки:" ;            
            byte first_or_second = 0;
            ClearLabeles_ComparingTableGrid();
            try
            {

                recive_arr = reciveString.ToCharArray();
                if (reciveString.Equals(QuickComparingString))
                {
                    QuickComparingButton.Background = Brushes.CadetBlue;
                    QuickComparingButton.Content = "ОК";
                    Console_comparingWindow.AppendText("  ОК\n");
                    first_or_second = 1;
                }
                
                else
                {
                    QuickComparingButton.Background = Brushes.Red;
                    QuickComparingButton.Content = "Несоответствие!";
                }

                if (first_or_second == 1)
                {
                    int i = 0;
                    foreach (var item in GetLabeles_ComparingTableGrid())
                    {
                        if (recive_arr[i] == ' ') i++;

                        item.Content = recive_arr[i];


                        if (QuickComparingArr[i] == recive_arr[i])
                        {
                            if (recive_arr[i] == '1') item.Background = Brushes.LimeGreen;
                            else if (recive_arr[i] == 'U') item.Background = Brushes.SlateBlue;
                        }
                        i++;
                    }
                }
                

                else if (first_or_second == 0) QuickComparingResultFalse();
            }
            catch { }
        }
        private void QuickComparingResultFalse()
        {            
            int i = 0;
            var StackPanelList = new List<StackPanel>();
                                      
                i = 0;
                foreach (var item in GetLabeles_ComparingTableGrid())
                {
                    if (recive_arr[i] == ' ') i++;
                    item.Content = recive_arr[i];

                    if (QuickComparingArr[i] == recive_arr[i])
                    {
                        if (recive_arr[i] == '1') item.Background = Brushes.LimeGreen;
                        else if (recive_arr[i] == 'U') item.Background = Brushes.SlateBlue;
                    }
                    else
                    {
                        item.Background = Brushes.DarkRed;
                        var stackpanel = item.Parent as StackPanel;
                        StackPanelList.Add(stackpanel);
                    }
                    i++;                
            }
            if (StackPanelList != null)
                Quick_report_to_Console_comparingWindow(StackPanelList);
        }

        private void Quick_report_to_Console_comparingWindow(List<StackPanel> stackPanelList)
        {
            string Xn_SourcePin = "";
            byte SourcePin = 0;
            string Xn_ErrorPin = "";
            byte ErrorPin = 0;
            string Connect_o_notConnect = "";
            var ReportStringList = new List<String>();
            Console_comparingWindow.AppendText("\n");

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
                        else if (lable.Content.ToString() == "0") Connect_o_notConnect = " НЕ  присоединён ";

                        ErrorPin = counterForeach;
                        Xn_ErrorPin = " X1.";
                        if (ErrorPin > 20)
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

        private void ChoiceFolder_Click(object sender, RoutedEventArgs e)
        {
            winForms.FolderBrowserDialog FBD = new winForms.FolderBrowserDialog();
            FBD.ShowDialog();
            Properties.Settings.Default.PathSaveFolder = FBD.SelectedPath;
            Properties.Settings.Default.Save();
        }

        private void Show_Click(object sender, RoutedEventArgs e)
        {
            generateTopology_window.Show();
        }

        private void _MainWindow_Closed(object sender, EventArgs e)
        {
            generateTopology_window.Close();
            Environment.Exit(0);
            
        }

        private void Console_comparingWindow_fontsize_Click(object sender, RoutedEventArgs e)
        {
            if (Console_comparingWindow_fontsize.Content.ToString() == "+")
            {
                Console_comparingWindow.FontSize = 15;
                Console_comparingWindow_fontsize.Content = "-";
            }
            else
            {
                Console_comparingWindow.FontSize = 12;
                Console_comparingWindow_fontsize.Content = "+";
            }
        }

        private void Create_PictureScheme()
        {
            SchemeStackPanel.Visibility = Visibility.Visible;
            
            string[] leftStrings_arr = ShortStringFromFile_2.Split(' ');
            string[] writeStrings_arr = ShortStringFromFile_3.Split(' ');
            string[] Kontakt = {"Контакт 1", "Контакт 2", "Контакт 3", "Контакт 4", "Контакт 5",
            "Контакт 6","Контакт 7","Контакт 8","Контакт 9","Контакт 10","Контакт 11","Контакт 12",
            "Контакт 13","Контакт 14","Контакт 15","Контакт 16","Контакт 17","Контакт 18","Контакт 19"};

            SchemeStackPanel.Children.Clear();

            DockPanel dpan = new DockPanel();
            dpan.HorizontalAlignment = HorizontalAlignment.Center;
            Label lab = new Label();
            lab.FontWeight = FontWeights.Bold;
            lab.Content = "Разъём Х1" + "   " + MorF_X1 + "     ";
            lab.HorizontalAlignment = HorizontalAlignment.Left;
            dpan.Children.Add(lab);

            Label _lab = new Label();
            _lab.MinWidth = 150;
            _lab.FontWeight = FontWeights.Bold;
            _lab.HorizontalAlignment = HorizontalAlignment.Left;
            _lab.BorderBrush = Brushes.RoyalBlue;
            dpan.Children.Add(_lab);

            Label lab2 = new Label();
            lab2.FontWeight = FontWeights.Bold;
            lab2.Content = "     " + MorF_X2 + "   " + "Разъём Х2";
            lab2.HorizontalAlignment = HorizontalAlignment.Right;
            dpan.Children.Add(lab2);

            SchemeStackPanel.Children.Add(dpan);

            byte count = 0;
            foreach (var stroke_in_table in leftStrings_arr)
            {
                DockPanel dpan_2 = new DockPanel();
                dpan_2.HorizontalAlignment = HorizontalAlignment.Center;
                Label lab_2 = new Label();
                lab_2.MinWidth = 85;
                lab_2.FontWeight = FontWeights.Bold;
                lab_2.HorizontalAlignment = HorizontalAlignment.Left;
                lab_2.BorderBrush = Brushes.RoyalBlue;
                lab_2.Content = Kontakt[count];
                if (count == 18) lab_2.BorderThickness = new Thickness(2, 2, 2, 2);
                else lab_2.BorderThickness = new Thickness(2, 2, 2, 1);
                dpan_2.Children.Add(lab_2);

                Label lab_3 = new Label();
                lab_3.MinWidth = 113;
                lab_3.FontWeight = FontWeights.Bold;
                lab_3.HorizontalAlignment = HorizontalAlignment.Left;
                lab_3.BorderBrush = Brushes.RoyalBlue;
                lab_3.HorizontalContentAlignment = HorizontalAlignment.Center;
                lab_3.Content = "Цепь  " + leftStrings_arr[count];
                if (leftStrings_arr[count] == "0") lab_3.Content = "";
                if (count == 18) lab_3.BorderThickness = new Thickness(1, 2, 2, 2);
                else lab_3.BorderThickness = new Thickness(1, 2, 2, 1);
                dpan_2.Children.Add(lab_3);

                Label lab_4 = new Label();
                lab_4.MinWidth = 150;
                lab_4.FontWeight = FontWeights.Bold;
                lab_4.HorizontalAlignment = HorizontalAlignment.Left;
                lab_4.BorderBrush = Brushes.RoyalBlue;
                if (count == 18) lab_4.BorderThickness = new Thickness(1, 2, 2, 2);
                else lab_4.BorderThickness = new Thickness(1, 2, 2, 1);
                dpan_2.Children.Add(lab_4);

                Label lab_5 = new Label();
                lab_5.MinWidth = 113;
                lab_5.FontWeight = FontWeights.Bold;
                lab_5.HorizontalAlignment = HorizontalAlignment.Left;
                lab_5.BorderBrush = Brushes.RoyalBlue;
                lab_5.HorizontalContentAlignment = HorizontalAlignment.Center;
                lab_5.Content = "Цепь  " + writeStrings_arr[count];
                if(writeStrings_arr[count] == "0") lab_5.Content = "";
                if (count == 18) lab_5.BorderThickness = new Thickness(1, 2, 1, 2);
                else lab_5.BorderThickness = new Thickness(1, 2, 1, 1);
                dpan_2.Children.Add(lab_5);

                Label lab_6 = new Label();
                lab_6.MinWidth = 85;
                lab_6.FontWeight = FontWeights.Bold;
                lab_6.HorizontalAlignment = HorizontalAlignment.Left;
                lab_6.BorderBrush = Brushes.RoyalBlue;
                lab_6.Content = Kontakt[count];
                if (count == 18) lab_6.BorderThickness = new Thickness(2, 2, 2, 2);
                else lab_6.BorderThickness = new Thickness(2, 2, 2, 1);
                dpan_2.Children.Add(lab_6);

                SchemeStackPanel.Children.Add(dpan_2);
                count++;
            }
        }

    }
}
