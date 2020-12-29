USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_divisions]    Script Date: 02/14/2014 13:04:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Division
CREATE PROCEDURE [dbo].[efrcrm_get_divisions] AS
begin

select Division_id, Division_name, Logo, Short_name from Division

end
GO
