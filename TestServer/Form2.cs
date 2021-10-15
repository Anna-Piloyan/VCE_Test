using LibraryClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using Repository;
using DAL1.Model;
using System.Data.Entity;
using System.Configuration;
using DAL1;

namespace TestServer
{
    public partial class Form2 : Form
    {
        GenericUnitOfWork work = new GenericUnitOfWork(new MyDbContext(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString));
        IGenericRepository<User> rUsers;
        IGenericRepository<Group> rGroups;
        IGenericRepository<DAL1.Model.Test> rTests;
        IGenericRepository<TestGroup> rTestGroups;
        private bool isCollapsed;
        string str = "";
        public Form2()
        {
            InitializeComponent();
            UnvisibleControls();
            rUsers = work.Repository<User>();
            rGroups = work.Repository<Group>();
            rTests = work.Repository<DAL1.Model.Test>();
            rTestGroups = work.Repository<TestGroup>();
        }

        private void UnvisibleControls()
        {           
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = false;
            checkBox1.Visible = false;
            AddToGroup.Visible = false;
            dataGridView2.Visible = false;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isCollapsed)
            {
                panel3.Height += 10;
                if (panel3.Size == panel3.MaximumSize)
                {
                    timer1.Stop();
                    isCollapsed = false;
                }
            }
            else
            {
                panel3.Height -= 10;
                if (panel3.Size == panel3.MinimumSize)
                {
                    timer1.Stop();
                    isCollapsed = true;
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (isCollapsed)
            {
                panel4.Height += 10;
                if (panel4.Size == panel4.MaximumSize)
                {
                    timer2.Stop();
                    isCollapsed = false;
                }
            }
            else
            {
                panel4.Height -= 10;
                if (panel4.Size == panel4.MinimumSize)
                {
                    timer2.Stop();
                    isCollapsed = true;
                }
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (isCollapsed)
            {
                panel5.Height += 10;
                if (panel5.Size == panel5.MaximumSize)
                {
                    timer3.Stop();
                    isCollapsed = false;
                }
            }
            else
            {
                panel5.Height -= 10;
                if (panel5.Size == panel5.MinimumSize)
                {
                    timer3.Stop();
                    isCollapsed = true;
                }
            }
        }

        private void Groups_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void Users_Click(object sender, EventArgs e)
        {
            timer2.Start();
        }

        private void Tests_Click(object sender, EventArgs e)
        {
            timer3.Start();
        }

        private void Results_Click(object sender, EventArgs e)
        {
            IGenericRepository<Result> rResults = work.Repository<Result>(); ;
            dataGridView1.Size = new Size(585, 320);
            UnvisibleControls();
            dataGridView1.DataSource = null;
            label1.Visible = false;
            textBox1.Visible = false;
            button12.Visible = false;
            dataGridView1.DataSource = rResults.GetAll().Select(x => new { Id = x.Id, TestTitle = x.Tests.Title,
               FirstName = x.User.FirstName, LastName = x.User.LastName, Mark = x.Mark, Date = x.DateUserResult }).ToList();
        }

        private void LoadTest_Click(object sender, EventArgs e)
        {
            button12.Visible = false;
            label1.Visible = false;
            textBox1.Visible = false;
            dataGridView1.Size = new Size(585, 320);
            UnvisibleControls();
            LibraryClass.Test test;
            IGenericRepository<DAL1.Model.Test> rTests = work.Repository<DAL1.Model.Test>();
            string path;
            DialogResult result = openFileDialog1.ShowDialog();
           
            if (result == DialogResult.OK)
            {
                path = openFileDialog1.FileName;
                XmlSerializer formatter = new XmlSerializer(typeof(LibraryClass.Test));
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    test = (LibraryClass.Test)formatter.Deserialize(fs);
                    DAL1.Model.Test tests = new DAL1.Model.Test()
                    {
                        Title = test.TestName,
                        Author = test.Author,
                        QtyOfQuestions = Convert.ToInt32(test.QuestionCount),
                        Difficulty = Convert.ToInt32(test.Difficulty)

                    };
                  //  if (tests.Title.Contains(openFileDialog1.SafeFileName))
                  //  {
                        rTests.Add(tests);
                        work.SaveChanges();
                  //  }
                  //  else
                     //   MessageBox.Show("Test already exists in List");
                };

                int idTest = rTests.GetAllData().Max(t => t.Id);
                IGenericRepository<DAL1.Model.Question> questions;
                IGenericRepository<DAL1.Model.Answer> answers;
                questions = work.Repository<DAL1.Model.Question>();
                answers = work.Repository<DAL1.Model.Answer>();
                foreach (var item in test.Question)
                {
                    DAL1.Model.Question q = new DAL1.Model.Question()
                    {
                        Description = item.Description,
                        TestId = idTest,
                    };
                    questions.Add(q);
                    work.SaveChanges();
                                      
                    int idQuestion = questions.GetAllData().Max(x => x.Id);
                    foreach (var i in item.Answer)
                    {
                        DAL1.Model.Answer answer = new DAL1.Model.Answer()
                        {
                            Description = i.Description,
                            isRight = Convert.ToBoolean(i.IsRight),
                            QuestionId = idQuestion
                        };
                        answers.Add(answer);
                        work.SaveChanges();
                    }
                }
            }
            dataGridView1.DataSource = rTests.GetAll().Select(x => new
            {
                Id = x.Id,
                Title = x.Title,
                Author = x.Author,
                QtyOfQuestions = x.QtyOfQuestions,
                Difficulty = x.Difficulty,
            }).ToList();
        }

        private void ShowAllTests_Click(object sender, EventArgs e)
        {           
            dataGridView1.Size = new Size(585, 320);
            UnvisibleControls();
            button12.Visible = false;
            label1.Visible = false;
            textBox1.Visible = false;
            dataGridView1.DataSource = rTests.GetAll().Select(x => new
            {
                Id = x.Id,
                Author = x.Author,
                Title = x.Title,
                QtyOfQuestions = x.QtyOfQuestions,
                Difficulty = x.Difficulty
            }).ToList();
        }

        private void AsignesTest_Click(object sender, EventArgs e)
        {
            dataGridView1.Size = new Size(585, 320);
            UnvisibleControls();
            dataGridView1.DataSource = null;
            label1.Visible = true;
            textBox1.Visible = true;
            button12.Visible = true; button12.Text = "Asign Test";
            dataGridView1.DataSource = rGroups.GetAll().Select(x => new { Id = x.Id, GroupName = x.GroupName }).ToList();
        }

        private void ShowTestOfGroup_Click(object sender, EventArgs e)
        {           
            dataGridView1.Size = new Size(585, 320);
            UnvisibleControls();
            dataGridView1.DataSource = null;
            label1.Visible = true;
            textBox1.Visible = true;
            button12.Visible = true; button12.Text = "Show Tests";
            dataGridView1.DataSource = rGroups.GetAll().Select(x => new { Id = x.Id, GroupName = x.GroupName }).ToList();
        }

        private void Groups_Show_Click(object sender, EventArgs e)
        {           
            dataGridView1.Size = new Size(585, 320);
            UnvisibleControls();
            dataGridView1.DataSource = null;
            label1.Visible = false;
            textBox1.Visible = false;
            button12.Visible = false;          
            dataGridView1.DataSource = rGroups.GetAll().Select(x => new { Id = x.Id, GroupName = x.GroupName }).ToList();
        }

        private void GroupsAdd_Click(object sender, EventArgs e)
        {         
            dataGridView1.Size = new Size(585, 320);
            UnvisibleControls();
            dataGridView1.DataSource = null;
            label1.Visible = true;
            textBox1.Visible = true;
            button12.Visible = true; button12.Text = "Add Group";
            dataGridView1.DataSource = rGroups.GetAll().Select(x => new { Id = x.Id, GroupName = x.GroupName }).ToList();
        }

        private void GroupsUpdate_Click(object sender, EventArgs e)
        {          
            dataGridView1.Size = new Size(585, 320);
            UnvisibleControls();
            dataGridView1.DataSource = null;
            label1.Visible = true;
            textBox1.Visible = true;
            button12.Visible = true; button12.Text = "Update";
            dataGridView1.DataSource = rGroups.GetAll().Select(x => new { Id = x.Id, GroupName = x.GroupName }).ToList();
        }

        private void GroupsAddUser_Click(object sender, EventArgs e)
        {          
            dataGridView1.Size = new Size(585, 320);
            UnvisibleControls();
            dataGridView1.DataSource = null;
            label1.Visible = true;
            textBox1.Visible = true;
            button12.Visible = true; button12.Text = "Show Users";                                                              
            dataGridView1.DataSource = rGroups.GetAll().Select(x => new { Id = x.Id, GroupName = x.GroupName }).ToList();
        }

        private void GroupsShowUsers_Click(object sender, EventArgs e)
        {          
            dataGridView1.Size = new Size(585, 320);
            UnvisibleControls();
            dataGridView1.DataSource = null;
            label1.Visible = true;
            textBox1.Visible = true;
            button12.Visible = true; button12.Text = "Show";
            dataGridView1.DataSource = rGroups.GetAll().Select(x => new { Id = x.Id, GroupName = x.GroupName }).ToList();
        }

        private void ShowUser_Click(object sender, EventArgs e)
        {
            dataGridView1.Size = new Size(585, 320);
            UnvisibleControls();
            dataGridView1.DataSource = null;
            label1.Visible = false;
            textBox1.Visible = false;
            button12.Visible = false;
            dataGridView1.DataSource = rUsers.GetAll().Select(x => new {
                Id = x.Id,
                Name = x.FirstName,
                Surname = x.LastName,
                IsAdmin = x.isAdmin,
                Login = x.Login,
                Password = x.Password
            }).ToList();
        }

        private void AddUser_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView2.Visible = false;
            dataGridView1.Size = new Size(420,360);
            label1.Visible = false; label2.Visible = true;
            textBox1.Visible = false; label3.Visible = true;
            checkBox1.Visible = true; label4.Visible = true;
            label5.Visible = true; textBox4.Visible = true;
            textBox2.Visible = true; textBox5.Visible = true;
            textBox3.Visible = true;          
            button12.Visible = true; button12.Text = "Add User";
            IGenericRepository<User> rUsers = work.Repository<User>();
            dataGridView1.DataSource = rUsers.GetAll().Select(x => new {
                Id = x.Id,
                Name = x.FirstName,
                Surname = x.LastName,
                IsAdmin = x.isAdmin,
                Login = x.Login,
                Password = x.Password
            }).ToList();
        }

