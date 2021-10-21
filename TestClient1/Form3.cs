using LibraryClass;
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
    
    public partial class Form3 : Form
    {
        NetworkStream streamForm3;
        TcpClient clientForm3;
        const int port = 8888;
        const string address = "127.0.0.1";
        string testId = "";
        string n_s = "";
        Test testToPass;
        List<string> userAnswers = new List<string>();
        List<string> correctAnswers = new List<string>();
        int Result = 0;
        int i = 1;
        int rightAnswers = 0;

        public Form3()
        {
            InitializeComponent();
        }
        public Form3(Test test, TcpClient clientForm2, NetworkStream streamForm2, string ns, int id)
        {
            InitializeComponent();
            streamForm3 = streamForm2;
            clientForm3 = clientForm2;
            testToPass = test;
            testId = id.ToString();
            n_s = ns;
            textBox1.Text = testToPass.Question[0].ToString();
            foreach (var answer in testToPass.Question[0].Answer)
            {
                listBox1.Items.Add(answer);
            }
            foreach (var q in testToPass.Question)
            {
                foreach (var answer in q.Answer)
                {
                    if (answer.IsRight == "True")
                        correctAnswers.Add(answer.Description);
                }
            }
        }
        private void Next_Click(object sender, EventArgs e)
        {
            int count = Convert.ToInt32(testToPass.QuestionCount);
            if (listBox1.SelectedItem != null)
            {
                userAnswers.Add(listBox1.SelectedItem.ToString());
                listBox1.Items.Clear();
                textBox1.Text = "";
                if (i == count)
                {
                    TestResult();
                    return;
                }
                textBox1.Text = testToPass.Question[i].ToString();
                foreach (var answer in testToPass.Question[i].Answer)
                {
                    listBox1.Items.Add(answer);
                }
                i++;
            }        
        }

        private void TestResult()
        {
            int count = Convert.ToInt32(testToPass.QuestionCount);
            for (int j = 0; j < count; j++)
            {
                if (correctAnswers[j] == userAnswers[j])
                    rightAnswers++;
            }
            Result = rightAnswers * 100 / count;
            MessageBox.Show($"Result - {Result}%");
           // записать в базу данных
            SendDataToDB();
            this.Close();
        }
       
        private void SendDataToDB()
        {
            try
            {
                clientForm3 = new TcpClient(address, port);
                streamForm3 = clientForm3.GetStream();
                string msg = "LoadResult";
                string result = Result.ToString();
                string message = String.Format("{0} {1} {2} {3}", msg, result, n_s, testId);
                byte[] data = Encoding.Unicode.GetBytes(message);
                streamForm3.Write(data, 0, data.Length);
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show("ArgumentNullException: {0}", ex.Message);
            }
            catch (SocketException ex)
            {
                MessageBox.Show("SocketException: {0}", ex.Message);
            }
        }
    }
}
