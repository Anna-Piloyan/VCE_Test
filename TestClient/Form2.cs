using LibraryClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestClient
{
    public partial class Form2 : Form
    {

        // NetworkStream streamForm2;
        TcpClient clientForm2; 
       
        const int port = 8888;
        const string address = "127.0.0.1";

        public Form2()
        {
            InitializeComponent();
        }
        public Form2(string message) //, TcpClient client, NetworkStream stream)
        {
            InitializeComponent();
            FillFormWithUserData(message);
            clientForm2 = new TcpClient(address, port);
            // streamForm2 = stream;

        }

        private void FillFormWithUserData(string str)
        {
            string[] log_data = str.Split(',');
            string n_s = log_data[0];
            for (int i = 1; i < log_data.Length; i++)
            {
                comboBox1.Items.Add(log_data[i]);
            }
            label1.Text = n_s;
        }
        BinaryFormatter bf;
        private void PassTest_Click(object sender, EventArgs e)
        {
            bf = new BinaryFormatter();
            NetworkStream streamForm3 = null;
            //TcpClient clientForm3 = null;
            Test test = new Test();
            try
            {

               // TcpClient clientForm3 = new TcpClient(address, port);
                streamForm3 = clientForm2.GetStream();

                while (true)
                {

                    // ввод сообщения
                    string passTest = "PassTest";
                    string testName = "Divide.xml"; // (string)dataGridView1.SelectedRows[0].Cells[2].Value;
                    string message = String.Format("{0} {1}", passTest, testName);
                    // преобразуем сообщение в массив байтов
                    byte[] data1 = Encoding.Unicode.GetBytes(message);
                    // отправка сообщения
                    streamForm3.Write(data1, 0, data1.Length);
                    MessageBox.Show($" Client Pass Test:{message}");
                    // получаем ответ
                    // ds = null;
                    byte[] buffer = new byte[16384];
                    using (MemoryStream memory = new MemoryStream(buffer))
                    {
                       
                        test = (Test)bf.Deserialize(streamForm3);
                        MessageBox.Show("XML Buffer");

                    }
                    if (test != null)
                    {
                        MessageBox.Show("Going to Open Form3");
                        Form3 form3 = new Form3();
                        form3.ShowDialog();
                      
                    }

                }
        }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (streamForm3 != null)
                    streamForm3.Close();
                if (clientForm2 != null)
                    clientForm2.Close();
            }
           
        }
       
        private void LoadTest_Click(object sender, EventArgs e)
        {
         //   bf = new BinaryFormatter();
         //   DataSet ds = new DataSet();
         //   NetworkStream streamForm2 = null;
         ////   TcpClient clientForm2 = null;
         //   try
         //     {

         ////   clientForm2 = new TcpClient(address, port);
         //       streamForm2 = clientForm2.GetStream();

         //   while (true)
         //       {

         //           // ввод сообщения
         //           string loadTest = "LoadTest";
         //           string group = comboBox1.SelectedItem.ToString();
         //           string message = String.Format("{0} {1}", loadTest, group);
         //           // преобразуем сообщение в массив байтов
         //           byte[] data1 = Encoding.Unicode.GetBytes(message);
         //           // отправка сообщения
         //           streamForm2.Write(data1, 0, data1.Length);
         //           MessageBox.Show($" Client Form2:{message}");
         //           // получаем ответ
         //          // ds = null;
         //           byte[] buffer = new byte[16384];
         //           using (MemoryStream memory = new MemoryStream(buffer))
         //           {
                                         
         //               ds = (DataSet)bf.Deserialize(streamForm2);
         //               MessageBox.Show("MemoryStream Buffer");
                       
         //           }
         //           if (ds != null)
         //           {
         //               MessageBox.Show("Table Form2");
         //               dataGridView1.DataSource = ds.Tables[0]; 
         //               LoadTest.Enabled = true;
                       
         //           }
                    
                 
         //       }
         //   }
         //   catch (Exception ex)
         //   {
         //       MessageBox.Show(ex.Message);
         //   }
         //   finally
         //   {
         //       if (streamForm2 != null)
         //           streamForm2.Close();
         //       if (clientForm2 != null)
         //           clientForm2.Close();

           // }
           
            //MemoryStream stream = new MemoryStream();
            ////и для сериализации
            //BinaryFormatter bf = new BinaryFormatter();

            ////создаем объект DataSet и производим для него десериализацию клиентского потока
            //DataSet data = (DataSet)bf.Deserialize(streamForm2);
            ////отобразили таблицу с индексом 0 в клиентском DataGridView
            ////а т.к. таблица у меня всего одна, то и выбор, соответственно, невелик
            //dataGridView1.DataSource = data.Tables[0].DefaultView;
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (streamForm2 != null)
            //    streamForm2.Close();
            //if (clientForm2 != null)
            //    clientForm2.Close();
        }
    }
}
