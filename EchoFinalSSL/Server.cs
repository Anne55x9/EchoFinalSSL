using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace EchoFinalSSL
{
    internal class Server
    {
        private int PORT;

        public Server(int port)
        {
            this.PORT = port;
        }

        public void StartServer()
        {
            TcpListener serverSocket = new TcpListener(IPAddress.Loopback,PORT);
            serverSocket.Start();

            Console.WriteLine("Server er tilsluttet");

            string serverCertificateASW = "C:/CertificatesFinal/ServerSSL.pfx";

            X509Certificate serverCertificate = new X509Certificate2(serverCertificateASW,"mysecret");
            bool leaveInnerStreamOpen = false;

            using (TcpClient connectionSocket = serverSocket.AcceptTcpClient())
            using (Stream unsecureStream = connectionSocket.GetStream())
            using (SslStream secureStream = new SslStream(unsecureStream,leaveInnerStreamOpen))
            {
               secureStream.AuthenticateAsServer(serverCertificate);
                using (StreamReader sr = new StreamReader(secureStream))
                using (StreamWriter sw = new StreamWriter(secureStream))
                {
                    Console.WriteLine("Server er tilgængelig");

                    sw.AutoFlush = true;

                    string message = sr.ReadLine();
                    string answer = "";

                    while (!string.IsNullOrEmpty(message))
                    {
                        Console.WriteLine("Client: " + message);
                        answer = message.ToUpper();
                        sw.WriteLine(answer);

                        message = sr.ReadLine();
                    }
                }
                    
                
            }
                
            
                
            
        }
    }
}
