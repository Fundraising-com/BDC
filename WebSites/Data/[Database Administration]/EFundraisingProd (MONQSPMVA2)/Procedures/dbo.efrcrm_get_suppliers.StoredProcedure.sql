USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_suppliers]    Script Date: 02/14/2014 13:06:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Supplier
CREATE PROCEDURE [dbo].[efrcrm_get_suppliers] AS
begin

select Supplier_id, Supplier_name, Street_adress, City, Zip, Contact_name, Phone, Fax, Account_no, Credit_margin, State_code, Country_code, Comments from Supplier

end
GO
