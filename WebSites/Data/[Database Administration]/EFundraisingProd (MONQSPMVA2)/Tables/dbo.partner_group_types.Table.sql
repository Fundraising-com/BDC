USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[partner_group_types]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[partner_group_types](
	[partner_group_type_id] [tinyint] IDENTITY(0,1) NOT FOR REPLICATION NOT NULL,
	[partner_group_type_desc] [varchar](20) NOT NULL,
 CONSTRAINT [PK_partner_group_types] PRIMARY KEY CLUSTERED 
(
	[partner_group_type_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
