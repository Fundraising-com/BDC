USE [QSPCanadaOrderManagement]
GO
/****** Object:  View [dbo].[vw_GetAllStatusByCODRH]    Script Date: 06/07/2017 09:18:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[vw_GetAllStatusByCODRH] AS


select  /*ABCCode,
BasePrice,
CatalogPrice,
Comment,
CountryCode,
CurrencyID,*/
CustomerOrderHeaderInstance,
CustomerRemitHistoryInstance,
rb.Date as DateChanged,
/*DefaultGrossValue,
EffortKey,
GiftCardDateGenerated,
GiftOrderStatus,
GiftOrderType,
ItemPriceTotal,
Lang,
MagazineTitle,
NumberOfIssues,
PremiumCode,
PremiumDescription,
PremiumIndicator,
Quantity,*/
rb.runid as RemitBatchID,
RemitRate,
Renewal,
codrh.Status,
SupporterName,
SwitchLetterBatchID,
TitleCode,
TransID,
codrh.UserIDChanged, cd.Description as StatusDescription 
from 
CustomerOrderDetailRemitHistory codrh,CodeDetail cd,remitbatch rb

where 
cd.Instance = codrh.Status 
and rb.id = codrh.remitbatchid
/*
union all 

select  ABCCode,
BasePrice,
CatalogPrice,
Comment,
CountryCode,
CurrencyID,
CustomerOrderHeaderInstance,
CustomerRemitHistoryInstance,
AuditDate as DateChanged,
DefaultGrossValue,
EffortKey,
GiftCardDateGenerated,
GiftOrderStatus,
GiftOrderType,
ItemPriceTotal,
Lang,
MagazineTitle,
NumberOfIssues,
PremiumCode,
PremiumDescription,
PremiumIndicator,
Quantity,
rb.runid as RemitBatchID,
RemitRate,
Renewal,
codrh.Status,
SupporterName,
SwitchLetterBatchID,
TitleCode,
TransID,
codrh.UserIDChanged, cd.Description as StatusDescription 
from 
CustomerOrderDetailRemitHistoryAudit codrh,CodeDetail cd,remitbatch rb

where 
cd.Instance = codrh.Status 
and rb.id = codrh.remitbatchid
*/
GO
