USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[InsertOrderInEnvelopeMap]    Script Date: 06/07/2017 09:19:39 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertOrderInEnvelopeMap]  
@lEnvelopeInstance int,
@COHInstance int

AS

insert into OrderInEnvelopeMap values (@lEnvelopeInstance, @COHInstance)
GO
