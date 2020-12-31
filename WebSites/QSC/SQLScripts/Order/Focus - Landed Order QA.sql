select *
from core.CustomerOrder co
join core.CustomerSubOrder cso on cso.CustomerOrderID = co.CustomerOrderID
join core.customerorderdetail cod on cod.CustomerSubOrderID = cso.CustomerSubOrderID
join core.Tote t on t.toteid = co.toteidprocess
join core.contract c on c.contractid = t.contractid
left join core.CustomerAddress ca on ca.CustomerAddressID = cso.CustomerAddressID
left join core.supertotetote stt on stt.toteid = t.toteid
left join core.supertote st on st.supertoteid = stt.supertoteid
left join core.sapsqlid s on s.sourceid = st.supertoteid and s.SQLIDTypeID = 6
join core.offerprice op on op.offerpriceid  =cod.offerpriceid
join core.offer o on o.offerid = op.offerid
where co.customerorderid in (79717822)--(75189653,75189654,75189655,75189656,75189657) --Landed TRT
--where t.ToteID in (541927, 541928) --Candle/CD
--where co.customerorderid = 75190180
--where t.ToteID in (541913, 541914, 541916) --Landed Mag
--where t.ToteID in (541870, 541973, 541974) -- TRT RedemptionS (0=G1, 3=G2, 4=GN)
--where t.ToteID in (541896)
order by co.customerorderid

select *
from core.Contract c
join core.ContractAddress ca on ca.ContractID = c.ContractID
where c.ContractID in (370355)

select *
from workflow

select *
from core.Contract
where ContractID between 358800 and 358999
order by ContractID desc

select *
from core.customerorderstate

select * from focus.workflow

Select t.ToteID,t.contractid,t.SwapContractID, b.*
From core.Tote as t
Join core.Contract as c on c.ContractID = t.SWAPContractID
Join core.contractbrochure cb on cb.ContractID = c.ContractID
join core.brochure b on b.BrochureID = cb.BrochureID
Where toteid in (541644,541645,541646,541647)

select *
from core.contractbrochure cb
join core.brochure
where contractid in (360252, 364217, 360260, 360992)

select top 99 *
from Integration.ETLLog
order by ETLLogID desc

