USE [eFundstore]
GO
/****** Object:  Table [dbo].[question]    Script Date: 02/14/2014 16:26:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[question](
	[question_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](100) NULL,
	[description] [varchar](600) NULL,
	[control_type_id] [int] NULL,
	[field_name] [varchar](100) NULL,
	[default_value] [varchar](100) NULL,
	[min_lenght] [int] NULL,
	[max_lenght] [int] NULL,
	[nbr_value] [int] NULL,
	[datestamp] [datetime] NULL,
	[stored_proc_to_call] [varchar](200) NULL,
	[answer_type] [varchar](20) NULL,
	[field_value] [varchar](100) NULL,
 CONSTRAINT [PK_question] PRIMARY KEY CLUSTERED 
(
	[question_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[question] ADD  CONSTRAINT [DF_question_control_type_id]  DEFAULT (null) FOR [control_type_id]
GO
ALTER TABLE [dbo].[question] ADD  CONSTRAINT [DF_question_datestamp]  DEFAULT (getdate()) FOR [datestamp]
GO
