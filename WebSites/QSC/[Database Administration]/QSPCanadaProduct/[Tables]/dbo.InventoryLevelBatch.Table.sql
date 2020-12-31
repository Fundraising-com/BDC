USE [QSPCanadaProduct]
GO
/****** Object:  Table [dbo].[InventoryLevelBatch]    Script Date: 06/07/2017 09:17:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InventoryLevelBatch](
	[InventoryLevelBatchID] [int] IDENTITY(1,1) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[FileName] [nvarchar](137) NULL,
 CONSTRAINT [PK_InventoryLevelBatch] PRIMARY KEY CLUSTERED 
(
	[InventoryLevelBatchID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[InventoryLevelBatch] ADD  CONSTRAINT [DF_InventoryLevelBatch_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
