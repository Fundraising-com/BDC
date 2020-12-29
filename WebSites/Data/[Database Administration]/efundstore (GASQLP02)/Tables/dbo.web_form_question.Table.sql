USE [eFundstore]
GO
/****** Object:  Table [dbo].[web_form_question]    Script Date: 02/14/2014 16:26:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[web_form_question](
	[question_id] [int] NOT NULL,
	[web_form_id] [int] NOT NULL,
	[required] [bit] NULL,
	[question_order] [int] NULL,
	[datestamp] [datetime] NULL,
 CONSTRAINT [PK_questions_entry_form] PRIMARY KEY CLUSTERED 
(
	[question_id] ASC,
	[web_form_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[web_form_question] ADD  CONSTRAINT [DF_web_forms_questions_datestamp]  DEFAULT (getdate()) FOR [datestamp]
GO
