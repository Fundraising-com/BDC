USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_brands]    Script Date: 02/14/2014 13:03:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Brand
CREATE PROCEDURE [dbo].[efrcrm_get_brands] AS
begin

select Brand_ID, Name, Promotion from Brand

end
GO
