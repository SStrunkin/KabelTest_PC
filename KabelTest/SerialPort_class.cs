using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KabelTest
{
    class SerialPort_class
    {
        public const byte op_Comparing = 0;
        public const byte op_SetQuickComparingString = 1;
        public const byte op_QuickComparingMethod = 2;
        public const byte op_StringToReverse = 3;
        public const byte op_StringToReverseFromEeprom = 4;
        public const byte op_arrayToEeprom = 5;

        public static byte StandBy = 0;
        public static byte StandartComparing = 1;
        public static byte ReadingFromEeprom = 2;
        public static byte WriteToEeprom_from_PC = 4;

        public static byte Cell_1 = 0;
        public static byte Cell_2 = 1;
        public static byte Cell_3 = 2;
        public static byte Cell_4 = 3;
        public static byte Cell_5 = 4;
        public static byte Cell_6 = 5;
        public static byte Cell_7 = 6;
        public static byte Cell_8 = 7;
       
        private static byte[] trancmittByte = new byte[1];
        private static byte OperationChoiceByte = 0;
        public static SerialPort _serialPort = new SerialPort();

        public static string ConnectingSerialPort(string s)
        {
            try
            {
                if (_serialPort.IsOpen)
                {                               
                    _serialPort.Close();
                }

                // Allow the user to set the appropriate properties.
                Properties.Settings.Default.COMport = s;
                Properties.Settings.Default.Save();
                _serialPort.PortName = s;
                _serialPort.BaudRate = 19200;
                _serialPort.Parity = Parity.None;
                _serialPort.DataBits = 8;
                _serialPort.StopBits = StopBits.One;
                _serialPort.Handshake = Handshake.None;

                // Set the read/write timeouts
                _serialPort.ReadTimeout = 2000;
                _serialPort.WriteTimeout = 2000;
                _serialPort.Open();
               
                return "Соединение по порту " + s + " установлено.\nТеперь это окно можно закрыть.";
            }
            catch { }
            return "Порт не выбран или занят!";
        }   
        
        public static void setOperationChoiceByte(byte x)
        {
            OperationChoiceByte = x;
        }

        public static byte getOperationChoiceByte()
        {
            return OperationChoiceByte;
        }

        public static void TransmitByte(byte operations)
        {
            trancmittByte[0] = operations;
            _serialPort.Write(trancmittByte, 0, 1);
        }      
    }
}
    

