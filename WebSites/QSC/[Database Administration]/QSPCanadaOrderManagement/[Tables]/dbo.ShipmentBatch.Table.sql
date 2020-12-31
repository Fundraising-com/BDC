USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[ShipmentBatch]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShipmentBatch](
	[ID] [int] IDENTITY(1000,1) NOT NULL,
	[CreationDate] [datetime] NULL,
	[Filename] [nvarchar](100) NULL,
 CONSTRAINT [PK_ShipmentBatch] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
