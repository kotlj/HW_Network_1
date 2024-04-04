using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HW_TcpServ_1
{
    internal class TCPServer
    {
        const int port = 11111;
        static string[] CompAnsw = { "Hello!", "How are you?", "Nice!", "Bye" };
        static void Main(string[] args)
        {
            var locIp = IPAddress.Any;
            
            var server = new TcpListener(locIp, port);
            server.Start();

            TcpClient client = server.AcceptTcpClient();
            Console.WriteLine("Connected");

            NetworkStream stream = client.GetStream();
            StreamReader sr = new StreamReader(stream, Encoding.UTF8);
            StreamWriter wr = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };

            try
            {
                string mesg = "";
                mesg = sr.ReadLine();
                if (mesg == "2")
                {
                    Random rand = new Random();
                    while ((mesg = sr.ReadLine()) != "bye" && mesg.ToLower() != "bye")
                    {
                        int r = rand.Next(4);
                        Console.WriteLine($"get: {mesg}");
                        wr.WriteLine($"{CompAnsw[r]}");
                    }
                    wr.WriteLine("bye");
                }
                else if (mesg == "1")
                {
                    string answ = "";
                    while ((mesg = sr.ReadLine()) != "bye" && mesg.ToLower() != "bye" && answ.ToLower() != "bye")
                    {
                        Console.WriteLine($"get: {mesg}");
                        Console.WriteLine("Enter your answer:\n");
                        answ = Console.ReadLine();
                        wr.WriteLine($"{answ}");
                    }
                    if (mesg == "bye")
                    {
                        wr.WriteLine("bye");
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                stream.Close();
                client.Close();
            }
            server.Stop();
        }
    }
}
