using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HW_NetworkPg_1
{
    internal class Program
    {
        static string[] CompAnsw = { "Hello!", "How are you?", "Nice!", "Bye" };
        static void Main(string[] args)
        {
            Console.WriteLine("Enter server IP:\n");
            string servIp = Console.ReadLine();
            Console.WriteLine("Enter port number:\n");
            int port = int.Parse(Console.ReadLine());

            try
            {
                using (var client = new TcpClient(servIp, port))
                {
                    Console.WriteLine("Connected");
                    using (NetworkStream stream = client.GetStream())
                    {
                        var reader = new StreamReader(stream, Encoding.UTF8);
                        var writer = new StreamWriter(stream, Encoding.UTF8) {AutoFlush = true};

                        Console.WriteLine("Choose mode(client - server):\n1 - human - human\n2 - human - computer\n3 - computer - computer\n");
                        string choise = Console.ReadLine();
                        string resp = "";
                        if (choise == "1")
                        {
                            writer.WriteLine("1");
                            Console.WriteLine("Type message(bye for exit):\n");
                            string userMsg = "";
                            while ((userMsg = Console.ReadLine()) != null && userMsg.ToLower() != "bye" && resp.ToLower() != "bye")
                            {
                                writer.WriteLine(userMsg);
                                resp = reader.ReadLine();
                                Console.WriteLine($"get: {resp}");
                            }
                        }
                        else if (choise == "2")
                        {
                            writer.WriteLine("2");
                            Console.WriteLine("Type message(bye for exit):\n");
                            string userMsg = "";
                            while ((userMsg = Console.ReadLine()) != null && userMsg.ToLower() != "bye" && resp.ToLower() != "bye")
                            {
                                writer.WriteLine(userMsg);
                                resp = reader.ReadLine();
                                Console.WriteLine($"get: {resp}");
                            }
                        }
                        else if (choise == "3")
                        {
                            writer.WriteLine("2");
                            string userMsg = "";
                            Random rand = new Random();
                            int r = rand.Next(4);
                            while ((userMsg = CompAnsw[r]) != null && userMsg.ToLower() != "bye" && resp.ToLower() != "bye")
                            {
                                writer.WriteLine(userMsg);
                                resp = reader.ReadLine();
                                Console.WriteLine($"get: {resp}");
                                r = rand.Next(4);
                            }
                        }
                        writer.WriteLine("bye");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.WriteLine("Disconected");
        }
    }
}
