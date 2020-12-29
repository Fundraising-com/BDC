USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_group_type_by_lead_id]    Script Date: 02/14/2014 13:04:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Group_type
CREATE PROCEDURE [dbo].[efrcrm_get_group_type_by_lead_id]
@leadid  int 
 AS
begin

select g.Group_type_id, g.Party_type_id, g.[Description] from Group_type g inner join Lead l on g.group_type_id = l.group_type_id
where l.lead_id = @leadid

end
GO
