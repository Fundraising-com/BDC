USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_grabber_by_id]    Script Date: 02/14/2014 13:04:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Grabber
CREATE PROCEDURE [dbo].[efrcrm_get_grabber_by_id] @Grabber_Id int AS
begin

select Grabber_Id, Grabber_Desc from Grabber where Grabber_Id=@Grabber_Id

end
GO