        private void UpdateUser_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Size = new Size(420, 360);
            label1.Visible = false; label2.Visible = true;
            textBox1.Visible = false; label3.Visible = true;
            checkBox1.Visible = true; label4.Visible = true;
            label5.Visible = true; textBox4.Visible = true;
            textBox2.Visible = true; textBox5.Visible = true;
            textBox3.Visible = true;         
            button12.Visible = true; button12.Text = "User Update";
            IGenericRepository<User> rUsers = work.Repository<User>();
            dataGridView1.DataSource = rUsers.GetAll().Select(x => new {
                Id = x.Id,
                Name = x.FirstName,
                Surname = x.LastName,
                IsAdmin = x.isAdmin,
                Login = x.Login,
                Password = x.Password
            }).ToList();
        }
        
        private void AddUpdateGroup_Click(object sender, EventArgs e)
        {
            if (button12.Text == "Add Group")
            {
                IGenericRepository<Group> rGroups = work.Repository<Group>();
                if (textBox1.Text != null)
                {
                    rGroups.Add(new Group() { GroupName = textBox1.Text });
                    work.SaveChanges();
                }
                textBox1.Text = "";
                dataGridView1.DataSource = rGroups.GetAll().Select(x => new { Id = x.Id, GroupName = x.GroupName }).ToList();

            }
            if (button12.Text == "Update")
            {
                if (textBox1.Text != null)
                {
                    int id = (int)dataGridView1.CurrentRow.Cells[0].Value;
                    rGroups.FindById(id).GroupName = textBox1.Text;
                    work.SaveChanges();
                }
                else
                    MessageBox.Show("Choose Group to Update!");             
                dataGridView1.DataSource = rGroups.GetAll().Select(x => new { Id = x.Id, GroupName = x.GroupName }).ToList();
                textBox1.Text = "";
            }
            if (button12.Text == "Show")
            {
                dataGridView1.DataSource = null;
                if (textBox1.Text != null)
                {
                    var group = rGroups.GetAll().Where(g => g.GroupName == textBox1.Text).FirstOrDefault();
                    dataGridView1.DataSource = rUsers.GetAll().Where(x => x.Groups.Contains(group)).ToList();
                }
                else
                    MessageBox.Show("Fill Group Name!");

            }
            if (button12.Text == "Show Users")
            {
                dataGridView1.DataSource = null;
                if (textBox1.Text != null)
                {
                    str = textBox1.Text;
                    var group = rGroups.GetAll().Where(g => g.GroupName == str).FirstOrDefault();
                    dataGridView1.DataSource = rUsers.GetAll().Where(x => x.Groups.Contains(group)).ToList();
                    button12.Visible = false;
                    label1.Visible = false;
                    textBox1.Visible = false;
                    AddToGroup.Visible = true; AddToGroup.Text = "Add User to the Group";
                    dataGridView2.Visible = true;
                    dataGridView1.Size = new Size(585, 180);
                    dataGridView2.Size = new Size(585, 180);
                    dataGridView2.DataSource = rUsers.GetAll().Select(x => new {
                        Id = x.Id,
                        Name = x.FirstName,
                        Surname = x.LastName,
                        IsAdmin = x.isAdmin,
                        Login = x.Login,
                        Password = x.Password
                    }).ToList();
                }
                else
                    MessageBox.Show("Fill Group Name!");              
            }
            if (button12.Text == "Asign Test")
            {
                dataGridView1.DataSource = null;
                if (textBox1.Text != null)
                {
                    str = textBox1.Text;
                    var group = rGroups.GetAll().Where(g => g.GroupName == str).FirstOrDefault();
                    dataGridView1.DataSource = rTestGroups.GetAll().Where(x => x.GroupId == group.Id).Select(x => x.Tests).ToList();
                    button12.Visible = false;
                    label1.Visible = false;
                    textBox1.Visible = false;
                    AddToGroup.Visible = true; AddToGroup.Text = "Add Test to the Group";
                    dataGridView2.Visible = true;
                    dataGridView1.Size = new Size(585, 180);
                    dataGridView2.Size = new Size(585, 180);
                    dataGridView2.DataSource = rTests.GetAll().Select(x => new
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Author = x.Author,
                        QtyOfQuestions = x.QtyOfQuestions,
                        Difficulty = x.Difficulty
                    }).ToList();
                }
                else
                    MessageBox.Show("Fill Group Name!");
            }
            if (button12.Text == "Add User")
            {
                IGenericRepository<User> rUsers = work.Repository<User>();
                string fn = textBox2.Text;
                string ln = textBox3.Text;
                string log = textBox4.Text;
                string pass = textBox5.Text;
                bool isAdm = checkBox1.Checked;
                if (textBox2.Text != null && textBox3.Text != null && textBox4.Text != null && textBox5.Text != null)
                {
                    rUsers.Add(new User()
                    {
                        FirstName = fn,
                        LastName = ln,
                        Login = log,
                        Password = pass,
                        isAdmin = isAdm
                    });
                    work.SaveChanges();
                }
                else
                    MessageBox.Show("Fill all fields");
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                checkBox1.Checked = false;
                ShowUser_Click(sender, e);
            }
            if (button12.Text == "User Update")
            {
                if (textBox2.Text != null && textBox3.Text != null && textBox4.Text != null && textBox5.Text != null)
                {
                    int id = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
                    rUsers.FindById(id).FirstName = textBox2.Text;
                    rUsers.FindById(id).LastName = textBox3.Text;
                    rUsers.FindById(id).Login = textBox4.Text;
                    rUsers.FindById(id).Password = textBox5.Text;
                    rUsers.FindById(id).isAdmin = checkBox1.Checked;
                    work.SaveChanges();
                }
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                checkBox1.Checked = false;
                ShowUser_Click(sender, e);
            }
            if (button12.Text == "Show Tests")
            {
                str = textBox1.Text;
                dataGridView1.DataSource = null;
                label1.Visible = false;
                textBox1.Visible = false;
                button12.Visible = false;
                var group = rGroups.GetAll().Where(g => g.GroupName == str).FirstOrDefault();
                if (textBox1.Text != null)
                {                  
                    dataGridView1.DataSource = rTestGroups.GetAll().Where(x => x.GroupId == group.Id).Select(x => x.Tests).ToList();
                }               
                else
                    MessageBox.Show("Fill Group Name!");
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (button12.Text == "Update" || button12.Text == "Show Users" 
                || button12.Text == "Show" || button12.Text == "Show Tests" || button12.Text == "Asign Test")
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                }
            }
            if (button12.Text == "User Update")
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                    textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                    textBox4.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString(); ;
                    textBox5.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                    checkBox1.Checked = Convert.ToBoolean(dataGridView1.SelectedRows[0].Cells[3].Value);
                }
            }
        }

        private void AddToGroup_Click(object sender, EventArgs e)
        {       
            if (AddToGroup.Text == "Add User to the Group")
            {               
                int id = (int)dataGridView2.SelectedRows[0].Cells[0].Value;
                var g = rGroups.GetAll().Where(h => h.GroupName == str).FirstOrDefault();
                try
                {
                    if (!g.Users.Contains(rUsers.FirstOrDefault(u => u.Id == id)))
                    {                     
                        g.Users.Add(rUsers.FirstOrDefault(u => u.Id == id));
                        work.SaveChanges();
                    }
                    else
                        MessageBox.Show("User already exists!");
                }
                catch { MessageBox.Show("Select user to add"); };

                dataGridView1.DataSource = g.Users.Where(x => x.Groups.Contains(g)).ToList();
            }
            if (AddToGroup.Text == "Add Test to the Group")
            {
                int id = (int)dataGridView2.SelectedRows[0].Cells[0].Value;
                var g = rGroups.FirstOrDefault(h => h.GroupName == str);    // добавляет ко всем тестам
                TestGroup rrr = new TestGroup() {
                    DateTestGroup = DateTime.Now,
                    Groups = g,
                    Tests = rTests.FindById(id)
                };               
                rTestGroups.Add(rrr);
                work.SaveChanges();
                dataGridView1.DataSource = rTestGroups.GetAll().Where(x => x.GroupId == g.Id).Select(x => x.Tests).ToList();
            }
        }
    }    
}
