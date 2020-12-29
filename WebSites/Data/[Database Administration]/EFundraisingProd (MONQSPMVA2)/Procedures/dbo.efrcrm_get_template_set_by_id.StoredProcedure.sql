USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_template_set_by_id]    Script Date: 02/14/2014 13:06:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Template_Set
CREATE PROCEDURE [dbo].[efrcrm_get_template_set_by_id] @Template_Set_ID int AS
begin

select Template_Set_ID, QSP_Program_ID, Supporter_Path, Generic_Path, Edit_Path from Template_Set where Template_Set_ID=@Template_Set_ID

end
GO
