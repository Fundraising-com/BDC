USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_advertising_support_contact_by_id]    Script Date: 02/14/2014 13:03:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Advertising_Support_Contact
CREATE PROCEDURE [dbo].[efrcrm_get_advertising_support_contact_by_id] @Advertising_Support_Contact_ID int AS
begin

select Advertising_Support_Contact_ID, Advertising_Support_ID, First_Name, Last_Name, Phone_Number, Fax_Number, Email from Advertising_Support_Contact where Advertising_Support_Contact_ID=@Advertising_Support_Contact_ID

end
GO
