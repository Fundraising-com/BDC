using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GA.BDC.Core.ESubsGlobal.DirectMail.Object;
using GA.BDC.Core.ESubsGlobal.DirectMail.Document;
using GA.BDC.Core.ESubsGlobal.Users;
using GA.BDC.Core.ESubsGlobal.Common;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace GA.BDC.Core.ESubsGlobal.DirectMail.Processing.Stages
{
    public class GeneratePDFStage: IStage
    {
        #region IStage Members

        public bool Execute(string filename, List<DirectMailInfo> directMailInfos)
        {
            List<int> CompletedDirectMailInfoId = new List<int>();

            foreach (DirectMailInfo directMailInfo in directMailInfos)
            {
                if (CompletedDirectMailInfoId.Contains(directMailInfo.DirectMailInfoId))
                {
                    continue;
                }

                List<DirectMail.Object.DirectMail> directMails = 
                    DirectMail.Object.DirectMail.GetDirectMailInfoById(directMailInfo.DirectMailInfoId);

                foreach (DirectMail.Object.DirectMail directMail in directMails)
                {
                    GA.BDC.Core.ESubsGlobal.DirectMail.Document.CreateMailPdf generateDirectMailPdf =
                        new GA.BDC.Core.ESubsGlobal.DirectMail.Document.CreateMailPdf();

                    GA.BDC.Core.ESubsGlobal.EventParticipation eventParticipation =
                        GA.BDC.Core.ESubsGlobal.EventParticipation.GetEventParticipationByEventParticipationID(directMail.EventParticipationId);

                    GA.BDC.Core.ESubsGlobal.Event e =
                        GA.BDC.Core.ESubsGlobal.Event.GetEventByEventID(eventParticipation.EventID);

                    UnknownUser sender = UnknownUser.LoadByHierarchyID(directMailInfo.MemberHierarchyId);

                    UnknownUser recipient = UnknownUser.LoadByHierarchyID(eventParticipation.MemberHierarchyID);
                    PostalAddress postalAddress = recipient.PostalAddresses[0];

                    string greeting = recipient.Greeting;
                    if (greeting == null)
                    {
                        // set default greeting
                        greeting = System.Configuration.ConfigurationSettings.AppSettings["greeting"];
                    }

                    if (directMailInfo.ImageUrl == null || directMailInfo.ImageUrl == string.Empty)
                    {
                        directMailInfo.ImageUrl = System.Configuration.ConfigurationSettings.AppSettings["defaultImage"];
                    }

                    string imagePath = System.Configuration.ConfigurationSettings.AppSettings["webpath"]; 

                    imagePath = imagePath + directMailInfo.ImageUrl;

                    if (imagePath.ToLower().StartsWith("http://"))
                    {
                        Image image = null;

                        try
                        {
                            imagePath = imagePath.Replace("\\", "/");
                            Image imageDownloaded = DownloadImage(imagePath);
                            String imageFilename = AppDomain.CurrentDomain.BaseDirectory + "\\Temp\\" + System.IO.Path.GetFileName("c:\\" + directMailInfo.ImageUrl);
                            if (!File.Exists(imageFilename))
                                imageDownloaded.Save(imageFilename);
                            imagePath = imageFilename;
                            imageDownloaded.Dispose();
                        }
                        catch (Exception exep)
                        {
                            MessageBox.Show("Unable to find image: " + imagePath);
                            throw exep;
                        }
                    }

                    string postalAddressString = postalAddress.Address1 + (postalAddress.Address2 == null ? "" : " " + postalAddress.Address2);

                    string temporaryPdfFile = generateDirectMailPdf.create(
                        greeting,
                        recipient.Name,
                        sender.Name,
                        e.Name,
                        imagePath,
                        directMailInfo.Message,
                        "It’s so easy for you to help me! Simply order or renew your favorite magazine subscriptions (people, Time and more) at up to 85% off, or enjoy exclusive offers on delicious cookie dough and Restaurant.com eCertificates. For each purchase you make, I’ll get credit for the sale and up to 40% will go back to our group!",
                        "",
                        directMail.EventParticipationId.ToString(),
                        postalAddressString,
                        postalAddress.City,
                        SubDivisionCode.GetSubDivisionDescriptionFromCode(postalAddress.SubDivisionCode),
                        postalAddress.ZipCode,
                        string.Empty,
                        AppDomain.CurrentDomain.BaseDirectory + "\\", //"C:\\DMFiles\\",
                        imagePath,
                        AppDomain.CurrentDomain.BaseDirectory + "\\Temp\\",
                        e.EventID,
                        eventParticipation.EventParticipationID, false);

                    generateDirectMailPdf.Append(AppDomain.CurrentDomain.BaseDirectory + temporaryPdfFile, filename);

                }

                CompletedDirectMailInfoId.Add(directMailInfo.DirectMailInfoId);
            }

            return true;
        }

        /// <summary>
        /// Function to download Image from website
        /// </summary>
        /// <param name="_URL">URL address to download image</param>
        /// <returns>Image</returns>
        public Image DownloadImage(string _URL)
        {
            Image _tmpImage = null;

            try
            {
                // Open a connection
                System.Net.HttpWebRequest _HttpWebRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(_URL);

                _HttpWebRequest.AllowWriteStreamBuffering = true;

                // You can also specify additional header values like the user agent or the referer: (Optional)
                _HttpWebRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1)";
                _HttpWebRequest.Referer = "http://www.google.com/";

                // set timeout for 20 seconds (Optional)
                _HttpWebRequest.Timeout = 20000;

                // Request response:
                System.Net.WebResponse _WebResponse = _HttpWebRequest.GetResponse();

                // Open data stream:
                System.IO.Stream _WebStream = _WebResponse.GetResponseStream();

                // convert webstream to image
                _tmpImage = Image.FromStream(_WebStream);

                // Cleanup
                _WebResponse.Close();
                _WebResponse.Close();
            }
            catch (Exception _Exception)
            {
                // Error
                Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());
                return null;
            }

            return _tmpImage;
        }


        #endregion
    }
}
