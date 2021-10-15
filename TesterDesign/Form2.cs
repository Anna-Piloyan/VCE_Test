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
    public partial class Form2 : Form
    {
        Test test;
        Question question;
        string path = @"TestFolder\";
        public Form2()
        {
            InitializeComponent();
            test = new Test();
        }

        private void AddQuestion_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != "")
            {
                question = new Question();
                question.Description = textBox4.Text;
                listBox1.Items.Add(question);
                textBox4.Text = "";
            }
            else
                MessageBox.Show("Fill question!");
        }

        private void AddAnswer_Click(object sender, EventArgs e)
        {
            if (textBox5.Text != "")
            {
                Answer answer = new Answer();
                answer.Description = textBox5.Text;
                answer.IsRight = checkBox1.Checked.ToString();
                listBox1.Items.Add(answer);
                textBox5.Text = "";
                question.Answer.Add(answer);
                checkBox1.Checked = false;
            }
            else
                MessageBox.Show("Fill answer!");
        }

        private void Next_Click(object sender, EventArgs e)
        {          
            test.Question.Add(question);
            MessageBox.Show($"Question was added");
        }

        private void SaveTest_Click(object sender, EventArgs e)
        {
            test.Author = textBox1.Text;
            test.TestName = textBox2.Text;
            test.QuestionCount = textBox3.Text;
            test.Difficulty = numericUpDown1.Value.ToString();
            XmlSerializer formatter = new XmlSerializer(typeof(Test));

            if (textBox6.Text != "")
            {
                using (FileStream fs = new FileStream(path + textBox6.Text, FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, test);
                    MessageBox.Show("Saved");
                }
            }
            else
                MessageBox.Show("Fill filename to save test!");
            listBox1.Items.Clear();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            numericUpDown1.Value = 0;
            textBox6.Text = "";
        }
    }
}
