USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_ar_activity_type]    Script Date: 02/14/2014 13:07:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for AR_Activity_Type
CREATE PROCEDURE [dbo].[efrcrm_update_ar_activity_type] @AR_Activity_Type_Id int, @Description varchar(50) AS
begin

update AR_Activity_Type set Description=@Description where AR_Activity_Type_Id=@AR_Activity_Type_Id

end
GO
