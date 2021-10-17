using DAL1;
using DAL1.Model;
using Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestServer
{
    public partial class Form1 : Form
    {
      
        GenericUnitOfWork work = new GenericUnitOfWork(new MyDbContext("conStr"));
        public Form1()
        {
            InitializeComponent();
           
        }
        private void Login_Click(object sender, EventArgs e)
        {
            IGenericRepository<User> rUser = work.Repository<User>();
            var user = rUser.FindAll(x => x.Login == textBox1.Text && x.Password == maskedTextBox1.Text).First();
            if (user != null || user.isAdmin == true)
            {
                Form2 form2 = new Form2();
                form2.ShowDialog();              
            }
            else
            {
                MessageBox.Show("Login or password is wrong!");
            }
            this.Close();
        }
       

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
