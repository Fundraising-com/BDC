USE [eFundstore]
GO
/****** Object:  Table [dbo].[template]    Script Date: 02/14/2014 16:26:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[template](
	[template_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](100) NOT NULL,
	[path] [varchar](1000) NULL,
	[create_date] [datetime] NULL,
 CONSTRAINT [PK_package_templates] PRIMARY KEY CLUSTERED 
(
	[template_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[template] ADD  CONSTRAINT [DF_template_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
