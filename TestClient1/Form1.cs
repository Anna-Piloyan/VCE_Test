using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestClient1
{
    public partial class Form1 : Form
    {
        const int port = 8888;
        const string address = "127.0.0.1";
        TcpClient client = null;
        NetworkStream stream;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                client = new TcpClient(address, port);
                stream = client.GetStream();
                // ввод сообщения
                string login = textBox1.Text;
                string password = maskedTextBox1.Text;
                string message = String.Format("{0} {1}", login, password);
                // преобразуем сообщение в массив байтов
                byte[] data = Encoding.Unicode.GetBytes(message);
                // отправка сообщения
                stream.Write(data, 0, data.Length);
                // получаем ответ
                data = new byte[2048]; // буфер для получаемых данных
                StringBuilder builder = new StringBuilder();
                int bytes = 0;
                do
                {
                    bytes = stream.Read(data, 0, data.Length);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                while (stream.DataAvailable);
                message = builder.ToString();
                if (message != "")
                {
                    Form2 form2 = new Form2(message, client, stream);
                    form2.ShowDialog();
                }

                stream.Close();
                client.Close();


            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show("ArgumentNullException: {0}", ex.Message);
            }
            catch (SocketException ex)
            {
                MessageBox.Show("SocketException TestClient Form1: {0}", ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;          
            if (stream != null)
                stream.Close();
            if (client != null)
                client.Close();
            this.Close();
        }
    }
}

