USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_local_sponsor_activity_types]    Script Date: 02/14/2014 13:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Local_Sponsor_Activity_Type
CREATE PROCEDURE [dbo].[efrcrm_get_local_sponsor_activity_types] AS
begin

select Local_Sponsor_Activity_Type_Id, Description from Local_Sponsor_Activity_Type

end
GO
