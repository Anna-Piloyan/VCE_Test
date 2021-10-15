using LibraryClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;

namespace TesterDesign
{
    public partial class Form3 : Form
    {
        Test test;
        Question question;
        string dirName = @"..\..\bin\Debug\TestFolder"; 
        string path = @"TestFolder\";
        public Form3()
        {
            InitializeComponent();
            test = new Test();
            DirectoryInfo dirInfo = new DirectoryInfo(dirName);
            FileInfo[] info = dirInfo.GetFiles();
            foreach(var fileName in info)
               listBox3.Items.Add(fileName);
        }

        private void TestList_SelectedIndexChanged(object sender, EventArgs e)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(Test));
            using (FileStream fs = new FileStream(path + listBox3.SelectedItem.ToString(), FileMode.OpenOrCreate))
            {
                test = (Test)formatter.Deserialize(fs);
                textBox1.Text = test.Author;
                textBox2.Text = test.TestName;
                textBox3.Text = test.QuestionCount;
                numericUpDown1.Value = Convert.ToDecimal(test.Difficulty);
                foreach (var q in test.Question)
                    listBox1.Items.Add(q);

            }
           
        }

        private void QuestionList_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            if (listBox1.SelectedItem != null)
            {
                question = test.Question[listBox1.SelectedIndex];
                foreach (var item in question.Answer)
                {
                    listBox2.Items.Add(item.Description);
                }
                textBox5.Text = question.Description;
            }
        }

        private void SaveChanges_Click(object sender, EventArgs e)
        {
            test.Author = textBox1.Text;
            test.TestName = textBox2.Text;
            test.QuestionCount = textBox3.Text;
            test.Difficulty = numericUpDown1.Value.ToString();
            XmlSerializer formatter = new XmlSerializer(typeof(Test));
            using (FileStream fs = new FileStream(path + listBox3.SelectedItem.ToString(), FileMode.Truncate))
            {
                formatter.Serialize(fs, test);
                MessageBox.Show("Saved");
            }
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            textBox1.Text = ""; textBox2.Text = ""; textBox3.Text = "";
            textBox5.Text = ""; textBox6.Text = "";
            numericUpDown1.Value = 0;
        }

        private void AnswersList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                question = test.Question[listBox1.SelectedIndex];
                foreach (var item in question.Answer)
                {
                    if (item == listBox2.SelectedItem && item.IsRight == "True")
                        checkBox1.Checked = true;
                    else
                        checkBox1.Checked = false;
                }
                textBox6.Text = listBox2.SelectedItem.ToString();
            }
        }

        private void EditQuestion_Click(object sender, EventArgs e)
        {
            if (textBox5.Text != "")
            {
                test.Question[listBox1.SelectedIndex].Description = textBox5.Text;
                listBox1.Items.Clear();
                foreach (var q in test.Question)
                    listBox1.Items.Add(q);            
            }
            textBox5.Text = "";
        }

        private void EditAnswer_Click(object sender, EventArgs e)
        {
            Answer answer = new Answer();
            if (textBox6.Text != "")
            {              
                test.Question[listBox1.SelectedIndex].Answer[listBox2.SelectedIndex].IsRight = checkBox2.Checked.ToString();
                test.Question[listBox1.SelectedIndex].Answer[listBox2.SelectedIndex].Description = textBox6.Text;
                listBox2.Items.Clear();
                question = test.Question[listBox1.SelectedIndex];
                foreach (var item in question.Answer)
                {
                    listBox2.Items.Add(item.Description);
                }
            }               
            textBox6.Text = "";         
            checkBox2.Checked = false;
        }

        private void RemoveQuestion_Click(object sender, EventArgs e)
        {
           int count = Convert.ToInt32(test.QuestionCount);
           int index = listBox1.SelectedIndex;
           if (count > 1)
           {
                test.Question.RemoveAt(index);
                listBox1.Items.RemoveAt(index);
                textBox5.Text = "";
                count--;
           }
            else
                MessageBox.Show("You can't remove last Question");

        }

        private void RemoveAnswer_Click(object sender, EventArgs e)
        {
            int index = listBox2.SelectedIndex;
            if (listBox2.Items.Count > 1)
            {              
                listBox2.Items.RemoveAt(index);
                test.Question[listBox1.SelectedIndex].Answer.RemoveAt(index);
                textBox6.Text = "";
            }
            else
                MessageBox.Show("You can't remove last Answer");
        }
    }
}
