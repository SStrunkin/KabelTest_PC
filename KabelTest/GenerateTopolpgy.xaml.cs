using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
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
using System.Windows.Shapes;

namespace KabelTest
{
    /// <summary>
    /// Логика взаимодействия для GenerateTopolpgy.xaml
    /// </summary>
    public partial class GenerateTopolpgy : Window
    {
        Topology_sourse_class tSc = new Topology_sourse_class();
        string Scheme_file_path;
        byte[] toSend_arr;

        public delegate void SaveAsDelegate();
        public event SaveAsDelegate SaveAs_event;


        public GenerateTopolpgy()
        {
            InitializeComponent();
            DataContext = new Create_Cabel_comboBoxes_model();
            Create_Cel_comboBox_model CCCM = new Create_Cel_comboBox_model();
            ChoiceCell.ItemsSource = CCCM.Cels;
            

            foreach (var combobox in My_GetAllCombobox())
            {
                combobox.MinWidth = 125;
                combobox.FontWeight = FontWeights.Bold;
                combobox.MouseRightButtonUp += To_setNetworcksIllumination;
                combobox.SelectionChanged += ComboBox_SelectionChanged;                          
            }

            this.SaveAs_event -= this.SaveAs;
            this.SaveAs_event += this.SaveAs;

            MorF_X1.Content = ">-";
            MorF_X2.Content = "-<";            
        }

        private void generate_kabelTopology_arr()
        {
            int index_kabelTopology_arr = 0;
            int index_X1 = 0;
            int index_X2 = 0;

            while (index_X1 < tSc.get_Sum_pins_in_X())
            {

                for (int i = 0; i < tSc.get_Sum_pins_in_X(); i++)
                {

                    if ((tSc.get_X1_Topology_sourse(index_X1) != 0) && ((tSc.get_X1_Topology_sourse(index_X1) == tSc.get_X1_Topology_sourse(i))))
                    {

                        tSc.setkabelTopology_arr_element(index_kabelTopology_arr, '1');   // Ставим 1 как наличие приходящего напряжения
                        index_kabelTopology_arr++;

                    }

                    else
                    {

                        tSc.setkabelTopology_arr_element(index_kabelTopology_arr, '0');   // Ставим 0 как отсутствие напряжения
                        index_kabelTopology_arr++;

                    }

                }

                tSc.setkabelTopology_arr_element(index_kabelTopology_arr, ' ');         // Ставим пробел для наглядности
                index_kabelTopology_arr++;

                for (int i = 0; i < tSc.get_Sum_pins_in_X(); i++)
                {

                    if ((tSc.get_X1_Topology_sourse(index_X1) != 0) && ((tSc.get_X1_Topology_sourse(index_X1) == tSc.get_X2_Topology_sourse(i))))
                    {

                        tSc.setkabelTopology_arr_element(index_kabelTopology_arr, '1');   // Ставим 1 как наличие приходящего напряжения
                        index_kabelTopology_arr++;

                    }

                    else
                    {

                        tSc.setkabelTopology_arr_element(index_kabelTopology_arr, '0');   // Ставим 0 как отсутствие напряжения
                        index_kabelTopology_arr++;

                    }

                }

                index_X1++;
            }

            while (index_X2 < tSc.get_Sum_pins_in_X())
            {

                for (int i = 0; i < tSc.get_Sum_pins_in_X(); i++)
                {

                    if ((tSc.get_X2_Topology_sourse(index_X2) != 0) && ((tSc.get_X2_Topology_sourse(index_X2) == tSc.get_X1_Topology_sourse(i))))
                    {

                        tSc.setkabelTopology_arr_element(index_kabelTopology_arr, '1');   // Ставим 1 как наличие приходящего напряжения
                        index_kabelTopology_arr++;

                    }

                    else
                    {

                        tSc.setkabelTopology_arr_element(index_kabelTopology_arr, '0');   // Ставим 0 как отсутствие напряжения
                        index_kabelTopology_arr++;

                    }

                }

                tSc.setkabelTopology_arr_element(index_kabelTopology_arr, ' ');         // Ставим пробел для наглядности
                index_kabelTopology_arr++;

                for (int i = 0; i < tSc.get_Sum_pins_in_X(); i++)
                {

                    if ((tSc.get_X2_Topology_sourse(index_X2) != 0) && ((tSc.get_X2_Topology_sourse(index_X2) == tSc.get_X2_Topology_sourse(i))))
                    {

                        tSc.setkabelTopology_arr_element(index_kabelTopology_arr, '1');   // Ставим 1 как наличие приходящего напряжения
                        index_kabelTopology_arr++;

                    }

                    else
                    {

                        tSc.setkabelTopology_arr_element(index_kabelTopology_arr, '0');   // Ставим 0 как отсутствие напряжения
                        index_kabelTopology_arr++;

                    }

                }

                index_X2++;
            }

            for (int i = 0; i < tSc.getKabel_topology_arr_lenght(); i++)
            {                                        // 1482 колличиство элементов массива

                tSc.setkabelTopology_arr_element(i, 'U');                               //Ставим звёздочку как наличие источника напряжения
                i += ((tSc.get_Sum_pins_in_X() * 2) + 1);                            // 39 + i расстояние между звёздочками в массиве 
                if (i == ((tSc.getKabel_topology_arr_lenght() / 2) + (tSc.get_Sum_pins_in_X() - 1))) i++;             // 759 мы подаём напряжение уже на второй разЪём - Х2
                                                                                                                      //System.out.println(i + " ");
            }

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            resetNetworcksIllumination();
            var combobox = sender as ComboBox;
            var dockPanel = combobox.Parent as DockPanel;
            var comboboxes = new List<ComboBox>();
            foreach (var item in dockPanel.Children)
            {
                var comboBox = item as ComboBox;
                if (comboBox == null) continue;
                comboboxes.Add(comboBox);

                if ((comboboxes.Count > 1) && (Same_second_connector.IsChecked == true))
                {
                    comboboxes[1].SelectedIndex = comboboxes[0].SelectedIndex;
                }
                setNetworcksIllumination(sender);
            }
        }

        private List<ComboBox> My_GetAllCombobox()
        {
            var comboboxes = new List<ComboBox>();
            foreach (var childDockPanel in CheckboxPanel.Children)
            {
                var dockPanel = childDockPanel as DockPanel;
                if (dockPanel == null)
                    continue;

                foreach (var childCombobox in dockPanel.Children)
                {
                    var combobox = childCombobox as ComboBox;
                    if (combobox == null)
                        continue;
                    comboboxes.Add(combobox);
                }
            }
            return comboboxes;
        }               

