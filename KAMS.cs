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

            if(args.Length == 0)
            {
                UDPSocket server = new UDPSocket();
                server.Server("127.0.0.1", 2727);

            }   
            else
            {
                UDPSocket client = new UDPSocket();
                client.Client("127.0.0.1", 2727);
                
                while(true)
                {
                    client.Send((byte)Console.ReadKey().Key);
                }
            }


            // while(true){
            //     var kC = Console.ReadKey();
            //     byte key = (byte)((int)kC.Key);
            //     string key = (kC.Key+ " " + kC.KeyChar + " " + kC.Modifiers).ToString();
            //     Console.WriteLine(key);
            //     client.Send(Console.ReadKey().KeyChar);
            // }
            //client.Send("test");

        }
    }
}
