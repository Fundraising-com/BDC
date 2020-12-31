USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[GetNextEnvelopeInstance]    Script Date: 06/07/2017 09:19:36 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetNextEnvelopeInstance]
	@date datetime,
	@id int
AS

SELECT isnull(max(instance),1) from Envelope --where orderbatchdate = @date and orderbatchid = @id
GO
