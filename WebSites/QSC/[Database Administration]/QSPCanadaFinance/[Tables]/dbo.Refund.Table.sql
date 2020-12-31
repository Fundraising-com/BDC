USE [QSPCanadaFinance]
GO
/****** Object:  Table [dbo].[Refund]    Script Date: 06/07/2017 09:16:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Refund](
	[Refund_ID] [int] IDENTITY(1,1) NOT NULL,
	[AP_Cheque_ID] [int] NULL,
	[Refund_Type_ID] [int] NOT NULL,
	[Amount] [decimal](10, 2) NOT NULL,
	[Address1] [varchar](50) NULL,
	[Address2] [varchar](50) NULL,
	[City] [varchar](50) NOT NULL,
	[Province] [varchar](10) NOT NULL,
	[PostalCode] [varchar](20) NOT NULL,
	[Country] [varchar](10) NOT NULL,
	[CustomerOrderHeaderInstance] [int] NULL,
	[TransID] [int] NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[Comment] [varchar](500) NULL,
	[Campaign_ID] [int] NULL,
	[Invoice_Amount] [numeric](12, 2) NULL,
	[Payment_Amount] [numeric](12, 2) NULL,
	[Adjustment_Amount] [numeric](12, 2) NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUserID] [varchar](30) NOT NULL,
	[UpdateDate] [datetime] NULL,
	[UpdateUserID] [varchar](30) NULL,
	[Cancelled] [bit] NOT NULL,
 CONSTRAINT [PK_Refund_1] PRIMARY KEY CLUSTERED 
(
	[Refund_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Refund]  WITH CHECK ADD  CONSTRAINT [FK_REFUND_AP_Cheque] FOREIGN KEY([AP_Cheque_ID])
REFERENCES [dbo].[AP_Cheque] ([AP_Cheque_ID])
GO
ALTER TABLE [dbo].[Refund] CHECK CONSTRAINT [FK_REFUND_AP_Cheque]
GO
ALTER TABLE [dbo].[Refund]  WITH CHECK ADD  CONSTRAINT [FK_REFUND_AP_Cheque_1] FOREIGN KEY([AP_Cheque_ID])
REFERENCES [dbo].[AP_Cheque] ([AP_Cheque_ID])
GO
ALTER TABLE [dbo].[Refund] CHECK CONSTRAINT [FK_REFUND_AP_Cheque_1]
GO
ALTER TABLE [dbo].[Refund]  WITH CHECK ADD  CONSTRAINT [FK_Refund_Refund_Type_1] FOREIGN KEY([Refund_Type_ID])
REFERENCES [dbo].[Refund_Type] ([Refund_Type_ID])
GO
ALTER TABLE [dbo].[Refund] CHECK CONSTRAINT [FK_Refund_Refund_Type_1]
GO
ALTER TABLE [dbo].[Refund] ADD  CONSTRAINT [DF_Refund_Cancelled]  DEFAULT ((0)) FOR [Cancelled]
GO
