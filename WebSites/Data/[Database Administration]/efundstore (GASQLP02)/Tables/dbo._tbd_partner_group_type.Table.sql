USE [eFundstore]
GO
/****** Object:  Table [dbo].[_tbd_partner_group_type]    Script Date: 02/14/2014 16:26:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[_tbd_partner_group_type](
	[partner_group_type_id] [tinyint] IDENTITY(0,1) NOT NULL,
	[description] [varchar](20) NULL,
 CONSTRAINT [PK_partner_group_type_id] PRIMARY KEY CLUSTERED 
(
	[partner_group_type_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
