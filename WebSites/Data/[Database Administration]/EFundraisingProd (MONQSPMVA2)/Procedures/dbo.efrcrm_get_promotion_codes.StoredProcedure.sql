USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_promotion_codes]    Script Date: 02/14/2014 13:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Promotion_Code
CREATE PROCEDURE [dbo].[efrcrm_get_promotion_codes] AS
begin

select Promotion_Code_ID, Promotion_Code_Desc from Promotion_Code

end
GO
