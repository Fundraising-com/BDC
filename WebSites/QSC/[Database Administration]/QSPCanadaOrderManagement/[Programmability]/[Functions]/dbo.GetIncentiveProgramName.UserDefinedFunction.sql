USE [QSPCanadaOrderManagement]
GO
/****** Object:  UserDefinedFunction [dbo].[GetIncentiveProgramName]    Script Date: 06/07/2017 09:21:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GetIncentiveProgramName] (@OrderId Int, @BatchId Int, @BatchDate DateTime)
RETURNS Varchar(30) AS  
BEGIN 
Return(
SELECT     TOP 1    QSPCanadaCommon.dbo.Program.Name
FROM         QSPCanadaCommon.dbo.CampaignProgram INNER JOIN
                      QSPCanadaCommon.dbo.Campaign c ON QSPCanadaCommon.dbo.CampaignProgram.CampaignID = c.ID INNER JOIN
                      QSPCanadaCommon.dbo.Program ON 
                      QSPCanadaCommon.dbo.CampaignProgram.ProgramID = QSPCanadaCommon.dbo.Program.ID RIGHT OUTER JOIN
                      dbo.Batch b ON c.ID = b.CampaignID
WHERE     (b.OrderID = @OrderId) 
AND b.id = @BatchId
AND b.Date = @BatchDate
AND (QSPCanadaCommon.dbo.Program.ProgramTypeID = 36002))

END
GO
