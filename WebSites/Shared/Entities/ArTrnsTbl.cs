using System;


namespace GA.BDC.Shared.Entities
{
    public class ArTrnsTbl
    {
        public int trnsid { get; set; }
        public string trnstypecde { get; set; }
        public DateTime? trnsdte { get; set; }
        public int? orgid { get; set; }
        public int? ordrid { get; set; }
        public string opposacctnbr { get; set; }
        public string cashbatchnbr { get; set; }
        public string pmtmethtypecde { get; set; }
        public string pmtmethrefnbr { get; set; }
        public decimal? trnsamt { get; set; }
        public int? jenbr { get; set; }
        public DateTime? qbkspostdte { get; set; }
        public DateTime? lastmodfdte { get; set; }
        public string lastmodfprsncde { get; set; }
        public int? paymentstatusid { get; set; }
        public int? extpaymentid { get; set; }

    }
}
