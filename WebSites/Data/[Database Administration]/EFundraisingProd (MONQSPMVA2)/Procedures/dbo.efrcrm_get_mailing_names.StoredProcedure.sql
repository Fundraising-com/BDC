USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_mailing_names]    Script Date: 02/14/2014 13:05:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Mailing_Name
CREATE PROCEDURE [dbo].[efrcrm_get_mailing_names] AS
begin

select Mailing_Name_ID, List_Name, List_ID, Contact_Name, Title, School_Name, School_Address, City, State_Code, Zip, Phone_Number, Fax_Number, Email, School_Type from Mailing_Name

end
GO
