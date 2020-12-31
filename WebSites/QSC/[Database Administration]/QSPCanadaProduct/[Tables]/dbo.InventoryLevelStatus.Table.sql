USE [QSPCanadaProduct]
GO
/****** Object:  Table [dbo].[InventoryLevelStatus]    Script Date: 06/07/2017 09:17:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InventoryLevelStatus](
	[InventoryLevelStatusID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_InventoryLevelStatus] PRIMARY KEY CLUSTERED 
(
	[InventoryLevelStatusID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
