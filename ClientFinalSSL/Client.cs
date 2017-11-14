using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientFinalSSL
{
    internal class Client
    {

        private int PORT;

        public Client(int port)
        {
            this.PORT = port;
        }

        public void StratClient()
        {
            bool leaveInnerStreamOpen = false;

            using (TcpClient connectionSocket = new TcpClient(IPAddress.Loopback.ToString(),PORT))

            using (Stream unsecureStream = connectionSocket.GetStream())
            using (SslStream secureStream = new SslStream(unsecureStream,leaveInnerStreamOpen))
            {
                secureStream.AuthenticateAsClient("FakeServerNameFinal");

                using (StreamReader sr = new StreamReader(secureStream))
                using (StreamWriter sw = new StreamWriter(secureStream))
                {
                    Console.WriteLine("Clienten er tilsluttet");
                    sw.AutoFlush = true;

                    Client3(sr, sw);

                    Console.WriteLine("Clientens forespørsel er klar.");

                }
                    
                
            }
                
            
        }

        private void Client3(StreamReader sr, StreamWriter sw)
        {
            for (int i = 0; i < 100; i++)
            {
                string message = "All work and no play makes Jack a dullboy." + i;
                sw.WriteLine(message);
                string serverAnswer = sr.ReadLine();
                Console.WriteLine("Server: " + serverAnswer);
            }
        }

    }
}
