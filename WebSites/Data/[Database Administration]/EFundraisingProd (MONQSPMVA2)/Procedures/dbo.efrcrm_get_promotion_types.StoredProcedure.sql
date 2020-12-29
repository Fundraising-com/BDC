USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_promotion_types]    Script Date: 02/14/2014 13:05:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Promotion_Type
CREATE PROCEDURE [dbo].[efrcrm_get_promotion_types] AS
begin

select Promotion_Type_Code, Description, Default_Commission_Rate, Channel from Promotion_Type

end
GO
