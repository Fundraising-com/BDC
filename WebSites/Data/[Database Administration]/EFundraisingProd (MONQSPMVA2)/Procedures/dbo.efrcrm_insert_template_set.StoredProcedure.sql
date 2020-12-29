USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_template_set]    Script Date: 02/14/2014 13:07:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Template_Set
CREATE PROCEDURE [dbo].[efrcrm_insert_template_set] @Template_Set_ID int OUTPUT, @QSP_Program_ID int, @Supporter_Path varchar(100), @Generic_Path varchar(100), @Edit_Path varchar(100) AS
begin

insert into Template_Set(QSP_Program_ID, Supporter_Path, Generic_Path, Edit_Path) values(@QSP_Program_ID, @Supporter_Path, @Generic_Path, @Edit_Path)

select @Template_Set_ID = SCOPE_IDENTITY()

end
GO
