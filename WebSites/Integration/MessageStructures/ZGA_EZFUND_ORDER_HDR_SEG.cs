
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
   ///   GA.BDC.Integration.MessageStructures.ZGA_BDC_ORDER_HDR_SEG
   ///      This is an auto-generated structure of the ZGA_BDC_ORDER_HDR_SEG IDoc Segment
   ///       (ZGA_BDC_ORDER IDoc)
   /// </summary>
   [StructLayout(LayoutKind.Sequential, Pack=1)]
   public struct ZGA_EZFUND_ORDER_HDR_SEG
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
      
      [MarshalAs(UnmanagedType.ByValArray, SizeConst=10)]
      private byte[] _bdcrepid;
      /// <summary>
      ///   ABAP Data Type: CHAR (10, 0)
      /// </summary>
      public string Bdcrepid
      {
         get
         {
            string exportBdcrepid = CleanConvert.ToSignificantString(this._bdcrepid);
            
            return (exportBdcrepid.Length > 0 ? exportBdcrepid : null);
         }
         
         set
         {
            CleanConvert.SetByValByteArrayStringField(ref this._bdcrepid, value);
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=10)]
      private byte[] _fieldsalesrepid;
      /// <summary>
      ///   ABAP Data Type: CHAR (10, 0)
      /// </summary>
      public string Fieldsalesrepid
      {
         get
         {
            string exportFieldsalesrepid = CleanConvert.ToSignificantString(this._fieldsalesrepid);
            
            return (exportFieldsalesrepid.Length > 0 ? exportFieldsalesrepid : null);
         }
         
         set
         {
            CleanConvert.SetByValByteArrayStringField(ref this._fieldsalesrepid, value);
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=10)]
      private byte[] _leadid;
      /// <summary>
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

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=8)]
      private byte[] _reqdelondate;
      /// <summary>
      ///   Date in Format YYYYMMSS in 8 Characters
      ///   -
      ///   ABAP Data Type: DATS (8, 0)
      /// </summary>
      public System.DateTime? Reqdelondate
      {
         get
         {
            System.DateTime? exportReqdelondate = CleanConvert.ToNullableDateTime(this._reqdelondate);
            
            return exportReqdelondate;
         }
         
         set
         {
            CleanConvert.SetByValByteArrayRfcDateField(ref this._reqdelondate, value);
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=2)]
      private byte[] _shipcond;
      /// <summary>
      ///   Shipping Conditions
      ///   -
      ///   ABAP Data Type: CHAR (2, 0)
      /// </summary>
      public string Shipcond
      {
         get
         {
            string exportShipcond = CleanConvert.ToSignificantString(this._shipcond);
            
            return (exportShipcond.Length > 0 ? exportShipcond : null);
         }
         
         set
         {
            CleanConvert.SetByValByteArrayStringField(ref this._shipcond, value);
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=6)]
      private byte[] _students;
      /// <summary>
      ///   Reserve
      ///   -
      ///   ABAP Data Type: CHAR (6, 0)
      /// </summary>
      public string Students
      {
         get
         {
            string exportStudents = CleanConvert.ToSignificantString(this._students);
            
            return (exportStudents.Length > 0 ? exportStudents : null);
         }
         
         set
         {
            CleanConvert.SetByValByteArrayStringField(ref this._students, value);
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=8)]
      private byte[] _signdate;
      /// <summary>
      ///   Date in Format YYYYMMSS in 8 Characters
      ///   -
      ///   ABAP Data Type: DATS (8, 0)
      /// </summary>
      public System.DateTime? Signdate
      {
         get
         {
            System.DateTime? exportSigndate = CleanConvert.ToNullableDateTime(this._signdate);
            
            return exportSigndate;
         }
         
         set
         {
            CleanConvert.SetByValByteArrayRfcDateField(ref this._signdate, value);
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=8)]
      private byte[] _startdate;
      /// <summary>
      ///   Date in Format YYYYMMSS in 8 Characters
      ///   -
      ///   ABAP Data Type: DATS (8, 0)
      /// </summary>
      public System.DateTime? Startdate
      {
         get
         {
            System.DateTime? exportStartdate = CleanConvert.ToNullableDateTime(this._startdate);
            
            return exportStartdate;
         }
         
         set
         {
            CleanConvert.SetByValByteArrayRfcDateField(ref this._startdate, value);
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=15)]
      private byte[] _shippingfee;
      /// <summary>
      ///   Amount
      ///   -
      ///   ABAP Data Type: CURR (15, 2)
      /// </summary>
      public decimal? Shippingfee
      {
         get
         {
            string exportShippingfee = CleanConvert.ToSignificantString(this._shippingfee);
            
            decimal ctsShippingfee;
            
            return (decimal.TryParse(exportShippingfee, out ctsShippingfee) ? (decimal?)ctsShippingfee : null);
         }
         
         set
         {
            if (value.HasValue)
            {
               string formattedValue = value.Value.ToString("000000000000.00;-00000000000.00");
               
               CleanConvert.SetByValByteArrayField(ref this._shippingfee, formattedValue);
            }
            else
            {
               CleanConvert.SetByValByteArrayField(ref this._shippingfee, new string(' ', 15));
            }
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=15)]
      private byte[] _surcharge;
      /// <summary>
      ///   Amount
      ///   -
      ///   ABAP Data Type: CURR (15, 2)
      /// </summary>
      public decimal? Surcharge
      {
         get
         {
            string exportSurcharge = CleanConvert.ToSignificantString(this._surcharge);
            
            decimal ctsSurcharge;
            
            return (decimal.TryParse(exportSurcharge, out ctsSurcharge) ? (decimal?)ctsSurcharge : null);
         }
         
         set
         {
            if (value.HasValue)
            {
               string formattedValue = value.Value.ToString("000000000000.00;-00000000000.00");
               
               CleanConvert.SetByValByteArrayField(ref this._surcharge, formattedValue);
            }
            else
            {
               CleanConvert.SetByValByteArrayField(ref this._surcharge, new string(' ', 15));
            }
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=15)]
      private byte[] _discount;
      /// <summary>
      ///   Amount
      ///   -
      ///   ABAP Data Type: CURR (15, 2)
      /// </summary>
      public decimal? Discount
      {
         get
         {
            string exportDiscount = CleanConvert.ToSignificantString(this._discount);
            
            decimal ctsDiscount;
            
            return (decimal.TryParse(exportDiscount, out ctsDiscount) ? (decimal?)ctsDiscount : null);
         }
         
         set
         {
            if (value.HasValue)
            {
               string formattedValue = value.Value.ToString("000000000000.00;-00000000000.00");
               
               CleanConvert.SetByValByteArrayField(ref this._discount, formattedValue);
            }
            else
            {
               CleanConvert.SetByValByteArrayField(ref this._discount, new string(' ', 15));
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

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=1)]
      private byte[] _origin;
      /// <summary>
      ///   ABAP Data Type: CHAR (1, 0)
      /// </summary>
      public char? Origin
      {
         get
         {
            string exportOrigin = CleanConvert.ToSignificantString(this._origin);
            
            return (exportOrigin.Length > 0 ? (char?)exportOrigin[0] : null);
         }
         
         set
         {
            if (value.HasValue && (value.Value != '\0'))
            {
               string formattedValue = new string(value.Value, 1);
               
               CleanConvert.SetByValByteArrayStringField(ref this._origin, formattedValue);
            }
            else
            {
               CleanConvert.SetByValByteArrayStringField(ref this._origin, null);
            }
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=100)]
      private byte[] _shipinst;
      /// <summary>
      ///   ABAP Data Type: CHAR (100, 0)
      /// </summary>
      public string Shipinst
      {
         get
         {
            string exportShipinst = CleanConvert.ToSignificantString(this._shipinst);
            
            return (exportShipinst.Length > 0 ? exportShipinst : null);
         }
         
         set
         {
            CleanConvert.SetByValByteArrayStringField(ref this._shipinst, value);
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
      public ZGA_EZFUND_ORDER_HDR_SEG(bool isInitializationRequired)
      {
         this = (ZGA_EZFUND_ORDER_HDR_SEG)MarshalUtility.CopyByteArrayToStructure(new byte[Marshal.SizeOf(typeof(ZGA_EZFUND_ORDER_HDR_SEG))], typeof(ZGA_EZFUND_ORDER_HDR_SEG));
         if (isInitializationRequired)
         {
            CleanConvert.SetByValByteArrayStringField(ref this._segmentName, SegmentNameAsDefined);
            CleanConvert.SetByValByteArrayStringField(ref this._control, string.Empty);
         }
      }
      
      /// <summary>
      ///   Segment Name as defined in the Data Dictionary (at gen-time)
      /// </summary>
      public const string SegmentNameAsDefined = "ZGA_BDC_ORDER_HDR_SEG000";
      
      #endregion
   }
}