        private void Create_cabel_topology_Click(object sender, RoutedEventArgs e)
        {
                           
                tSc.set_X1_Topology_sourse(0, tSc.itemText_to_X1_or_X2_arrays((X1_1.Text)));
                tSc.set_X1_Topology_sourse(1, tSc.itemText_to_X1_or_X2_arrays((X1_2.Text)));
                tSc.set_X1_Topology_sourse(2, tSc.itemText_to_X1_or_X2_arrays((X1_3.Text)));
                tSc.set_X1_Topology_sourse(3, tSc.itemText_to_X1_or_X2_arrays((X1_4.Text)));
                tSc.set_X1_Topology_sourse(4, tSc.itemText_to_X1_or_X2_arrays((X1_5.Text)));
                tSc.set_X1_Topology_sourse(5, tSc.itemText_to_X1_or_X2_arrays((X1_6.Text)));
                tSc.set_X1_Topology_sourse(6, tSc.itemText_to_X1_or_X2_arrays((X1_7.Text)));
                tSc.set_X1_Topology_sourse(7, tSc.itemText_to_X1_or_X2_arrays((X1_8.Text)));
                tSc.set_X1_Topology_sourse(8, tSc.itemText_to_X1_or_X2_arrays((X1_9.Text)));
                tSc.set_X1_Topology_sourse(9, tSc.itemText_to_X1_or_X2_arrays((X1_10.Text)));
                tSc.set_X1_Topology_sourse(10, tSc.itemText_to_X1_or_X2_arrays((X1_11.Text)));
                tSc.set_X1_Topology_sourse(11, tSc.itemText_to_X1_or_X2_arrays((X1_12.Text)));
                tSc.set_X1_Topology_sourse(12, tSc.itemText_to_X1_or_X2_arrays((X1_13.Text)));
                tSc.set_X1_Topology_sourse(13, tSc.itemText_to_X1_or_X2_arrays((X1_14.Text)));
                tSc.set_X1_Topology_sourse(14, tSc.itemText_to_X1_or_X2_arrays((X1_15.Text)));
                tSc.set_X1_Topology_sourse(15, tSc.itemText_to_X1_or_X2_arrays((X1_16.Text)));
                tSc.set_X1_Topology_sourse(16, tSc.itemText_to_X1_or_X2_arrays((X1_17.Text)));
                tSc.set_X1_Topology_sourse(17, tSc.itemText_to_X1_or_X2_arrays((X1_18.Text)));
                tSc.set_X1_Topology_sourse(18, tSc.itemText_to_X1_or_X2_arrays((X1_19.Text)));

                tSc.set_X2_Topology_sourse(0, tSc.itemText_to_X1_or_X2_arrays((X2_1.Text)));
                tSc.set_X2_Topology_sourse(1, tSc.itemText_to_X1_or_X2_arrays((X2_2.Text)));
                tSc.set_X2_Topology_sourse(2, tSc.itemText_to_X1_or_X2_arrays((X2_3.Text)));
                tSc.set_X2_Topology_sourse(3, tSc.itemText_to_X1_or_X2_arrays((X2_4.Text)));
                tSc.set_X2_Topology_sourse(4, tSc.itemText_to_X1_or_X2_arrays((X2_5.Text)));
                tSc.set_X2_Topology_sourse(5, tSc.itemText_to_X1_or_X2_arrays((X2_6.Text)));
                tSc.set_X2_Topology_sourse(6, tSc.itemText_to_X1_or_X2_arrays((X2_7.Text)));
                tSc.set_X2_Topology_sourse(7, tSc.itemText_to_X1_or_X2_arrays((X2_8.Text)));
                tSc.set_X2_Topology_sourse(8, tSc.itemText_to_X1_or_X2_arrays((X2_9.Text)));
                tSc.set_X2_Topology_sourse(9, tSc.itemText_to_X1_or_X2_arrays((X2_10.Text)));
                tSc.set_X2_Topology_sourse(10, tSc.itemText_to_X1_or_X2_arrays((X2_11.Text)));
                tSc.set_X2_Topology_sourse(11, tSc.itemText_to_X1_or_X2_arrays((X2_12.Text)));
                tSc.set_X2_Topology_sourse(12, tSc.itemText_to_X1_or_X2_arrays((X2_13.Text)));
                tSc.set_X2_Topology_sourse(13, tSc.itemText_to_X1_or_X2_arrays((X2_14.Text)));
                tSc.set_X2_Topology_sourse(14, tSc.itemText_to_X1_or_X2_arrays((X2_15.Text)));
                tSc.set_X2_Topology_sourse(15, tSc.itemText_to_X1_or_X2_arrays((X2_16.Text)));
                tSc.set_X2_Topology_sourse(16, tSc.itemText_to_X1_or_X2_arrays((X2_17.Text)));
                tSc.set_X2_Topology_sourse(17, tSc.itemText_to_X1_or_X2_arrays((X2_18.Text)));
                tSc.set_X2_Topology_sourse(18, tSc.itemText_to_X1_or_X2_arrays((X2_19.Text)));

                generate_kabelTopology_arr();
                tSc.Copy_Kabeltopology_arr_to_Kabeltopology_arr1();


                tSc.swap_Topology_source_arrays();
                generate_kabelTopology_arr();
                tSc.Copy_Kabeltopology_arr_to_Kabeltopology_arr2();
                tSc.swap_Topology_source_arrays();
                
                MessageBox.Show("Создание топологии завершено.");
            
          
        }        

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            SchemeStackPanel.Visibility = Visibility.Hidden;
            image_Scheme.Visibility = Visibility.Visible;
            openTopology();
        }

