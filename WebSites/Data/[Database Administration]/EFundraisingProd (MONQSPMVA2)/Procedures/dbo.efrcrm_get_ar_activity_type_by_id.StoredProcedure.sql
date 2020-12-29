USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_ar_activity_type_by_id]    Script Date: 02/14/2014 13:03:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for AR_Activity_Type
CREATE PROCEDURE [dbo].[efrcrm_get_ar_activity_type_by_id] @AR_Activity_Type_Id int AS
begin

select AR_Activity_Type_Id, Description from AR_Activity_Type where AR_Activity_Type_Id=@AR_Activity_Type_Id

end
GO
