
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
   ///   GA.BDC.Integration.MessageStructures.ZGA_BDC_GRP_CONTRACT_SEG
   ///      This is an auto-generated structure of the ZGA_BDC_GRP_CONTRACT_SEG IDoc Segment
   ///       (ZGA_BDC_GRP_CONTRACT IDoc)
   /// </summary>
   [StructLayout(LayoutKind.Sequential, Pack=1)]
   public struct ZGA_BDC_GRP_CONTRACT_SEG
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
      
      [MarshalAs(UnmanagedType.ByValArray, SizeConst=3)]
      private byte[] _transType;
      /// <summary>
      ///   ABAP Data Type: CHAR (3, 0)
      /// </summary>
      public string TransType
      {
         get
         {
            string exportTransType = CleanConvert.ToSignificantString(this._transType);
            
            return (exportTransType.Length > 0 ? exportTransType : null);
         }
         
         set
         {
            CleanConvert.SetByValByteArrayStringField(ref this._transType, value);
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=10)]
      private byte[] _bdcPartnerId;
      /// <summary>
      ///   ABAP Data Type: CHAR (10, 0)
      /// </summary>
      public string BdcPartnerId
      {
         get
         {
            string exportBdcPartnerId = CleanConvert.ToSignificantString(this._bdcPartnerId);
            
            return (exportBdcPartnerId.Length > 0 ? exportBdcPartnerId : null);
         }
         
         set
         {
            CleanConvert.SetByValByteArrayStringField(ref this._bdcPartnerId, value);
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=10)]
      private byte[] _bdcGroupId;
      /// <summary>
      ///   ABAP Data Type: CHAR (10, 0)
      /// </summary>
      public string BdcGroupId
      {
         get
         {
            string exportBdcGroupId = CleanConvert.ToSignificantString(this._bdcGroupId);
            
            return (exportBdcGroupId.Length > 0 ? exportBdcGroupId : null);
         }
         
         set
         {
            CleanConvert.SetByValByteArrayStringField(ref this._bdcGroupId, value);
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=11)]
      private byte[] _focusContractId;
      /// <summary>
      ///   Focus Contract No
      ///   -
      ///   ABAP Data Type: NUMC (11, 0)
      /// </summary>
      public string FocusContractId
      {
         get
         {
            string exportFocusContractId = CleanConvert.ToSignificantString(this._focusContractId);
            
            return (exportFocusContractId.Length > 0 ? exportFocusContractId : null);
         }
         
         set
         {
            if (value != null)
            {
               string trimmedValue = value.Trim();
               if ((trimmedValue.Length > 0) && !CleanConvert.IsNumeric(trimmedValue, true))
               {
                  throw (new System.ArgumentException("FocusContractId must be numeric."));
               }
            }
            CleanConvert.SetByValByteArrayNumericField(ref this._focusContractId, value);
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=100)]
      private byte[] _email;
      /// <summary>
      ///   ABAP Data Type: CHAR (100, 0)
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

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=14)]
      private byte[] _timestamp;
      /// <summary>
      ///   Time stamp
      ///   -
      ///   ABAP Data Type: CHAR (14, 0)
      /// </summary>
      public string Timestamp
      {
         get
         {
            string exportTimestamp = CleanConvert.ToSignificantString(this._timestamp);
            
            return (exportTimestamp.Length > 0 ? exportTimestamp : null);
         }
         
         set
         {
            CleanConvert.SetByValByteArrayStringField(ref this._timestamp, value);
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=10)]
      private byte[] _salespAcct;
      /// <summary>
      ///   Customer
      ///   -
      ///   ABAP Data Type: CHAR (10, 0)
      /// </summary>
      public string SalespAcct
      {
         get
         {
            string exportSalespAcct = CleanConvert.ToSignificantString(this._salespAcct);
            
            return (exportSalespAcct.Length > 0 ? exportSalespAcct : null);
         }
         
         set
         {
            CleanConvert.SetByValByteArrayStringField(ref this._salespAcct, value);
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=35)]
      private byte[] _groupName;
      /// <summary>
      ///   Name
      ///   -
      ///   ABAP Data Type: CHAR (35, 0)
      /// </summary>
      public string GroupName
      {
         get
         {
            string exportGroupName = CleanConvert.ToSignificantString(this._groupName);
            
            return (exportGroupName.Length > 0 ? exportGroupName : null);
         }
         
         set
         {
            CleanConvert.SetByValByteArrayStringField(ref this._groupName, value);
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=35)]
      private byte[] _sponsorName;
      /// <summary>
      ///   Name
      ///   -
      ///   ABAP Data Type: CHAR (35, 0)
      /// </summary>
      public string SponsorName
      {
         get
         {
            string exportSponsorName = CleanConvert.ToSignificantString(this._sponsorName);
            
            return (exportSponsorName.Length > 0 ? exportSponsorName : null);
         }
         
         set
         {
            CleanConvert.SetByValByteArrayStringField(ref this._sponsorName, value);
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=60)]
      private byte[] _address1;
      /// <summary>
      ///   Street
      ///   -
      ///   ABAP Data Type: CHAR (60, 0)
      /// </summary>
      public string Address1
      {
         get
         {
            string exportAddress1 = CleanConvert.ToSignificantString(this._address1);
            
            return (exportAddress1.Length > 0 ? exportAddress1 : null);
         }
         
         set
         {
            CleanConvert.SetByValByteArrayStringField(ref this._address1, value);
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=60)]
      private byte[] _address2;
      /// <summary>
      ///   Street
      ///   -
      ///   ABAP Data Type: CHAR (60, 0)
      /// </summary>
      public string Address2
      {
         get
         {
            string exportAddress2 = CleanConvert.ToSignificantString(this._address2);
            
            return (exportAddress2.Length > 0 ? exportAddress2 : null);
         }
         
         set
         {
            CleanConvert.SetByValByteArrayStringField(ref this._address2, value);
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=40)]
      private byte[] _city;
      /// <summary>
      ///   City
      ///   -
      ///   ABAP Data Type: CHAR (40, 0)
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
      private byte[] _state;
      /// <summary>
      ///   Region
      ///   -
      ///   ABAP Data Type: CHAR (3, 0)
      /// </summary>
      public string State
      {
         get
         {
            string exportState = CleanConvert.ToSignificantString(this._state);
            
            return (exportState.Length > 0 ? exportState : null);
         }
         
         set
         {
            CleanConvert.SetByValByteArrayStringField(ref this._state, value);
         }
      }

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=10)]
      private byte[] _zip;
      /// <summary>
      ///   Postal Code
      ///   -
      ///   ABAP Data Type: CHAR (10, 0)
      /// </summary>
      public string Zip
      {
         get
         {
            string exportZip = CleanConvert.ToSignificantString(this._zip);
            
            return (exportZip.Length > 0 ? exportZip : null);
         }
         
         set
         {
            CleanConvert.SetByValByteArrayStringField(ref this._zip, value);
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

      [MarshalAs(UnmanagedType.ByValArray, SizeConst=30)]
      private byte[] _telephone;
      /// <summary>
      ///   Telephone
      ///   -
      ///   ABAP Data Type: CHAR (30, 0)
      /// </summary>
      public string Telephone
      {
         get
         {
            string exportTelephone = CleanConvert.ToSignificantString(this._telephone);
            
            return (exportTelephone.Length > 0 ? exportTelephone : null);
         }
         
         set
         {
            CleanConvert.SetByValByteArrayStringField(ref this._telephone, value);
         }
      }

      #endregion
      
      #region initialization
      
      /// <summary>
      ///   Constructor w/optional initialization
      /// </summary>
      public ZGA_BDC_GRP_CONTRACT_SEG(bool isInitializationRequired)
      {
         this = (ZGA_BDC_GRP_CONTRACT_SEG)MarshalUtility.CopyByteArrayToStructure(new byte[Marshal.SizeOf(typeof(ZGA_BDC_GRP_CONTRACT_SEG))], typeof(ZGA_BDC_GRP_CONTRACT_SEG));
         if (isInitializationRequired)
         {
            CleanConvert.SetByValByteArrayStringField(ref this._segmentName, SegmentNameAsDefined);
            CleanConvert.SetByValByteArrayStringField(ref this._control, string.Empty);
         }
      }
      
      /// <summary>
      ///   Segment Name as defined in the Data Dictionary (at gen-time)
      /// </summary>
      public const string SegmentNameAsDefined = "ZGA_BDC_GRP_CONTRACT_SEG000";
      
      #endregion
   }
}