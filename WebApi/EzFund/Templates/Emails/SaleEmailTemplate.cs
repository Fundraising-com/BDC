﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 16.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace GA.BDC.WebApi.EzFund.Templates.Emails
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System.Globalization;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "C:\SourceCode\Southwestern-Fundraising\1.DEV\GA\BDC\WebApi\EzFund\Templates\Emails\SaleEmailTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public partial class SaleEmailTemplate : SaleEmailTemplateBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\">\r\n<html>\r\n<head>\r\n " +
                    "   <meta http-equiv=\"content-type\" content=\"text/html; charset=ISO-8859-1\">\r\n   " +
                    " <meta content=\"MSHTML 6.00.2800.1498\" name=\"GENERATOR\">\r\n    <title>");
            
            #line 12 "C:\SourceCode\Southwestern-Fundraising\1.DEV\GA\BDC\WebApi\EzFund\Templates\Emails\SaleEmailTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Subject));
            
            #line default
            #line hidden
            this.Write(@"</title>
</head>
<body bgcolor=""#FAFAFA"" style=""-webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; height: 100% !important; width: 100% !important; background-color: #FAFAFA; margin: 0; padding: 0;"">
   <center>
     
      <table width=""800"">
         <tr>
            <td width=""50%"" style=""vertical-align: top;"">
               <table width=""100%"" style=""border: solid 2px #DAF7A6;"">
                  <thead>
                     <tr>
                        <th colspan=""2"" style=""background-color: #FFC300; font-family: Arial; font-size: 12px; text-align: center;"">
                           <strong>SALES</strong>
                        </th>
                     </tr>
                  </thead>
                  <tbody>
                     <tr>
                        <td style=""font-family: Arial; font-size: 12px;"">
                           <strong>Total:</strong>
                        </td>
                        <td style=""font-family: Arial; font-size: 12px;"">
                           ");
            
            #line 34 "C:\SourceCode\Southwestern-Fundraising\1.DEV\GA\BDC\WebApi\EzFund\Templates\Emails\SaleEmailTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(sales.TotalAmount.ToString("C")));
            
            #line default
            #line hidden
            this.Write(@"
                        </td>
                     </tr>
                     <tr>
                        <td style=""font-family: Arial; font-size: 12px;"">
                           <strong>Payment Method:</strong>
                        </td>
                        <td style=""font-family: Arial; font-size: 12px;"">
                           ");
            
            #line 42 "C:\SourceCode\Southwestern-Fundraising\1.DEV\GA\BDC\WebApi\EzFund\Templates\Emails\SaleEmailTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(sales.InternalPaymentMethod));
            
            #line default
            #line hidden
            this.Write(@"
                        </td>
                     </tr>
                    
                        <tr>
                           <td colspan=""2"">
                              <table width=""100%"" style=""border: solid 1px #FF5733;"">
                                 <tr>
                                    <td style=""font-family: Arial; font-size: 10px;"" width=""30%"">
                                       <strong>Sale Id:</strong>
                                    </td>
                                    <td style=""font-family: Arial; font-size: 10px;"" width=""70%"">
                                       ");
            
            #line 54 "C:\SourceCode\Southwestern-Fundraising\1.DEV\GA\BDC\WebApi\EzFund\Templates\Emails\SaleEmailTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(sales.OrderId));
            
            #line default
            #line hidden
            this.Write(@"
                                    </td>
                                 </tr>
                                 <tr>
                                    <td style=""font-family: Arial; font-size: 10px;"" width=""30%"">
                                       <strong>Products:</strong>
                                    </td>
                                    <td style=""font-family: Arial; font-size: 10px;"" width=""70%"">
                                       ");
            
            #line 62 "C:\SourceCode\Southwestern-Fundraising\1.DEV\GA\BDC\WebApi\EzFund\Templates\Emails\SaleEmailTemplate.tt"
 foreach(var items in sales.SubProducts) { 
            
            #line default
            #line hidden
            this.Write("                                          ");
            
            #line 63 "C:\SourceCode\Southwestern-Fundraising\1.DEV\GA\BDC\WebApi\EzFund\Templates\Emails\SaleEmailTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(items.SelectedQuantity));
            
            #line default
            #line hidden
            this.Write(" item(s) of ");
            
            #line 63 "C:\SourceCode\Southwestern-Fundraising\1.DEV\GA\BDC\WebApi\EzFund\Templates\Emails\SaleEmailTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(items.Name));
            
            #line default
            #line hidden
            this.Write("\r\n                                       ");
            
            #line 64 "C:\SourceCode\Southwestern-Fundraising\1.DEV\GA\BDC\WebApi\EzFund\Templates\Emails\SaleEmailTemplate.tt"
 } 
            
            #line default
            #line hidden
            this.Write(@"                                    </td>
                                 </tr>
                              </table>
                              <br>
                           </td>
                        </tr>
                   
                  </tbody>
               </table>
			   
            </td>
            <td width=""50%"" style=""vertical-align: top;"">
               
               <table width=""100%"" style=""border: solid 2px #DAF7A6;"">
                  <thead>
                     <tr>
                        <th colspan=""2"" style=""background-color: #FFC300; font-family: Arial; font-size: 12px; text-align: center;"">
                           <strong>Client</strong>
                        </th>
                     </tr>
                  </thead>
                  <tbody>
                     <tr>
                        <td style=""font-family: Arial; font-size: 12px;"">
                           <strong>Organization Id:</strong>
                        </td>
                        <td style=""font-family: Arial; font-size: 12px;"">
                           ");
            
            #line 92 "C:\SourceCode\Southwestern-Fundraising\1.DEV\GA\BDC\WebApi\EzFund\Templates\Emails\SaleEmailTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(sales.OrganizationId));
            
            #line default
            #line hidden
            this.Write(@"
                        </td>
                     </tr>
                     <tr>
                        <td style=""font-family: Arial; font-size: 12px;"">
                           <strong>Name:</strong>
                        </td>
                        <td style=""font-family: Arial; font-size: 12px;"">
                           ");
            
            #line 100 "C:\SourceCode\Southwestern-Fundraising\1.DEV\GA\BDC\WebApi\EzFund\Templates\Emails\SaleEmailTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(sales.Client.FirstName));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 100 "C:\SourceCode\Southwestern-Fundraising\1.DEV\GA\BDC\WebApi\EzFund\Templates\Emails\SaleEmailTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(sales.Client.LastName));
            
            #line default
            #line hidden
            this.Write(@"
                        </td>
                     </tr>
                     <tr>
                        <td style=""font-family: Arial; font-size: 12px;"">
                           <strong>Email:</strong>
                        </td>
                        <td style=""font-family: Arial; font-size: 12px;"">
                           ");
            
            #line 108 "C:\SourceCode\Southwestern-Fundraising\1.DEV\GA\BDC\WebApi\EzFund\Templates\Emails\SaleEmailTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(sales.Client.Email));
            
            #line default
            #line hidden
            this.Write(@"
                        </td>
                     </tr>
                     <tr>
                        <td style=""font-family: Arial; font-size: 12px;"">
                           <strong>Phone:</strong>
                        </td>
                        <td style=""font-family: Arial; font-size: 12px;"">
                           ");
            
            #line 116 "C:\SourceCode\Southwestern-Fundraising\1.DEV\GA\BDC\WebApi\EzFund\Templates\Emails\SaleEmailTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(sales.Client.Phone));
            
            #line default
            #line hidden
            this.Write(@"
                        </td>
                     </tr>
					 <tr>
                        <td style=""font-family: Arial; font-size: 12px;"">
                           <strong>Referral:</strong>
                        </td>
                        <td style=""font-family: Arial; font-size: 12px;"">
                           ");
            
            #line 124 "C:\SourceCode\Southwestern-Fundraising\1.DEV\GA\BDC\WebApi\EzFund\Templates\Emails\SaleEmailTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(sales.ReferralCode));
            
            #line default
            #line hidden
            this.Write(@"
                        </td>
                     </tr>
                  </tbody>
               </table>
               <br><br>
			   <table width=""100%"" style=""border: solid 2px #DAF7A6;"">
                  <thead>
                     <tr>
                        <th colspan=""2"" style=""background-color: #FFC300; font-family: Arial; font-size: 12px; text-align: center;"">
                           <strong>Billing</strong>
                        </th>
                     </tr>
                  </thead>
                  <tbody>
                     <tr>
                        <td style=""font-family: Arial; font-size: 12px;"">
                           ");
            
            #line 141 "C:\SourceCode\Southwestern-Fundraising\1.DEV\GA\BDC\WebApi\EzFund\Templates\Emails\SaleEmailTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(sales.Client.FirstName));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 141 "C:\SourceCode\Southwestern-Fundraising\1.DEV\GA\BDC\WebApi\EzFund\Templates\Emails\SaleEmailTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(sales.Client.LastName));
            
            #line default
            #line hidden
            this.Write("<br />\r\n\t\t\t\t\t\t   ");
            
            #line 142 "C:\SourceCode\Southwestern-Fundraising\1.DEV\GA\BDC\WebApi\EzFund\Templates\Emails\SaleEmailTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(sales.Client.Addresses[0].Address1));
            
            #line default
            #line hidden
            this.Write("<br />\r\n\t\t\t\t\t\t   ");
            
            #line 143 "C:\SourceCode\Southwestern-Fundraising\1.DEV\GA\BDC\WebApi\EzFund\Templates\Emails\SaleEmailTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(sales.Client.Addresses[0].Address2));
            
            #line default
            #line hidden
            this.Write("<br />\r\n\t\t\t\t\t\t   ");
            
            #line 144 "C:\SourceCode\Southwestern-Fundraising\1.DEV\GA\BDC\WebApi\EzFund\Templates\Emails\SaleEmailTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(sales.Client.Addresses[0].City));
            
            #line default
            #line hidden
            this.Write(" (");
            
            #line 144 "C:\SourceCode\Southwestern-Fundraising\1.DEV\GA\BDC\WebApi\EzFund\Templates\Emails\SaleEmailTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(sales.Client.Addresses[0].Region.Code));
            
            #line default
            #line hidden
            this.Write(") ");
            
            #line 144 "C:\SourceCode\Southwestern-Fundraising\1.DEV\GA\BDC\WebApi\EzFund\Templates\Emails\SaleEmailTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(sales.Client.Addresses[0].PostCode));
            
            #line default
            #line hidden
            this.Write(@"

                        </td>
                        
                     </tr>
                     
                  </tbody>
               </table>
			   <br><br>
			   <table width=""100%"" style=""border: solid 2px #DAF7A6;"">
                  <thead>
                     <tr>
                        <th colspan=""2"" style=""background-color: #FFC300; font-family: Arial; font-size: 12px; text-align: center;"">
                           <strong>Shipping</strong>
                        </th>
                     </tr>
                  </thead>
                  <tbody>
                     <tr>
                        <td style=""font-family: Arial; font-size: 12px;"">
                           ");
            
            #line 164 "C:\SourceCode\Southwestern-Fundraising\1.DEV\GA\BDC\WebApi\EzFund\Templates\Emails\SaleEmailTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(sales.Client.FirstName));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 164 "C:\SourceCode\Southwestern-Fundraising\1.DEV\GA\BDC\WebApi\EzFund\Templates\Emails\SaleEmailTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(sales.Client.LastName));
            
            #line default
            #line hidden
            this.Write("<br />\r\n\t\t\t\t\t\t   ");
            
            #line 165 "C:\SourceCode\Southwestern-Fundraising\1.DEV\GA\BDC\WebApi\EzFund\Templates\Emails\SaleEmailTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(sales.Client.Addresses[1].Address1));
            
            #line default
            #line hidden
            this.Write("<br />\r\n\t\t\t\t\t\t   ");
            
            #line 166 "C:\SourceCode\Southwestern-Fundraising\1.DEV\GA\BDC\WebApi\EzFund\Templates\Emails\SaleEmailTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(sales.Client.Addresses[1].Address2));
            
            #line default
            #line hidden
            this.Write("<br />\r\n\t\t\t\t\t\t   ");
            
            #line 167 "C:\SourceCode\Southwestern-Fundraising\1.DEV\GA\BDC\WebApi\EzFund\Templates\Emails\SaleEmailTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(sales.Client.Addresses[1].City));
            
            #line default
            #line hidden
            this.Write(" (");
            
            #line 167 "C:\SourceCode\Southwestern-Fundraising\1.DEV\GA\BDC\WebApi\EzFund\Templates\Emails\SaleEmailTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(sales.Client.Addresses[1].Region.Code));
            
            #line default
            #line hidden
            this.Write(") ");
            
            #line 167 "C:\SourceCode\Southwestern-Fundraising\1.DEV\GA\BDC\WebApi\EzFund\Templates\Emails\SaleEmailTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(sales.Client.Addresses[1].PostCode));
            
            #line default
            #line hidden
            this.Write("<br />\r\n\t\t\t\t\t\t  Delivery Information: ");
            
            #line 168 "C:\SourceCode\Southwestern-Fundraising\1.DEV\GA\BDC\WebApi\EzFund\Templates\Emails\SaleEmailTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(sales.DeliveryComments));
            
            #line default
            #line hidden
            this.Write("\r\n\t\t\t\t\t\t</td>\r\n                        </tr>\r\n                     \r\n            " +
                    "      </tbody>\r\n               </table>\r\n\r\n\r\n\r\n               \r\n            </td" +
                    ">\r\n         </tr>\r\n      </table>\r\n   </center>\r\n</body>\r\n</html>");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public class SaleEmailTemplateBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}
