USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_efo_supporter_email_sent]    Script Date: 02/14/2014 13:06:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for EFO_Supporter_Email_Sent
CREATE PROCEDURE [dbo].[efrcrm_insert_efo_supporter_email_sent] @Supporter_Email_Sent_ID int OUTPUT, @Email_Type_ID int, @Supporter_ID int, @Date_Sent datetime AS
begin

insert into EFO_Supporter_Email_Sent(Email_Type_ID, Supporter_ID, Date_Sent) values(@Email_Type_ID, @Supporter_ID, @Date_Sent)

select @Supporter_Email_Sent_ID = SCOPE_IDENTITY()

end
GO
