USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_grabber]    Script Date: 02/14/2014 13:08:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Grabber
CREATE PROCEDURE [dbo].[efrcrm_update_grabber] @Grabber_Id int, @Grabber_Desc varchar(100) AS
begin

update Grabber set Grabber_Desc=@Grabber_Desc where Grabber_Id=@Grabber_Id

end
GO
