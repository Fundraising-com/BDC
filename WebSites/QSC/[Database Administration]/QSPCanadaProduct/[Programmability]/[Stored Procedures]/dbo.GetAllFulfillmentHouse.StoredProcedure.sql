USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[GetAllFulfillmentHouse]    Script Date: 06/07/2017 09:17:48 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetAllFulfillmentHouse] AS
select * from Fulfillment_House
order by Ful_Name
GO
