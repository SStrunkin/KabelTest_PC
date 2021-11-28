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
               "Не подсоединён", "Цепь 1", "Цепь 2", "Цепь 3", "Цепь 4","Цепь 5", "Цепь 6", "Цепь 7", "Цепь 8", "Цепь 9",
                "Цепь 10", "Цепь 11","Цепь 12", "Цепь 13", "Цепь 14","Цепь 15", "Цепь 16", "Цепь 17", "Цепь 18", "Цепь 19"
            };
        }       
    }   
}
