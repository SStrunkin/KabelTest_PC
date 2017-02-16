using System;
using System.Collections.Generic;
using System.IO;
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
        Example_topology_Class1 eTc1 = new Example_topology_Class1();
        string Scheme_file_path;
        
        public GenerateTopolpgy()
        {
            InitializeComponent();
            DataContext = new Create_Cabel_comboBoxes_model();
           
            foreach (var tuple in GetAllCombobox())
            {
                tuple.Item1.SelectionChanged += ComboBox_SelectionChanged;

            }
            Same_second_connector.Click += Same_second_connector_Click;

            foreach (var combobox in My_GetAllCombobox())
            {
               // combobox.Background = Brushes.LightGray;
                combobox.MouseRightButtonUp += setNetworcksIllumination;
            }
            
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

                tSc.setkabelTopology_arr_element(i, '*');                               //Ставим звёздочку как наличие источника напряжения
                i += ((tSc.get_Sum_pins_in_X() * 2) + 1);                            // 39 + i расстояние между звёздочками в массиве 
                if (i == ((tSc.getKabel_topology_arr_lenght() / 2) + (tSc.get_Sum_pins_in_X() - 1))) i++;             // 759 мы подаём напряжение уже на второй разЪём - Х2
                                                                                                                      //System.out.println(i + " ");
            }

        }

        private void Same_second_connector_Click(object sender, RoutedEventArgs e)
        {            
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            resetNetworcksIllumination();
            var tuple = GetAllCombobox().FirstOrDefault(i => i.Item1 == sender);

            if (tuple.Item1 != null)
            {
                if (Same_second_connector.IsChecked == true)
                    tuple.Item2.SelectedItem = tuple.Item1.SelectedItem;
            }

        }

        private byte ParseComboboxValue(ComboBox combobox)
        {
            if (combobox.SelectedItem == null)
                return 0;

            byte value;
            byte.TryParse(combobox.SelectedItem.ToString(), out value);
            return value;
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


        private List<Tuple<ComboBox, ComboBox>> GetAllCombobox()
        {
            var result = new List<Tuple<ComboBox, ComboBox>>();
            foreach (var childDockPanel in CheckboxPanel.Children)
            {
                var dockPanel = childDockPanel as DockPanel;
                if (dockPanel == null)
                    continue;

                var comboboxes = new List<ComboBox>();
                foreach (var childCombobox in dockPanel.Children)
                {
                    var combobox = childCombobox as ComboBox;
                    if (combobox == null)
                        continue;
                    comboboxes.Add(combobox);
                }

                if (comboboxes.Count > 1)
                    result.Add(Tuple.Create(comboboxes[0], comboboxes[1]));
            }
            return result;
        }


        private void Open_picture_scheme_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension 
            dlg.Filter = "JPEG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|JPG Files (*.jpeg)|*.jpeg|GIF Files (*.gif)|*.gif|BMP Files (*.bmp)|*.bmp|TIF Files (*.tif)|*.tif";

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



                var bmi = new BitmapImage();
                bmi.BeginInit();
                bmi.CacheOption = BitmapCacheOption.OnLoad;
                bmi.UriSource = new Uri(dlg.FileName, UriKind.Absolute);
                bmi.EndInit();

                image_Scheme.Source = bmi;                
            }
        }
        
        private void Create_cabel_topology_Click(object sender, RoutedEventArgs e)
        {
            if (Name_or_NKRM_t.Text != "")
            {

                this.Cursor = Cursors.Wait;

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
                                   
                this.Cursor = Cursors.Arrow;
                MessageBox.Show("Создание топологии завершено.");
            }
            else
            {
                Name_or_NKRM_t.Focus();
                MessageBox.Show("Введите Название или НКРМ");
            }
        }
       
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {            
            this.Close();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
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
                    
                }
                catch
                {

                }

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
                { }
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
                { }
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
                { }
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
                { }
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
                { }
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
            if (Name_or_NKRM_t.Text != "")
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
                   
                    Properties.Settings.Default.PathSaveFolder = System.IO.Path.GetDirectoryName(dlg.FileName);
                    Properties.Settings.Default.Save();
                    //MessageBox.Show(Properties.Settings.Default.PathSaveFolder);

                    System.IO.File.WriteAllLines(dlg.FileName, Save_Array);
                    if ((Scheme_file_path != null) && (Scheme_file_path != System.IO.Path.Combine(System.IO.Path.GetDirectoryName(dlg.FileName), (System.IO.Path.GetFileNameWithoutExtension(dlg.FileName) + System.IO.Path.GetExtension(Scheme_file_path)))))
                        File.Copy(Scheme_file_path, System.IO.Path.Combine(System.IO.Path.GetDirectoryName(dlg.FileName), (System.IO.Path.GetFileNameWithoutExtension(dlg.FileName) + System.IO.Path.GetExtension(Scheme_file_path))), true);

                }
            }

            else
            {
                Name_or_NKRM_t.Focus();
                MessageBox.Show("Введите Название или НКРМ");
            }

        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            Window1 ComPortOptions_window = new Window1();
            ComPortOptions_window.Owner = this;
            ComPortOptions_window.Show();
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            Name_or_NKRM_t.Text = "";
            foreach (var combobox in My_GetAllCombobox())
            {
                combobox.SelectedIndex = 0;
            }

            image_Scheme.Source = null;

            tSc.ClearArrays();
        }

        private void setNetworcksIllumination(object sender, MouseButtonEventArgs e)
        {
            ComboBox com_box = sender as ComboBox;
            resetNetworcksIllumination();
            foreach (var combobox in My_GetAllCombobox())
            {
                if ((combobox.SelectedIndex.ToString() == com_box.SelectedIndex.ToString()) && (CheckMode.IsChecked == true))
                {
                    combobox.Foreground = Brushes.Red;
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
                                        
                    System.IO.File.WriteAllLines(Properties.Settings.Default.PathSaveFolder + "\\"+ Name_or_NKRM_t.Text + ".kts", Save_Array);
                    if(Scheme_file_path != "")
                        File.Copy(Scheme_file_path, System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Properties.Settings.Default.PathSaveFolder + "\\"), (System.IO.Path.GetFileNameWithoutExtension(Name_or_NKRM_t.Text) + System.IO.Path.GetExtension(Scheme_file_path))), true);                
            }
            else
            {
                Name_or_NKRM_t.Focus();
                MessageBox.Show("Введите Название или НКРМ");
            }
        }
    }
}
