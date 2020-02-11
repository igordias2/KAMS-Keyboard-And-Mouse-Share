using System;

using System.Text;
using System.Net;
using System.Net.Sockets;

using System.Threading;
using System.Threading.Tasks;

// using System.Numerics


namespace KAMS_Keyboard_And_Mouse_Share
{
    class Program{
        static void Main(string[] args){
            
            if(args.Length > 0){
            

                UdpClient udpClient = new UdpClient(8080);
                while(true)
                {
                    IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
                    Byte[] receiveBytes = udpClient.Receive(ref RemoteIpEndPoint);
                    string returnData = Encoding.ASCII.GetString(receiveBytes);
                    Console.WriteLine(returnData);
                    // lbConnections.Items.Add(RemoteIpEndPoint.Address.ToString() 
                    //                         + ":" +  returnData.ToString());
                }
            }
            else
            {
                string rt = Console.ReadLine();
                UdpClient udpClient = new UdpClient();
                udpClient.Connect(IPAddress.Any, 8080);
                Byte[] senddata = Encoding.ASCII.GetBytes(rt);
                udpClient.Send(senddata, senddata.Length);
            }        
        
        
        }
    
    
    
    }




}
