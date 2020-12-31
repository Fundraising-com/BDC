USE GA
GO

/*
select *
from core.tote t
join core.contract c on c.contractid = t.contractid
where c.sapcontractno = '41023713'
*/

declare @ToteID int
declare @OnlineTotes table (ToteID int)
declare @StartDate date
declare @ItemCount int

declare @LandedItems table (SAPContractNo int,ItemID int, ItemDescShort varchar(60))
declare @MissingItems table (CustomerOrderDetailID int, ItemID int, ItemDescShort varchar(60),SAPMaterialNo varchar(18), OfferPrice money)

set @ToteID = 815392  

insert @OnlineTotes
select distinct tol.toteid from core.Tote t
join core.ContractAddress ca on ca.ContractID = t.ContractID and ca.IsSoldTo = 1
join core.ContractAddress cao on cao.SAPAcctNo = ca.SAPAcctNo and cao.IsSoldTo = 1
join core.Contract c on c.ContractID = cao.ContractID and c.ContractTypeID = 3
join core.Tote tol on tol.ContractID = c.ContractID and tol.WorkflowID = 14
where t.ToteID = @ToteID

select @StartDate = DATEADD(DAY,-14, dbo.ConvertIntYYYYMMDDtoDate(c.ProgramStartDate))
 from core.Tote t
join core.Contract c on c.ContractID = t.ContractID
where t.ToteID = @ToteID

insert @LandedItems
select distinct c.SAPContractNo, i.ItemID, i.ItemDescShort
from core.Tote t
join core.ToteContractBrochure tcb on tcb.ToteID = t.ToteID
join core.Contract c on c.ContractID = t.contractid
join core.ContractBrochure cb on cb.ContractBrochureID = tcb.ContractBrochureID
join core.BrochureOffer bo on bo.BrochureID = cb.BrochureID
join core.Item i on i.ItemID = bo.itemid
join core.ItemType it on it.Code = i.ItemTypeID
where t.ToteID = @ToteID and it.IsAplusReportMagazine = 0
and dbo.ConverDateTOintYYYYMMDD(getdate()) between bo.EffectiveBegin and bo.EffectiveEnd
  and dbo.ConverDateTOintYYYYMMDD(getdate()) between cb.EffectiveBegin and cb.EffectiveEnd

select distinct SAPContractNo from @LandedItems

select 'Available Landed Items',* from @LandedItems order by ItemID

insert @MissingItems
select  cod.CustomerOrderDetailID, i.ItemID, i.ItemDescShort,i.SAPID, op.offerprice
from @OnlineTotes ot
join core.CustomerOrder co on co.ToteIDContract = ot.ToteID	
join core.CustomerSubOrder cso on cso.CustomerOrderID = co.CustomerOrderID
join core.CustomerOrderDetail cod on cod.CustomerSubOrderID = cso.CustomerSubOrderID
join core.OfferPrice op on op.OfferPriceID = cod.OfferPriceID
join core.BrochureOffer bo on bo.BrochureID = cod.BrochureID and bo.OfferID = op.OfferID
join core.Item i on i.ItemID = bo.ItemID
join core.ItemType it on it.Code = i.ItemTypeID
left join @LandedItems li on li.ItemID = i.ItemID
where it.IsEligibleForShippingToAccount = 1 and cod.IsShippedToAccount = 1 and li.ItemID is null
  and co.SearchDate >= @StartDate
  and co.CustomerOrderStateID IN (11)

select @ItemCount =  COUNT(*) from @MissingItems

select 'Missing Items', * from @MissingItems

Select 'Current Landed Brochures', b.BrochureID, b.SAPID, b.BrochureName, cb.EffectiveBegin, cb.EffectiveEnd, tcb.ContractBrochureID
 from core.Tote t
join core.ContractBrochure cb on cb.ContractID = t.ContractID
join core.Brochure b on b.BrochureID = cb.BrochureID
left join core.ToteContractBrochure tcb on tcb.ContractBrochureID = cb.ContractBrochureID
where t.ToteID = @ToteID

