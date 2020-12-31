USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[Refund]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Refund](
	[CustomerInstance] [int] NOT NULL,
	[RefundSequence] [int] NOT NULL,
	[CustomerOrderHeaderInstance] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Amount] [float] NOT NULL,
	[StatusInstance] [int] NOT NULL,
 CONSTRAINT [aaaaaRefund_PK] PRIMARY KEY CLUSTERED 
(
	[CustomerInstance] ASC,
	[RefundSequence] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Refund] ADD  CONSTRAINT [DF__Refund__Customer__32E0915F]  DEFAULT (0) FOR [CustomerInstance]
GO
ALTER TABLE [dbo].[Refund] ADD  CONSTRAINT [DF__Refund__RefundSe__33D4B598]  DEFAULT (0) FOR [RefundSequence]
GO
ALTER TABLE [dbo].[Refund] ADD  CONSTRAINT [DF__Refund__Customer__34C8D9D1]  DEFAULT (0) FOR [CustomerOrderHeaderInstance]
GO
ALTER TABLE [dbo].[Refund] ADD  CONSTRAINT [DF__Refund__Date__35BCFE0A]  DEFAULT ('1/1/1995') FOR [Date]
GO
ALTER TABLE [dbo].[Refund] ADD  CONSTRAINT [DF__Refund__Amount__36B12243]  DEFAULT (0.0) FOR [Amount]
GO
ALTER TABLE [dbo].[Refund] ADD  CONSTRAINT [DF__Refund__StatusIn__37A5467C]  DEFAULT (0) FOR [StatusInstance]
GO
