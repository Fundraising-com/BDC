USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[OrderClosingLog]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[OrderClosingLog](
	[LogId] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[CustomerOrderHeaderInstance] [int] NULL,
	[TransId] [int] NULL,
	[ProductCode] [varchar](20) NULL,
	[ProductName] [varchar](50) NULL,
	[ClosingErrorMessage] [varchar](200) NULL,
	[ClosingEvent] [varchar](10) NULL,
	[DateTimeCreated] [datetime] NULL,
	[IsFixed] [int] NULL,
	[DateFixed] [datetime] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[OrderClosingLog]  WITH NOCHECK ADD  CONSTRAINT [CK__OrderClos__Closi__5F74D762] CHECK  (([ClosingEvent] = 'POST' or [ClosingEvent] = 'PRE'))
GO
ALTER TABLE [dbo].[OrderClosingLog] CHECK CONSTRAINT [CK__OrderClos__Closi__5F74D762]
GO
ALTER TABLE [dbo].[OrderClosingLog] ADD  CONSTRAINT [DF__OrderClos__Closi__5E80B329]  DEFAULT ('PRE') FOR [ClosingEvent]
GO
ALTER TABLE [dbo].[OrderClosingLog] ADD  CONSTRAINT [DF__OrderClos__IsFix__6068FB9B]  DEFAULT (0) FOR [IsFixed]
GO
