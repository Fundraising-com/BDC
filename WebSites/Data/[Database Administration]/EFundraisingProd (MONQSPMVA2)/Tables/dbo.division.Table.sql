USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[division]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[division](
	[division_id] [tinyint] NOT NULL,
	[division_name] [varchar](50) NOT NULL,
	[logo] [varchar](50) NULL,
	[short_name] [varchar](10) NOT NULL,
 CONSTRAINT [PK_division] PRIMARY KEY CLUSTERED 
(
	[division_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UX_division_division_name] UNIQUE NONCLUSTERED 
(
	[division_name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
