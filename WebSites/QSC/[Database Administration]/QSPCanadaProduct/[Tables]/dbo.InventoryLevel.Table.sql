USE [QSPCanadaProduct]
GO
/****** Object:  Table [dbo].[InventoryLevel]    Script Date: 06/07/2017 09:17:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InventoryLevel](
	[InventoryLevelID] [int] IDENTITY(1,1) NOT NULL,
	[InventoryLevelBatchID] [int] NOT NULL,
	[ProductCode] [nvarchar](255) NOT NULL,
	[QtyAvailable] [int] NOT NULL,
	[EffectiveDate] [datetime] NOT NULL,
	[StatusID] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[TP_InventoryLevelID] [int] NULL,
 CONSTRAINT [PK_InventoryLevel] PRIMARY KEY CLUSTERED 
(
	[InventoryLevelID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[InventoryLevel]  WITH CHECK ADD  CONSTRAINT [FK_InventoryLevel_InventoryLevelBatch] FOREIGN KEY([InventoryLevelBatchID])
REFERENCES [dbo].[InventoryLevelBatch] ([InventoryLevelBatchID])
GO
ALTER TABLE [dbo].[InventoryLevel] CHECK CONSTRAINT [FK_InventoryLevel_InventoryLevelBatch]
GO
ALTER TABLE [dbo].[InventoryLevel]  WITH CHECK ADD  CONSTRAINT [FK_InventoryLevel_InventoryLevelStatus] FOREIGN KEY([StatusID])
REFERENCES [dbo].[InventoryLevelStatus] ([InventoryLevelStatusID])
GO
ALTER TABLE [dbo].[InventoryLevel] CHECK CONSTRAINT [FK_InventoryLevel_InventoryLevelStatus]
GO
ALTER TABLE [dbo].[InventoryLevel] ADD  CONSTRAINT [DF_InventoryLevel_Processed]  DEFAULT ((1)) FOR [StatusID]
GO
ALTER TABLE [dbo].[InventoryLevel] ADD  CONSTRAINT [DF_InventoryLevel_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
