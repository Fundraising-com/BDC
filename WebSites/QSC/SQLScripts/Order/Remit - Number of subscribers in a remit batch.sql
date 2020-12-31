select	cod.recipient, c.instance
from	customerorderdetail cod,
		customerorderheader coh,
		remitbatch rb,
		customerorderdetailremithistory codrh,
		customerremithistory crh,
		customer c
where	rb.id = codrh.remitbatchid
and		cod.customerorderheaderinstance = coh.instance
and		codrh.remitbatchid = rb.id
and		codrh.customerorderheaderinstance = cod.customerorderheaderinstance
and		codrh.transid = cod.transid
and		rb.runid = 1205
and		codrh.status = 42001
and		crh.instance = codrh.customerremithistoryinstance
and		coh.customerbilltoinstance = c.instance
group by cod.recipient, c.instance

select * from qspcanadacommon..codedetail
select * from customerremithistory
select top 100 * from customerorderdetailremithistory
select * from customer