USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_commission_outstanding]    Script Date: 02/14/2014 13:07:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Commission_Outstanding
CREATE PROCEDURE [dbo].[efrcrm_update_commission_outstanding] @Sales_ID int, @Consultant_ID int, @Sales_Date datetime, @Shipped_Date datetime, @Status varchar(50), @Payment_Term varchar(50), @First_Name varchar(50), @Last_Name varchar(50), @Organization varchar(100), @Day_Phone varchar(20), @Outstanding_Amount varchar(125), @Currency_Code varchar(10), @Outstanding_Commission varchar(125) AS
begin

update Commission_Outstanding set Consultant_ID=@Consultant_ID, Sales_Date=@Sales_Date, Shipped_Date=@Shipped_Date, Status=@Status, Payment_Term=@Payment_Term, First_Name=@First_Name, Last_Name=@Last_Name, Organization=@Organization, Day_Phone=@Day_Phone, Outstanding_Amount=@Outstanding_Amount, Currency_Code=@Currency_Code, Outstanding_Commission=@Outstanding_Commission where Sales_ID=@Sales_ID

end
GO
