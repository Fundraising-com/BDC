USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[ewnpaticipant_possibleduplicate]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ewnpaticipant_possibleduplicate](
	[Name Last] [varchar](800) NULL,
	[Name First] [varchar](800) NULL,
	[State] [varchar](800) NULL,
	[Current Chapter City] [varchar](800) NULL,
	[eMail Address] [varchar](800) NULL,
	[id] [int] NOT NULL,
	[done] [bit] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
