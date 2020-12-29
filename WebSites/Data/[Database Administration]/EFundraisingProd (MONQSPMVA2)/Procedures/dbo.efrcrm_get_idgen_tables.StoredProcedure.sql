USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_idgen_tables]    Script Date: 02/14/2014 13:04:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for IDGen_Table
CREATE PROCEDURE [dbo].[efrcrm_get_idgen_tables] AS
begin

select Context, ID_Name, Last_Value, Comment from IDGen_Table

end
GO
