USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_state_taxs]    Script Date: 02/14/2014 13:06:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for State_Tax
CREATE PROCEDURE [dbo].[efrcrm_get_state_taxs] AS
begin

select State_Code, Tax_Code, Effective_Date, Tax_Rate, Tax_order from State_Tax

end
GO
