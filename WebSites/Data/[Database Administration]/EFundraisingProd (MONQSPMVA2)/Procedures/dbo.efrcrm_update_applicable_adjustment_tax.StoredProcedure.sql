USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_applicable_adjustment_tax]    Script Date: 02/14/2014 13:07:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Applicable_Adjustment_Tax
CREATE PROCEDURE [dbo].[efrcrm_update_applicable_adjustment_tax] @Sales_Id int, @Adjustement_No int, @Tax_Code varchar(4), @Tax_Amount decimal AS
begin

update Applicable_Adjustment_Tax set Adjustement_No=@Adjustement_No, Tax_Code=@Tax_Code, Tax_Amount=@Tax_Amount where Sales_Id=@Sales_Id

end
GO
