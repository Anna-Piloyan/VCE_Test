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

namespace TestServer
{
    public class ClientObject
    {
        public TcpClient client;
        IGenericRepository<User> rUsers;
        IGenericRepository<Group> rGroups;
        IGenericRepository<DAL1.Model.Test> rTests;
        IGenericRepository<TestGroup> rTestGroups;
        public ClientObject(TcpClient tcpClient, IGenericRepository<User> rUsers, IGenericRepository<Group> rGroups,
            IGenericRepository<DAL1.Model.Test> rTests, IGenericRepository<TestGroup> rTestGroups) //, IGenericRepository<User> rUsers)
        {
            client = tcpClient;
            this.rUsers = rUsers;
            this.rGroups = rGroups;
            this.rTests = rTests;
            this.rTestGroups = rTestGroups;
        }
       
        public void Process()
        {
           
            NetworkStream stream = null;
            try
            {
                stream = client.GetStream();
                byte[] data = new byte[2048]; // буфер для получаемых данных
                while (true)
                {
                    MessageBox.Show("Подключен клиент. Выполнение запроса...");
                    // получаем сообщение
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
                          
                           foreach(var i in item)
                                dt.Rows.Add(i.Id, i.Author, i.Title, i.QtyOfQuestions);
                            }
                        }
                        catch(ArgumentNullException)
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
                      
                    }
                    else if (log_pass[0] == "PassTest")
                    {
                        string testToPass = log_pass[1];
                        // LibraryClass.Test test;
                       
                        MessageBox.Show($"I'm in pass test button { testToPass}!");
                        try
                        {
                            var testF = rTests.FirstOrDefault(g => g.Title == testToPass);
                            MessageBox.Show("Trying to send xmlFile!");
                            if (testF == null)
                                throw new ArgumentNullException();
                            else
                            {
                                string path = @"..\..\bin\Debug\TestFolder\";
                                FileStream fs = new FileStream(path + testToPass + ".xml", FileMode.Open);
                                BinaryFormatter bFormat = new BinaryFormatter();
                                byte[] buffer = null;
                                using (MemoryStream memory = new MemoryStream())
                                {
                                    bFormat.Serialize(memory, fs);
                                    buffer = memory.ToArray();
                                }
                                stream.Write(buffer, 0, buffer.Length);
                                MessageBox.Show("Server: I send XML!");
                            }
                        }
                        catch (ArgumentNullException)
                        {
                            MessageBox.Show("This test is not exists!");

                        }
                       
                       
                     
                      
                       
                    }
                    else
                    {
                        //  System.Windows.Forms.MessageBox.Show($"Server receive: {message}");
                        string messageOut = LoginDataConnect(message);
                        // System.Windows.Forms.MessageBox.Show($"Server send: {messageOut}");
                        data = Encoding.Unicode.GetBytes(messageOut);
                        stream.Write(data, 0, data.Length);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (stream != null)
                    stream.Close();
                if (client != null)
                    client.Close();
               
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

        //private byte[] GetByteDataSet(DataSet data)
        //{
        //    //создали буфер для переконвертированного DataSet
        //    byte[] data_rez = null;
        //    //создали поток, используемый для сериализации в качестве временного буфера
        //    MemoryStream mem_streem = new MemoryStream();
        //    //объект, выполняющий сериализацию
        //    BinaryFormatter bin_format = new BinaryFormatter();

        //    //собственно сама сериализация из DataSet в MemoryStreem
        //    bin_format.Serialize(mem_streem, data);
        //    //перевели в массив byte[]
        //    data_rez = mem_streem.ToArray();

        //    //обрубили поток
        //    mem_streem.Close();
        //    mem_streem.Dispose();

        //    return data_rez;
        //}
    }
}
