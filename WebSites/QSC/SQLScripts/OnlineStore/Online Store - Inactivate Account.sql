select *
from core.Contract c
join core.ContractAddress ca on ca.ContractID = c.ContractID
--where SAPContractNo in ('92886','92884','92885','92883','92882','91861','91427','92622','88616')
where ca.SAPAcctNo = '8055'

begin tran
/*update core.Contract
set programstartdate = 20120701, programenddate = 20121231
where SAPContractNo in ('92886','92884','92885','92883','92882','91861','91427','92622','88616')
*/

insert store.contractonlineblock values ()
