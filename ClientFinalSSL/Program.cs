using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientFinalSSL
{
    class Program
    {
        private const int PORT = 7777;
        static void Main(string[] args)
        {
            Client client = new Client(PORT);
            client.StratClient();

            Console.ReadLine();
        }
    }
}
