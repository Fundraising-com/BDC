USE [QSPCanadaOrderManagement]
GO
/****** Object:  View [dbo].[vw_GiftCardDetailHoliday]    Script Date: 06/07/2017 09:18:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_GiftCardDetailHoliday] AS
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
       IsStaffOrder,
	   CreationDate    
  FROM vw_GiftCardDetailAll
 WHERE GiftOrderType = 'X'
AND  Year(getDate()) = Year(CreationDate) --Jeff:11/30/06: So holiday cards are only created from subs from this season
AND  Month(CreationDate) >= 7
AND
     getDate() >= (SELECT CAST(TextValue AS DATETIME)   
	    	       FROM QSPCanadaCommon..SystemOptions 
		      WHERE KeyValue = 'first_mail_date_xmas_gift_card') AND
     getDate() < (SELECT CAST(TextValue AS DATETIME)   
	    	       FROM QSPCanadaCommon..SystemOptions 
		      WHERE KeyValue = 'last_mail_date_xmas_gift_card')
)
GO
