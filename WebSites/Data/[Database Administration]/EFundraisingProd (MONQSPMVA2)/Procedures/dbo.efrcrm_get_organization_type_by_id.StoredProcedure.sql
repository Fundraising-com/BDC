USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_organization_type_by_id]    Script Date: 02/14/2014 13:05:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Organization_type
create  PROCEDURE [dbo].[efrcrm_get_organization_type_by_id] 
                    @organization_type_id int
AS
begin

select Organization_type_id, Party_type_id, Organization_type_desc, Is_school from Organization_type
where organization_type_id = @organization_type_id

end
GO
