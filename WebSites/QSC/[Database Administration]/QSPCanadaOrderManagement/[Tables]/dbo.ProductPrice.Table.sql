USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[ProductPrice]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProductPrice](
	[Code] [varchar](4) NOT NULL,
	[Term] [int] NOT NULL,
	[Price] [float] NOT NULL,
	[Season] [varchar](1) NULL,
	[Available] [dbo].[Boolean_UDDT] NOT NULL,
	[MusicType] [varchar](1) NULL,
	[PublisherAcceptPrice] [dbo].[Boolean_UDDT] NOT NULL,
	[PctRemitToPublisher] [float] NOT NULL,
 CONSTRAINT [aaaaaProductPrice_PK] PRIMARY KEY CLUSTERED 
(
	[Code] ASC,
	[Term] ASC,
	[Price] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[ProductPrice] ADD  CONSTRAINT [DF__ProductPri__Code__2A4B4B5E]  DEFAULT (' ') FOR [Code]
GO
ALTER TABLE [dbo].[ProductPrice] ADD  CONSTRAINT [DF__ProductPri__Term__2B3F6F97]  DEFAULT (1) FOR [Term]
GO
ALTER TABLE [dbo].[ProductPrice] ADD  CONSTRAINT [DF__ProductPr__Price__2C3393D0]  DEFAULT (0.0) FOR [Price]
GO
ALTER TABLE [dbo].[ProductPrice] ADD  CONSTRAINT [DF__ProductPr__Seaso__2D27B809]  DEFAULT (null) FOR [Season]
GO
ALTER TABLE [dbo].[ProductPrice] ADD  CONSTRAINT [DF__ProductPr__Music__2E1BDC42]  DEFAULT (null) FOR [MusicType]
GO
ALTER TABLE [dbo].[ProductPrice] ADD  CONSTRAINT [DF__ProductPr__PctRe__2F10007B]  DEFAULT (0.0) FOR [PctRemitToPublisher]
GO
