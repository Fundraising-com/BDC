﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GA.BDC.Core.EmailVerification.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class StrikeIronMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal StrikeIronMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("GA.BDC.Core.EmailVerification.Properties.StrikeIronMessages", typeof(StrikeIronMessages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;MonitorVerification&apos; is enabled but you have not specified a valid value for &apos;AnnualQuota&apos;. Please specify an integer value for it..
        /// </summary>
        internal static string MissingAnnualQuota {
            get {
                return ResourceManager.GetString("MissingAnnualQuota", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to StrikeIron Email Verification Error : {0}.
        /// </summary>
        internal static string ValidationError {
            get {
                return ResourceManager.GetString("ValidationError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;ValidationRequest&apos; object requires a valid email address.
        /// </summary>
        internal static string ValidationRequestMissingEmailError {
            get {
                return ResourceManager.GetString("ValidationRequestMissingEmailError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A &apos;ValidationRequest&apos; object needs to be instantiated.
        /// </summary>
        internal static string ValidationRequestNullError {
            get {
                return ResourceManager.GetString("ValidationRequestNullError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0}.
        /// </summary>
        internal static string ValidationResponseDescription {
            get {
                return ResourceManager.GetString("ValidationResponseDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0}: {1} ({2}: {3}).
        /// </summary>
        internal static string ValidationResponseFullDescription {
            get {
                return ResourceManager.GetString("ValidationResponseFullDescription", resourceCulture);
            }
        }
    }
}
