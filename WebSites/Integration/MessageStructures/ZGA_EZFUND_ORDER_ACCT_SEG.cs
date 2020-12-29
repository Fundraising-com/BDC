
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
   ///   GA.BDC.Integration.MessageStructures.ZGA_BDC_ORDER_ACCT_SEG
   ///      This is an auto-generated structure of the ZGA_BDC_ORDER_ACCT_SEG IDoc Segment
   ///       (ZGA_BDC_ORDER IDoc)
   /// </summary>
   [StructLayout(LayoutKind.Sequential, Pack=1)]
   public struct ZGA_EZFUND_ORDER_ACCT_SEG
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
      private byte[] _type;
      /// <summary>
      ///   Partner Function
      ///   -
      ///   ABAP Data Type: CHAR (2, 0)
      /// </summary>
      public string Type
      {
         get
         {
            string exportType = CleanConvert.ToSignificantString(this._type);
            
            return (exportType.Length > 0 ? exportType : null);
         }
         
         set
         {
            CleanConvert.SetByValByteArrayStringField(ref this._type, value);
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=35)]
      private byte[] _name1;
      /// <summary>
      ///   Name
      ///   -
      ///   ABAP Data Type: CHAR (35, 0)
      /// </summary>
      public string Name1
      {
         get
         {
            string exportName1 = CleanConvert.ToSignificantString(this._name1);
            
            return (exportName1.Length > 0 ? exportName1 : null);
         }
         
         set
         {
            CleanConvert.SetByValByteArrayStringField(ref this._name1, value);
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=35)]
      private byte[] _name2;
      /// <summary>
      ///   Name 2
      ///   -
      ///   ABAP Data Type: CHAR (35, 0)
      /// </summary>
      public string Name2
      {
         get
         {
            string exportName2 = CleanConvert.ToSignificantString(this._name2);
            
            return (exportName2.Length > 0 ? exportName2 : null);
         }
         
         set
         {
            CleanConvert.SetByValByteArrayStringField(ref this._name2, value);
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=35)]
      private byte[] _street1;
      /// <summary>
      ///   House number/street
      ///   -
      ///   ABAP Data Type: CHAR (30, 0)
      /// </summary>
      public string Street1
      {
         get
         {
            string exportStreet1 = CleanConvert.ToSignificantString(this._street1);
            
            return (exportStreet1.Length > 0 ? exportStreet1 : null);
         }
         
         set
         {
            CleanConvert.SetByValByteArrayStringField(ref this._street1, value);
         }
      }

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 35)]
        private byte[] _street2;
        /// <summary>
        ///   House number/street
        ///   -
        ///   ABAP Data Type: CHAR (30, 0)
        /// </summary>
        public string Street2
        {
            get
            {
                string exportStreet2 = CleanConvert.ToSignificantString(this._street2);

                return (exportStreet2.Length > 0 ? exportStreet2 : null);
            }

            set
            {
                CleanConvert.SetByValByteArrayStringField(ref this._street2, value);
            }
        }

        [MarshalAs(UnmanagedType.ByValArray, SizeConst=35)]
      private byte[] _city;
      /// <summary>
      ///   City
      ///   -
      ///   ABAP Data Type: CHAR (35, 0)
      /// </summary>
      public string City
      {
         get
         {
            string exportCity = CleanConvert.ToSignificantString(this._city);
            
            return (exportCity.Length > 0 ? exportCity : null);
         }
         
         set
         {
            CleanConvert.SetByValByteArrayStringField(ref this._city, value);
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=3)]
      private byte[] _region;
      /// <summary>
      ///   Region
      ///   -
      ///   ABAP Data Type: CHAR (3, 0)
      /// </summary>
      public string Region
      {
         get
         {
            string exportRegion = CleanConvert.ToSignificantString(this._region);
            
            return (exportRegion.Length > 0 ? exportRegion : null);
         }
         
         set
         {
            CleanConvert.SetByValByteArrayStringField(ref this._region, value);
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=10)]
      private byte[] _postcode;
      /// <summary>
      ///   Postal Code
      ///   -
      ///   ABAP Data Type: CHAR (10, 0)
      /// </summary>
      public string Postcode
      {
         get
         {
            string exportPostcode = CleanConvert.ToSignificantString(this._postcode);
            
            return (exportPostcode.Length > 0 ? exportPostcode : null);
         }
         
         set
         {
            CleanConvert.SetByValByteArrayStringField(ref this._postcode, value);
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=10)]
      private byte[] _pobox;
      /// <summary>
      ///   PO Box
      ///   -
      ///   ABAP Data Type: CHAR (10, 0)
      /// </summary>
      public string Pobox
      {
         get
         {
            string exportPobox = CleanConvert.ToSignificantString(this._pobox);
            
            return (exportPobox.Length > 0 ? exportPobox : null);
         }
         
         set
         {
            CleanConvert.SetByValByteArrayStringField(ref this._pobox, value);
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=10)]
      private byte[] _poboxpostcode;
      /// <summary>
      ///   P.O. Box Postal Code
      ///   -
      ///   ABAP Data Type: CHAR (10, 0)
      /// </summary>
      public string Poboxpostcode
      {
         get
         {
            string exportPoboxpostcode = CleanConvert.ToSignificantString(this._poboxpostcode);
            
            return (exportPoboxpostcode.Length > 0 ? exportPoboxpostcode : null);
         }
         
         set
         {
            CleanConvert.SetByValByteArrayStringField(ref this._poboxpostcode, value);
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=3)]
      private byte[] _country;
      /// <summary>
      ///   Country Key
      ///   -
      ///   ABAP Data Type: CHAR (3, 0)
      /// </summary>
      public string Country
      {
         get
         {
            string exportCountry = CleanConvert.ToSignificantString(this._country);
            
            return (exportCountry.Length > 0 ? exportCountry : null);
         }
         
         set
         {
            CleanConvert.SetByValByteArrayStringField(ref this._country, value);
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=80)]
      private byte[] _email;
      /// <summary>
      ///   E-mail Address
      ///   -
      ///   ABAP Data Type: CHAR (80, 0)
      /// </summary>
      public string Email
      {
         get
         {
            string exportEmail = CleanConvert.ToSignificantString(this._email);
            
            return (exportEmail.Length > 0 ? exportEmail : null);
         }
         
         set
         {
            CleanConvert.SetByValByteArrayStringField(ref this._email, value);
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=16)]
      private byte[] _dayphone;
      /// <summary>
      ///   Telephone 1
      ///   -
      ///   ABAP Data Type: CHAR (16, 0)
      /// </summary>
      public string Dayphone
      {
         get
         {
            string exportDayphone = CleanConvert.ToSignificantString(this._dayphone);
            
            return (exportDayphone.Length > 0 ? exportDayphone : null);
         }
         
         set
         {
            CleanConvert.SetByValByteArrayStringField(ref this._dayphone, value);
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=10)]
      private byte[] _dayphoneext;
      /// <summary>
      ///   Extension
      ///   -
      ///   ABAP Data Type: CHAR (10, 0)
      /// </summary>
      public string Dayphoneext
      {
         get
         {
            string exportDayphoneext = CleanConvert.ToSignificantString(this._dayphoneext);
            
            return (exportDayphoneext.Length > 0 ? exportDayphoneext : null);
         }
         
         set
         {
            CleanConvert.SetByValByteArrayStringField(ref this._dayphoneext, value);
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=16)]
      private byte[] _evephone;
      /// <summary>
      ///   Telephone 1
      ///   -
      ///   ABAP Data Type: CHAR (16, 0)
      /// </summary>
      public string Evephone
      {
         get
         {
            string exportEvephone = CleanConvert.ToSignificantString(this._evephone);
            
            return (exportEvephone.Length > 0 ? exportEvephone : null);
         }
         
         set
         {
            CleanConvert.SetByValByteArrayStringField(ref this._evephone, value);
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=16)]
      private byte[] _mobilephone;
      /// <summary>
      ///   Telephone 1
      ///   -
      ///   ABAP Data Type: CHAR (16, 0)
      /// </summary>
      public string Mobilephone
      {
         get
         {
            string exportMobilephone = CleanConvert.ToSignificantString(this._mobilephone);
            
            return (exportMobilephone.Length > 0 ? exportMobilephone : null);
         }
         
         set
         {
            CleanConvert.SetByValByteArrayStringField(ref this._mobilephone, value);
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=16)]
      private byte[] _fax;
      /// <summary>
      ///   Telephone 1
      ///   -
      ///   ABAP Data Type: CHAR (16, 0)
      /// </summary>
      public string Fax
      {
         get
         {
            string exportFax = CleanConvert.ToSignificantString(this._fax);
            
            return (exportFax.Length > 0 ? exportFax : null);
         }
         
         set
         {
            CleanConvert.SetByValByteArrayStringField(ref this._fax, value);
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=10)]
      private byte[] _faxext;
      /// <summary>
      ///   Extension
      ///   -
      ///   ABAP Data Type: CHAR (10, 0)
      /// </summary>
      public string Faxext
      {
         get
         {
            string exportFaxext = CleanConvert.ToSignificantString(this._faxext);
            
            return (exportFaxext.Length > 0 ? exportFaxext : null);
         }
         
         set
         {
            CleanConvert.SetByValByteArrayStringField(ref this._faxext, value);
         }
      }

      #endregion
      
      #region initialization
      
      /// <summary>
      ///   Constructor w/optional initialization
      /// </summary>
      public ZGA_EZFUND_ORDER_ACCT_SEG(bool isInitializationRequired)
      {
         this = (ZGA_EZFUND_ORDER_ACCT_SEG)MarshalUtility.CopyByteArrayToStructure(new byte[Marshal.SizeOf(typeof(ZGA_EZFUND_ORDER_ACCT_SEG))], typeof(ZGA_EZFUND_ORDER_ACCT_SEG));
         if (isInitializationRequired)
         {
            CleanConvert.SetByValByteArrayStringField(ref this._segmentName, SegmentNameAsDefined);
            CleanConvert.SetByValByteArrayStringField(ref this._control, string.Empty);
         }
      }
      
      /// <summary>
      ///   Segment Name as defined in the Data Dictionary (at gen-time)
      /// </summary>
      public const string SegmentNameAsDefined = "ZGA_BDC_ORDER_ACCT_SEG000";
      
      #endregion
   }
}