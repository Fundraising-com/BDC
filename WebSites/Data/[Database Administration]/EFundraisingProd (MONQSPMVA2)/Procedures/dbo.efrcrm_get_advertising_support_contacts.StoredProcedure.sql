USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_advertising_support_contacts]    Script Date: 02/14/2014 13:03:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Advertising_Support_Contact
CREATE PROCEDURE [dbo].[efrcrm_get_advertising_support_contacts] AS
begin

select Advertising_Support_Contact_ID, Advertising_Support_ID, First_Name, Last_Name, Phone_Number, Fax_Number, Email from Advertising_Support_Contact

end
GO
