using Example_5.TheClient;
using Example_5.TheServer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;

namespace Example_5.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        //Create a STATIC random variable and initialize it outside of the method so as not to get the same number. Static means it is associated with the type and not an instance.
        private static Random random = new Random();
        
        private const string ip = "127.0.0.1";
        private const int port = 10100;

        public string Date { get; set; }
        public string Data { get; set; }
        public string SentReceived { get; set; }

        private bool isConnected = false;

        public ObservableCollection<ToggleVM> ToggleButtonCollection { get; set; }

        public ObservableCollection<HistoryVM> HistoryCollection { get; set; }

        public RelayCommand<ToggleVM> ToggleButton { get; set; }

        public RelayCommand ListenBtnClicked { get; set; }
        public RelayCommand ConnectBtnClicked { get; set; }

        public RelayCommand ClearBtnClicked { get; set; }

        Server server;
        Client client;

        private bool isClient = false;
        private bool isServer = false;

        public MainViewModel()
        {
            ToggleButtonCollection = new ObservableCollection<ToggleVM>();
            HistoryCollection = new ObservableCollection<HistoryVM>();

            GenerateDemoData(5);

            ToggleButton = new RelayCommand<ToggleVM>((p)=> 
            {
                //int randomNumber = p.Value;
                DateTime TimeBtnClicked = DateTime.Now;

                if (isServer)
                {
                    server.NewMessageReceive(p.ButtonId + "," + p.Value + "," + TimeBtnClicked);
                }


                if (isClient)
                {
                    client.NewMessageReceive(p.ButtonId + "," + p.Value + "," + TimeBtnClicked);
                }
                //if (p.Value == 0)
                //{
                //    p.Value = 1;
                //}
                //else if (p.Value == 1)
                //{
                //    p.Value = 0;
                //}
            });

            ListenBtnClicked = new RelayCommand(()=> 
            {
                server = new Server(port, ip, NewMessageReceived);
                isConnected = true;
                isServer = true;
            },
            ()=> { return !isConnected; });
            ConnectBtnClicked = new RelayCommand(()=> 
            {
                client = new Client(port, ip, NewMessageReceived);
                isConnected = true;
                isClient = true;
            },
            ()=> { return !isConnected; });

            ClearBtnClicked = new RelayCommand(() => { HistoryCollection.Clear(); });
        }

        private void GenerateDemoData(int NumberOfButtons)
        {

            ToggleButtonCollection.Add(new ToggleVM(1 , "Button_" + 1));
            ToggleButtonCollection.Add(new ToggleVM(0 , "Button_" + 2));
            ToggleButtonCollection.Add(new ToggleVM(0 , "Button_" + 3));
            ToggleButtonCollection.Add(new ToggleVM(1, "Button_" + 4));
            ToggleButtonCollection.Add(new ToggleVM(1, "Button_" + 5));

            //for (int i = 0; i < NumberOfButtons; i++)
            //{
            //    ToggleButtonCollection.Add(new ToggleVM(RandomNumberGenerator(), "Button_" + i));
            //}

        }

        public int RandomNumberGenerator()
        {
            //random = new Random();
            int randomNumber = random.Next(0, 2);
            return randomNumber;
        }

        public void NewMessageReceived(string message)
        {
            string[] receivedMessage = message.Split(',');
            App.Current.Dispatcher.Invoke(() => 
            {
                //ToggleButtonCollection.Add(new ToggleVM(Int32.Parse(receivedMessage[1]), receivedMessage[0]));

                string color;

                if (receivedMessage[1] == "1")
                {
                    color = "Green";
                }
                else
                {
                    color = "Orange";
                }

                string sentReceived;

                if (message.Contains("received"))
                {
                    sentReceived = "received";
                }
                else
                {
                    sentReceived = "sent";
                }

                HistoryCollection.Add(new HistoryVM(receivedMessage[2], receivedMessage[0] + ":" + color, sentReceived));

                foreach (var item in ToggleButtonCollection)
                {
                    if (item.ButtonId == receivedMessage[0])
                    {
                        if (item.Value == 0)
                        {
                            item.Value = 1;
                        }
                        else if (item.Value == 1)
                        {
                            item.Value = 0;
                        }
                    }
                }
            });
            
        }
    }
}