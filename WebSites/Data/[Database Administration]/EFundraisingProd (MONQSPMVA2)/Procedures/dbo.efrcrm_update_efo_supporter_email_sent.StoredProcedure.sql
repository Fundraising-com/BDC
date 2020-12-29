USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_efo_supporter_email_sent]    Script Date: 02/14/2014 13:07:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for EFO_Supporter_Email_Sent
CREATE PROCEDURE [dbo].[efrcrm_update_efo_supporter_email_sent] @Supporter_Email_Sent_ID int, @Email_Type_ID int, @Supporter_ID int, @Date_Sent datetime AS
begin

update EFO_Supporter_Email_Sent set Email_Type_ID=@Email_Type_ID, Supporter_ID=@Supporter_ID, Date_Sent=@Date_Sent where Supporter_Email_Sent_ID=@Supporter_Email_Sent_ID

end
GO
