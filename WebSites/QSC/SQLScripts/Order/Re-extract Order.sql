--know the order(s) that you want to reExport
--go FFS.QSPCanadaOrderManagement
--could be online order
select * from internetorderid where internetorderid in (75189914 ,75189960 )
select * from internetorderid where internetorderid in (75190550, 75190551, 75190552 ) --TRT Redemption
--could be landed order
select * from LandedOrder where LandedOrderID in (75189653,75189654,75189655,75189656,75189657) --Landed TRT
select * from LandedOrder where LandedOrderID between 75189864 and 75189887 or LandedOrderID between 75189916 and 75189931 --Landed Mag
select * from LandedOrder where LandedOrderID between 75190024 and 75190028 or LandedOrderID between 75190030 and 75190034 --Landed CD/Candle

--so, delete from landed or internetorderid tables
begin tran
delete from LandedOrder where LandedOrderID between 75189864 and 75189887 or LandedOrderID between 75189916 and 75189931
delete from LandedOrder where LandedOrderID between 75190024 and 75190028 or LandedOrderID between 75190030 and 75190034 --Landed CD/Candle
OR
delete internetorderid where internetorderid in (75189914 ,75189960 )
delete internetorderid where internetorderid in (75190550, 75190551, 75190552 ) --TRT Redemption

rollback tran

select  Date, ID, AccountID, ORderID from dbo.Batch with(nolock) where orderid in (7571578) --Landed Mag
select  Date, ID, AccountID, ORderID from dbo.Batch with(nolock) where orderid in (7571552) --Landed CD/Candle
select  Date, ID, AccountID, ORderID from dbo.Batch with(nolock) where orderid in (7571918, 7571921, 7571922) --TRT Redemption

begin tran
DELETE from dbo.Batch
where orderid = 7571578
DELETE from dbo.Batch
where orderid = 7571552 --Landed CD/Candle
DELETE from dbo.Batch
where orderid in (7571918, 7571921, 7571922) --TRT Redemption

--commit tran

--ON the GA side
--set the customerorderStateID to 38
select * from core.CustomerOrder where CustomerOrderID between 75189864 and 75189887 or CustomerOrderID between 75189916 and 75189931 --Landed Mag
select * from core.CustomerOrder where CustomerOrderID between 75190024 and 75190028 or CustomerOrderID between 75190030 and 75190034 --Landed CD/Candle
select * from core.CustomerOrder where CustomerOrderID in (75190550, 75190551, 75190552 ) --TRT Redemption

select st.*
from core.SuperTote st with(nolock)
join core.SAPSQLID s with(nolock) on s.SourceID = st.SuperToteID
join core.SuperToteTote stt with(nolock) on stt.SuperToteID = st.SuperToteID
join core.CustomerOrder co with(nolock) on co.ToteIDContract = stt.toteid
where co.CustomerOrderID between 75189864 and 75189887 or CustomerOrderID between 75189916 and 75189931
--where co.CustomerOrderID between 75190024 and 75190028 or CustomerOrderID between 75190030 and 75190034 --Landed CD/Candle
where co.CustomerOrderID in (75190550, 75190551, 75190552 ) --TRT Redemption --Landed CD/Candle

begin tran
update core.CustomerOrder
set CustomerOrderStateID = 38
where CustomerOrderID between 75189864 and 75189887 or CustomerOrderID between 75189916 and 75189931
--where CustomerOrderID between 75190024 and 75190028 or CustomerOrderID between 75190030 and 75190034 --Landed CD/Candle
where CustomerOrderID in (75190550, 75190551, 75190552 ) --TRT Redemption --Landed CD/Candle


update core.SuperTote
set SAPTransmittedDate = null
where SuperToteID = 5628

update core.SalesOrder
set  SAPTransmittedDate = null
where SalesOrderID in (2105823, 2105824, 2105825) --TRT Redemptions

--rollback tran

--ready for reExport
--end - 