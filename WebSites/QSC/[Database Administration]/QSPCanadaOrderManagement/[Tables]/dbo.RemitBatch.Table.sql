USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[RemitBatch]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RemitBatch](
	[ID] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[CountryCode] [dbo].[CountryCode_UDDT] NULL,
	[Status] [int] NULL,
	[Filename] [varchar](200) NULL,
	[FulfillmentHouseNbr] [varchar](3) NULL,
	[TotalBasePrice] [numeric](10, 2) NULL,
	[TotalUnits] [int] NULL,
	[TotalCHADD] [int] NULL,
	[TotalCancelled] [int] NULL,
	[DateChanged] [datetime] NULL,
	[UserIDChanged] [dbo].[UserID_UDDT] NULL,
	[TotalCatalogPrice] [numeric](18, 0) NULL,
	[TotalItemPrice] [numeric](18, 0) NULL,
	[APStatus] [int] NULL,
	[RunID] [int] NULL,
	[Count] [int] NOT NULL,
 CONSTRAINT [PK_RemitBatch] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 mean Ap invoices are not generated yet and 1 means AP invoices has been generated.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RemitBatch', @level2type=N'COLUMN',@level2name=N'APStatus'
GO
ALTER TABLE [dbo].[RemitBatch] ADD  CONSTRAINT [DF_RemitBatch_Date]  DEFAULT (1 / 1 / 95) FOR [Date]
GO
ALTER TABLE [dbo].[RemitBatch] ADD  CONSTRAINT [DF_RemitBatch_APStatus]  DEFAULT (0) FOR [APStatus]
GO
ALTER TABLE [dbo].[RemitBatch] ADD  CONSTRAINT [DF_RemitBatch_Count]  DEFAULT (0) FOR [Count]
GO
