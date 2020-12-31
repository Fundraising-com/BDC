USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_RemitTest_GetRemitTests]    Script Date: 06/07/2017 09:20:27 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE  PROCEDURE [dbo].[pr_RemitTest_GetRemitTests]

AS

SELECT 

[ID]
, [Name]
, [Description]
, [Script]
, [CorrectionDescription]
, [CorrectionScript]
, [IsCritical] 

FROM [dbo].[RemitTest]

WHERE COALESCE([DeletedTF],0) <> 1

ORDER BY [SequenceID]
GO
