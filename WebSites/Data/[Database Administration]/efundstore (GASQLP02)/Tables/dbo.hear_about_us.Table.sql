USE [eFundstore]
GO
/****** Object:  Table [dbo].[hear_about_us]    Script Date: 02/14/2014 16:26:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[hear_about_us](
	[hear_about_us_id] [tinyint] NOT NULL,
	[party_type_id] [tinyint] NOT NULL,
	[name] [varchar](50) NOT NULL,
	[order_on_web] [tinyint] NOT NULL,
	[is_active] [bit] NULL,
 CONSTRAINT [PK_hear_about_us] PRIMARY KEY CLUSTERED 
(
	[hear_about_us_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
