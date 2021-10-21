using DAL1;
using DAL1.Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using LibraryClass;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Data.Entity;
using System.Net;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;

namespace TestServer
{
    public class ClientObject
    {
        GenericUnitOfWork work = new GenericUnitOfWork(new MyDbContext("conStr"));
        TcpClient client;
        TcpListener listener;
        IGenericRepository<User> rUsers;
        IGenericRepository<Group> rGroups;
        IGenericRepository<DAL1.Model.Test> rTests;
        IGenericRepository<TestGroup> rTestGroups;
        IGenericRepository<Result> rResults;
        public ClientObject(TcpClient tcpClient, TcpListener tcpListener, IGenericRepository<User> rUsers, IGenericRepository<Group> rGroups,
            IGenericRepository<DAL1.Model.Test> rTests, IGenericRepository<TestGroup> rTestGroups, IGenericRepository<Result> rResults) //, IGenericRepository<User> rUsers)
        {
            client = tcpClient;
            listener = tcpListener;
            this.rUsers = rUsers;
            this.rGroups = rGroups;
            this.rTests = rTests;
            this.rTestGroups = rTestGroups;
            this.rResults = rResults;
        }
       
        public void Process()
        {
            NetworkStream stream = null;
            try
            {
                stream = client.GetStream();
                byte[] data = new byte[2048];
                while (true)
                {
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);

                    string message = builder.ToString();
                    string[] log_pass = message.Split(' ');

                    if (log_pass[0] == "LoadTest")
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("Id", typeof(Int32));
                        dt.Columns.Add("Author", typeof(string));
                        dt.Columns.Add("TestName", typeof(string));
                        dt.Columns.Add("QuestionCount", typeof(Int32));
                        string nameG = log_pass[1];
                        int grId = rGroups.FirstOrDefault(g => g.GroupName == nameG).Id;
                        try
                        {
                            var testGroupData = rTestGroups.GetAll().Where(g => g.GroupId == grId);
                            if (testGroupData == null)
                                throw new ArgumentNullException();
                            foreach (var it in testGroupData)
                            {
                                var item = rTests.GetAll().Where(t => t.Id == it.TestId);

                                foreach (var i in item)
                                    dt.Rows.Add(i.Id, i.Author, i.Title, i.QtyOfQuestions);
                            }                            
                        }
                        catch (ArgumentNullException)
                        {
                            MessageBox.Show("No tests is added to this group");
                        }
                        DataSet ds = new DataSet();
                        ds.Tables.Add(dt);
                        BinaryFormatter bFormat = new BinaryFormatter();
                        byte[] buffer = null;                      
                        using (MemoryStream memory = new MemoryStream())
                        {
                            bFormat.Serialize(memory, ds);
                            buffer = memory.ToArray();
                        }                    
                        stream.Write(buffer, 0, buffer.Length);
                        return;                    
                    }
                    if (log_pass[0] == "PassTest")
                    {
                        string testToPass = log_pass[1];                      
                        try
                        {
                            var testF = rTests.FirstOrDefault(g => g.Title == testToPass);
                            if (testF == null)
                                throw new ArgumentNullException();
                            else
                            {
                               // string path = @"..\..\bin\Debug\TestFolder\";
                                string path = @"D:\MyProject_TestCreate\TesterDesign\bin\Debug\TestFolder\" + testToPass + ".xml";                             
                                byte[] buffer = Encoding.UTF8.GetBytes(File.ReadAllText(path));
                                stream.Write(buffer, 0, buffer.Length);                            
                            }
                         }
                        catch (ArgumentNullException)
                        {
                            MessageBox.Show("This test is not exists!");
                        }
                        return;
                    }
                    if (log_pass[0] == "LoadResult")
                    {
                        int result = Convert.ToInt32(log_pass[1]);
                        string n = log_pass[2];
                        string s = log_pass[3];
                        int tId = Convert.ToInt32(log_pass[4]);
                        var userId = rUsers.FirstOrDefault(x => x.FirstName == n && x.LastName == s).Id;
                        Result res = new Result() { DateUserResult = DateTime.Now, Mark = result, UserId = userId, TestId = tId };
                        if(res != null)
                        rResults.Add(res);
                       // MessageBox.Show("Result Loaded");
                    }
                    else
                    {
                        string messageOut = LoginDataConnect(message);
                        data = Encoding.Unicode.GetBytes(messageOut);
                        stream.Write(data, 0, data.Length);
                    }
                }
            }
            catch (SocketException ex)
            {
                MessageBox.Show("SocketException ClientObject: {0}", ex.Message);               
            }
            finally
            {
               // Stop listening for new clients.
               // client.Close();
               // listener.Stop();
            }
        }
        private string LoginDataConnect(string msg)
        {
            string[] log_pass = msg.Split(' ');
            string l = log_pass[0];
            string p = log_pass[1];
            User user = rUsers.FirstOrDefault(x => x.Login == l && x.Password == p);
            var groups = rGroups.GetAll().Where(u => u.Users.Contains(user)).Select(g => g.GroupName);
            string m = String.Format("{0} {1},{2}", user.FirstName, user.LastName, string.Join(",", groups));
            return m;
        }
    }
}
