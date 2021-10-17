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

namespace TestClient
{
    public partial class Form2 : Form
    {
        TcpClient clientForm2;
        NetworkStream streamForm2;
        string[] log_data = null; 
        string n_s = ""; 
        string g = null;
        public Form2()
        {
            InitializeComponent();
        }
        public Form2(string name_surname, TcpClient client, NetworkStream stream)
        {
            InitializeComponent();
            log_data = name_surname.Split(',');
            n_s = log_data[0];
            for (int i = 1; i < log_data.Length; i++)
            {
                comboBox1.Items.Add(log_data[i]);
            }          
            MessageBox.Show($"Form2: {n_s}");           
            label1.Text = n_s;          
            clientForm2 = client;
            streamForm2 = stream;

        }

       
        private void PassTest_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.ShowDialog();
        }

        private void LoadTest_Click(object sender, EventArgs e)
        {

        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (streamForm2 != null)
                streamForm2.Close();
            if (clientForm2 != null)
                clientForm2.Close();
        }
    }
}
