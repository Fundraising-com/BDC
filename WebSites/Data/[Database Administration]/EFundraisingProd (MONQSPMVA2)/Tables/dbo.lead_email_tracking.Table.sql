USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[lead_email_tracking]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[lead_email_tracking](
	[lead_id] [int] NOT NULL,
	[sale_id] [int] NOT NULL,
	[order_confirmation] [bit] NOT NULL,
	[order_shipped] [bit] NOT NULL,
	[follow_up] [bit] NOT NULL,
	[back_order_notification] [bit] NOT NULL,
	[issue_reported] [bit] NOT NULL,
 CONSTRAINT [PK_lead_email_tracking] PRIMARY KEY CLUSTERED 
(
	[lead_id] ASC,
	[sale_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[lead_email_tracking] ADD  CONSTRAINT [DF_lead_email_tracking_sale_id]  DEFAULT ((0)) FOR [sale_id]
GO
ALTER TABLE [dbo].[lead_email_tracking] ADD  CONSTRAINT [DF_lead_email_tracking_orderConfirmation]  DEFAULT ((0)) FOR [order_confirmation]
GO
ALTER TABLE [dbo].[lead_email_tracking] ADD  CONSTRAINT [DF_lead_email_tracking_orderShipped]  DEFAULT ((0)) FOR [order_shipped]
GO
ALTER TABLE [dbo].[lead_email_tracking] ADD  CONSTRAINT [DF_lead_email_tracking_followUp]  DEFAULT ((0)) FOR [follow_up]
GO
ALTER TABLE [dbo].[lead_email_tracking] ADD  CONSTRAINT [DF_lead_email_tracking_backOrderNotification]  DEFAULT ((0)) FOR [back_order_notification]
GO
ALTER TABLE [dbo].[lead_email_tracking] ADD  CONSTRAINT [DF_lead_email_tracking_issueReported]  DEFAULT ((0)) FOR [issue_reported]
GO
