USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[ShipmentRequest_SelectError]    Script Date: 06/07/2017 09:20:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ShipmentRequest_SelectError]

AS

SELECT	*
FROM	QSPCanadaCommon..SystemErrorLog sel
WHERE	ISNULL(IsFixed, 0) = 0
AND		ISNULL(IsReviewed, 0) = 0
AND		ProcName = 'ShipmentRequest_ValidateOrder'
GO
