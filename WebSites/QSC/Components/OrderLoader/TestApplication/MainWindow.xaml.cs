using System.Windows;
using TestApplication.ViewModel;
using Microsoft.Practices.ServiceLocation;
using BusinessServices;
using Data;
using System.Xml.Linq;
using ActivityLibrary;
using System.Activities;
using System;
using System.Collections.Generic; 

namespace TestApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            BatchNodeParser aBatch = new BatchNodeParser();
            XElement x = new XElement("BATCH", new XElement("ORDERID"),new XElement("CAMPAIGNID"));

           // aBatch.ParseInternetNode(x);

           

            /*
            var f = ServiceLocator.Current.GetInstance<OrderBatchService>();

            f.SetCurrentBatch   (39009, 40002, 86072, 6849);
            f.SetCurrentTeacher("SCARIA", "", "UNKNOWN");
            //this.campaignID.GetValue(
             * */
        }

        private void textBox1_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}