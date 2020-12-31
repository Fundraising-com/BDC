USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[PrizeRollup]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrizeRollup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NOT NULL,
	[CustomerOrderHeaderInstance] [int] NOT NULL,
	[TransId] [int] NOT NULL,
	[MagPrice_Instance] [int] NOT NULL,
	[Quantity] [int] NOT NULL
) ON [PRIMARY]
GO
