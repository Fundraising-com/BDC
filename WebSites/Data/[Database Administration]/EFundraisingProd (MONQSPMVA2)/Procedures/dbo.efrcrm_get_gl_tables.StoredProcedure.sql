USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_gl_tables]    Script Date: 02/14/2014 13:04:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for GL_Table
CREATE PROCEDURE [dbo].[efrcrm_get_gl_tables] AS
begin

select GL_Code, Description, GL_Account_No, Debit_Credit from GL_Table

end
GO
