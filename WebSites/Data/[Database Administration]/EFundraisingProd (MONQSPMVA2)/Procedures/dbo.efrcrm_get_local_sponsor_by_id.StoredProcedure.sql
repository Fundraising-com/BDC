USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_local_sponsor_by_id]    Script Date: 02/14/2014 13:05:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Local_Sponsor
CREATE PROCEDURE [dbo].[efrcrm_get_local_sponsor_by_id] @Brand_ID int AS
begin

select Brand_ID, Local_Sponsor_ID, Salutation, First_Name, Middle_Initial, Last_Name, Title, Street_Address, City_Name, State_Code, Zip_Code, Country_Code, Day_Phone, Day_Time_Call, Evening_Phone, Evening_Time_Call, Alternate_Phone, Fax_Number, Email, Approval_Date, Comment, Sponsor_Consultant_ID, Last_Contact, Local_Sponsor_Steps_Id from Local_Sponsor where Brand_ID=@Brand_ID

end
GO
