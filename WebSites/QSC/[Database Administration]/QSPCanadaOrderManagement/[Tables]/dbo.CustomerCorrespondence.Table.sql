USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[CustomerCorrespondence]    Script Date: 06/07/2017 09:18:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerCorrespondence](
	[CustomerInstance] [int] NOT NULL,
	[DateSent] [datetime] NOT NULL,
	[CodeDetailInstance] [int] NOT NULL,
	[StatusInstance] [int] NOT NULL,
	[CustomerOrderHeader] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[UserIDCreated] [varchar](4) NULL,
	[DateChanged] [datetime] NOT NULL,
	[UserIDChanged] [varchar](4) NOT NULL,
	[CustomerPaymentHeader] [int] NULL,
	[SendType] [int] NULL,
 CONSTRAINT [aaaaaCustCorresp_PK] PRIMARY KEY CLUSTERED 
(
	[CustomerInstance] ASC,
	[DateSent] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[CustomerCorrespondence] ADD  CONSTRAINT [DF__CustomerC__Custo__44952D46]  DEFAULT (0) FOR [CustomerInstance]
GO
ALTER TABLE [dbo].[CustomerCorrespondence] ADD  CONSTRAINT [DF__CustomerC__DateS__4589517F]  DEFAULT ('1/1/1995') FOR [DateSent]
GO
ALTER TABLE [dbo].[CustomerCorrespondence] ADD  CONSTRAINT [DF__CustomerC__CodeD__467D75B8]  DEFAULT (0) FOR [CodeDetailInstance]
GO
ALTER TABLE [dbo].[CustomerCorrespondence] ADD  CONSTRAINT [DF__CustomerC__Statu__477199F1]  DEFAULT (0) FOR [StatusInstance]
GO
ALTER TABLE [dbo].[CustomerCorrespondence] ADD  CONSTRAINT [DF__CustomerC__Custo__4865BE2A]  DEFAULT (0) FOR [CustomerOrderHeader]
GO
ALTER TABLE [dbo].[CustomerCorrespondence] ADD  CONSTRAINT [DF__CustomerC__DateC__4959E263]  DEFAULT ('1/1/1995') FOR [DateCreated]
GO
ALTER TABLE [dbo].[CustomerCorrespondence] ADD  CONSTRAINT [DF__CustomerC__UserI__4A4E069C]  DEFAULT (' ') FOR [UserIDCreated]
GO
ALTER TABLE [dbo].[CustomerCorrespondence] ADD  CONSTRAINT [DF__CustomerC__DateC__4B422AD5]  DEFAULT ('1/1/1995') FOR [DateChanged]
GO
ALTER TABLE [dbo].[CustomerCorrespondence] ADD  CONSTRAINT [DF__CustomerC__UserI__4C364F0E]  DEFAULT (null) FOR [UserIDChanged]
GO
ALTER TABLE [dbo].[CustomerCorrespondence] ADD  CONSTRAINT [DF_CustomerCo_CustomerPa1__106]  DEFAULT (0) FOR [CustomerPaymentHeader]
GO
ALTER TABLE [dbo].[CustomerCorrespondence] ADD  CONSTRAINT [DF__CustomerC__SendT__2A363CC5]  DEFAULT (0) FOR [SendType]
GO
