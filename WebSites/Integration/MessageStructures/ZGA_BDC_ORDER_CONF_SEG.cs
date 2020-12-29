
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
   ///   GA.BDC.Integration.MessageStructures.ZGA_BDC_ORDER_CONF_SEG
   ///      This is an auto-generated structure of the ZGA_BDC_ORDER_CONF_SEG IDoc Segment
   ///       (ZGA_BDC_ORDER_CONF IDoc)
   /// </summary>
   [StructLayout(LayoutKind.Sequential, Pack=1)]
   public struct ZGA_BDC_ORDER_CONF_SEG
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

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=10)]
      private byte[] _shiptoacct;
      /// <summary>
      ///   Customer
      ///   -
      ///   ABAP Data Type: CHAR (10, 0)
      /// </summary>
      public string Shiptoacct
      {
         get
         {
            string exportShiptoacct = CleanConvert.ToSignificantString(this._shiptoacct);
            
            return (exportShiptoacct.Length > 0 ? exportShiptoacct : null);
         }
         
         set
         {
            CleanConvert.SetByValByteArrayStringField(ref this._shiptoacct, value);
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=10)]
      private byte[] _billtoacct;
      /// <summary>
      ///   Customer
      ///   -
      ///   ABAP Data Type: CHAR (10, 0)
      /// </summary>
      public string Billtoacct
      {
         get
         {
            string exportBilltoacct = CleanConvert.ToSignificantString(this._billtoacct);
            
            return (exportBilltoacct.Length > 0 ? exportBilltoacct : null);
         }
         
         set
         {
            CleanConvert.SetByValByteArrayStringField(ref this._billtoacct, value);
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=10)]
      private byte[] _contractno;
      /// <summary>
      ///   Sales Document
      ///   -
      ///   ABAP Data Type: CHAR (10, 0)
      /// </summary>
      public string Contractno
      {
         get
         {
            string exportContractno = CleanConvert.ToSignificantString(this._contractno);
            
            return (exportContractno.Length > 0 ? exportContractno : null);
         }
         
         set
         {
            CleanConvert.SetByValByteArrayStringField(ref this._contractno, value);
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=10)]
      private byte[] _orderno;
      /// <summary>
      ///   Sales Document
      ///   -
      ///   ABAP Data Type: CHAR (10, 0)
      /// </summary>
      public string Orderno
      {
         get
         {
            string exportOrderno = CleanConvert.ToSignificantString(this._orderno);
            
            return (exportOrderno.Length > 0 ? exportOrderno : null);
         }
         
         set
         {
            CleanConvert.SetByValByteArrayStringField(ref this._orderno, value);
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=8)]
      private byte[] _maodate;
      /// <summary>
      ///   Date in Format YYYYMMSS in 8 Characters
      ///   -
      ///   ABAP Data Type: DATS (8, 0)
      /// </summary>
      public System.DateTime? Maodate
      {
         get
         {
            System.DateTime? exportMaodate = CleanConvert.ToNullableDateTime(this._maodate);
            
            return exportMaodate;
         }
         
         set
         {
            CleanConvert.SetByValByteArrayRfcDateField(ref this._maodate, value);
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=8)]
      private byte[] _estshipdate;
      /// <summary>
      ///   Date in Format YYYYMMSS in 8 Characters
      ///   -
      ///   ABAP Data Type: DATS (8, 0)
      /// </summary>
      public System.DateTime? Estshipdate
      {
         get
         {
            System.DateTime? exportEstshipdate = CleanConvert.ToNullableDateTime(this._estshipdate);
            
            return exportEstshipdate;
         }
         
         set
         {
            CleanConvert.SetByValByteArrayRfcDateField(ref this._estshipdate, value);
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=15)]
      private byte[] _prodretail;
      /// <summary>
      ///   Amount
      ///   -
      ///   ABAP Data Type: CURR (15, 2)
      /// </summary>
      public decimal? Prodretail
      {
         get
         {
            string exportProdretail = CleanConvert.ToSignificantString(this._prodretail);
            
            decimal ctsProdretail;
            
            return (decimal.TryParse(exportProdretail, out ctsProdretail) ? (decimal?)ctsProdretail : null);
         }
         
         set
         {
            if (value.HasValue)
            {
               string formattedValue = value.Value.ToString("000000000000.00;-00000000000.00");
               
               CleanConvert.SetByValByteArrayField(ref this._prodretail, formattedValue);
            }
            else
            {
               CleanConvert.SetByValByteArrayField(ref this._prodretail, new string(' ', 15));
            }
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

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=15)]
      private byte[] _taxamt;
      /// <summary>
      ///   Amount
      ///   -
      ///   ABAP Data Type: CURR (15, 2)
      /// </summary>
      public decimal? Taxamt
      {
         get
         {
            string exportTaxamt = CleanConvert.ToSignificantString(this._taxamt);
            
            decimal ctsTaxamt;
            
            return (decimal.TryParse(exportTaxamt, out ctsTaxamt) ? (decimal?)ctsTaxamt : null);
         }
         
         set
         {
            if (value.HasValue)
            {
               string formattedValue = value.Value.ToString("000000000000.00;-00000000000.00");
               
               CleanConvert.SetByValByteArrayField(ref this._taxamt, formattedValue);
            }
            else
            {
               CleanConvert.SetByValByteArrayField(ref this._taxamt, new string(' ', 15));
            }
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=15)]
      private byte[] _ordertotal;
      /// <summary>
      ///   Amount
      ///   -
      ///   ABAP Data Type: CURR (15, 2)
      /// </summary>
      public decimal? Ordertotal
      {
         get
         {
            string exportOrdertotal = CleanConvert.ToSignificantString(this._ordertotal);
            
            decimal ctsOrdertotal;
            
            return (decimal.TryParse(exportOrdertotal, out ctsOrdertotal) ? (decimal?)ctsOrdertotal : null);
         }
         
         set
         {
            if (value.HasValue)
            {
               string formattedValue = value.Value.ToString("000000000000.00;-00000000000.00");
               
               CleanConvert.SetByValByteArrayField(ref this._ordertotal, formattedValue);
            }
            else
            {
               CleanConvert.SetByValByteArrayField(ref this._ordertotal, new string(' ', 15));
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
      private byte[] _actshipdate;
      /// <summary>
      ///   Date in Format YYYYMMSS in 8 Characters
      ///   -
      ///   ABAP Data Type: DATS (8, 0)
      /// </summary>
      public System.DateTime? Actshipdate
      {
         get
         {
            System.DateTime? exportActshipdate = CleanConvert.ToNullableDateTime(this._actshipdate);
            
            return exportActshipdate;
         }
         
         set
         {
            CleanConvert.SetByValByteArrayRfcDateField(ref this._actshipdate, value);
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=4)]
      private byte[] _scac;
      /// <summary>
      ///   Carrier SCAC
      ///   -
      ///   ABAP Data Type: CHAR (4, 0)
      /// </summary>
      public string Scac
      {
         get
         {
            string exportScac = CleanConvert.ToSignificantString(this._scac);
            
            return (exportScac.Length > 0 ? exportScac : null);
         }
         
         set
         {
            CleanConvert.SetByValByteArrayStringField(ref this._scac, value);
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=25)]
      private byte[] _scacdesc;
      /// <summary>
      ///   Carrier decsription
      ///   -
      ///   ABAP Data Type: CHAR (25, 0)
      /// </summary>
      public string Scacdesc
      {
         get
         {
            string exportScacdesc = CleanConvert.ToSignificantString(this._scacdesc);
            
            return (exportScacdesc.Length > 0 ? exportScacdesc : null);
         }
         
         set
         {
            CleanConvert.SetByValByteArrayStringField(ref this._scacdesc, value);
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=60)]
      private byte[] _trackno;
      /// <summary>
      ///   Deliv Tracking Number
      ///   -
      ///   ABAP Data Type: CHAR (60, 0)
      /// </summary>
      public string Trackno
      {
         get
         {
            string exportTrackno = CleanConvert.ToSignificantString(this._trackno);
            
            return (exportTrackno.Length > 0 ? exportTrackno : null);
         }
         
         set
         {
            CleanConvert.SetByValByteArrayStringField(ref this._trackno, value);
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=1)]
      private byte[] _endsegment;
      /// <summary>
      ///   ABAP Data Type: CHAR (1, 0)
      /// </summary>
      public char? Endsegment
      {
         get
         {
            string exportEndsegment = CleanConvert.ToSignificantString(this._endsegment);
            
            return (exportEndsegment.Length > 0 ? (char?)exportEndsegment[0] : null);
         }
         
         set
         {
            if (value.HasValue && (value.Value != '\0'))
            {
               string formattedValue = new string(value.Value, 1);
               
               CleanConvert.SetByValByteArrayStringField(ref this._endsegment, formattedValue);
            }
            else
            {
               CleanConvert.SetByValByteArrayStringField(ref this._endsegment, null);
            }
         }
      }

      #endregion
      
      #region initialization
      
      /// <summary>
      ///   Constructor w/optional initialization
      /// </summary>
      public ZGA_BDC_ORDER_CONF_SEG(bool isInitializationRequired)
      {
         this = (ZGA_BDC_ORDER_CONF_SEG)MarshalUtility.CopyByteArrayToStructure(new byte[Marshal.SizeOf(typeof(ZGA_BDC_ORDER_CONF_SEG))], typeof(ZGA_BDC_ORDER_CONF_SEG));
         if (isInitializationRequired)
         {
            CleanConvert.SetByValByteArrayStringField(ref this._segmentName, SegmentNameAsDefined);
            CleanConvert.SetByValByteArrayStringField(ref this._control, string.Empty);
         }
      }
      
      /// <summary>
      ///   Segment Name as defined in the Data Dictionary (at gen-time)
      /// </summary>
      public const string SegmentNameAsDefined = "ZGA_BDC_ORDER_CONF_SEG000";
      
      #endregion
   }
}