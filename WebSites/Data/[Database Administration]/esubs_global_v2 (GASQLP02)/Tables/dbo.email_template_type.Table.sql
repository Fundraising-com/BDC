USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[email_template_type]    Script Date: 02/14/2014 16:26:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[email_template_type](
	[email_template_type_id] [int] NOT NULL,
	[email_template_name] [varchar](50) NOT NULL,
	[create_date] [datetime] NOT NULL,
 CONSTRAINT [PK_email_template_type] PRIMARY KEY CLUSTERED 
(
	[email_template_type_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[email_template_type] ADD  CONSTRAINT [DF_email_template_type_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
