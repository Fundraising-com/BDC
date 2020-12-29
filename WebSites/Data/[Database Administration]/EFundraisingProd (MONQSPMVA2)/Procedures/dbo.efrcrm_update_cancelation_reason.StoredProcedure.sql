USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_cancelation_reason]    Script Date: 02/14/2014 13:07:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Cancelation_Reason
CREATE PROCEDURE [dbo].[efrcrm_update_cancelation_reason] @Cancelation_Reason_Id int, @Description varchar(100) AS
begin

update Cancelation_Reason set Description=@Description where Cancelation_Reason_Id=@Cancelation_Reason_Id

end
GO
