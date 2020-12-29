USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[Results]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Results](
	[email_template_id] [int] NOT NULL,
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
	[create_date] [datetime] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
