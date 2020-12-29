USE [eFundstore]
GO
/****** Object:  Table [dbo].[question_param_target]    Script Date: 02/14/2014 16:26:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[question_param_target](
	[question_id] [int] NOT NULL,
	[web_form_id] [int] NOT NULL,
	[parameter_target] [varchar](75) NOT NULL,
 CONSTRAINT [PK_question_params_target] PRIMARY KEY CLUSTERED 
(
	[question_id] ASC,
	[web_form_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
