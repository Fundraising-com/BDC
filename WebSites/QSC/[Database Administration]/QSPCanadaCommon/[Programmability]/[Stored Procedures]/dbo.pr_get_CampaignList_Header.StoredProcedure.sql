USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_get_CampaignList_Header]    Script Date: 06/07/2017 09:33:21 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_get_CampaignList_Header]
  @AccountID int
AS

SELECT     
	Id AS AccountID,
	Name, 
	Country, 
	Lang, 
	CAccountCodeClass, 
	CAccountCodeGroup, 
	Sponsor, 
	StatusID
FROM
	CAccount
WHERE
	Id = @AccountID
GO
