USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_bounced_emails]    Script Date: 02/14/2014 13:03:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Bounced_Email
CREATE PROCEDURE [dbo].[efrcrm_get_bounced_emails] AS
begin

select Email from Bounced_Email

end
GO