        private void openTopology()
        {
            tSc.ClearArrays();
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Title = "Открыть топологию кабеля.";
            // Set filter for file extension 
            //dlg.Filter = "Text files(*.txt)|*.txt";
            dlg.Filter = "Topology source files(*.kts)|*.kts";
            dlg.InitialDirectory = Properties.Settings.Default.PathSaveFolder;
            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = Scheme_file_path = dlg.FileName;

                string[] pathArr = filename.Split('\\');
                string[] fileArr = pathArr.Last().Split('\\');
                string filename_only = fileArr.Last().ToString();

                try
                {
                    // Open the file to read from.
                    string s;
                    string[] s_arr;
                    string[] OpenFile_strings = new string[Properties.Settings.Default.Stings_in_saved_file];
                    OpenFile_strings = File.ReadAllLines(dlg.FileName);
                    Name_or_NKRM_t.Text = OpenFile_strings[0];

                    if (OpenFile_strings[1].Equals("Same_second_connector_Set"))
                    {
                        Same_second_connector.IsChecked = true;
                    }

                    else if (OpenFile_strings[1].Equals("Same_second_connector_Reset"))
                    {
                        Same_second_connector.IsChecked = false;
                    }

                    s = OpenFile_strings[2];
                    s_arr = s.Split(' ');

                    for (int i = 0; i < tSc.get_Sum_pins_in_X(); i++)
                    {
                        tSc.set_X1_Topology_sourse(i, Int32.Parse(s_arr[i]));
                    }

                    X1_1.SelectedIndex = Int32.Parse(s_arr[0]);
                    X1_2.SelectedIndex = Int32.Parse(s_arr[1]);
                    X1_3.SelectedIndex = Int32.Parse(s_arr[2]);
                    X1_4.SelectedIndex = Int32.Parse(s_arr[3]);
                    X1_5.SelectedIndex = Int32.Parse(s_arr[4]);
                    X1_6.SelectedIndex = Int32.Parse(s_arr[5]);
                    X1_7.SelectedIndex = Int32.Parse(s_arr[6]);
                    X1_8.SelectedIndex = Int32.Parse(s_arr[7]);
                    X1_9.SelectedIndex = Int32.Parse(s_arr[8]);
                    X1_10.SelectedIndex = Int32.Parse(s_arr[9]);
                    X1_11.SelectedIndex = Int32.Parse(s_arr[10]);
                    X1_12.SelectedIndex = Int32.Parse(s_arr[11]);
                    X1_13.SelectedIndex = Int32.Parse(s_arr[12]);
                    X1_14.SelectedIndex = Int32.Parse(s_arr[13]);
                    X1_15.SelectedIndex = Int32.Parse(s_arr[14]);
                    X1_16.SelectedIndex = Int32.Parse(s_arr[15]);
                    X1_17.SelectedIndex = Int32.Parse(s_arr[16]);
                    X1_18.SelectedIndex = Int32.Parse(s_arr[17]);
                    X1_19.SelectedIndex = Int32.Parse(s_arr[18]);

                    s = OpenFile_strings[3];
                    s_arr = s.Split(' ');

                    for (int i = 0; i < tSc.get_Sum_pins_in_X(); i++)
                    {
                        tSc.set_X2_Topology_sourse(i, Int32.Parse(s_arr[i]));
                    }

                    X2_1.SelectedIndex = Int32.Parse(s_arr[0]);
                    X2_2.SelectedIndex = Int32.Parse(s_arr[1]);
                    X2_3.SelectedIndex = Int32.Parse(s_arr[2]);
                    X2_4.SelectedIndex = Int32.Parse(s_arr[3]);
                    X2_5.SelectedIndex = Int32.Parse(s_arr[4]);
                    X2_6.SelectedIndex = Int32.Parse(s_arr[5]);
                    X2_7.SelectedIndex = Int32.Parse(s_arr[6]);
                    X2_8.SelectedIndex = Int32.Parse(s_arr[7]);
                    X2_9.SelectedIndex = Int32.Parse(s_arr[8]);
                    X2_10.SelectedIndex = Int32.Parse(s_arr[9]);
                    X2_11.SelectedIndex = Int32.Parse(s_arr[10]);
                    X2_12.SelectedIndex = Int32.Parse(s_arr[11]);
                    X2_13.SelectedIndex = Int32.Parse(s_arr[12]);
                    X2_14.SelectedIndex = Int32.Parse(s_arr[13]);
                    X2_15.SelectedIndex = Int32.Parse(s_arr[14]);
                    X2_16.SelectedIndex = Int32.Parse(s_arr[15]);
                    X2_17.SelectedIndex = Int32.Parse(s_arr[16]);
                    X2_18.SelectedIndex = Int32.Parse(s_arr[17]);
                    X2_19.SelectedIndex = Int32.Parse(s_arr[18]);

                    tSc.Set_Kabel_topology_arr_1_WhenOpen(OpenFile_strings[4]);
                    tSc.Set_Kabel_topology_arr_2_WhenOpen(OpenFile_strings[5]);

                    MorF_X1.Content = OpenFile_strings[6];
                    MorF_X2.Content = OpenFile_strings[7];

                }
                catch
                {

                }

                try
                {
                    byte picture_absence_counter = 0;
                    string cheme_path = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(dlg.FileName), System.IO.Path.GetFileNameWithoutExtension(dlg.FileName));
                    try
                    {
                        var bmi = new BitmapImage();
                        bmi.BeginInit();
                        bmi.CacheOption = BitmapCacheOption.OnLoad;
                        bmi.UriSource = new Uri(cheme_path + ".jpg", UriKind.Absolute);
                        bmi.EndInit();

                        image_Scheme.Source = bmi;
                        Scheme_file_path = cheme_path + ".jpg";
                    }
                    catch
                    { picture_absence_counter++; }
                    try
                    {
                        var bmi = new BitmapImage();
                        bmi.BeginInit();
                        bmi.CacheOption = BitmapCacheOption.OnLoad;
                        bmi.UriSource = new Uri(cheme_path + ".png", UriKind.Absolute);
                        bmi.EndInit();

                        image_Scheme.Source = bmi;
                        Scheme_file_path = cheme_path + ".png";
                    }
                    catch
                    { picture_absence_counter++; }
                    try
                    {
                        var bmi = new BitmapImage();
                        bmi.BeginInit();
                        bmi.CacheOption = BitmapCacheOption.OnLoad;
                        bmi.UriSource = new Uri(cheme_path + ".jpeg", UriKind.Absolute);
                        bmi.EndInit();

                        image_Scheme.Source = bmi;
                        Scheme_file_path = cheme_path + ".jpeg";
                    }
                    catch
                    { picture_absence_counter++; }
                    try
                    {
                        var bmi = new BitmapImage();
                        bmi.BeginInit();
                        bmi.CacheOption = BitmapCacheOption.OnLoad;
                        bmi.UriSource = new Uri(cheme_path + ".gif", UriKind.Absolute);
                        bmi.EndInit();

                        image_Scheme.Source = bmi;
                        Scheme_file_path = cheme_path + ".gif";
                    }
                    catch
                    { picture_absence_counter++; }
                    try
                    {
                        var bmi = new BitmapImage();
                        bmi.BeginInit();
                        bmi.CacheOption = BitmapCacheOption.OnLoad;
                        bmi.UriSource = new Uri(cheme_path + ".bmp", UriKind.Absolute);
                        bmi.EndInit();

                        image_Scheme.Source = bmi;
                        Scheme_file_path = cheme_path + ".bmp";
                    }
                    catch
                    { picture_absence_counter++; }
                    try
                    {
                        var bmi = new BitmapImage();
                        bmi.BeginInit();
                        bmi.CacheOption = BitmapCacheOption.OnLoad;
                        bmi.UriSource = new Uri(cheme_path + ".tif", UriKind.Absolute);
                        bmi.EndInit();

                        image_Scheme.Source = bmi;
                        Scheme_file_path = cheme_path + ".tif";
                    }
                    catch
                    { picture_absence_counter++; }

                    if (picture_absence_counter >= 6) _CreateScreenScheme();
                }
                catch
                { }                                
            }
        }

        private void Clear_all_Comboboxes_Click(object sender, RoutedEventArgs e)
        {
            var comboboxes_list = My_GetAllCombobox();
            foreach (var combobox in comboboxes_list)
            {
                combobox.SelectedIndex = 0;
            }
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            SaveAs();            
        }

        private void SaveAs()
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();

            dlg.Title = "Сохранить топологию кабеля.";
            //отображать ли предупреждение, если пользователь указывает имя уже существующего файла
            dlg.OverwritePrompt = true;
            //отображать ли предупреждение, если пользователь указывает несуществующий путь
            dlg.CheckPathExists = true;

            dlg.FileName = Name_or_NKRM_t.Text;

            dlg.Filter = "Topology source files(*.kts)|*.kts|All files(*.*)|*.*";
            dlg.InitialDirectory = Properties.Settings.Default.PathSaveFolder;
            dlg.DefaultExt = "kts";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            if (result == true)
            {
                string[] Save_Array = new string[Properties.Settings.Default.Stings_in_saved_file];

                Save_Array[0] = System.IO.Path.GetFileNameWithoutExtension(dlg.FileName);
                Name_or_NKRM_t.Text = System.IO.Path.GetFileNameWithoutExtension(dlg.FileName);
                if (Same_second_connector.IsChecked == true)
                {
                    Save_Array[1] = "Same_second_connector_Set";
                }
                else Save_Array[1] = "Same_second_connector_Reset";

                Save_Array[2] = tSc.get_X1_Topology_sourse_string();
                Save_Array[3] = tSc.get_X2_Topology_sourse_string();
                Save_Array[4] = tSc.getkabelTopology_arr1_string();
                Save_Array[5] = tSc.getkabelTopology_arr2_string();

                Save_Array[6] = MorF_X1.Content.ToString();
                Save_Array[7] = MorF_X2.Content.ToString();

                Properties.Settings.Default.PathSaveFolder = System.IO.Path.GetDirectoryName(dlg.FileName);
                Properties.Settings.Default.Save();
                //MessageBox.Show(Properties.Settings.Default.PathSaveFolder);

                System.IO.File.WriteAllLines(dlg.FileName, Save_Array);
                if ((Scheme_file_path != null) && (Scheme_file_path != System.IO.Path.Combine(System.IO.Path.GetDirectoryName(dlg.FileName), (System.IO.Path.GetFileNameWithoutExtension(dlg.FileName) + System.IO.Path.GetExtension(Scheme_file_path)))))
                {
                    try
                    {                       
                        File.Delete(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(dlg.FileName), (System.IO.Path.GetFileNameWithoutExtension(dlg.FileName) + ".jpg")));
                    }
                    catch { }
                    try
                    {
                        File.Delete(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(dlg.FileName), (System.IO.Path.GetFileNameWithoutExtension(dlg.FileName) + ".png")));
                    }
                    catch { }
                    try
                    {
                        File.Delete(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(dlg.FileName), (System.IO.Path.GetFileNameWithoutExtension(dlg.FileName) + ".jpeg")));
                    }
                    catch { }
                    try
                    {
                        File.Delete(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(dlg.FileName), (System.IO.Path.GetFileNameWithoutExtension(dlg.FileName) + ".gif")));
                    }
                    catch { }
                    try
                    {
                        File.Delete(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(dlg.FileName), (System.IO.Path.GetFileNameWithoutExtension(dlg.FileName) + ".bmp")));
                    }
                    catch { }
                    try
                    {
                        File.Delete(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(dlg.FileName), (System.IO.Path.GetFileNameWithoutExtension(dlg.FileName) + ".tif")));
                    }
                    catch { }

                    try
                    {
                        File.Copy(Scheme_file_path, System.IO.Path.Combine(System.IO.Path.GetDirectoryName(dlg.FileName), (System.IO.Path.GetFileNameWithoutExtension(dlg.FileName) + System.IO.Path.GetExtension(Scheme_file_path))), true);
                    }
                    catch { }                                     
                }
            }
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            Window1 ComPortOptions_window = new Window1();
            ComPortOptions_window.Owner = this;
            //ComPortOptions_window.Show();
            ComPortOptions_window.ShowDialog();
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            createNewTopology();
        }

        private void createNewTopology()
        {
            Name_or_NKRM_t.Text = "";
            foreach (var combobox in My_GetAllCombobox())
            {
                combobox.SelectedIndex = 0;
            }
            Scheme_file_path = null;
            image_Scheme.Source = null;
            tSc.ClearArrays();
        }

        private void To_setNetworcksIllumination(object sender, MouseButtonEventArgs e)
        {
            setNetworcksIllumination(sender);
        }

        private void setNetworcksIllumination(object sender)
        {
            if (CheckMode.IsChecked == true)
            {
                ComboBox com_box = sender as ComboBox;
                resetNetworcksIllumination();
                foreach (var combobox in My_GetAllCombobox())
                {
                    if ((combobox.SelectedIndex.ToString().Equals(com_box.SelectedIndex.ToString())))
                    {
                        combobox.Foreground = Brushes.Red;
                    }
                    else combobox.Foreground = Brushes.Gray;
                }
            }
        }

        private void resetNetworcksIllumination()
        {
            foreach (var combobox in My_GetAllCombobox())
            {
                combobox.Foreground = Brushes.Black;
            }
        }

        private void CheckMode_Click(object sender, RoutedEventArgs e)
        {
            if (CheckMode.IsChecked == true)
            {
            }
            else if (CheckMode.IsChecked == false)
            {
                resetNetworcksIllumination();
            }
        }

        private void image_Scheme_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (image_Scheme.Stretch == Stretch.Uniform)
            {
                image_Scheme.Stretch = Stretch.None;
            }
            else if (image_Scheme.Stretch == Stretch.None)
            {
                image_Scheme.Stretch = Stretch.Fill;
            }
            else if (image_Scheme.Stretch == Stretch.Fill)
            {
                image_Scheme.Stretch = Stretch.Uniform;
            }
        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            saveTopology();
        }

        private void saveTopology()
        {
            if (Name_or_NKRM_t.Text != "")
            {

                string[] Save_Array = new string[Properties.Settings.Default.Stings_in_saved_file];

                Save_Array[0] = Name_or_NKRM_t.Text;
                if (Same_second_connector.IsChecked == true)
                {
                    Save_Array[1] = "Same_second_connector_Set";
                }
                else Save_Array[1] = "Same_second_connector_Reset";

                Save_Array[2] = tSc.get_X1_Topology_sourse_string();
                Save_Array[3] = tSc.get_X2_Topology_sourse_string();
                Save_Array[4] = tSc.getkabelTopology_arr1_string();
                Save_Array[5] = tSc.getkabelTopology_arr2_string();

                Save_Array[6] = MorF_X1.Content.ToString();
                Save_Array[7] = MorF_X2.Content.ToString();


                System.IO.File.WriteAllLines(Properties.Settings.Default.PathSaveFolder + "\\" + Name_or_NKRM_t.Text + ".kts", Save_Array);
                if (Scheme_file_path != null)
                {
                    try
                    {
                        File.Delete(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Properties.Settings.Default.PathSaveFolder + "\\"), (System.IO.Path.GetFileNameWithoutExtension(Name_or_NKRM_t.Text) + ".jpg")));
                    }
                    catch { }
                    try
                    {
                        File.Delete(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Properties.Settings.Default.PathSaveFolder + "\\"), (System.IO.Path.GetFileNameWithoutExtension(Name_or_NKRM_t.Text) + ".png")));
                    }
                    catch { }
                    try
                    {
                        File.Delete(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Properties.Settings.Default.PathSaveFolder + "\\"), (System.IO.Path.GetFileNameWithoutExtension(Name_or_NKRM_t.Text) + ".jpeg")));
                    }
                    catch { }
                    try
                    {
                        File.Delete(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Properties.Settings.Default.PathSaveFolder + "\\"), (System.IO.Path.GetFileNameWithoutExtension(Name_or_NKRM_t.Text) + ".gif")));
                    }
                    catch { }
                    try
                    {
                        File.Delete(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Properties.Settings.Default.PathSaveFolder + "\\"), (System.IO.Path.GetFileNameWithoutExtension(Name_or_NKRM_t.Text) + ".bmp")));
                    }
                    catch { }
                    try
                    {
                        File.Delete(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Properties.Settings.Default.PathSaveFolder + "\\"), (System.IO.Path.GetFileNameWithoutExtension(Name_or_NKRM_t.Text) + ".tif")));
                    }
                    catch { }


                    try
                    {
                        File.Copy(Scheme_file_path, System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Properties.Settings.Default.PathSaveFolder + "\\"), (System.IO.Path.GetFileNameWithoutExtension(Name_or_NKRM_t.Text) + System.IO.Path.GetExtension(Scheme_file_path))), true);
                    }
                    catch
                    {
                       
                    }
                }
            }
            else
            {
                SaveAs_event();
            }
        }

        private void CheckMode_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Reverse_Button_Click(object sender, RoutedEventArgs e)
        {
            Same_second_connector.IsChecked = false;
            disableButtons();
            Name_or_NKRM_t.Clear();
            image_Scheme.Source = null;
            if (SerialPort_class._serialPort.IsOpen)
            {
                SerialPort_class._serialPort.DiscardInBuffer();
                SerialPort_class._serialPort.DiscardOutBuffer();
                Same_second_connector.IsChecked = false;
                SerialPort_class.TransmitByte(SerialPort_class.StandartComparing);
                SerialPort_class.setOperationChoiceByte(3);
            }
            else
            {
                Window1 ComPortOptions_window = new Window1();
                ComPortOptions_window.Owner = this;
                //ComPortOptions_window.Show();
                ComPortOptions_window.ShowDialog();
            }
        }

        public void Reverse(string s)
        {
            if (ChoiceCell.IsEnabled == true)
            {
                enableButtons();
            }           
            Reverse_Button.IsEnabled = true;

            try
            {

                if (SerialPort_class.getOperationChoiceByte() == SerialPort_class.op_StringToReverseFromEeprom)
                {
                    char[] temp_arr = new char[tSc.getKabel_topology_arr_lenght()];
                    temp_arr = s.ToCharArray(0, tSc.getKabel_topology_arr_lenght());
                   
                    Console.WriteLine();

                    for (int i = 0; i < tSc.getKabel_topology_arr_lenght(); i++)
                    {
                        temp_arr[i] = 'U';                                                                         //Ставим звёздочку как наличие источника напряжения
                        i += ((tSc.get_Sum_pins_in_X() * 2) + 1);                                                  // 39 + i расстояние между звёздочками в массиве 
                        if (i == ((tSc.getKabel_topology_arr_lenght() / 2) + (tSc.get_Sum_pins_in_X() - 1))) i++;             // 759 мы подаём напряжение уже на второй разЪём - Х2
                                                                                                                              //System.out.println(i + " ");
                    }                  

                    s = new string(temp_arr);
                }

                string[] substrings = new String[38];
                int begin = 0;

                for (byte i = 0; i < 38; i++)
                {
                    substrings[i] = s.Substring(begin, 39);
                    begin += 39;
                }

                int numberNet = 0;
                int pin = 0;
                bool is_one = false;
                int[] x1 = new int[19];
                int[] x2 = new int[19];
                foreach (var item in substrings)
                {
                    char[] Char_arr = item.ToCharArray();
                    pin = 0;
                    is_one = false;
                    foreach (var oneChar in Char_arr)
                    {
                        if (oneChar == '1')
                        {
                            is_one = true;
                        }
                        else if (oneChar == 'U')
                        {
                            numberNet = pin + 1;
                            if (numberNet > 19)
                            {
                                numberNet -= 20;
                            }

                            foreach (var x1arrMember in x1)
                            {
                                if (x1arrMember == numberNet)
                                {
                                    numberNet++;
                                }
                            }
                            foreach (var x2arrMember in x2)
                            {
                                if (x2arrMember == numberNet)
                                {
                                    numberNet++;
                                }
                            }

                        }
                        pin++;
                    }
                    if (is_one == false) continue;

                    pin = 0;
                    foreach (var oneChar in Char_arr)
                    {
                        if ((oneChar == 'U') || (oneChar == '1'))
                        {
                            if (pin <= 19)
                            {
                                if (x1[pin] == 0) x1[pin] = numberNet;
                            }
                            else if (pin > 19)
                            {
                                if (x2[pin - 20] == 0) x2[pin - 20] = numberNet;
                            }
                        }
                        pin++;
                    }

                }

                X1_1.SelectedIndex = x1[0];
                X1_2.SelectedIndex = x1[1];
                X1_3.SelectedIndex = x1[2];
                X1_4.SelectedIndex = x1[3];
                X1_5.SelectedIndex = x1[4];
                X1_6.SelectedIndex = x1[5];
                X1_7.SelectedIndex = x1[6];
                X1_8.SelectedIndex = x1[7];
                X1_9.SelectedIndex = x1[8];
                X1_10.SelectedIndex = x1[9];
                X1_11.SelectedIndex = x1[10];
                X1_12.SelectedIndex = x1[11];
                X1_13.SelectedIndex = x1[12];
                X1_14.SelectedIndex = x1[13];
                X1_15.SelectedIndex = x1[14];
                X1_16.SelectedIndex = x1[15];
                X1_17.SelectedIndex = x1[16];
                X1_18.SelectedIndex = x1[17];
                X1_19.SelectedIndex = x1[18];

                X2_1.SelectedIndex = x2[0];
                X2_2.SelectedIndex = x2[1];
                X2_3.SelectedIndex = x2[2];
                X2_4.SelectedIndex = x2[3];
                X2_5.SelectedIndex = x2[4];
                X2_6.SelectedIndex = x2[5];
                X2_7.SelectedIndex = x2[6];
                X2_8.SelectedIndex = x2[7];
                X2_9.SelectedIndex = x2[8];
                X2_10.SelectedIndex = x2[9];
                X2_11.SelectedIndex = x2[10];
                X2_12.SelectedIndex = x2[11];
                X2_13.SelectedIndex = x2[12];
                X2_14.SelectedIndex = x2[13];
                X2_15.SelectedIndex = x2[14];
                X2_16.SelectedIndex = x2[15];
                X2_17.SelectedIndex = x2[16];
                X2_18.SelectedIndex = x2[17];
                X2_19.SelectedIndex = x2[18];
            }
            catch { }       
        }

        private void disableButtons()
        {
            ToCell.IsEnabled = false;
            Reverse_Button.IsEnabled = false;            
            FromCell.IsEnabled = false;
        }

        private void enableButtons()
        {
            ToCell.IsEnabled = true;
            Reverse_Button.IsEnabled = true;            
            FromCell.IsEnabled = true;
        }

        private void FromCell_Click(object sender, RoutedEventArgs e)
        {
            Same_second_connector.IsChecked = false;
            disableButtons();
            string cell = ChoiceCell.Text;
            byte number_cell = 0;
            int send = 0;

            switch (cell)
            {
                case "Ячейка 1":
                    number_cell = SerialPort_class.Cell_1;
                    break;
                case "Ячейка 2":
                    number_cell = SerialPort_class.Cell_2;
                    break;
                case "Ячейка 3":
                    number_cell = SerialPort_class.Cell_3;
                    break;
                case "Ячейка 4":
                    number_cell = SerialPort_class.Cell_4;
                    break;
                case "Ячейка 5":
                    number_cell = SerialPort_class.Cell_5;
                    break;
                case "Ячейка 6":
                    number_cell = SerialPort_class.Cell_6;
                    break;
                case "Ячейка 7":
                    number_cell = SerialPort_class.Cell_7;
                    break;
                case "Ячейка 8":
                    number_cell = SerialPort_class.Cell_8;
                    break;
            }
            send = number_cell << 4;
            send += SerialPort_class.ReadingFromEeprom;
            byte byte_send = (byte)send;

            if (SerialPort_class._serialPort.IsOpen)
            {                
                SerialPort_class.setOperationChoiceByte(SerialPort_class.op_StringToReverseFromEeprom);
                SerialPort_class.TransmitByte(byte_send);
            }
            else
            {
                Window1 ComPortOptions_window = new Window1();
                ComPortOptions_window.Owner = this;
                //ComPortOptions_window.Show();
                ComPortOptions_window.ShowDialog();
            }
        }

        private void Mode_Click(object sender, RoutedEventArgs e)
        {
            if (Mode.IsChecked == false)
            {
                Mode.IsChecked = true;
                FromCell.IsEnabled = true;
                ToCell.IsEnabled = true;
                ChoiceCell.IsEnabled = true;
                Reverse_Button.IsEnabled = true;
            }
            else
            {
                Mode.IsChecked = false;
                FromCell.IsEnabled = false;
                ToCell.IsEnabled = false;
                ChoiceCell.IsEnabled = false;

                if (SerialPort_class._serialPort.IsOpen)
                {
                    SerialPort_class.setOperationChoiceByte(SerialPort_class.op_StringToReverse);
                    SerialPort_class.TransmitByte(SerialPort_class.StandBy);
                }
                else
                {
                    Window1 ComPortOptions_window = new Window1();
                    ComPortOptions_window.Owner = this;
                    //ComPortOptions_window.Show();
                    ComPortOptions_window.ShowDialog();
                }
            }
        }

        private void ToCell_Click(object sender, RoutedEventArgs e)
        {
            Same_second_connector.IsChecked = false;
            disableButtons();
            Same_second_connector.IsChecked = false;
            string cell = ChoiceCell.Text;
            byte number_cell = 0;
            int send = 0;

            switch (cell)
            {
                case "Ячейка 1":
                    number_cell = SerialPort_class.Cell_1;
                    break;
                case "Ячейка 2":
                    number_cell = SerialPort_class.Cell_2;
                    break;
                case "Ячейка 3":
                    number_cell = SerialPort_class.Cell_3;
                    break;
                case "Ячейка 4":
                    number_cell = SerialPort_class.Cell_4;
                    break;
                case "Ячейка 5":
                    number_cell = SerialPort_class.Cell_5;
                    break;
                case "Ячейка 6":
                    number_cell = SerialPort_class.Cell_6;
                    break;
                case "Ячейка 7":
                    number_cell = SerialPort_class.Cell_7;
                    break;
                case "Ячейка 8":
                    number_cell = SerialPort_class.Cell_8;
                    break;
            }
            send = number_cell << 4;
            send += SerialPort_class.WriteToEeprom_from_PC;
            byte byte_send = (byte)send;

            if (SerialPort_class._serialPort.IsOpen)
            {               
                SerialPort_class.setOperationChoiceByte(SerialPort_class.op_arrayToEeprom);
                SerialPort_class.TransmitByte(byte_send);               
            }
            else
            {
                Window1 ComPortOptions_window = new Window1();
                ComPortOptions_window.Owner = this;
                //ComPortOptions_window.Show();
                ComPortOptions_window.ShowDialog();
            }
        }

        int __countBytesToEeprom = 0;
        public void sequenceWriteToEeprom(string s)
        {
            try
            {
                const string redy_recive = "rr";
                const string redy_next = "rn";

                if (s == redy_recive)
                {
                    toSend_arr = Generate_arrayTo_Eeprom();
                    __countBytesToEeprom = 0;
                    SerialPort_class.TransmitByte(toSend_arr[__countBytesToEeprom]);
                    __countBytesToEeprom++;
                }

                else if (s == redy_next)
                {
                    if (__countBytesToEeprom < tSc.getToEeprom_arrayLength())
                    {
                        SerialPort_class.TransmitByte(toSend_arr[__countBytesToEeprom]);
                        __countBytesToEeprom++;

                        if (__countBytesToEeprom == tSc.getToEeprom_arrayLength())
                        {
                            enableButtons();                           
                        }
                    }
                    else
                    {
                        SerialPort_class.setOperationChoiceByte(SerialPort_class.op_Comparing);                        
                    }
                }
            }
            catch { SerialPort_class.setOperationChoiceByte(SerialPort_class.op_Comparing); }                                                                    
         }

       

        private byte[] Generate_arrayTo_Eeprom()
        {
            char[] Source_arr = new char[tSc.getKabel_topology_arr_lenght()];
            byte[] toEeprom_arr = new byte[tSc.getToEeprom_arrayLength()];
            int counter = 0;
            Source_arr = tSc.getKabel_topology_arr_1();

            byte workByte = 0;

           for(int i = 0; counter < tSc.getToEeprom_arrayLength();)
            {
                //************************************************************************* 0b11110000       PORTF
                workByte = 0;
                workByte |= 0xF0;
                if (Source_arr[i] == ' ') i++;
                if ((Source_arr[i] == 'U') || (Source_arr[i] == '1'))                
                    workByte |= (1 << 3);
                i++;
                if (Source_arr[i] == ' ') i++;
                if ((Source_arr[i] == 'U') || (Source_arr[i] == '1'))
                    workByte |= (1 << 2);
                i++;
                if (Source_arr[i] == ' ') i++;
                if ((Source_arr[i] == 'U') || (Source_arr[i] == '1'))
                    workByte |= (1 << 1);
                i++;
                if (Source_arr[i] == ' ') i++;
                if ((Source_arr[i] == 'U') || (Source_arr[i] == '1'))
                    workByte |= (1 << 0);
                i++;
                toEeprom_arr[counter] = workByte;
                counter++;
                //************************************************************************* 0b00000011       PORTE
                workByte = 0;
                workByte |= 0x03;
                if (Source_arr[i] == ' ') i++;
                if ((Source_arr[i] == 'U') || (Source_arr[i] == '1'))
                    workByte |= (1 << 2);
                i++;
                if (Source_arr[i] == ' ') i++;
                if ((Source_arr[i] == 'U') || (Source_arr[i] == '1'))
                    workByte |= (1 << 3);
                i++;
                if (Source_arr[i] == ' ') i++;
                if ((Source_arr[i] == 'U') || (Source_arr[i] == '1'))
                    workByte |= (1 << 4);
                i++;
                if (Source_arr[i] == ' ') i++;
                if ((Source_arr[i] == 'U') || (Source_arr[i] == '1'))
                    workByte |= (1 << 5);
                i++;
                if (Source_arr[i] == ' ') i++;
                if ((Source_arr[i] == 'U') || (Source_arr[i] == '1'))
                    workByte |= (1 << 6);
                i++;
                if (Source_arr[i] == ' ') i++;
                if ((Source_arr[i] == 'U') || (Source_arr[i] == '1'))
                    workByte |= (1 << 7);
                i++;
                toEeprom_arr[counter] = workByte;
                counter++;
                //*************************************************************************                 PORTB
                workByte = 0;
                if (Source_arr[i] == ' ') i++;
                if ((Source_arr[i] == 'U') || (Source_arr[i] == '1'))
                    workByte |= (1 << 0);
                i++;
                if (Source_arr[i] == ' ') i++;
                if ((Source_arr[i] == 'U') || (Source_arr[i] == '1'))
                    workByte |= (1 << 1);
                i++;
                if (Source_arr[i] == ' ') i++;
                if ((Source_arr[i] == 'U') || (Source_arr[i] == '1'))
                    workByte |= (1 << 2);
                i++;
                if (Source_arr[i] == ' ') i++;
                if ((Source_arr[i] == 'U') || (Source_arr[i] == '1'))
                    workByte |= (1 << 3);
                i++;
                if (Source_arr[i] == ' ') i++;
                if ((Source_arr[i] == 'U') || (Source_arr[i] == '1'))
                    workByte |= (1 << 4);
                i++;
                if (Source_arr[i] == ' ') i++;
                if ((Source_arr[i] == 'U') || (Source_arr[i] == '1'))
                    workByte |= (1 << 5);
                i++;
                if (Source_arr[i] == ' ') i++;
                if ((Source_arr[i] == 'U') || (Source_arr[i] == '1'))
                    workByte |= (1 << 6);
                i++;
                if (Source_arr[i] == ' ') i++;
                if ((Source_arr[i] == 'U') || (Source_arr[i] == '1'))
                    workByte |= (1 << 7);
                i++;
                toEeprom_arr[counter] = workByte;
                counter++;
                //************************************************************************* 0b00011110    PORTD
                workByte = 0;
                workByte |= 0x1E;
                if (Source_arr[i] == ' ') i++;
                if ((Source_arr[i] == 'U') || (Source_arr[i] == '1'))
                    workByte |= (1 << 0);
                i++;
                if (Source_arr[i] == ' ') i++;
                if ((Source_arr[i] == 'U') || (Source_arr[i] == '1'))
                    workByte |= (1 << 5);
                i++;
                if (Source_arr[i] == ' ') i++;
                if ((Source_arr[i] == 'U') || (Source_arr[i] == '1'))
                    workByte |= (1 << 6);
                i++;
                if (Source_arr[i] == ' ') i++;
                if ((Source_arr[i] == 'U') || (Source_arr[i] == '1'))
                    workByte |= (1 << 7);
                i++;
                toEeprom_arr[counter] = workByte;
                counter++;
                //*************************************************************************                 PORTC
                workByte = 0;
                if (Source_arr[i] == ' ') i++;
                if ((Source_arr[i] == 'U') || (Source_arr[i] == '1'))
                    workByte |= (1 << 0);
                i++;
                if (Source_arr[i] == ' ') i++;
                if ((Source_arr[i] == 'U') || (Source_arr[i] == '1'))
                    workByte |= (1 << 1);
                i++;
                if (Source_arr[i] == ' ') i++;
                if ((Source_arr[i] == 'U') || (Source_arr[i] == '1'))
                    workByte |= (1 << 2);
                i++;
                if (Source_arr[i] == ' ') i++;
                if ((Source_arr[i] == 'U') || (Source_arr[i] == '1'))
                    workByte |= (1 << 3);
                i++;
                if (Source_arr[i] == ' ') i++;
                if ((Source_arr[i] == 'U') || (Source_arr[i] == '1'))
                    workByte |= (1 << 4);
                i++;
                if (Source_arr[i] == ' ') i++;
                if ((Source_arr[i] == 'U') || (Source_arr[i] == '1'))
                    workByte |= (1 << 5);
                i++;
                if (Source_arr[i] == ' ') i++;
                if ((Source_arr[i] == 'U') || (Source_arr[i] == '1'))
                    workByte |= (1 << 6);
                i++;
                if (Source_arr[i] == ' ') i++;
                if ((Source_arr[i] == 'U') || (Source_arr[i] == '1'))
                    workByte |= (1 << 7);
                i++;
                toEeprom_arr[counter] = workByte;
                counter++;
                //*************************************************************************                 PORTA
                workByte = 0;
                if (Source_arr[i] == ' ') i++;
                if ((Source_arr[i] == 'U') || (Source_arr[i] == '1'))
                    workByte |= (1 << 7);
                i++;
                if (Source_arr[i] == ' ') i++;
                if ((Source_arr[i] == 'U') || (Source_arr[i] == '1'))
                    workByte |= (1 << 6);
                i++;
                if (Source_arr[i] == ' ') i++;
                if ((Source_arr[i] == 'U') || (Source_arr[i] == '1'))
                    workByte |= (1 << 5);
                i++;
                if (Source_arr[i] == ' ') i++;
                if ((Source_arr[i] == 'U') || (Source_arr[i] == '1'))
                    workByte |= (1 << 4);
                i++;
                if (Source_arr[i] == ' ') i++;
                if ((Source_arr[i] == 'U') || (Source_arr[i] == '1'))
                    workByte |= (1 << 3);
                i++;
                if (Source_arr[i] == ' ') i++;
                if ((Source_arr[i] == 'U') || (Source_arr[i] == '1'))
                    workByte |= (1 << 2);
                i++;
                if (Source_arr[i] == ' ') i++;
                if ((Source_arr[i] == 'U') || (Source_arr[i] == '1'))
                    workByte |= (1 << 1);
                i++;
                if (Source_arr[i] == ' ') i++;
                if ((Source_arr[i] == 'U') || (Source_arr[i] == '1'))
                    workByte |= (1 << 0);
                i++;
                toEeprom_arr[counter] = workByte;
                counter++;
            }
            return toEeprom_arr;                       
        }
       
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Hide();
            e.Cancel = true;            
        }

        public void Executed_New(object sender, ExecutedRoutedEventArgs e)
        {
            createNewTopology();
        }

        public void CanExecute_New(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            openTopology();
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
       
        private void CommandBinding_Executed_1(object sender, ExecutedRoutedEventArgs e)
        {
            
            saveTopology();
        }

        private void CommandBinding_CanExecute_1(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_Executed_2(object sender, ExecutedRoutedEventArgs e)
        {
            SaveAs();
        }

        private void CommandBinding_CanExecute_2(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }                      

        private void MorF_X1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (MorF_X1.Content.ToString() == ">-") MorF_X1.Content = "<-";
            else MorF_X1.Content = ">-";
        }

        private void MorF_X2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (MorF_X2.Content.ToString() == "-<") MorF_X2.Content = "->";
            else MorF_X2.Content = "-<";
        }

        private void CreateScreenScheme(object sender, RoutedEventArgs e)
        {
            _CreateScreenScheme();
        }

        private void _CreateScreenScheme()
        {
            SchemeStackPanel.Visibility = Visibility.Visible;
            image_Scheme.Visibility = Visibility.Hidden;
            Create_PictureScheme();
        }

        private void Open_picture_scheme_Click(object sender, RoutedEventArgs e)
        {
            SchemeStackPanel.Visibility = Visibility.Hidden;
            image_Scheme.Visibility = Visibility.Visible;
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension 
            dlg.Filter = "JPEG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|JPG Files (*.jpeg)|*.jpeg|GIF Files (*.gif)|*.gif|BMP Files (*.bmp)|*.bmp|TIF Files (*.tif)|*.tif";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                try
                {// Open document 
                    string filename = Scheme_file_path = dlg.FileName;

                    string[] pathArr = filename.Split('\\');
                    string[] fileArr = pathArr.Last().Split('\\');
                    string filename_only = fileArr.Last().ToString();



                    var bmi = new BitmapImage();
                    bmi.BeginInit();
                    bmi.CacheOption = BitmapCacheOption.OnLoad;
                    bmi.UriSource = new Uri(dlg.FileName, UriKind.Absolute);
                    bmi.EndInit();

                    image_Scheme.Source = bmi;
                }
                catch { }
            }           
        }

        private void Create_PictureScheme()
        {
            if (image_Scheme.Visibility == Visibility.Hidden)
            {
                image_Scheme.Visibility = Visibility.Hidden;
                SchemeStackPanel.Visibility = Visibility.Visible;
                SchemeStackPanel.Children.Clear();
                byte DockPanelCounter = 0;

                DockPanel dpan = new DockPanel();
                Label lab = new Label();
                lab.FontWeight = FontWeights.Bold;
                lab.Content = X1.Content + "  " + MorF_X1.Content;
                lab.HorizontalAlignment = HorizontalAlignment.Left;
                dpan.Children.Add(lab);

                Label lab2 = new Label();
                lab2.FontWeight = FontWeights.Bold;
                lab2.Content = MorF_X2.Content.ToString() + "  " + X2.Content.ToString() + "           ";
                lab2.HorizontalAlignment = HorizontalAlignment.Right;
                dpan.Children.Add(lab2);

                SchemeStackPanel.Children.Add(dpan);

                foreach (var item in CheckboxPanel.Children)
                {
                    StackPanel sp = new StackPanel();
                    sp.Orientation = Orientation.Horizontal;
                    SchemeStackPanel.Children.Add(sp);
                    Label Rightlable = new Label();
                    Rightlable.HorizontalAlignment = HorizontalAlignment.Right;

                    var dp = item as DockPanel;
                    if (item == null) continue;
                    byte NumberControls = 0;

                    foreach (var dp_item in dp.Children)
                    {
                        var left_label = dp_item as Label;
                        if ((left_label != null) && (NumberControls == 0))
                        {
                            Label lable = new Label();
                            lable.HorizontalAlignment = HorizontalAlignment.Left;
                            lable.BorderBrush = Brushes.RoyalBlue;
                            if (DockPanelCounter == 18) lable.BorderThickness = new Thickness(2, 2, 1, 2);
                            else lable.BorderThickness = new Thickness(2, 2, 1, 1);
                            lable.MinWidth = 90;
                            lable.Content = left_label.Content;
                            sp.Children.Add(lable);
                            NumberControls = 1;
                            continue;
                        }

                        var left_cb = dp_item as ComboBox;
                        if ((left_cb != null) && (NumberControls == 1))
                        {
                            Label lable = new Label();
                            lable.HorizontalAlignment = HorizontalAlignment.Left;
                            lable.BorderBrush = Brushes.RoyalBlue;
                            if (DockPanelCounter == 18) lable.BorderThickness = new Thickness(1, 2, 2, 2);
                            else lable.BorderThickness = new Thickness(1, 2, 2, 1);
                            lable.MinWidth = 90;
                            if (left_cb.SelectedItem.ToString() != "Не подсоединён")
                            {
                                lable.Content = left_cb.SelectedItem.ToString();
                            }

                            sp.Children.Add(lable);
                            NumberControls = 2;
                            continue;
                        }

                        var sep = dp_item as Separator;
                        if (sep != null)
                        {
                            Label lable = new Label();
                            lable.Content = "                                        ";
                            sp.Children.Add(lable);
                            continue;
                        }

                        var right_label = dp_item as Label;
                        if ((right_label != null) && (NumberControls == 2))
                        {
                            Label lable = new Label();
                            lable.HorizontalAlignment = HorizontalAlignment.Right;
                            lable.BorderBrush = Brushes.RoyalBlue;
                            if (DockPanelCounter == 18) lable.BorderThickness = new Thickness(1, 2, 2, 2);
                            else lable.BorderThickness = new Thickness(1, 2, 2, 1);
                            lable.MinWidth = 90;
                            lable.Content = right_label.Content;
                            Rightlable = lable;
                            NumberControls = 3;
                            continue;
                        }

                        var right_cb = dp_item as ComboBox;
                        if ((right_cb != null) && (NumberControls == 3))
                        {
                            Label lable = new Label();
                            lable.HorizontalAlignment = HorizontalAlignment.Right;
                            lable.BorderBrush = Brushes.RoyalBlue;
                            if (DockPanelCounter == 18) lable.BorderThickness = new Thickness(2, 2, 1, 2);
                            else lable.BorderThickness = new Thickness(2, 2, 1, 1);
                            lable.MinWidth = 90;
                            if (right_cb.SelectedItem.ToString() != "Не подсоединён")
                            {
                                lable.Content = right_cb.SelectedItem.ToString();
                            }
                            sp.Children.Add(lable);
                            sp.Children.Add(Rightlable);
                            NumberControls = 0;
                            continue;
                        }
                    }
                    DockPanelCounter++;
                }

            }
        }
    }
}
