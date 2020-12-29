USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_applicable_tax]    Script Date: 02/14/2014 13:06:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Applicable_Tax
CREATE  PROCEDURE [dbo].[efrcrm_insert_applicable_tax] @Sales_ID int, @Tax_Code varchar(4), @Tax_Amount decimal(10,2) AS
begin

insert into Applicable_Tax values(@sales_id, @Tax_Code, @Tax_Amount)

end
GO
