USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_group_type_descs]    Script Date: 02/14/2014 13:04:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Group_type_desc
CREATE PROCEDURE [dbo].[efrcrm_get_group_type_descs] AS
begin

select Group_type_id, Language_id, Description from Group_type_desc

end
GO
