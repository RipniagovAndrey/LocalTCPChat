using System.Net;
using System.Net.Sockets;

namespace TcpServer
{
    internal static class ListenerTcp
    {
        private static IPAddress _ipAddress;
        private static TcpListener _server;

        public static void StartListen(string ipAddress, Int32 port)
        {
            try
            {
                _ipAddress = IPAddress.Parse(ipAddress);

                _server = new TcpListener(_ipAddress, port);
                _server.Start();

                Byte[] bytes = new Byte[256];
                String data = null;

                while (true)
                {
                    Console.Write("Waiting for a connection... ");

                    //Connected client//
                    TcpClient client = _server.AcceptTcpClient();
                    NetworkStream stream = client.GetStream();

                    Console.WriteLine("\n[Client] Connected!");

                    int i;
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        //Message received from Client//
                        data = System.Text.Encoding.UTF8.GetString(bytes, 0, i);
                        Console.WriteLine("Received: [Client] {0}", data);

                        //Send back message//
                        data = data.ToUpper();
                        byte[] msg = System.Text.Encoding.UTF8.GetBytes(data);
                        stream.Write(msg, 0, msg.Length);
                        Console.WriteLine("Sent: [Server] {0}", data);
                    }

                    client.Close();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                _server.Stop();
            }

            Console.WriteLine("\nHit enter to continue...");
            Console.Read();
        }
        }
    }


