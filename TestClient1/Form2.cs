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
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace TestClient1
{
    public partial class Form2 : Form
    {
        NetworkStream streamForm2;
        TcpClient clientForm2;
        const int port = 8888;
        const string address = "127.0.0.1";
        int testId = 0;
        string testName = "";
        public Form2()
        {
            InitializeComponent();
        }
        public Form2(string message, TcpClient client, NetworkStream stream)
        {
            InitializeComponent();
            FillFormWithUserData(message);
             clientForm2 = client;
             streamForm2 = stream;
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
      
        private void PassTest_Click(object sender, EventArgs e)
        {          
            Test test = new Test();
            try
            {
                clientForm2 = new TcpClient(address, port);
                streamForm2 = clientForm2.GetStream();

                string passTest = "PassTest";
                string message = String.Format("{0} {1}", passTest, testName);              
                byte[] data1 = Encoding.Unicode.GetBytes(message);
                streamForm2.Write(data1, 0, data1.Length);
                byte[] buffer = new byte[16384];
                int bytes = 0;
                bytes = streamForm2.Read(buffer, 0, buffer.Length);
                string str = Encoding.UTF8.GetString(buffer, 0, bytes);
                XDocument sss = XDocument.Parse(str);
                if (File.Exists("111.xml"))
                    File.Delete("111.xml");
                sss.Save("111.xml");
                testId = GetTestId();
                XmlSerializer formatter = new XmlSerializer(typeof(LibraryClass.Test));
                using (FileStream fs = new FileStream("111.xml", FileMode.OpenOrCreate))
                {
                    test = (LibraryClass.Test)formatter.Deserialize(fs);
                }
                if (test != null)
                {
                    Form3 form3 = new Form3(test, clientForm2, streamForm2, label1.Text, testId);
                    form3.ShowDialog();
                }
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show("ArgumentNullException: {0}", ex.Message);
            }
            catch (SocketException ex)
            {
                MessageBox.Show("SocketException TestClient Form2 PassTest: {0}", ex.Message);
            }
            finally
            {
                //if (streamForm2 != null)
                //    streamForm2.Close();
                //if (clientForm2 != null)
                //    clientForm2.Close();
            }
        }

        private void LoadTest_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            BinaryFormatter bf = new BinaryFormatter();
            DataSet ds = new DataSet();
            try
            {
                clientForm2 = new TcpClient(address, port);
                streamForm2 = clientForm2.GetStream();             
                string loadTest = "LoadTest";
                string group = comboBox1.SelectedItem.ToString();
                string message = String.Format("{0} {1}", loadTest, group);              
                byte[] data1 = Encoding.Unicode.GetBytes(message);             
                streamForm2.Write(data1, 0, data1.Length);            
                byte[] buffer = new byte[16384];
                ds = (DataSet)bf.Deserialize(streamForm2);           
                if (ds != null)
                {           
                    dataGridView1.DataSource = ds.Tables[0];
                }
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show("ArgumentNullException: {0}", ex.Message);
            }
            catch (SocketException ex)
            {
                MessageBox.Show("SocketException TestClient Form2 LoadTest: {0}", ex.Message);
            }
            finally
            {
                //if (streamForm2 != null)
                //    streamForm2.Close();
                //if (clientForm2 != null)
                //    clientForm2.Close();
            }
        }

        private int GetTestId()
        {
            int id = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
            return id; 
        }
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {

            //if (streamForm2 != null)
            //    streamForm2.Close();
            //if (clientForm2 != null)
            //    clientForm2.Close();
            streamForm2.Close();
            clientForm2.Close();
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            testName = dataGridView1.CurrentCell.Value.ToString();
        }
    }
}

