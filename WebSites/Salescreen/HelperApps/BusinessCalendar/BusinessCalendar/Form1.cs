using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BusinessCalendar
{
    public partial class Form1 : Form
    {
        #region Constants

        const int CREATE_USER_ID = 100689;    // Juan Martinez
        const string CONNECTION_STRING = "data source=qspdevdb1.qsp;initial catalog=QSPFulfillment;user id=QSPFormWebUser;password=QSPFormWebUser;Connect Timeout=240;Application Name=OrderExpress;";

        // DEV
        // const string CONNECTION_STRING = "data source=qspdevdb1.qsp;initial catalog=QSPFulfillment;user id=QSPFormWebUser;password=QSPFormWebUser;Connect Timeout=240;Application Name=OrderExpress;";

        // TEST

        // STAGING

        // PROD
        //const string CONNECTION_STRING = "data source=qspproddb1.qsp;initial catalog=QSPFulfillment;user id=QSPFormWebUser;password=somewhereakinghasnowife;Connect Timeout=240;Application Name=OEDataGenerator;";

        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.button1.Enabled = false;

            #region Declare variables

            int yearToGenerate = 2010;
            DateTime endDate = new DateTime(yearToGenerate, 12, 31);
            List<DateTime> holidays = new List<DateTime>(); 
            
            #endregion

            #region Set defaults

            yearToGenerate = 2010;

            holidays.Add(new DateTime(yearToGenerate, 1, 1));
            holidays.Add(new DateTime(yearToGenerate, 6, 4));
            holidays.Add(new DateTime(yearToGenerate, 12, 24));

            #endregion

            StringBuilder sb = new StringBuilder();
            for (DateTime date = new DateTime(yearToGenerate, 1, 1); date <= endDate; date = date.AddDays(1))
            {
                bool isHoliday = false;
                bool isWeekend = false;

                if (holidays.Contains(date))
                {
                    isHoliday = true;
                }

                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                {
                    isWeekend = true;
                }

                //this.textBox1.Text += String.Format("{0} - {1} - {2}{3}", date.ToLongDateString(), isHoliday ? "holiday" : "", isWeekend ? "weekend" : "", System.Environment.NewLine);
                sb.AppendFormat("INSERT INTO business_calendar VALUES ('{0}-{1}-{2}', {3}, {4});{5}", date.Year.ToString(), date.Month.ToString(), date.Day.ToString(), isWeekend ? "1" : "0", isHoliday ? "1" : "0", System.Environment.NewLine);
            }

            this.textBox1.Text = sb.ToString();

            this.button1.Enabled = true;
        }
    }
}
