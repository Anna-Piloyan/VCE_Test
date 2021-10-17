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
        //IGenericRepository<DAL1.Model.Test> rTests;
        //IGenericRepository<TestGroup> rTestGroups;
        public ClientObject(TcpClient tcpClient, IGenericRepository<User> rUsers, IGenericRepository<Group> rGroups) //, IGenericRepository<User> rUsers)
        {
            client = tcpClient;
            this.rUsers = rUsers;
            this.rGroups = rGroups;
            //rTests = work.Repository<DAL1.Model.Test>();
            //rTestGroups = work.Repository<TestGroup>();
        }
       
        public void Process()
        {
            NetworkStream stream = null;
            try
            {
                stream = client.GetStream();
                byte[] data = new byte[1024]; // буфер для получаемых данных
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
                    //  System.Windows.Forms.MessageBox.Show($"Server receive: {message}");


                    // my code
                  //  string msg = "";
                    string[] log_pass = message.Split(' ');
                    string l = log_pass[0];
                    string p = log_pass[1];
                    User user = rUsers.FirstOrDefault(x => x.Login == l && x.Password == p);
                    var groups = rGroups.GetAll().Where(u => u.Users.Contains(user)).Select(g => g.GroupName);
                   // foreach (var item in groups)
                      //  string.Join(",", groups);
                    string m = String.Format("{0} {1},{2}", user.FirstName, user.LastName, string.Join(",", groups));
                  //  System.Windows.Forms.MessageBox.Show($"Server send: {m}");

                    data = Encoding.Unicode.GetBytes(m);
                    stream.Write(data, 0, data.Length);
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
