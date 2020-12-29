USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_applicable_tax]    Script Date: 02/14/2014 13:07:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Applicable_Tax
CREATE   PROCEDURE [dbo].[efrcrm_update_applicable_tax] @Sales_ID int, @Tax_Code varchar(4), @Tax_Amount decimal(10,2) AS
begin

update Applicable_Tax set Tax_Amount=@Tax_Amount where Sales_ID=@Sales_ID and Tax_Code=@Tax_Code

end
GO
