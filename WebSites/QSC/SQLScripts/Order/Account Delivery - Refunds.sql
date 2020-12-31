select	CAST(c.ContractID as varchar) + CAST(dbo.CalcMod10Checksum(c.ContractID) as varchar) as GroupOnlineID, conAdd.Name1 AccountName, conAdd.SAPAcctNo GroupNum, conAddBill.SAPAcctNo SponsorNum,
		co.CustomerOrderID, cod.CustomerOrderDetailID, cod.Quantity, cod.OfferCode, cod.OfferValue OfferPrice, coar.Amount TotalRefundAmount, coar.CustomerOrderARID, coar.DateTimeCreated RefundDate
from core.CustomerOrderdetail cod
join core.customersuborder cso on cso.customersuborderid = cod.customersuborderid
join core.customerorder co on co.customerorderid = cso.customerorderid
join core.Tote t on t.toteid = co.toteidcontract
join core.contract c on c.contractid = t.contractid
join core.contractaddress conAdd on conAdd.ContractID = c.ContractID and conAdd.IsSoldTo = 1
join core.contractaddress conAddBill on conAddBill.ContractID = c.ContractID and conAddBill.IsBillTo = 1
join ar.customerenvelopecustomerorder ceco on ceco.customerorderid = co.customerorderid
join ar.customerorderar coar on coar.CustomerEnvelopeID = ceco.CustomerEnvelopeID
where c.divisioncode <> 40
and cod.isshippedtoaccount = 1
and c.ContractTypeID = 3
and coar.IsReversal = 1
and coar.CustomerOrderARID > 14763965
order by coar.CustomerOrderARID