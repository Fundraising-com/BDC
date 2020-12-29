using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GA.BDC.Core.BarCode;
using GA.BDC.Core.ESubsGlobal.DirectMail.Object;
using GA.BDC.Core.ESubsGlobal.Users;
using GA.BDC.Core.ESubsGlobal.Common;

namespace GA.BDC.Core.ESubsGlobal.DirectMail.Processing.Stages
{
    public class Generate2DBarCodeState : IStage
    {
        private const int BARCODE_WIDTH = 560;
        private const int BARCODE_FONT_SIZE = 10;

        //private const int BARCODE_WIDTH = 202;
        //private const int BARCODE_FONT_SIZE = 42;

        #region IStage Members

        public bool Execute(string filename, List<DirectMailInfo> directMailInfos)
        {
            foreach (DirectMailInfo directMailInfo in directMailInfos)
            {
                BarcodeWriter barcodeWriter = new BarcodeWriter();

                barcodeWriter.Width = BARCODE_WIDTH;
                barcodeWriter.FontSize = BARCODE_FONT_SIZE;

                UnknownUser recipient = UnknownUser.LoadByHierarchyID(directMailInfo.MemberHierarchyId);
                PostalAddress postalAddress = recipient.PostalAddresses[0];

                barcodeWriter.Text = postalAddress.ToString();
                barcodeWriter.SaveBarcodeImageToDisk("c:\\DMFiles\\barcode_" + directMailInfo.DirectMailInfoId + ".jpg", false);
            }

            return true;
        }

        #endregion
    }
}
