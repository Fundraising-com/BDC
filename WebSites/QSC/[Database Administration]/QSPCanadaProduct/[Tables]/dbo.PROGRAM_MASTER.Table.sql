USE [QSPCanadaProduct]
GO
/****** Object:  Table [dbo].[PROGRAM_MASTER]    Script Date: 06/07/2017 09:17:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PROGRAM_MASTER](
	[Program_ID] [int] IDENTITY(1,1) NOT NULL,
	[Program_Type] [varchar](50) NOT NULL,
	[SubType] [int] NOT NULL,
	[Season] [int] NULL,
	[Alpha] [varchar](50) NULL,
	[Code] [varchar](50) NULL,
	[Status] [int] NULL,
	[Country] [varchar](10) NULL,
	[Lang] [varchar](10) NULL,
	[IsReplacement] [char](1) NULL,
	[IsNational] [char](1) NULL,
	[DateCreated] [datetime] NULL,
	[UserIDCreated] [dbo].[UserID_UDDT] NULL,
	[DateChanged] [datetime] NULL,
	[UserIDChanged] [dbo].[UserID_UDDT] NULL,
 CONSTRAINT [PK_PROGRAM_MASTER] PRIMARY KEY CLUSTERED 
(
	[Program_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
