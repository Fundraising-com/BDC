USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_selectQSPCampaign]    Script Date: 06/07/2017 09:20:34 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_selectQSPCampaign] 

AS

/*SELECT MAX(ID) AS CampaignID 
FROM    QSPCanadaCommon..Campaign 
WHERE startdate <= getDate() 
	  and enddate >= getDate() 
	  and status in ('37002', '37007')
	  and shiptoaccountid = 1*/

select 41200
GO
