USE [eFundstore]
GO
/****** Object:  Table [dbo].[web_form]    Script Date: 02/14/2014 16:26:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[web_form](
	[web_form_id] [int] IDENTITY(1,1) NOT NULL,
	[web_form_desc] [varchar](600) NOT NULL,
	[web_form_type_id] [int] NOT NULL,
	[lead_status_id] [int] NOT NULL,
	[stored_proc_to_call] [varchar](200) NULL,
	[datestamp] [datetime] NULL,
 CONSTRAINT [PK_web_forms] PRIMARY KEY CLUSTERED 
(
	[web_form_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[web_form] ADD  CONSTRAINT [DF_web_forms_web_form_type_id]  DEFAULT (1) FOR [web_form_type_id]
GO
ALTER TABLE [dbo].[web_form] ADD  CONSTRAINT [DF_web_forms_datestamp]  DEFAULT (getdate()) FOR [datestamp]
GO
