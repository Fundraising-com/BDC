USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_efo_supporter_email_sents]    Script Date: 02/14/2014 13:04:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for EFO_Supporter_Email_Sent
CREATE PROCEDURE [dbo].[efrcrm_get_efo_supporter_email_sents] AS
begin

select Supporter_Email_Sent_ID, Email_Type_ID, Supporter_ID, Date_Sent from EFO_Supporter_Email_Sent

end
GO
