USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[ShipmentVariation]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ShipmentVariation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SessionId] [varchar](100) NOT NULL,
	[CustomerOrderHeaderInstance] [int] NOT NULL,
	[TransId] [int] NOT NULL,
	[QuantityShipped] [int] NOT NULL,
	[QuantityReplaced] [int] NOT NULL,
	[ReplacementItemId] [int] NULL,
	[ShipTF] [bit] NOT NULL,
	[Comment] [varchar](255) NULL,
	[CustomerComment] [varchar](255) NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModifyDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[ShipmentVariation] ADD  CONSTRAINT [DF_ShipmentVariations_ShipTF]  DEFAULT (1) FOR [ShipTF]
GO
ALTER TABLE [dbo].[ShipmentVariation] ADD  CONSTRAINT [DF_ShipmentVariations_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[ShipmentVariation] ADD  CONSTRAINT [DF_ShipmentVariations_ModifyDate]  DEFAULT (getdate()) FOR [ModifyDate]
GO
