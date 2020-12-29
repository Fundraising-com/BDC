USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_referees]    Script Date: 02/14/2014 13:05:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Referee
CREATE PROCEDURE [dbo].[efrcrm_get_referees] AS
begin

select Referee_Id, Lead_Id, Entry_Date, First_Name, Last_Name, Email, Phone_Number, Is_Entered from Referee

end
GO
