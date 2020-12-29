USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_contactss]    Script Date: 02/14/2014 13:04:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Contacts
CREATE PROCEDURE [dbo].[efrcrm_get_contactss] AS
begin

select Contact_ID, First_Name, Last_Name, Phone_Number, Phone_Ext, Street_Address, City, State_Code, Country_Code, Zip_Code, Comments from Contacts

end
GO
