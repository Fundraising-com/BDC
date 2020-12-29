USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efr_update_lead_news_unsubscribe_new]    Script Date: 02/14/2014 13:05:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Jason Farrell>
-- Create date: <June 16, 2009>
-- Description:	<Match dbo.Temp_Newsletter_Unsub with dbo.lead and update onemaillist field>
-- =============================================

Create PROCEDURE [dbo].[efr_update_lead_news_unsubscribe_new]


AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	-- SET NOCOUNT ON;

    -- Insert statements for procedure here
	 UPDATE eFundraisingProd.dbo.Lead 
		set onemaillist = 0
		from efundstore.dbo.Temp_Newsletter_Unsub
		where Lead.email = Temp_Newsletter_Unsub.email

	UPDATE efundstore.dbo.newsletter_subscription
		set unsubscribed = 1
		from efundstore.dbo.Temp_Newsletter_Unsub
		where newsletter_subscription.email = Temp_Newsletter_Unsub.email	
		
		DELETE FROM [efundstore].[dbo].[Temp_Newsletter_Unsub]
		return 1
		
END
GO
