USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_efo_group_types]    Script Date: 02/14/2014 13:04:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for EFO_Group_Type
CREATE PROCEDURE [dbo].[efrcrm_get_efo_group_types] AS
begin

select Group_Type_ID, Description from EFO_Group_Type

end
GO
