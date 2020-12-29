USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_reason]    Script Date: 02/14/2014 13:08:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Reason
CREATE PROCEDURE [dbo].[efrcrm_update_reason] @Reason_ID int, @Description varchar(50), @Is_Active bit AS
begin

update Reason set Description=@Description, Is_Active=@Is_Active where Reason_ID=@Reason_ID

end
GO
