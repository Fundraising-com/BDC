USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[TaxDetail]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TaxDetail](
	[CustomerOrderHeaderInstance] [int] NOT NULL,
	[TransID] [int] NOT NULL,
	[AccountID] [int] NOT NULL,
	[ProductCode] [varchar](4) NULL,
	[ProductName] [varchar](30) NULL,
	[ProductType] [varchar](3) NULL,
	[Price] [float] NOT NULL,
	[PriceA] [float] NOT NULL,
	[Tax] [float] NOT NULL,
	[TaxA] [float] NOT NULL,
	[CustomerInstance] [int] NOT NULL,
	[City] [varchar](20) NULL,
	[County] [varchar](31) NULL,
	[State] [dbo].[State_UDDT] NULL,
	[Zip] [varchar](6) NULL,
	[CreationDate] [datetime] NOT NULL,
	[CrossedBridgeDate] [datetime] NOT NULL,
 CONSTRAINT [aaaaaTaxDetail_PK] PRIMARY KEY CLUSTERED 
(
	[CustomerOrderHeaderInstance] ASC,
	[TransID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[TaxDetail] ADD  CONSTRAINT [DF__TaxDetail__Custo__318258D2]  DEFAULT (0) FOR [CustomerOrderHeaderInstance]
GO
ALTER TABLE [dbo].[TaxDetail] ADD  CONSTRAINT [DF__TaxDetail__Trans__32767D0B]  DEFAULT (0) FOR [TransID]
GO
ALTER TABLE [dbo].[TaxDetail] ADD  CONSTRAINT [DF__TaxDetail__Accou__336AA144]  DEFAULT (0) FOR [AccountID]
GO
ALTER TABLE [dbo].[TaxDetail] ADD  CONSTRAINT [DF__TaxDetail__Produ__345EC57D]  DEFAULT (null) FOR [ProductCode]
GO
ALTER TABLE [dbo].[TaxDetail] ADD  CONSTRAINT [DF__TaxDetail__Produ__3552E9B6]  DEFAULT (null) FOR [ProductName]
GO
ALTER TABLE [dbo].[TaxDetail] ADD  CONSTRAINT [DF__TaxDetail__Produ__36470DEF]  DEFAULT (null) FOR [ProductType]
GO
ALTER TABLE [dbo].[TaxDetail] ADD  CONSTRAINT [DF__TaxDetail__Price__373B3228]  DEFAULT (0.0) FOR [Price]
GO
ALTER TABLE [dbo].[TaxDetail] ADD  CONSTRAINT [DF__TaxDetail__Price__382F5661]  DEFAULT (0.0) FOR [PriceA]
GO
ALTER TABLE [dbo].[TaxDetail] ADD  CONSTRAINT [DF__TaxDetail__Tax__39237A9A]  DEFAULT (0.0) FOR [Tax]
GO
ALTER TABLE [dbo].[TaxDetail] ADD  CONSTRAINT [DF__TaxDetail__TaxA__3A179ED3]  DEFAULT (0.0) FOR [TaxA]
GO
ALTER TABLE [dbo].[TaxDetail] ADD  CONSTRAINT [DF__TaxDetail__Custo__3B0BC30C]  DEFAULT (0) FOR [CustomerInstance]
GO
ALTER TABLE [dbo].[TaxDetail] ADD  CONSTRAINT [DF__TaxDetail__City__3BFFE745]  DEFAULT (null) FOR [City]
GO
ALTER TABLE [dbo].[TaxDetail] ADD  CONSTRAINT [DF__TaxDetail__Count__3CF40B7E]  DEFAULT (null) FOR [County]
GO
ALTER TABLE [dbo].[TaxDetail] ADD  CONSTRAINT [DF__TaxDetail__State__3DE82FB7]  DEFAULT (' ') FOR [State]
GO
ALTER TABLE [dbo].[TaxDetail] ADD  CONSTRAINT [DF__TaxDetail__Zip__3EDC53F0]  DEFAULT (null) FOR [Zip]
GO
ALTER TABLE [dbo].[TaxDetail] ADD  CONSTRAINT [DF__TaxDetail__Creat__3FD07829]  DEFAULT ('1/1/1995') FOR [CreationDate]
GO
ALTER TABLE [dbo].[TaxDetail] ADD  CONSTRAINT [DF__TaxDetail__Cross__40C49C62]  DEFAULT ('1/1/1995') FOR [CrossedBridgeDate]
GO
