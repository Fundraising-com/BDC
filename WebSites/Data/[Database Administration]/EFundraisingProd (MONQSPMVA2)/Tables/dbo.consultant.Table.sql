USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[consultant]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[consultant](
	[consultant_id] [int] NOT NULL,
	[division_id] [tinyint] NOT NULL,
	[client_id] [int] NULL,
	[client_sequence_code] [varchar](4) NULL,
	[department_id] [int] NULL,
	[partner_id] [int] NULL,
	[consultant_transfer_status_id] [tinyint] NOT NULL,
	[territory_id] [smallint] NULL,
	[ext_consultant_id] [int] NULL,
	[name] [varchar](50) NOT NULL,
	[is_agent] [bit] NOT NULL,
	[is_active] [bit] NOT NULL,
	[nt_login] [varchar](50) NULL,
	[phone_extension] [varchar](50) NULL,
	[email_address] [varchar](50) NULL,
	[home_phone] [varchar](15) NULL,
	[work_phone] [varchar](15) NULL,
	[fax_number] [varchar](15) NULL,
	[toll_free_phone] [varchar](15) NULL,
	[mobile_phone] [varchar](15) NULL,
	[pager_phone] [varchar](15) NULL,
	[default_proposal_text] [text] NULL,
	[csr_consultant] [bit] NOT NULL,
	[objectives] [float] NULL,
	[is_available] [bit] NOT NULL,
	[password] [varchar](255) NULL,
	[kit_paid] [bit] NOT NULL,
	[is_fm] [bit] NULL,
	[create_date] [datetime] NOT NULL,
	[wfc_id] [bigint] NULL,
 CONSTRAINT [PK_consultant] PRIMARY KEY CLUSTERED 
(
	[consultant_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UX_consultant_name] UNIQUE NONCLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[consultant]  WITH CHECK ADD  CONSTRAINT [FK_consultant_consultant_transfer_status] FOREIGN KEY([consultant_transfer_status_id])
REFERENCES [dbo].[consultant_transfer_status] ([consultant_transfer_status_id])
GO
ALTER TABLE [dbo].[consultant] CHECK CONSTRAINT [FK_consultant_consultant_transfer_status]
GO
ALTER TABLE [dbo].[consultant]  WITH NOCHECK ADD  CONSTRAINT [FK_consultant_department] FOREIGN KEY([department_id])
REFERENCES [dbo].[Department] ([Department_Id])
GO
ALTER TABLE [dbo].[consultant] CHECK CONSTRAINT [FK_consultant_department]
GO
ALTER TABLE [dbo].[consultant]  WITH NOCHECK ADD  CONSTRAINT [FK_consultant_division] FOREIGN KEY([division_id])
REFERENCES [dbo].[division] ([division_id])
GO
ALTER TABLE [dbo].[consultant] CHECK CONSTRAINT [FK_consultant_division]
GO
ALTER TABLE [dbo].[consultant]  WITH NOCHECK ADD  CONSTRAINT [FK_consultant_partner] FOREIGN KEY([partner_id])
REFERENCES [dbo].[_tbd_partner] ([partner_id])
GO
ALTER TABLE [dbo].[consultant] CHECK CONSTRAINT [FK_consultant_partner]
GO
ALTER TABLE [dbo].[consultant]  WITH CHECK ADD  CONSTRAINT [FK_consultant_territory] FOREIGN KEY([territory_id])
REFERENCES [dbo].[territory] ([territory_id])
GO
ALTER TABLE [dbo].[consultant] CHECK CONSTRAINT [FK_consultant_territory]
GO
ALTER TABLE [dbo].[consultant] ADD  CONSTRAINT [DF_consultant_department_id]  DEFAULT (9) FOR [department_id]
GO
ALTER TABLE [dbo].[consultant] ADD  CONSTRAINT [DF_consultant_consultant_transfer_status_id]  DEFAULT (1) FOR [consultant_transfer_status_id]
GO
ALTER TABLE [dbo].[consultant] ADD  CONSTRAINT [DF_consultant_is_agent]  DEFAULT (0) FOR [is_agent]
GO
ALTER TABLE [dbo].[consultant] ADD  CONSTRAINT [DF_consultant_csr_consultant]  DEFAULT (0) FOR [csr_consultant]
GO
ALTER TABLE [dbo].[consultant] ADD  CONSTRAINT [DF_consultant_objectives]  DEFAULT (0) FOR [objectives]
GO
ALTER TABLE [dbo].[consultant] ADD  CONSTRAINT [DF_consultant_is_available]  DEFAULT (0) FOR [is_available]
GO
ALTER TABLE [dbo].[consultant] ADD  CONSTRAINT [DF_consultant_password]  DEFAULT ('pass') FOR [password]
GO
ALTER TABLE [dbo].[consultant] ADD  CONSTRAINT [DF_consultant_kit_paid]  DEFAULT (0) FOR [kit_paid]
GO
ALTER TABLE [dbo].[consultant] ADD  CONSTRAINT [DF_consultant_is_fm]  DEFAULT (0) FOR [is_fm]
GO
ALTER TABLE [dbo].[consultant] ADD  CONSTRAINT [DF_consultant_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
