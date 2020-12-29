USE [esubs_global_v2]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[email_template_field](
	[email_template_field_id] [int] IDENTITY(1,1) NOT NULL,
	[field_name] [varchar](50) NOT NULL,
	[table_name] [varchar](50) NOT NULL,
	[type_name] [varchar](50) NOT NULL,
	[create_date] [datetime] NOT NULL
 CONSTRAINT [PK_email_template_field] PRIMARY KEY CLUSTERED 
(
	[email_template_field_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[email_template_field] ADD  CONSTRAINT [DF_email_template_field_create_date]  DEFAULT (getdate()) FOR [create_date]
GO