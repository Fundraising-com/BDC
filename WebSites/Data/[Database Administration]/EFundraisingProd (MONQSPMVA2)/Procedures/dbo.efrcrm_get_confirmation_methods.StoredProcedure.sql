USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_confirmation_methods]    Script Date: 02/14/2014 13:04:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Confirmation_Method
CREATE PROCEDURE [dbo].[efrcrm_get_confirmation_methods] AS
begin

select Confirmation_Method_ID, Description from Confirmation_Method

end
GO
