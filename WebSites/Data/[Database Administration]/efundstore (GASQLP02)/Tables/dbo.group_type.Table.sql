USE [eFundstore]
GO
/****** Object:  Table [dbo].[group_type]    Script Date: 02/14/2014 16:26:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[group_type](
	[group_type_id] [tinyint] NOT NULL,
	[party_type_id] [tinyint] NULL,
	[description] [varchar](50) NOT NULL,
 CONSTRAINT [PK_group_type] PRIMARY KEY CLUSTERED 
(
	[group_type_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
