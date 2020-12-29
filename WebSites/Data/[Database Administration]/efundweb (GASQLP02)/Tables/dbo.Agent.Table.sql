USE [eFundweb]
GO
/****** Object:  Table [dbo].[Agent]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Agent](
	[Agent_ID] [int] IDENTITY(1,1) NOT NULL,
	[URL] [varchar](40) NULL,
	[Company] [varchar](50) NULL,
	[Agent_Name] [varchar](40) NULL,
	[Logo] [varchar](70) NULL,
	[Add_Timestamp] [timestamp] NOT NULL,
	[Add_By_User] [varchar](10) NULL,
 CONSTRAINT [PK_Agent_ID] PRIMARY KEY CLUSTERED 
(
	[Agent_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
