USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_qsp_program]    Script Date: 02/14/2014 13:07:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for QSP_Program
CREATE PROCEDURE [dbo].[efrcrm_insert_qsp_program] @QSP_Program_ID int OUTPUT, @Description varchar(100), @Base_Directory varchar(100) AS
begin

insert into QSP_Program(Description, Base_Directory) values(@Description, @Base_Directory)

select @QSP_Program_ID = SCOPE_IDENTITY()

end
GO
