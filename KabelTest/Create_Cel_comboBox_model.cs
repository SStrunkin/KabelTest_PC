using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KabelTest
{
    class Create_Cel_comboBox_model
    {
        public ObservableCollection<string> Cels { get; private set; }

        public Create_Cel_comboBox_model()
        {
            Cels = new ObservableCollection<string>
            {
               "Ячейка 1", "Ячейка 2", "Ячейка 3", "Ячейка 4","Ячейка 5", "Ячейка 6", "Ячейка 7", "Ячейка 8"
            };
        }
    }
}
