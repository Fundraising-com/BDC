USE [fastfundraising]
GO
/****** Object:  Table [dbo].[category]    Script Date: 02/14/2014 16:35:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[category](
	[categoryname] [varchar](50) NULL,
	[categoryid] [int] NOT NULL,
	[catdisplayitemid] [int] NULL,
	[displayitemimagepath] [varchar](50) NULL,
	[status] [int] NULL,
	[SortOrder] [int] NULL,
	[shipping_group_id] [int] NULL,
 CONSTRAINT [PK_category] PRIMARY KEY CLUSTERED 
(
	[categoryid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
