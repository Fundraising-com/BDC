USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[email_template]    Script Date: 02/14/2014 16:26:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[email_template](
	[email_template_id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[email_template_type_id] [int] NOT NULL,
	[email_template_name] [varchar](50) NOT NULL,
	[description] [varchar](255) NOT NULL,
	[param_procedure_call] [varchar](100) NULL,
	[from_name] [varchar](50) NOT NULL,
	[from_email_address] [varchar](100) NOT NULL,
	[reply_to_name] [varchar](50) NOT NULL,
	[reply_to_email_address] [varchar](100) NOT NULL,
	[bounce_name] [varchar](50) NOT NULL,
	[bounce_email_address] [varchar](100) NOT NULL,
	[create_date] [datetime] NOT NULL,
 CONSTRAINT [PK_email_template] PRIMARY KEY CLUSTERED 
(
	[email_template_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[email_template]  WITH CHECK ADD  CONSTRAINT [FK_email_template_email_template_type] FOREIGN KEY([email_template_type_id])
REFERENCES [dbo].[email_template_type] ([email_template_type_id])
GO
ALTER TABLE [dbo].[email_template] CHECK CONSTRAINT [FK_email_template_email_template_type]
GO
ALTER TABLE [dbo].[email_template] ADD  CONSTRAINT [DF_email_template_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
