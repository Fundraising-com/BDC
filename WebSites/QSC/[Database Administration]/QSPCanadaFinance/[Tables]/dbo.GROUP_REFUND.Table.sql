USE [QSPCanadaFinance]
GO
/****** Object:  Table [dbo].[GROUP_REFUND]    Script Date: 06/07/2017 09:16:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GROUP_REFUND](
	[Group_Refund_ID] [int] IDENTITY(1,1) NOT NULL,
	[AP_Cheque_ID] [int] NULL,
	[CreationDate] [datetime] NOT NULL,
	[Address1] [varchar](50) NOT NULL,
	[Address2] [varchar](50) NULL,
	[City] [varchar](50) NOT NULL,
	[Province] [varchar](10) NOT NULL,
	[Postal_Code] [varchar](7) NOT NULL,
	[Country] [varchar](10) NOT NULL,
	[Campaign_ID] [int] NOT NULL,
	[Invoice_Amount] [numeric](12, 2) NOT NULL,
	[Payment_Amount] [numeric](12, 2) NOT NULL,
	[Adjustment_Amount] [numeric](12, 2) NOT NULL,
	[Refund_Amount] [numeric](12, 2) NOT NULL,
 CONSTRAINT [PK_GROUP_REFUND] PRIMARY KEY CLUSTERED 
(
	[Group_Refund_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[GROUP_REFUND]  WITH CHECK ADD  CONSTRAINT [FK_GROUP_REFUND_AP_Cheque] FOREIGN KEY([AP_Cheque_ID])
REFERENCES [dbo].[AP_Cheque] ([AP_Cheque_ID])
GO
ALTER TABLE [dbo].[GROUP_REFUND] CHECK CONSTRAINT [FK_GROUP_REFUND_AP_Cheque]
GO
ALTER TABLE [dbo].[GROUP_REFUND]  WITH NOCHECK ADD  CONSTRAINT [GROUP_REFUND_NEW_Refund_Amount_Range] CHECK  (([Refund_Amount] > 0))
GO
ALTER TABLE [dbo].[GROUP_REFUND] CHECK CONSTRAINT [GROUP_REFUND_NEW_Refund_Amount_Range]
GO
