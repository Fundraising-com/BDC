USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_template_sets]    Script Date: 02/14/2014 13:06:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Template_Set
CREATE PROCEDURE [dbo].[efrcrm_get_template_sets] AS
begin

select Template_Set_ID, QSP_Program_ID, Supporter_Path, Generic_Path, Edit_Path from Template_Set

end
GO
