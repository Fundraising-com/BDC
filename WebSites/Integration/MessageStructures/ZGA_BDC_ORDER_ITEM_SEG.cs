
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
   ///   GA.BDC.Integration.MessageStructures.ZGA_BDC_ORDER_ITEM_SEG
   ///      This is an auto-generated structure of the ZGA_BDC_ORDER_ITEM_SEG IDoc Segment
   ///       (ZGA_BDC_ORDER IDoc)
   /// </summary>
   [StructLayout(LayoutKind.Sequential, Pack=1)]
   public struct ZGA_BDC_ORDER_ITEM_SEG
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


        


        [MarshalAs(UnmanagedType.ByValArray, SizeConst=18)]
      private byte[] _materialno;
      /// <summary>
      ///   Material
      ///   -
      ///   ABAP Data Type: CHAR (18, 0)
      /// </summary>
      public string Materialno
      {
         get
         {
            string exportMaterialno = CleanConvert.ToSignificantString(this._materialno);
            
            return (exportMaterialno.Length > 0 ? exportMaterialno : null);
         }
         
         set
         {
            CleanConvert.SetByValByteArrayStringField(ref this._materialno, value);
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=2)]
      private byte[] _profitpct;
      /// <summary>
      ///   Customer group
      ///   -
      ///   ABAP Data Type: CHAR (2, 0)
      /// </summary>
      public string Profitpct
      {
         get
         {
            string exportProfitpct = CleanConvert.ToSignificantString(this._profitpct);
            
            return (exportProfitpct.Length > 0 ? exportProfitpct : null);
         }
         
         set
         {
            CleanConvert.SetByValByteArrayStringField(ref this._profitpct, value);
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=2)]
      private byte[] _pricecode;
      /// <summary>
      ///   Price List
      ///   -
      ///   ABAP Data Type: CHAR (2, 0)
      /// </summary>
      public string Pricecode
      {
         get
         {
            string exportPricecode = CleanConvert.ToSignificantString(this._pricecode);
            
            return (exportPricecode.Length > 0 ? exportPricecode : null);
         }
         
         set
         {
            CleanConvert.SetByValByteArrayStringField(ref this._pricecode, value);
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=17)]
      private byte[] _quantity;
      /// <summary>
      ///   Order Quantity
      ///   -
      ///   ABAP Data Type: QUAN (17, 3)
      /// </summary>
      public decimal? Quantity
      {
         get
         {
            string exportQuantity = CleanConvert.ToSignificantString(this._quantity);
            
            decimal ctsQuantity;
            
            return (decimal.TryParse(exportQuantity, out ctsQuantity) ? (decimal?)ctsQuantity : null);
         }
         
         set
         {
            if (value.HasValue)
            {
               string formattedValue = value.Value.ToString("0000000000000.000;-000000000000.000");
               
               CleanConvert.SetByValByteArrayField(ref this._quantity, formattedValue);
            }
            else
            {
               CleanConvert.SetByValByteArrayField(ref this._quantity, new string(' ', 17));
            }
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=3)]
      private byte[] _uom;
      /// <summary>
      ///   Base Unit of Measure
      ///   -
      ///   ABAP Data Type: UNIT (3, 0)
      /// </summary>
      public string Uom
      {
         get
         {
            string exportUom = CleanConvert.ToSignificantString(this._uom);
            
            return (exportUom.Length > 0 ? exportUom : null);
         }
         
         set
         {
            CleanConvert.SetByValByteArrayStringField(ref this._uom, value);
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=15)]
      private byte[] _unitprice;
      /// <summary>
      ///   Amount
      ///   -
      ///   ABAP Data Type: CURR (15, 2)
      /// </summary>
      public decimal? Unitprice
      {
         get
         {
            string exportUnitprice = CleanConvert.ToSignificantString(this._unitprice);
            
            decimal ctsUnitprice;
            
            return (decimal.TryParse(exportUnitprice, out ctsUnitprice) ? (decimal?)ctsUnitprice : null);
         }
         
         set
         {
            if (value.HasValue)
            {
               string formattedValue = value.Value.ToString("000000000000.00;-00000000000.00");
               
               CleanConvert.SetByValByteArrayField(ref this._unitprice, formattedValue);
            }
            else
            {
               CleanConvert.SetByValByteArrayField(ref this._unitprice, new string(' ', 15));
            }
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=1)]
      private byte[] _free;
      /// <summary>
      ///   ABAP Data Type: CHAR (1, 0)
      /// </summary>
      public bool? Free
      {
         get
         {
            bool? exportFree = CleanConvert.ToNullableBoolean(this._free);
            
            return exportFree;
         }
         
         set
         {
            if (value.HasValue)
            {
               string formattedValue = new string(value.Value ? 'X' : ' ', 1);
               
               CleanConvert.SetByValByteArrayStringField(ref this._free, formattedValue);
            }
            else
            {
               string formattedValue = new string('/', 1);
               
               CleanConvert.SetByValByteArrayStringField(ref this._free, formattedValue);
            }
         }
      }

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        private byte[] _shipsource;
        /// <summary>
        ///   Shipping Source
        ///   -
        ///   ABAP Data Type: CHAR (1, 0)
        /// </summary>
        public string Shipsource
        {
            get
            {
                string exportShipsource = CleanConvert.ToSignificantString(this._shipsource);

                return (exportShipsource.Length > 0 ? exportShipsource : null);
            }

            set
            {
                CleanConvert.SetByValByteArrayStringField(ref this._shipsource, value);
            }
        }



        #endregion

        #region initialization

        /// <summary>
        ///   Constructor w/optional initialization
        /// </summary>
        public ZGA_BDC_ORDER_ITEM_SEG(bool isInitializationRequired)
      {
         this = (ZGA_BDC_ORDER_ITEM_SEG)MarshalUtility.CopyByteArrayToStructure(new byte[Marshal.SizeOf(typeof(ZGA_BDC_ORDER_ITEM_SEG))], typeof(ZGA_BDC_ORDER_ITEM_SEG));
         if (isInitializationRequired)
         {
            CleanConvert.SetByValByteArrayStringField(ref this._segmentName, SegmentNameAsDefined);
            CleanConvert.SetByValByteArrayStringField(ref this._control, string.Empty);
         }
      }
      
      /// <summary>
      ///   Segment Name as defined in the Data Dictionary (at gen-time)
      /// </summary>
      public const string SegmentNameAsDefined = "ZGA_BDC_ORDER_ITEM_SEG000";
      
      #endregion
   }
}