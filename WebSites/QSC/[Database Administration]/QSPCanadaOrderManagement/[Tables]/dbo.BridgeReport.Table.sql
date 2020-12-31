USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[BridgeReport]    Script Date: 06/07/2017 09:18:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BridgeReport](
	[BridgeFileReportDate] [datetime] NOT NULL,
	[BatchNumber] [varchar](6) NOT NULL,
	[TickNumber] [varchar](4) NOT NULL,
	[AccountNumber] [int] NOT NULL,
	[CustomerName] [varchar](20) NULL,
	[ProductCode] [varchar](4) NULL,
	[ProductTitle] [varchar](20) NULL,
	[ProductPrice] [float] NOT NULL,
	[Term] [int] NOT NULL,
	[ProcessingFee] [float] NOT NULL,
	[Donation] [float] NOT NULL,
	[TotalOrder] [float] NOT NULL,
 CONSTRAINT [aaaaaBridgeReport_PK] PRIMARY KEY CLUSTERED 
(
	[BridgeFileReportDate] ASC,
	[BatchNumber] ASC,
	[TickNumber] ASC,
	[AccountNumber] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[BridgeReport] ADD  CONSTRAINT [DF__BridgeRep__Bridg__4E33A619]  DEFAULT ('1/1/1995') FOR [BridgeFileReportDate]
GO
ALTER TABLE [dbo].[BridgeReport] ADD  CONSTRAINT [DF__BridgeRep__Batch__4F27CA52]  DEFAULT (' ') FOR [BatchNumber]
GO
ALTER TABLE [dbo].[BridgeReport] ADD  CONSTRAINT [DF__BridgeRep__TickN__501BEE8B]  DEFAULT (' ') FOR [TickNumber]
GO
ALTER TABLE [dbo].[BridgeReport] ADD  CONSTRAINT [DF__BridgeRep__Accou__511012C4]  DEFAULT (0) FOR [AccountNumber]
GO
ALTER TABLE [dbo].[BridgeReport] ADD  CONSTRAINT [DF__BridgeRep__Custo__520436FD]  DEFAULT (null) FOR [CustomerName]
GO
ALTER TABLE [dbo].[BridgeReport] ADD  CONSTRAINT [DF__BridgeRep__Produ__52F85B36]  DEFAULT (null) FOR [ProductCode]
GO
ALTER TABLE [dbo].[BridgeReport] ADD  CONSTRAINT [DF__BridgeRep__Produ__53EC7F6F]  DEFAULT (null) FOR [ProductTitle]
GO
ALTER TABLE [dbo].[BridgeReport] ADD  CONSTRAINT [DF__BridgeRep__Produ__54E0A3A8]  DEFAULT (0.0) FOR [ProductPrice]
GO
ALTER TABLE [dbo].[BridgeReport] ADD  CONSTRAINT [DF__BridgeRepo__Term__55D4C7E1]  DEFAULT (0) FOR [Term]
GO
ALTER TABLE [dbo].[BridgeReport] ADD  CONSTRAINT [DF__BridgeRep__Proce__56C8EC1A]  DEFAULT (0.0) FOR [ProcessingFee]
GO
ALTER TABLE [dbo].[BridgeReport] ADD  CONSTRAINT [DF__BridgeRep__Donat__57BD1053]  DEFAULT (0.0) FOR [Donation]
GO
ALTER TABLE [dbo].[BridgeReport] ADD  CONSTRAINT [DF__BridgeRep__Total__58B1348C]  DEFAULT (0.0) FOR [TotalOrder]
GO