select b.SAPID, b.BrochureName, mi.CustomerOrderDetailID, co.CustomerOrderID, s.Firstname StudentFirstName, s.Lastname StudentLastName, ca.FirstName CustomerFirstName, ca.LastName CustomerLastName
from @MissingItems mi
join core.BrochureOffer bo on bo.ItemID = mi.ItemID
join core.Brochure b on b.BrochureID = bo.BrochureID
join core.ProgramType pt on pt.ProgramTypeID = b.ProgramTypeID
join core.CustomerOrderDetail cod on cod.CustomerOrderDetailID = mi.CustomerOrderDetailID
join core.CustomerSubOrder cso on cso.CustomerSubOrderID = cod.CustomerSubOrderID
join core.CustomerOrder co on co.CustomerOrderID = cso.CustomerOrderID
join core.Student s on s.studentid= cod.studentid
join core.CustomerAddress ca on ca.CustomerAddressID = cso.CustomerAddressID
where dbo.ConverDateTOintYYYYMMDD(getdate()) between bo.EffectiveBegin and bo.EffectiveEnd
  and dbo.ConverDateTOintYYYYMMDD(getdate()) between b.EffectiveBegin and b.EffectiveEnd
and (pt.SAPID like 'B%' or pt.SAPID like 'F%')
--group by b.SAPID, b.BrochureName
--having COUNT(distinct bo.itemid) = @ItemCount
order by b.BrochureName, co.CustomerOrderID
--order by cod.CustomerOrderDetailID, b.BrochureName, co.CustomerOrderID

select distinct cod.CustomerOrderDetailID, co.CustomerOrderID, i.ItemID, i.ItemDescShort,i.SAPID, op.offerprice, bL.BrochureName, bL.BrochureID
from @OnlineTotes ot
join core.CustomerOrder co on co.ToteIDContract = ot.ToteID	
join core.CustomerSubOrder cso on cso.CustomerOrderID = co.CustomerOrderID
join core.CustomerOrderDetail cod on cod.CustomerSubOrderID = cso.CustomerSubOrderID
join core.OfferPrice op on op.OfferPriceID = cod.OfferPriceID
join core.BrochureOffer bo on bo.BrochureID = cod.BrochureID and bo.OfferID = op.OfferID
join core.Item i on i.ItemID = bo.ItemID
join core.ItemType it on it.Code = i.ItemTypeID
join core.BrochureOffer boL on boL.ItemID = i.ItemID
								and dbo.ConverDateTOintYYYYMMDD(getdate()) between boL.EffectiveBegin and boL.EffectiveEnd
join core.Brochure bL on bL.BrochureID = boL.BrochureID
								and dbo.ConverDateTOintYYYYMMDD(getdate()) between bL.EffectiveBegin and bL.EffectiveEnd
join core.ProgramType ptL on ptL.ProgramTypeID = bL.ProgramTypeID
								and (substring(ptL.SAPID,1,1) in ('B','F'))
join core.OfferPrice opL on opL.OfferID = boL.OfferID and opL.OfferPrice = op.OfferPrice and opL.PriceCodeID = op.PriceCodeID
join @MissingItems mi on mi.CustomerOrderDetailID = cod.CustomerOrderDetailID
where it.IsEligibleForShippingToAccount = 1 
  and cod.IsShippedToAccount = 1
  and co.SearchDate >= @StartDate
  and co.CustomerOrderStateID IN (11)
  and cod.IsShippedToAccount = 1
order by cod.CustomerOrderDetailID

---Ensure Brochure is in SiteTable
select *
from core.customerorderdetail cod
join core.brochure b on b.brochureid = cod.brochureid
left join store.sitebrochure sb on sb.brochureid = b.brochureid
where cod.CustomerOrderDetailID = 193856136


