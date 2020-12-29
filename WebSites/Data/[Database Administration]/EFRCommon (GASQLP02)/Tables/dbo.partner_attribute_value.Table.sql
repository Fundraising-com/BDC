USE [EFRCommon]
GO
/****** Object:  Table [dbo].[partner_attribute_value]    Script Date: 02/14/2014 16:26:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[partner_attribute_value](
	[partner_id] [int] NOT NULL,
	[partner_attribute_id] [int] NOT NULL,
	[culture_code] [nvarchar](5) NOT NULL,
	[value] [varchar](255) NOT NULL,
	[create_date] [datetime] NOT NULL,
 CONSTRAINT [PK_partner_attribute_value] PRIMARY KEY CLUSTERED 
(
	[partner_id] ASC,
	[partner_attribute_id] ASC,
	[culture_code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[partner_attribute_value]  WITH CHECK ADD  CONSTRAINT [FK_partner_attribute_value_partner] FOREIGN KEY([partner_id])
REFERENCES [dbo].[partner] ([partner_id])
GO
ALTER TABLE [dbo].[partner_attribute_value] CHECK CONSTRAINT [FK_partner_attribute_value_partner]
GO
ALTER TABLE [dbo].[partner_attribute_value]  WITH CHECK ADD  CONSTRAINT [FK_partner_attribute_value_partner_attribute] FOREIGN KEY([partner_attribute_id])
REFERENCES [dbo].[partner_attribute] ([partner_attribute_id])
GO
ALTER TABLE [dbo].[partner_attribute_value] CHECK CONSTRAINT [FK_partner_attribute_value_partner_attribute]
GO
