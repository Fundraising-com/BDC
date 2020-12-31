USE [QSPCanadaOrderManagement]
GO
/****** Object:  View [dbo].[vw_GiftCardDetailRegular]    Script Date: 06/07/2017 09:18:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_GiftCardDetailRegular] AS 
(SELECT RemitBatchID, 
       CustomerOrderHeaderInstance,
       TransID,
       CampaignID, 
       OrderID, 
       TitleCode,
       MagazineTitle, 
       Lang,
       NumberOfIssues,
       SupporterName,
       FirstName,
       LastName,
       Address1,
       Address2,
       City,
       State,
       Zip,
       GroupName,
       RunID,
       IsStaffOrder    
  FROM vw_GiftCardDetailAll
 WHERE GiftOrderType = 'R'
)
GO
