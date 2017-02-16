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
        //static bool serial_ready = false;
        //static Thread SerialreadThread = new Thread(SerialRead);
        public static SerialPort _serialPort = new SerialPort();

        public static string ConnectingSerialPort(string s)
        {

            try
            {
                if (_serialPort.IsOpen)
                {
                    //serial_ready = false;               
                    _serialPort.Close();
                }

                // Allow the user to set the appropriate properties.
                Properties.Settings.Default.COMport = s;
                Properties.Settings.Default.Save();
                _serialPort.PortName = s;
                _serialPort.BaudRate = 9600;
                _serialPort.Parity = Parity.None;
                _serialPort.DataBits = 8;
                _serialPort.StopBits = StopBits.One;
                _serialPort.Handshake = Handshake.None;

                // Set the read/write timeouts
                _serialPort.ReadTimeout = 1000;
                _serialPort.WriteTimeout = 1000;
                _serialPort.Open();
                //serial_ready = true;
                //SerialreadThread.Start();
                // _serialPort.Write("1");

                return "Соединение по порту " + s + " установлено.\nТеперь это окно можно закрыть.";
            }
            catch { }
            return "Порт не выбран или занят!";
        }

        /*       
                public static void SerialRead()
                {
                    Action action = () =>
                    {
                        while (serial_ready)
                        {
                            try
                            {
                                string message = _serialPort.ReadLine();

                            }
                            catch (TimeoutException) { }
                        }
                    };
                    Invoke(action);
                }
        */
    }
}
    

