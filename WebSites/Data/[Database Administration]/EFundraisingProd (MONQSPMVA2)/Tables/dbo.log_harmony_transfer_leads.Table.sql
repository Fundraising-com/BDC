USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[log_harmony_transfer_leads]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[log_harmony_transfer_leads](
	[ID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[list_name] [varchar](100) NULL,
	[list_desc] [varchar](100) NULL,
	[old_consultant_id] [int] NULL,
	[new_consultant_id] [int] NULL,
	[transferer_id] [int] NULL,
	[transfer_date] [datetime] NULL,
	[lead_id] [int] NULL,
 CONSTRAINT [PK_log_harmony_transfer_leads] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[log_harmony_transfer_leads] ADD  CONSTRAINT [DF_log_harmony_transfer_leads_transfer_date]  DEFAULT (getdate()) FOR [transfer_date]
GO
