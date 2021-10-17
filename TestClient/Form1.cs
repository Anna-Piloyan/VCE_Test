using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestClient
{
    public partial class Form1 : Form
    {

        const int port = 8888;
        const string address = "127.0.0.1";
       // TcpClient client = null;
       
        public Form1()
        {
            InitializeComponent();
          
        }
            
        private void button1_Click(object sender, EventArgs e)
        {
            NetworkStream stream = null;
            TcpClient client = null;
            try
            {
                client = new TcpClient(address, port);
                stream = client.GetStream();

                while (true)
                {
                  
                    // ввод сообщения
                    string login = textBox1.Text;
                    string password = maskedTextBox1.Text;
                    string message = String.Format("{0} {1}", login, password);
                    // преобразуем сообщение в массив байтов
                    byte[] data = Encoding.Unicode.GetBytes(message);
                    // отправка сообщения
                    stream.Write(data, 0, data.Length);
                    MessageBox.Show($" Client To Server:{message}");
                    // получаем ответ
                    data = new byte[1024]; // буфер для получаемых данных
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);
                   
                    message = builder.ToString();
                    MessageBox.Show($" Client From Server:{message}");
                   
                    if (message != "")
                    {
                        Form2 form2 = new Form2(message, client, stream);
                        form2.ShowDialog();
                    }
                   
               
                    MessageBox.Show($"Client2: I reach HERE!!!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (stream != null)
                    stream.Close();
                if (client != null)
                    client.Close();
            }
        }

       

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
           // if (stream != null)
           //     stream.Close();
           // if (client != null)
             //   client.Close();
        }

        //private void connect_to_server_Click(object sender, EventArgs e)
        //{
        //    //создаем клиента на подключение к 200 порту по IP-сервера
        //    TcpClient client = new TcpClient("127.0.0.1", 200);
        //    //получаем поток для этого клиента. По нему пошел DataSet
        //    NetworkStream client_streem = client.GetStream();
        //    //опять врменный буфер
        //    MemoryStream streem = new MemoryStream();
        //    //и для сериализации
        //    BinaryFormatter bf = new BinaryFormatter();

        //    //создаем объект DataSet и производим для него десериализацию клиентского потока
        //    DataSet data = (DataSet)bf.Deserialize(client_streem);
        //    //отобразили таблицу с индексом 0 в клиентском DataGridView
        //    //а т.к. таблица у меня всего одна, то и выбор, соответственно, невелик
        //  //  dataGridView1.DataSource = data.Tables[0].DefaultView;
        //}
    }
}
