USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_grabbers]    Script Date: 02/14/2014 13:04:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Grabber
CREATE PROCEDURE [dbo].[efrcrm_get_grabbers] AS
begin

select Grabber_Id, Grabber_Desc from Grabber

end
GO
