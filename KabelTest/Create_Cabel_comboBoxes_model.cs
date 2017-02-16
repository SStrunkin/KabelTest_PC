using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KabelTest
{
    class Create_Cabel_comboBoxes_model
    {
        public ObservableCollection<string> CmbContent { get; private set; }

        public Create_Cabel_comboBoxes_model()
        {
            CmbContent = new ObservableCollection<string>
            {
               "Не подсоединён", "Сеть 1", "Сеть 2", "Сеть 3", "Сеть 4","Сеть 5", "Сеть 6", "Сеть 7", "Сеть 8", "Сеть 9",
                "Сеть 10", "Сеть 11","Сеть 12", "Сеть 13", "Сеть 14","Сеть 15", "Сеть 16", "Сеть 17", "Сеть 18", "Сеть 19"
            };
        }
    }
}
