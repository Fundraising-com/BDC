USE [fastfundraising]
GO
/****** Object:  Table [dbo].[fftechsupport]    Script Date: 02/14/2014 16:35:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[fftechsupport](
	[ticketid] [int] IDENTITY(1000,1) NOT NULL,
	[tcustname] [varchar](100) NULL,
	[temail] [varchar](255) NULL,
	[tmessage] [varchar](4000) NULL,
	[receiveddate] [datetime] NULL,
	[replystatus] [int] NULL,
	[replieddate] [datetime] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
