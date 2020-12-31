USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[GetTotalOnlineAdjustmentAmount]    Script Date: 06/07/2017 09:19:36 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetTotalOnlineAdjustmentAmount] @campaignId int
AS
Select Sum(Adjustment_amount) TotalAmount from QSPCanadaFinance.dbo.adjustment where Adjustment_Type_Id=49028
and campaign_id=@CampaignId
GO
