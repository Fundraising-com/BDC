USE [eFundweb]
GO
/****** Object:  Table [dbo].[Agent_Comment]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Agent_Comment](
	[Comment_ID] [int] IDENTITY(1,1) NOT NULL,
	[Agent_ID] [int] NOT NULL,
	[Comment] [varchar](250) NULL,
	[Timestamp] [datetime] NOT NULL,
	[Added_By] [varchar](15) NULL,
 CONSTRAINT [PK_comment_id] PRIMARY KEY CLUSTERED 
(
	[Comment_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
