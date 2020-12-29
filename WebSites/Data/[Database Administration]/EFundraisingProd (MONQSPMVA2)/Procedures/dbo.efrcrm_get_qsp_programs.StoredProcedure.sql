USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_qsp_programs]    Script Date: 02/14/2014 13:05:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for QSP_Program
CREATE PROCEDURE [dbo].[efrcrm_get_qsp_programs] AS
begin

select QSP_Program_ID, Description, Base_Directory from QSP_Program

end
GO
