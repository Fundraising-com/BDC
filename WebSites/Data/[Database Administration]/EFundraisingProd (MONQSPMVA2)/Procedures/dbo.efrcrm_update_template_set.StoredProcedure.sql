USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_template_set]    Script Date: 02/14/2014 13:08:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Template_Set
CREATE PROCEDURE [dbo].[efrcrm_update_template_set] @Template_Set_ID int, @QSP_Program_ID int, @Supporter_Path varchar(100), @Generic_Path varchar(100), @Edit_Path varchar(100) AS
begin

update Template_Set set QSP_Program_ID=@QSP_Program_ID, Supporter_Path=@Supporter_Path, Generic_Path=@Generic_Path, Edit_Path=@Edit_Path where Template_Set_ID=@Template_Set_ID

end
GO
