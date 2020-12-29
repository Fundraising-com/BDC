USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[business_rule]    Script Date: 02/14/2014 16:26:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[business_rule](
	[business_rule_id] [int] NOT NULL,
	[email_template_id] [int] NOT NULL,
	[business_rule_name] [varchar](200) NOT NULL,
	[stored_procedure_call] [varchar](100) NULL,
	[priority_level] [smallint] NOT NULL,
	[member_type_id] [smallint] NOT NULL,
	[email_priority] [smallint] NOT NULL,
	[active] [bit] NOT NULL,
	[create_date] [datetime] NOT NULL,
 CONSTRAINT [PK_business_rule] PRIMARY KEY CLUSTERED 
(
	[business_rule_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[business_rule]  WITH NOCHECK ADD  CONSTRAINT [FK_business_rule_member_type] FOREIGN KEY([member_type_id])
REFERENCES [dbo].[member_type] ([member_type_id])
GO
ALTER TABLE [dbo].[business_rule] CHECK CONSTRAINT [FK_business_rule_member_type]
GO
ALTER TABLE [dbo].[business_rule] ADD  CONSTRAINT [DF_business_rule_email_priority]  DEFAULT (1) FOR [email_priority]
GO
ALTER TABLE [dbo].[business_rule] ADD  CONSTRAINT [DF_business_rule_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
