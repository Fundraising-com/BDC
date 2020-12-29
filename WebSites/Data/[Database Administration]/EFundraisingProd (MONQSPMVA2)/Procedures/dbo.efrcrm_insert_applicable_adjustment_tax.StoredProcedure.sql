USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_applicable_adjustment_tax]    Script Date: 02/14/2014 13:06:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Applicable_Adjustment_Tax
CREATE PROCEDURE [dbo].[efrcrm_insert_applicable_adjustment_tax] @Sales_Id int OUTPUT, @Adjustement_No int, @Tax_Code varchar(4), @Tax_Amount decimal AS
begin

insert into Applicable_Adjustment_Tax(Adjustement_No, Tax_Code, Tax_Amount) values(@Adjustement_No, @Tax_Code, @Tax_Amount)

select @Sales_Id = SCOPE_IDENTITY()

end
GO
