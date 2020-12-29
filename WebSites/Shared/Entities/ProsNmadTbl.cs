using System;
using System.Collections.Generic;


namespace GA.BDC.Shared.Entities
{
    public class ProsNmadTbl
    {
        public int SeqNbr { get; set; }
        public string CtctNme { get; set; }
        public string CtctTitlTxt { get; set; }
        public string OrgNme { get; set; }
        public string Addr1Txt { get; set; }
        public string Addr2Txt { get; set; }
        public string CityNme { get; set; }
        public string StCde { get; set; }
        public string ZipCde { get; set; }
        public string EmlTxt { get; set; }
        public string Ph1Nbr { get; set; }
        public string Ph2Nbr { get; set; }
        public string FaxNbr { get; set; }
        public string OrgMembQtyTxt { get; set; }
        public int OrgMembQty { get; set; }
        public string TargPrftAmtTxt { get; set; }
        public string UnitSlsSizeTxt { get; set; }
        public string SlsStrtTxt { get; set; }
        public int SlsInqQty { get; set; }
        public string CmntTxt { get; set; }
        public string SrcCde { get; set; }
        public int SrcSeqNbr { get; set; }
        public string RfrlCde { get; set; }
        public DateTime? OrigProsDte { get; set; }
        public string SessIdNbr { get; set; }
        public string RmtIpAddr { get; set; }
        public string ProsStatCdc { get; set; }
        public DateTime? LastModfDte { get; set; }
        public string LastModfPrsnCde { get; set; }
        public string ProcMailDte { get; set; }
        public string SlspRfrlCde { get; set; }
        public string ReferralUrl { get; set; }
    }
}
