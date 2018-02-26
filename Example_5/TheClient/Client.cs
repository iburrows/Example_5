using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Example_5.TheClient
{
    public class Client
    {
        Thread receivingThread;
        Socket clientSocket;
        byte[] buffer = new byte[512];
        Action<string> guiUpdater;
        Socket hostSocket;
        

        public Client(int port, string ip, Action<string> UpdateGuiMessage)
        {
            TcpClient client = new TcpClient();
            client.Connect(IPAddress.Parse(ip), port);
            clientSocket = client.Client;
            
            this.guiUpdater = UpdateGuiMessage;

            StartReceiving();
        }

        public void StartReceiving()
        {
            receivingThread = new Thread(new ThreadStart(Receive));
            receivingThread.IsBackground = true;
            receivingThread.Start();
        }

        private void Receive()
        {
            string message = "";
            while (receivingThread.IsAlive)
            {
                int length = clientSocket.Receive(buffer);
                message = Encoding.UTF8.GetString(buffer, 0, length);
                guiUpdater(message);
            }
        }

        //addition
        public void Send(string message)
        {
            clientSocket.Send(Encoding.UTF8.GetBytes(message));
        }

        public void NewMessageReceive(string message)
        {
            guiUpdater(message);
            Send(message + ",sent");
        }



    }
}
