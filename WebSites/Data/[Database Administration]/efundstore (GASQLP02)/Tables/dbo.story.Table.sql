USE [eFundstore]
GO
/****** Object:  Table [dbo].[story]    Script Date: 02/14/2014 16:26:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[story](
	[story_id] [int] IDENTITY(1,1) NOT NULL,
	[story_type_id] [int] NOT NULL,
	[group_type_id] [int] NOT NULL,
	[story_text] [text] NULL,
 CONSTRAINT [PK_sotry] PRIMARY KEY CLUSTERED 
(
	[story_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
