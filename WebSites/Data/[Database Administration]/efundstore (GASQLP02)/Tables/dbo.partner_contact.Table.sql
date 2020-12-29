USE [eFundstore]
GO
/****** Object:  Table [dbo].[partner_contact]    Script Date: 02/14/2014 16:26:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[partner_contact](
	[partner_contact_id] [smallint] IDENTITY(1,1) NOT NULL,
	[partner_id] [int] NOT NULL,
	[culture_code] [nvarchar](5) NULL,
	[section_name] [varchar](50) NOT NULL,
	[section_value] [varchar](500) NOT NULL,
	[display_order] [tinyint] NOT NULL,
 CONSTRAINT [PK_partner_contacts] PRIMARY KEY NONCLUSTERED 
(
	[partner_contact_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
