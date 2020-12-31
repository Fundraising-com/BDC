USE [QSPCanadaOrderManagement]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_InterfaceLayout33009]    Script Date: 06/07/2017 09:21:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[UDF_InterfaceLayout33009]

	(@RemitBatchID INT)

RETURNS TABLE
AS
RETURN

(select top 9999 textline
from (select '41'+
case when left(codrh.TitleCode,1) = 'S' and codrh.TitleCode not in (SELECT Product_Code FROM QSPCanadaProduct..Product WHERE (Pub_Nbr = 42 OR Product_Code = 'S210' OR Fulfill_House_Nbr IN (7, 258, 281, 207))) then 'WH  ' when left(codrh.TitleCode,1) <> 'S' and codrh.TitleCode in (SELECT Product_Code FROM QSPCanadaProduct..Product WHERE Pub_Nbr = 999) then 'D2  ' when left(codrh.TitleCode,1) = 'S' and codrh.TitleCode in (SELECT Product_Code FROM QSPCanadaProduct..Product WHERE (Fulfill_House_Nbr IN (7))) then 'PE  ' else cast(coalesce(fh.QSPAgencyCode, '') as char(4)) end +
'0000000000000'+
cast(REPLACE(LTRIM(REPLACE(codrh.RemitCode, '0', ' ')), ' ', '0') as char(5)) +
right('000'+cast(coalesce(codrh.NumberOfIssues,0) as varchar),3)+
'00001'+
right('0000000'+cast(cast(coalesce(codrh.BasePrice,0)*1000 as int) as varchar),7)+
right('0000000'+cast(cast(round(coalesce(codrh.RemitRate*codrh.BasePrice + CASE WHEN fh.Ful_Nbr IN (55,163,258) THEN coalesce(codrh.Tax,0) + coalesce(codrh.Tax2,0) ELSE 0 END,0),3)*1000 as int) as varchar),7)+
coalesce(codrh.ABCCode,'1')+
' '+
coalesce(cast(codrh.PremiumIndicator as char(1)),' ')+
cast(coalesce(codrh.PremiumDescription,'') as char(20))+
cast(datepart(yyyy,rb.Date) as varchar)+right('0'+cast(datepart(mm,rb.Date) as varchar), 2)+right('0'+cast(datepart(dd,rb.Date) as varchar),2)+
'00000000000 0000000000'+
cast(rtrim(coalesce(old.FirstName,''))+' '+rtrim(coalesce(old.LastName,'')) as char(24))+
space(6)+
cast(coalesce(old.Address1,'') as char(24))+
space(6)+
cast(coalesce(old.Address2,'') as char(24))+
space(36)+
cast(coalesce(old.City,'') as char(15))+
cast(coalesce(old.State,'') as char(2))+
'CAN'+
cast(coalesce(old.Zip,'') as char(9))+
space(95)+
cast(coalesce(codrh.EffortKey,'') as char(9))+
space(31)+
'1'+
case when fh.Ful_Nbr NOT IN (7, 207) then space(66) else space(336) + cast(coalesce(pd.ListAgentCode, '') as char(5)) end
as textline,
codrh.CustomerOrderHeaderInstance,
codrh.TransId,
codrh.RemitBatchId
from RemitBatch rb,
QSPCanadaProduct.dbo.Fulfillment_House fh,
CustomerRemitHistory crh,
(select crha.*
from CustomerRemitHistoryAudit crha,
(select crha.RemitBatchId,crha.Instance,max(crha.AuditDate) as AuditDate
from CustomerRemitHistoryAudit crha
group by crha.RemitBatchId,crha.Instance) crhamax
where crha.RemitBatchId=crhamax.RemitBatchId
and crha.Instance=crhamax.Instance
and crha.AuditDate=crhamax.AuditDate) old,
CustomerOrderDetailRemitHistory codrh
JOIN CustomerOrderDetail cod on cod.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance and cod.TransID = codrh.TransID
JOIN QSPCanadaProduct..Pricing_Details pd on pd.MagPrice_Instance = cod.PricingDetailsID 
where rb.ID=@RemitBatchId
and rb.Status=42000
and fh.Ful_Nbr=rb.FulfillmentHouseNbr
and crh.RemitBatchId=rb.Id
and crh.StatusInstance=42006
and old.RemitBatchId=crh.RemitBatchId
and old.Instance=crh.Instance
and codrh.RemitBatchId=crh.RemitBatchId
and codrh.CustomerRemitHistoryInstance=crh.Instance
and codrh.Status=42006
union all select '42'+
 case when left(codrh.TitleCode,1) = 'S' and codrh.TitleCode not in (SELECT Product_Code FROM QSPCanadaProduct..Product WHERE (Pub_Nbr = 42 OR Product_Code = 'S210' OR Fulfill_House_Nbr IN (7, 258, 281))) then 'WH  ' when left(codrh.TitleCode,1) <> 'S' and codrh.TitleCode in (SELECT Product_Code FROM QSPCanadaProduct..Product WHERE Pub_Nbr = 42) then 'D2  ' when left(codrh.TitleCode,1) = 'S' and codrh.TitleCode in (SELECT Product_Code FROM QSPCanadaProduct..Product WHERE (Fulfill_House_Nbr IN (7))) then 'PE  ' else cast(coalesce(fh.QSPAgencyCode, '') as char(4)) end +
'0000000000000'+
cast(REPLACE(LTRIM(REPLACE(codrh.RemitCode, '0', ' ')), ' ', '0') as char(5)) +
right('000'+cast(coalesce(codrh.NumberOfIssues,0) as varchar),3)+
'00001'+
right('0000000'+cast(cast(coalesce(codrh.BasePrice,0)*1000 as int) as varchar),7)+
right('0000000'+cast(cast(round(coalesce(codrh.RemitRate*codrh.BasePrice,0),3)*1000 as int) as varchar),7)+
coalesce(codrh.ABCCode,'1')+
' '+
coalesce(cast(codrh.PremiumIndicator as char(1)),' ')+
cast(coalesce(codrh.PremiumDescription,'') as char(20))+
cast(datepart(yyyy,rb.Date) as varchar)+right('0'+cast(datepart(mm,rb.Date) as varchar),2)+right('0'+cast(datepart(dd,rb.Date) as varchar),2)+
'00000000000 0000000000'+
cast(rtrim(coalesce(crh.FirstName,''))+' '+rtrim(coalesce(crh.LastName,'')) as char(24))+
space(6)+
cast(coalesce(crh.Address1,'') as char(24))+
space(6)+
cast(coalesce(crh.Address2,'') as char(24))+
space(36)+
cast(coalesce(crh.City,'') as char(15))+
cast(coalesce(crh.State,'') as char(2))+
'CAN'+
cast(coalesce(crh.Zip,'') as char(9))+
space(95)+
cast(coalesce(codrh.EffortKey,'') as char(9))+
space(31)+
'1'+
case when fh.Ful_Nbr NOT IN (7, 207) then space(66) else space(336) + cast(coalesce(pd.ListAgentCode, '') as char(5)) end
as textline,
codrh.CustomerOrderHeaderInstance,
codrh.TransId,
codrh.RemitBatchId
from RemitBatch rb,
QSPCanadaProduct.dbo.Fulfillment_House fh,
CustomerRemitHistory crh,
CustomerOrderDetailRemitHistory codrh
JOIN CustomerOrderDetail cod on cod.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance and cod.TransID = codrh.TransID
JOIN QSPCanadaProduct..Pricing_Details pd on pd.MagPrice_Instance = cod.PricingDetailsID 
where rb.ID=@RemitBatchId
and rb.Status=42000
and fh.Ful_Nbr=rb.FulfillmentHouseNbr
and crh.RemitBatchId=rb.Id
and crh.StatusInstance=42006
and codrh.RemitBatchId=crh.RemitBatchId
and codrh.CustomerRemitHistoryInstance=crh.Instance
and codrh.Status=42006) xyz
order by CustomerOrderHeaderInstance,TransId,RemitBatchId,left(textline,2)
)
GO
