using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace WorkstationApp
{
    class Program
    {
        public static TcpClient client;
        private static TcpListener listener;
        private static string ipString;


        static void Main(string[] args)
        {
            //ПОЛУЧАЕМ адресс локального компьютера
            IPAddress[] localIp = Dns.GetHostAddresses(Dns.GetHostName());

            foreach (IPAddress address in localIp)
            {
                if (address.AddressFamily == AddressFamily.InterNetwork) 
                {
                    ipString = address.ToString();
                }
            }

            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ipString), 1234);
            listener = new TcpListener(ep);
            listener.Start();

            Console.WriteLine(@"
            ================================================================
            Start listening requests at: {0}:{1}
            ================================================================",
            ep.Address, ep.Port);
            client = listener.AcceptTcpClient();
            Console.WriteLine("Connected to client!" + " \n");

            try
            {
                const int bytesize = 1024 * 1024;
                byte[] buffer = new byte[bytesize];
                string x = client.GetStream().Read(buffer, 0, bytesize).ToString();

                var data = ASCIIEncoding.ASCII.GetString(buffer);
                if (data.ToUpper().Contains("SLP2")) 
                {
                    Console.WriteLine("PC is going to Sleep Mode!" + "\n");
                    Sleep();
                }
            }
            catch (Exception exc)
            {
                client.Dispose();
                client.Close();
                
            }

            void Sleep()
            {
                Console.WriteLine("Sleep!!");
            }

        }
        
    }
}
