USE [eFundstore]
GO
/****** Object:  Table [dbo].[newsletter_subscription]    Script Date: 02/14/2014 16:26:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[newsletter_subscription](
	[subscription_id] [int] IDENTITY(1,1) NOT NULL,
	[partner_id] [int] NOT NULL,
	[culture_code] [nvarchar](5) NOT NULL,
	[referrer] [varchar](120) NULL,
	[email] [varchar](100) NOT NULL,
	[fullname] [varchar](100) NOT NULL,
	[unsubscribed] [bit] NOT NULL,
	[subscribe_date] [datetime] NOT NULL,
	[unsubscribe_date] [datetime] NULL,
 CONSTRAINT [PK_newsletter_subscriptions] PRIMARY KEY CLUSTERED 
(
	[subscription_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[newsletter_subscription] ADD  CONSTRAINT [DF_newsletter_subscriptions_unsubscribed]  DEFAULT (0) FOR [unsubscribed]
GO
ALTER TABLE [dbo].[newsletter_subscription] ADD  CONSTRAINT [DF_newsletter_subscriptions_subscribe_date]  DEFAULT (getdate()) FOR [subscribe_date]
GO
