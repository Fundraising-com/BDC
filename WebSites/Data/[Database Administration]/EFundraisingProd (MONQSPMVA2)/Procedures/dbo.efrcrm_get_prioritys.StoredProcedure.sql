USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_prioritys]    Script Date: 02/14/2014 13:05:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Priority
CREATE PROCEDURE [dbo].[efrcrm_get_prioritys] AS
begin

select Priority_ID, Description, Color_Code from Priority

end
GO
