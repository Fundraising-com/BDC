USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_confirmation_method_by_id]    Script Date: 02/14/2014 13:04:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Confirmation_Method
CREATE PROCEDURE [dbo].[efrcrm_get_confirmation_method_by_id] @Confirmation_Method_ID int AS
begin

select Confirmation_Method_ID, Description from Confirmation_Method where Confirmation_Method_ID=@Confirmation_Method_ID

end
GO
