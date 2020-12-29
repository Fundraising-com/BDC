USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[partner_web_details]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[partner_web_details](
	[partner_id] [int] NOT NULL,
	[top_menu] [varchar](30) NOT NULL,
	[left_menu] [varchar](30) NOT NULL,
	[right_menu] [varchar](30) NOT NULL,
	[images_path] [varchar](30) NOT NULL,
	[default_color] [varchar](20) NOT NULL,
	[short_cut_menu] [varchar](30) NOT NULL,
	[product_image_map] [varchar](30) NOT NULL,
 CONSTRAINT [PK_partner_web_details] PRIMARY KEY CLUSTERED 
(
	[partner_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[partner_web_details] ADD  CONSTRAINT [DF_partner_web_details_top_menu]  DEFAULT ('default') FOR [top_menu]
GO
ALTER TABLE [dbo].[partner_web_details] ADD  CONSTRAINT [DF_partner_web_details_left_menu]  DEFAULT ('default') FOR [left_menu]
GO
ALTER TABLE [dbo].[partner_web_details] ADD  CONSTRAINT [DF_partner_web_details_right_menu]  DEFAULT ('default') FOR [right_menu]
GO
ALTER TABLE [dbo].[partner_web_details] ADD  CONSTRAINT [DF_partner_web_details_images_path]  DEFAULT ('default') FOR [images_path]
GO
ALTER TABLE [dbo].[partner_web_details] ADD  CONSTRAINT [DF_partner_web_details_short_cut_menu]  DEFAULT ('default') FOR [short_cut_menu]
GO
ALTER TABLE [dbo].[partner_web_details] ADD  CONSTRAINT [DF_partner_web_details_product_image_map]  DEFAULT ('default') FOR [product_image_map]
GO
