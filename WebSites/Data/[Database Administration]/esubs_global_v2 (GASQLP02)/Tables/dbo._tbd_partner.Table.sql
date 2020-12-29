USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[_tbd_partner]    Script Date: 02/14/2014 16:26:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[_tbd_partner](
	[partner_id] [int] NOT NULL,
	[partner_type_id] [int] NOT NULL,
	[partner_name] [varchar](100) NULL,
	[has_collection_site] [bit] NOT NULL,
	[guid] [uniqueidentifier] NOT NULL,
	[create_date] [datetime] NOT NULL,
	[is_active] [bit] NOT NULL,
 CONSTRAINT [PK_partner] PRIMARY KEY CLUSTERED 
(
	[partner_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[_tbd_partner] ADD  CONSTRAINT [DF_partner_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
ALTER TABLE [dbo].[_tbd_partner] ADD  CONSTRAINT [DFP_profit_id]  DEFAULT ((1)) FOR [is_active]
GO
