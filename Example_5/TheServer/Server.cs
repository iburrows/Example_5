using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Example_5.TheServer
{
    public class Server
    {
        Socket serverSocket;
        Socket clientSocket;
        Action<string> GuiUpdater;
        Thread acceptingThread;
        private byte[] buffer = new byte[512];


        public Server(int port, string ip, Action<string> UpdateGuiMessage)
        {
            this.GuiUpdater = UpdateGuiMessage;
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(IPAddress.Parse(ip), port));
            serverSocket.Listen(5);
            clientSocket = serverSocket.Accept();

            //StartAccepting();
            
        }

        private void StartAccepting()
        {
            acceptingThread = new Thread(new ThreadStart(Accept));
        }

        private void Accept()
        {
            clientSocket = serverSocket.Accept();
        }

        private void Receive()
        {
            string message = "";
            int length = clientSocket.Receive(buffer);
            message = Encoding.UTF8.GetString(buffer, 0, length);
        }

        public void Send(string message)
        {
            clientSocket.Send(Encoding.UTF8.GetBytes(message));
        }

        public void NewMessageReceive(string message)
        {
            GuiUpdater(message);
            Send(message + ",received");
        }


    }
}
