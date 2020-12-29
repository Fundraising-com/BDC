
/*****************************************************************************************************************
-- Generated using CodeSmith v4.1.2.2729
-- 
-- DO NOT ALTER!
-- 
-- Template Authors: Leonard Lewis
/****************************************************************************************************************/
using System;
using System.Runtime.InteropServices;

using SWCorporate.SAP.Shared;

namespace GA.BDC.Integration.MessageStructures
{
   /// <summary>
   ///   GA.BDC.Integration.MessageStructures.ZGA_BDC_PAYMENT_SEG
   ///      This is an auto-generated structure of the ZGA_BDC_PAYMENT_SEG IDoc Segment
   ///       (ZGA_BDC_PAYMENT IDoc)
   /// </summary>
   [StructLayout(LayoutKind.Sequential, Pack=1)]
   public struct ZGA_BDC_PAYMENT_SEG
   {
      #region segment name and control fields (internal)
      
      [MarshalAs(UnmanagedType.ByValArray, SizeConst=30)]
      private byte[] _segmentName;
      /// <summary>
      ///   Segment Name
      /// </summary>
      public string SegmentName
      {
         get
         {
            return CleanConvert.ToSignificantString(this._segmentName);
         }
      }
      
      /// <summary>
      ///   Control Fields (internal)
      /// </summary>
      [MarshalAs(UnmanagedType.ByValArray, SizeConst=33)]
      private byte[] _control;
      
      #endregion
      
      #region segment data fields
      
      [MarshalAs(UnmanagedType.ByValArray, SizeConst=2)]
      private byte[] _pmttype;
      /// <summary>
      ///   Payment Type
      ///   -
      ///   ABAP Data Type: CHAR (2, 0)
      /// </summary>
      public string Pmttype
      {
         get
         {
            string exportPmttype = CleanConvert.ToSignificantString(this._pmttype);
            
            return (exportPmttype.Length > 0 ? exportPmttype : null);
         }
         
         set
         {
            CleanConvert.SetByValByteArrayStringField(ref this._pmttype, value);
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=10)]
      private byte[] _leadid;
      /// <summary>
      ///   Customer
      ///   -
      ///   ABAP Data Type: CHAR (10, 0)
      /// </summary>
      public string Leadid
      {
         get
         {
            string exportLeadid = CleanConvert.ToSignificantString(this._leadid);
            
            return (exportLeadid.Length > 0 ? exportLeadid : null);
         }
         
         set
         {
            CleanConvert.SetByValByteArrayStringField(ref this._leadid, value);
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=10)]
      private byte[] _orderrefid;
      /// <summary>
      ///   Sales Document
      ///   -
      ///   ABAP Data Type: CHAR (10, 0)
      /// </summary>
      public string Orderrefid
      {
         get
         {
            string exportOrderrefid = CleanConvert.ToSignificantString(this._orderrefid);
            
            return (exportOrderrefid.Length > 0 ? exportOrderrefid : null);
         }
         
         set
         {
            CleanConvert.SetByValByteArrayStringField(ref this._orderrefid, value);
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=15)]
      private byte[] _amount;
      /// <summary>
      ///   Amount
      ///   -
      ///   ABAP Data Type: CURR (15, 2)
      /// </summary>
      public decimal? Amount
      {
         get
         {
            string exportAmount = CleanConvert.ToSignificantString(this._amount);
            
            decimal ctsAmount;
            
            return (decimal.TryParse(exportAmount, out ctsAmount) ? (decimal?)ctsAmount : null);
         }
         
         set
         {
            if (value.HasValue)
            {
               string formattedValue = value.Value.ToString("000000000000.00;-00000000000.00");
               
               CleanConvert.SetByValByteArrayField(ref this._amount, formattedValue);
            }
            else
            {
               CleanConvert.SetByValByteArrayField(ref this._amount, new string(' ', 15));
            }
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=5)]
      private byte[] _currency;
      /// <summary>
      ///   Currency
      ///   -
      ///   ABAP Data Type: CUKY (5, 0)
      /// </summary>
      public string Currency
      {
         get
         {
            string exportCurrency = CleanConvert.ToSignificantString(this._currency);
            
            return (exportCurrency.Length > 0 ? exportCurrency : null);
         }
         
         set
         {
            CleanConvert.SetByValByteArrayStringField(ref this._currency, value);
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=8)]
      private byte[] _bankdate;
      /// <summary>
      ///   Date
      ///   -
      ///   ABAP Data Type: DATS (8, 0)
      /// </summary>
      public System.DateTime? Bankdate
      {
         get
         {
            System.DateTime? exportBankdate = CleanConvert.ToNullableDateTime(this._bankdate);
            
            return exportBankdate;
         }
         
         set
         {
            CleanConvert.SetByValByteArrayRfcDateField(ref this._bankdate, value);
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=1)]
      private byte[] _revind;
      /// <summary>
      ///   Reversal Indicator
      ///   -
      ///   ABAP Data Type: CHAR (1, 0)
      /// </summary>
      public char? Revind
      {
         get
         {
            string exportRevind = CleanConvert.ToSignificantString(this._revind);
            
            return (exportRevind.Length > 0 ? (char?)exportRevind[0] : null);
         }
         
         set
         {
            if (value.HasValue && (value.Value != '\0'))
            {
               string formattedValue = new string(value.Value, 1);
               
               CleanConvert.SetByValByteArrayStringField(ref this._revind, formattedValue);
            }
            else
            {
               CleanConvert.SetByValByteArrayStringField(ref this._revind, null);
            }
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=10)]
      private byte[] _sqlid;
      /// <summary>
      ///   SQL ID
      ///   -
      ///   ABAP Data Type: CHAR (10, 0)
      /// </summary>
      public string Sqlid
      {
         get
         {
            string exportSqlid = CleanConvert.ToSignificantString(this._sqlid);
            
            return (exportSqlid.Length > 0 ? exportSqlid : null);
         }
         
         set
         {
            CleanConvert.SetByValByteArrayStringField(ref this._sqlid, value);
         }
      }

      #endregion
      
      #region initialization
      
      /// <summary>
      ///   Constructor w/optional initialization
      /// </summary>
      public ZGA_BDC_PAYMENT_SEG(bool isInitializationRequired)
      {
         this = (ZGA_BDC_PAYMENT_SEG)MarshalUtility.CopyByteArrayToStructure(new byte[Marshal.SizeOf(typeof(ZGA_BDC_PAYMENT_SEG))], typeof(ZGA_BDC_PAYMENT_SEG));
         if (isInitializationRequired)
         {
            CleanConvert.SetByValByteArrayStringField(ref this._segmentName, SegmentNameAsDefined);
            CleanConvert.SetByValByteArrayStringField(ref this._control, string.Empty);
         }
      }
      
      /// <summary>
      ///   Segment Name as defined in the Data Dictionary (at gen-time)
      /// </summary>
      public const string SegmentNameAsDefined = "ZGA_BDC_PAYMENT_SEG000";
      
      #endregion
   }
}