USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_efo_group_type_by_id]    Script Date: 02/14/2014 13:04:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for EFO_Group_Type
CREATE PROCEDURE [dbo].[efrcrm_get_efo_group_type_by_id] @Group_Type_ID int AS
begin

select Group_Type_ID, Description from EFO_Group_Type where Group_Type_ID=@Group_Type_ID

end
GO
