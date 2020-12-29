USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_qsp_program_by_id]    Script Date: 02/14/2014 13:05:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for QSP_Program
CREATE PROCEDURE [dbo].[efrcrm_get_qsp_program_by_id] @QSP_Program_ID int AS
begin

select QSP_Program_ID, Description, Base_Directory from QSP_Program where QSP_Program_ID=@QSP_Program_ID

end
GO
