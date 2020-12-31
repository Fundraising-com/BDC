using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Data
{
    public partial class CreditCardPayment : IObjectWithChangeTracker, INotifyPropertyChanged
    {
        public string UnEncryptedCC { get; set; }

    }
}
