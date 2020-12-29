USE [fastfundraising]
GO
/****** Object:  Table [dbo].[fffeedback]    Script Date: 02/14/2014 16:35:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[fffeedback](
	[fbid] [int] IDENTITY(1000,1) NOT NULL,
	[fbcustname] [varchar](100) NULL,
	[fbemail] [varchar](255) NULL,
	[fbmessage] [varchar](4000) NULL,
	[receiveddate] [datetime] NULL,
	[replystatus] [int] NULL,
	[replieddate] [datetime] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
