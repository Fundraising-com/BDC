USE [eFundstore]
GO
/****** Object:  Table [dbo].[_tbd_partner_type]    Script Date: 02/14/2014 16:26:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[_tbd_partner_type](
	[partner_type_id] [int] NOT NULL,
	[name] [varchar](50) NOT NULL,
	[create_date] [datetime] NOT NULL,
 CONSTRAINT [PK_partner_type] PRIMARY KEY CLUSTERED 
(
	[partner_type_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[_tbd_partner_type] ADD  CONSTRAINT [DF_partner_type_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
