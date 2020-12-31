USE [QSPCanadaOrderManagement]
GO
/****** Object:  View [dbo].[vw_GetGrossForReporting]    Script Date: 06/07/2017 09:18:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_GetGrossForReporting] AS

 Select c.startdate, bca.AccountID, bca.campaignid, bca.ordertypecode, bca.orderqualifierid, codca.producttype, codca.quantity, codca.price as Gross from
				QspcanadaOrderManagement.dbo.batch as bca join QSPCanadaOrderManagement.dbo.CustomerOrderHeader cohca on bca.date=cohca.orderbatchdate
						and bca.id = cohca.orderbatchid
							join QSPCanadaOrderManagement.dbo.CustomerOrderDetail codca on codca.customerorderheaderinstance = cohca.instance
							join QSPCanadaCommon.dbo.Campaign c on c.id = bca.campaignid
							where 							
								codca.delflag <> 1 
								And codca.statusinstance not in (501, 506) 
								And bca.statusinstance <> 40005 
							--and codca.invoicenumber is not null
GO
