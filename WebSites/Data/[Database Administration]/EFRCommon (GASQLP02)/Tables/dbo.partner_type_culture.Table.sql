USE [EFRCommon]
GO
/****** Object:  Table [dbo].[partner_type_culture]    Script Date: 02/14/2014 16:26:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[partner_type_culture](
	[partner_type_id] [int] NOT NULL,
	[culture_code] [nvarchar](5) NOT NULL,
	[partner_type_name] [varchar](255) NOT NULL,
	[create_date] [datetime] NOT NULL,
 CONSTRAINT [PK_partner_type_culture] PRIMARY KEY CLUSTERED 
(
	[partner_type_id] ASC,
	[culture_code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[partner_type_culture]  WITH CHECK ADD  CONSTRAINT [FK_partner_type_culture_partner_type] FOREIGN KEY([partner_type_id])
REFERENCES [dbo].[partner_type] ([partner_type_id])
GO
ALTER TABLE [dbo].[partner_type_culture] CHECK CONSTRAINT [FK_partner_type_culture_partner_type]
GO
