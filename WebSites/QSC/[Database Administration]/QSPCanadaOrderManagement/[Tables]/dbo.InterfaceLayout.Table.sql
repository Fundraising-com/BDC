USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[InterfaceLayout]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[InterfaceLayout](
	[InterfaceLayoutId] [int] NOT NULL,
	[InterfaceMediaId] [int] NOT NULL,
	[SequenceId] [int] NOT NULL,
	[InterfaceLayoutName] [varchar](50) NOT NULL,
	[SQLStatement] [varchar](5000) NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
