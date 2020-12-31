USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[SendEmailFor_MissingTransmittedOrder]    Script Date: 06/07/2017 09:20:47 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[SendEmailFor_MissingTransmittedOrder]
AS

BEGIN

Set   ANSI_NULLS ON
Set  ANSI_WARNINGS ON

EXEC QSPCanadaOrderManagement.dbo.MissingTransmittedOrder_Resolve 

Set   ANSI_NULLS OFF
Set  ANSI_WARNINGS OFF

END
GO
