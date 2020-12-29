USE [fastfundraising]
GO
/****** Object:  Table [dbo].[ffquestions]    Script Date: 02/14/2014 16:35:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ffquestions](
	[fname] [varchar](50) NULL,
	[email] [varchar](255) NULL,
	[question] [varchar](1000) NULL,
	[entrydate] [datetime] NULL,
	[rowid] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
