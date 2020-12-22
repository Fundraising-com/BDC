using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PostalAddressFix.Logic
{
    public class FixWorkLogic
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="worker"></param>
        /// <param name="settings"></param>
        public void FixZipCodes(BackgroundWorker worker, FixWorkSettings settings)
        {
            // Get parameters
            OrderExpressDataContext db = new OrderExpressDataContext(settings.ConnectionString);
            OrderExpressDataContext db2 = new OrderExpressDataContext(settings.ConnectionString);
            db.CommandTimeout = 240;

            // Get addresses
            var postalAddresses = (from pa in db.PostalAddresses
                                   where pa.Zip != null
                                   orderby pa.PostalAddressId
                                   select pa
                                   ).Skip(0);

            if (settings.Page.HasValue)
            {
                postalAddresses = postalAddresses.Skip(settings.Page.Value * settings.PageSize).Take(settings.PageSize);
            }

            // Loop variables
            int total = postalAddresses.Count();
            int count = 1;
            int updateResult = 0;

            foreach (PostalAddress postalAddress in postalAddresses)
            {
                #region Fix the zip code

                string newZipCode = string.Empty;

                ZipCodeTypeEnum zipCodeType = this.GetZipCodeType(postalAddress.SubDivisionCode);

                if (zipCodeType == ZipCodeTypeEnum.Unknown)
                {
                }
                else  if (zipCodeType == ZipCodeTypeEnum.USA)
                {
                    newZipCode = ZipCodeFixer.FixUSAZip(postalAddress.Zip);
                }
                else if (zipCodeType == ZipCodeTypeEnum.Canada)
                {
                    newZipCode = ZipCodeFixer.FixCanadaZip(postalAddress.Zip);
                }

                #endregion

                #region Update zip

                if (newZipCode != postalAddress.Zip)
                {
                    if (newZipCode != null)
                    {
                        if (newZipCode != string.Empty)
                        {
                            if (newZipCode.Length > 0)
                            {
                                if (settings.UpdateData)
                                {
                                    updateResult = db2.ExecuteCommand("UPDATE postal_address SET zip = {1} WHERE postal_address_id = {0}", postalAddress.PostalAddressId, newZipCode);

                                    if (settings.DelayInMiliseconds > 0)
                                    {
                                        Thread.Sleep(settings.DelayInMiliseconds);
                                    }
                                }

                                #region Report progress

                                FixWorkState fixWorkState = new FixWorkState
                                {
                                    Count = count,
                                    Total = total,
                                    IsZipValid = newZipCode == postalAddress.Zip,
                                    OriginalZip = postalAddress.Zip,
                                    NewZip = newZipCode,
                                    PostalAddressId = postalAddress.PostalAddressId
                                };

                                worker.ReportProgress(Convert.ToInt32(count / total), fixWorkState);

                                #endregion
                            }
                        }
                    }
                }

                #endregion

                count++;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subdivisionCode"></param>
        /// <returns></returns>
        public ZipCodeTypeEnum GetZipCodeType(string subdivisionCode)
        {
            ZipCodeTypeEnum result = ZipCodeTypeEnum.Unknown;

            if (subdivisionCode != null)
            {
                subdivisionCode = subdivisionCode.Trim();
                subdivisionCode = subdivisionCode.ToUpper();

                if (subdivisionCode.Length >= 2)
                {
                    string countryCode = subdivisionCode.Substring(0, 2);

                    if (countryCode == "US")
                    {
                        result = ZipCodeTypeEnum.USA;
                    }
                    else if (countryCode == "CA")
                    {
                        result = ZipCodeTypeEnum.Canada;
                    }
                }
            }

            return result;
        }

    }
}
