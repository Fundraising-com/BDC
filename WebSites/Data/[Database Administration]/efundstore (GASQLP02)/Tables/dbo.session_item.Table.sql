USE [eFundstore]
GO
/****** Object:  Table [dbo].[session_item]    Script Date: 02/14/2014 16:26:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[session_item](
	[session_item_id] [int] IDENTITY(1,1) NOT NULL,
	[session_id] [int] NOT NULL,
	[name] [varchar](25) NOT NULL,
	[value] [varchar](25) NOT NULL,
 CONSTRAINT [PK_session_item] PRIMARY KEY CLUSTERED 
(
	[session_item_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
