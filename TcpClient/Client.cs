using System.Text;
using System.Net.Sockets;


namespace ClientTcp
{
    internal class Client
    {
        private static TcpClient _client;
        public Client(String server, Int32 port)
        {
            _client = new TcpClient(server, port);
        }     
        public void SendMessage(String message)
        {
            try
            {
                NetworkStream stream = _client.GetStream();

                //Send message//
                Byte[] data = Encoding.UTF8.GetBytes(message);
                stream.Write(data, 0, data.Length);
                Console.WriteLine("Sent: [Client] {0}", message);

                //Message received from the Server//
                data = new Byte[256];
                String responseData = String.Empty;
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.UTF8.GetString(data, 0, bytes);
                Console.WriteLine("Received: [Server] {0}", responseData);

                stream.Close();
                _client.Close();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }

            Console.WriteLine("\n Press Enter to continue...");
            Console.Read();
        }
    }
}
