USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_group_types]    Script Date: 02/14/2014 13:04:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Group_type
CREATE PROCEDURE [dbo].[efrcrm_get_group_types] AS
begin

select Group_type_id, Party_type_id, Description from Group_type

end
GO
