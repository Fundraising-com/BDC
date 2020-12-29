USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_contacts_by_id]    Script Date: 02/14/2014 13:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Contacts
CREATE PROCEDURE [dbo].[efrcrm_get_contacts_by_id] @Contact_ID int AS
begin

select Contact_ID, First_Name, Last_Name, Phone_Number, Phone_Ext, Street_Address, City, State_Code, Country_Code, Zip_Code, Comments from Contacts where Contact_ID=@Contact_ID

end
GO
