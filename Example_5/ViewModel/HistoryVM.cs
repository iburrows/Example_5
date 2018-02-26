using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example_5.ViewModel
{
    public class HistoryVM:ViewModelBase
    {
        public HistoryVM(string date, string data, string sentReceived)
        {
            Date = date;
            Data = data;
            SentReceived = sentReceived;
        }

        public string Date { get; set; }
        public string Data { get; set; }
        public string SentReceived { get; set; }
    }
}
