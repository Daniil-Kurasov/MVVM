using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace MVVM
{
    [DataContract]
    internal class Phone : INotifyPropertyChanged, ICloneable
    {
        private string title, company;
        private int price;

        [DataMember]
        public string Title
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }
        [DataMember]
        public string Company
        {
            get => company;
            set
            {
                company = value;
                OnPropertyChanged("Company");
            }
        }
        [DataMember]
        public int Price
        {
            get => price;
            set
            {
                price = value;
                OnPropertyChanged("Price");
            }
        }

        public Phone(string title = "title", string company = "company", int price = 0)
        {
            Title = title;
            Company = company;
            Price = price;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public object Clone() => MemberwiseClone();
    }
}
