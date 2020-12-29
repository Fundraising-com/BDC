USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_commission_outstanding]    Script Date: 02/14/2014 13:06:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Commission_Outstanding
CREATE PROCEDURE [dbo].[efrcrm_insert_commission_outstanding] @Sales_ID int OUTPUT, @Consultant_ID int, @Sales_Date datetime, @Shipped_Date datetime, @Status varchar(50), @Payment_Term varchar(50), @First_Name varchar(50), @Last_Name varchar(50), @Organization varchar(100), @Day_Phone varchar(20), @Outstanding_Amount varchar(125), @Currency_Code varchar(10), @Outstanding_Commission varchar(125) AS
begin

insert into Commission_Outstanding(Consultant_ID, Sales_Date, Shipped_Date, Status, Payment_Term, First_Name, Last_Name, Organization, Day_Phone, Outstanding_Amount, Currency_Code, Outstanding_Commission) values(@Consultant_ID, @Sales_Date, @Shipped_Date, @Status, @Payment_Term, @First_Name, @Last_Name, @Organization, @Day_Phone, @Outstanding_Amount, @Currency_Code, @Outstanding_Commission)

select @Sales_ID = SCOPE_IDENTITY()

end
GO
