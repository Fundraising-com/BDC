using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;



namespace GA.BDC.Integration
{
   public class Helper
   {
      public static char SAP_TRUE
      {
         get { return 'X'; }
      }

      public static char SAP_FALSE
      {
         get
         {
            return ' ';
         }
      }

      public static string SAP_BLANK
      {
         get
         {
            return '/'.ToString();
         }
      }

      public static char[] SAP_DATE_BLANK
      {
         get { return new char[8] { '0', '0', '0', '0', '0', '0', '0', '0' }; }
      }


      public static string BULK_ORDER
      {
         get { return "L1".ToString(); }
      }

      public static string AccountShipping(string accountShipping)
      {
         return accountShipping;
      }

      /*public static string AccountBilling(string accountBilling)
      {
          return accountBilling;
      }*/

      public static char SalesScreen
      {
         get { return 'S'; }
      }

      public static char OnlineOrder
      {
         get { return 'O'; }
      }


      public static bool IsAccountBilling(string type, string accountBilling)
      {
         return type == accountBilling;

      }

      public static string IsTaxExluded(bool? input)
      {
         return (bool)input ? "TE".ToString() : string.Empty;

      }

      public static string GetCurrency(string input)
      {
         return input == "US" ? "USD".ToString() : "CAD".ToString();
      }

      public static bool IsOnlineOrder(string input)
      {
         return input.ToUpper() == "TE".ToUpper().ToString();

      }

      public static string GetPaymentType(string input)
      {
         switch (input)
         {
            case "Check":
               return "EC".ToString();
            case "Check-by-phone":
               return "EC".ToString();
            case "VISA":
            case "MASTERCARD":
            case "AMEX":
            case "Discover":
               return "CC".ToString();
            case "Paypal":
               return "PP".ToString();
            default:
               return "EC".ToString();
         }
      }


        public static string EZGetPaymentType(string input)
        {
            switch (input)
            {
                case "CK":
                    return "EC".ToString();
                case "Check-by-phone":
                    return "EC".ToString();
                case "CCRD":
                case "EZF CK":
                    return "CC".ToString();
                case "Paypal":
                    return "PP".ToString();
                default:
                    return "EC".ToString();
            }
        }

        public static string ConvertSAPToEZGetPaymentType(string input)
        {
            switch (input)
            {
                case "CC":
                    return "CCRD".ToString();
                case "CK":
                    return "CK".ToString();
                default:
                    return "Other".ToString();
            }
        }



        public static string GetSAPPaymentType(string input)
      {
         switch (input)
         {
            case "BD":
            case "CK":
            case "CS":
            case "ES":
               return "Check".ToString();
            case "CCRD":
               return "VISA".ToString();
            case "CC":
               return "VISA".ToString();
            case "DG":
               return "Adjustment".ToString();
            case "PP":
               return "Paypal".ToString();
            case "EC":
               return "Check-by-phone".ToString();
            default:
               return "Other".ToString();
         }
      }

        public static string GetShippingSource(string input)
        {
            switch (input)
            {
                case "EZFGAO":
                    return "G".ToString();
               default:
                    return "V".ToString();
            }
        }


        public static string FormatPhone(string input)
      {
         char[] temp = input.ToCharArray();
         temp = Array.FindAll<char>(temp, (c => (char.IsDigit(c) || c == '-')));
         return new string(temp);

      }

      public static string FormatPostalCodeCA(string input)
      {
         if (!string.IsNullOrEmpty(input))
         {
            input = input.ToUpper().Trim();
            if (input.Contains('-'))
            {
               input = input.Replace('-', ' ');
            }
            if (!input.Contains(' '))
            {
               input = input.Insert(3, " ");
            }
         }
         return input;

      }

      //public static string FormatPostalCode(string input)
      //{
      //   if (!string.IsNullOrEmpty(input))
      //   {
      //      input = input.ToUpper().Trim();
      //      if (input.Contains('-'))
      //      {
      //         input = input.Replace('-', ' ');
      //      }
      //      if (!input.Contains(' '))
      //      {
      //         input = input.Insert(3, " ");
      //      }
      //   }
      //   return input;

      //}

      public static string ConvertSCAC(string input)
      {
         switch (input)
         {
            case "FEDX":
            case "FED2":
            case "FED3":
            case "FEDG":
            case "FEDN":
            case "FEPO":
               return "FEDX".ToString();
            case "U2A":
            case "U2AM":
            case "U3S":
            case "UBAS":
            case "UGRD":
            case "UMIN":
            case "UNA":
            case "UNAM":
            case "UNAS":
            case "USF":
               return "UGRD".ToString();
            default:
               return input;
         }
      }

      public static string PaymentConfirmed(string paymentConfirmed)
      {
         return paymentConfirmed;
      }

      public static string PaymentReceived(string paymentReceived)
      {
         return paymentReceived;
      }

      public static string RemoveNonNumeric(string input)
      {
         return Regex.Replace(input, "[^.0-9]", "");
      }

      public static int ReasonSAPAdjustment
      {
         get { return 34; }
      }

      public static int Paid
      {
         get { return 8; }
      }

      public static int Refund
      {
         get { return 12; }
      }

      public static int ExternalAjustment
      {
         get { return 7; }
      }

      public static int ProductClassOmit
      {
         get { return 8; }
      }

      public static int PaymentSentFromSAP
      {
         get { return 2; }
      }

      public static int PaymentConfirmedBySAP
      {
         get { return 3; }
      }

      public static int ShippedSale
      {
         get { return 6; }
      }

      public static int SalesScreenShippedSale
      {
          get { return 4; }
      }

        public static int SaleInSAPWithPay
      {
         get { return 5; }
      }

      
   }
}
