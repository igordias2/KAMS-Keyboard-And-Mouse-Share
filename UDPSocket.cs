using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using GregsStack.InputSimulatorStandard;
using GregsStack.InputSimulatorStandard.Native;


namespace KAMS_Keyboard_And_Mouse_Share
{

    public class UDPSocket
    {
        private Socket _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        private const int bufSize = 8 * 1024;
        private State state = new State();
        private EndPoint epFrom = new IPEndPoint(IPAddress.Any, 0);
        private AsyncCallback recv = null;

        public class State
        {
            public byte[] buffer = new byte[bufSize];
        }

        public void Server(string address, int port)
        {
            _socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.ReuseAddress, true);
            _socket.Bind(new IPEndPoint(IPAddress.Parse(address), port));
            Receive();            
        }

        public void Client(string address, int port)
        {
            _socket.Connect(IPAddress.Parse(address), port);
            Receive();            
        }

        public void Send(string text)
        {

            byte[] data = Encoding.ASCII.GetBytes(text);
            _socket.BeginSend(data, 0, data.Length, SocketFlags.None, (ar) =>
            {
                State so = (State)ar.AsyncState;
                int bytes = _socket.EndSend(ar);
                Console.WriteLine("SEND: {0}, {1}", bytes, text);
            }, state);
        }
        public void Send(byte c)
        {
            byte[] data = new byte[c];
            _socket.BeginSend(data, 0, data.Length, SocketFlags.None, (ar) =>
            {
                State so = (State)ar.AsyncState;
                int bytes = _socket.EndSend(ar);
                Console.WriteLine("SEND: {0}, {1}", bytes, data);
            }, state);
        }

        public void Receive()
        {            
            _socket.BeginReceiveFrom(state.buffer, 0, bufSize, SocketFlags.None, ref epFrom, recv = (ar) =>
            {
                State so = (State)ar.AsyncState;
                int bytes = _socket.EndReceiveFrom(ar, ref epFrom);
                _socket.BeginReceiveFrom(so.buffer, 0, bufSize, SocketFlags.None, ref epFrom, recv, so);
                Keyboard.ScanCodeShort k = (Keyboard.ScanCodeShort)(int)so.buffer[0];//(int)so.buffer[0];
                Keyboard.INPUT[] Inputs = new Keyboard.INPUT[1];
                Keyboard.INPUT Input = new Keyboard.INPUT();
                Input.type = 1; // 1 = Keyboard Input
                Input.U.ki.wScan = k;
                Input.U.ki.dwFlags = Keyboard.KEYEVENTF.SCANCODE;
                Inputs[0] = Input;
                Keyboard.SendInput(1, Inputs, Keyboard.INPUT.Size);
                // Keyboard.Send(k);
                // VirtualKeyCode key =  (VirtualKeyCode) (int)so.buffer[0];
                // InputSimulator input = new InputSimulator();
                // input.Keyboard.KeyPress(key);
                


                Console.WriteLine("RECV: {0}: {1}, {2}", epFrom.ToString(), bytes, Encoding.ASCII.GetString(so.buffer, 0, bytes));
            }, state);
           // Keyboard.ScanCodeShort k = (int)0;
            //Keyboard.Send(k);
        }
    }
}