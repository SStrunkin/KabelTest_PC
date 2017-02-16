using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KabelTest
{
    class Topology_sourse_class
    {
        private const int Kabel_topology_arr_lenght = 1482;
        private const int Sum_pins_in_X = 19;

        private int[] X1_Topology_sourse = new int[Sum_pins_in_X];
        private int[] X2_Topology_sourse = new int[Sum_pins_in_X];
        private char[] Kabel_topology_arr = new char[Kabel_topology_arr_lenght];
        private char[] Kabel_topology_arr_1 = new char[Kabel_topology_arr_lenght];
        private char[] Kabel_topology_arr_2 = new char[Kabel_topology_arr_lenght];

        public void ClearArrays()
        {
            Array.Clear(X1_Topology_sourse, 0, X1_Topology_sourse.Length);
            Array.Clear(X2_Topology_sourse, 0, X2_Topology_sourse.Length);
            Array.Clear(Kabel_topology_arr, 0, Kabel_topology_arr.Length);
            Array.Clear(Kabel_topology_arr_1, 0, Kabel_topology_arr_1.Length);
            Array.Clear(Kabel_topology_arr_2, 0, Kabel_topology_arr_2.Length);
        }

        public void Set_Kabel_topology_arr_1_WhenOpen(string s)
        {
            Kabel_topology_arr_1 = s.ToCharArray();
        }

        public void Set_Kabel_topology_arr_2_WhenOpen(string s)
        {
            Kabel_topology_arr_2 = s.ToCharArray();
        }

        public void Copy_Kabeltopology_arr_to_Kabeltopology_arr1()
        {
            for (int i = 0; i < Kabel_topology_arr_lenght; i++)
            {
                Kabel_topology_arr_1[i] = Kabel_topology_arr[i];
            }
        }

        public void Copy_Kabeltopology_arr_to_Kabeltopology_arr2()
        {
            for (int i = 0; i < Kabel_topology_arr_lenght; i++)
            {
                Kabel_topology_arr_2[i] = Kabel_topology_arr[i];
            }
        }

        public void swap_Topology_source_arrays()
        {
            int[] swap_array = new int[Sum_pins_in_X];

            for (int i = 0; i < Sum_pins_in_X; i++)
            {
                swap_array[i] = X1_Topology_sourse[i];
            }

            for (int i = 0; i < Sum_pins_in_X; i++)
            {
                X1_Topology_sourse[i] = X2_Topology_sourse[i];
            }

            for (int i = 0; i < Sum_pins_in_X; i++)
            {
                X2_Topology_sourse[i] = swap_array[i];
            }
        }


        public void setkabelTopology_arr_element(int index, char value)
        {
            Kabel_topology_arr[index] = value;
        }

        public char getkabelTopology_arr_element(int index)
        {
            return Kabel_topology_arr[index];
        }

        public string getkabelTopology_arr_string()
        {
            string s = new string(Kabel_topology_arr);
            return s;
        }

        public string getkabelTopology_arr1_string()
        {
            string s = new string(Kabel_topology_arr_1);
            return s;
        }

        public string getkabelTopology_arr2_string()
        {
            string s = new string(Kabel_topology_arr_2);
            return s;
        }

        public void set_X1_Topology_sourse(int index, int value)
        {
            X1_Topology_sourse[index] = value;
        }

        public int get_X1_Topology_sourse(int index)
        {
            return X1_Topology_sourse[index];
        }

        public string get_X1_Topology_sourse_string()
        {
            string s = string.Join(" ", X1_Topology_sourse);

            return s;
        }

        public string get_X2_Topology_sourse_string()
        {
            string s = string.Join(" ", X2_Topology_sourse);

            return s;
        }

        public void set_X2_Topology_sourse(int index, int value)
        {
            X2_Topology_sourse[index] = value;
        }

        public int get_X2_Topology_sourse(int index)
        {
            return X2_Topology_sourse[index];
        }

        public int get_Sum_pins_in_X()
        {
            return Sum_pins_in_X;
        }

        public int getKabel_topology_arr_lenght()
        {
            return Kabel_topology_arr_lenght;
        }




        public int itemText_to_X1_or_X2_arrays(string s)
        {
            int c = 0;
            switch (s)
            {
                case "Не подсоединён":
                    c = 0;
                    break;

                case "Сеть 1":
                    c = 1;
                    break;

                case "Сеть 2":
                    c = 2;
                    break;

                case "Сеть 3":
                    c = 3;
                    break;

                case "Сеть 4":
                    c = 4;
                    break;

                case "Сеть 5":
                    c = 5;
                    break;

                case "Сеть 6":
                    c = 6;
                    break;

                case "Сеть 7":
                    c = 7;
                    break;

                case "Сеть 8":
                    c = 8;
                    break;

                case "Сеть 9":
                    c = 9;
                    break;

                case "Сеть 10":
                    c = 10;
                    break;

                case "Сеть 11":
                    c = 11;
                    break;

                case "Сеть 12":
                    c = 12;
                    break;

                case "Сеть 13":
                    c = 13;
                    break;

                case "Сеть 14":
                    c = 14;
                    break;

                case "Сеть 15":
                    c = 15;
                    break;

                case "Сеть 16":
                    c = 16;
                    break;

                case "Сеть 17":
                    c = 17;
                    break;

                case "Сеть 18":
                    c = 18;
                    break;

                case "Сеть 19":
                    c = 19;
                    break;

            }

            return c;

        }
    }
}
