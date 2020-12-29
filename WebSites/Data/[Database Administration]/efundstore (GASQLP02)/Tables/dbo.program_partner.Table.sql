USE [eFundstore]
GO
/****** Object:  Table [dbo].[program_partner]    Script Date: 02/14/2014 16:26:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[program_partner](
	[program_id] [int] NOT NULL,
	[partner_id] [int] NOT NULL,
	[program_url] [varchar](255) NOT NULL,
	[create_date] [datetime] NOT NULL,
 CONSTRAINT [PK_program_partner] PRIMARY KEY CLUSTERED 
(
	[program_id] ASC,
	[partner_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[program_partner] ADD  CONSTRAINT [DF_program_partner_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
