using System;

using System.Text;
using System.Net;
using System.Net.Sockets;

using System.Threading;
using System.Threading.Tasks;

// using System.Numerics


namespace KAMS_Keyboard_And_Mouse_Share
{
    class KAMS
    {
        static void Main(string[] args)
        {
            UDPSocket server = new UDPSocket();
            server.Server("127.0.0.1", 2727);

            UDPSocket client = new UDPSocket();
            client.Client("127.0.0.1", 2727);

            //while(true)
                client.Send("test");

        }
    }
}
