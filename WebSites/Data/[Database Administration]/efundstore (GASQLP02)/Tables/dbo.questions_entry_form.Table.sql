USE [eFundstore]
GO
/****** Object:  Table [dbo].[questions_entry_form]    Script Date: 02/14/2014 16:26:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[questions_entry_form](
	[question_id] [int] NOT NULL,
	[web_form_id] [int] NOT NULL,
	[required] [bit] NULL,
	[question_order] [int] NULL,
	[insert_table] [varchar](100) NULL,
	[insert_column] [varchar](100) NOT NULL,
	[default_value] [varchar](200) NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
