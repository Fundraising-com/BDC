﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EfundraisingCRM.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://webservices.efundraising.com/paymentech.asmx")]
        public string EfundraisingCRM_com_efundraising_webservices_PaymenTech {
            get {
                return ((string)(this["EfundraisingCRM_com_efundraising_webservices_PaymenTech"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://wsi.qsp.com/qpay/bpps.asmx")]
        public string EfundraisingCRM_ePay_WebReference_BatchPaymentSystemWebservice {
            get {
                return ((string)(this["EfundraisingCRM_ePay_WebReference_BatchPaymentSystemWebservice"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://wsi.qsp.com/QSP.AddressHygiene/AddressHygieneContract.asmx")]
        public string EfundraisingCRM_com_qsp_staging_wsi_AddressHygieneContract {
            get {
                return ((string)(this["EfundraisingCRM_com_qsp_staging_wsi_AddressHygieneContract"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://dev-wsi.qsp.com/qpay/bpps.asmx")]
        public string EfundraisingCRM_com_qsp_dev_wsi_BatchPaymentSystemWebservice {
            get {
                return ((string)(this["EfundraisingCRM_com_qsp_dev_wsi_BatchPaymentSystemWebservice"]));
            }
        }
    }
}
