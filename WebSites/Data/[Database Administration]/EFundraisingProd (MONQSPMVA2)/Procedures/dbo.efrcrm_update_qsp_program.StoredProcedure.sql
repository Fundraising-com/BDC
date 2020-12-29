USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_qsp_program]    Script Date: 02/14/2014 13:08:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for QSP_Program
CREATE PROCEDURE [dbo].[efrcrm_update_qsp_program] @QSP_Program_ID int, @Description varchar(100), @Base_Directory varchar(100) AS
begin

update QSP_Program set Description=@Description, Base_Directory=@Base_Directory where QSP_Program_ID=@QSP_Program_ID

end
GO
