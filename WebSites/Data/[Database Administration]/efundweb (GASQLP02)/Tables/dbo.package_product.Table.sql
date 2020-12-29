USE [eFundweb]
GO
/****** Object:  Table [dbo].[package_product]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[package_product](
	[package_id] [int] NOT NULL,
	[product_id] [int] NOT NULL,
	[display_order] [smallint] NULL,
	[displayable] [bit] NOT NULL,
 CONSTRAINT [PK_campaign_address] PRIMARY KEY CLUSTERED 
(
	[package_id] ASC,
	[product_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[package_product]  WITH CHECK ADD  CONSTRAINT [FK_package_product_package] FOREIGN KEY([package_id])
REFERENCES [dbo].[Package] ([Package_ID])
GO
ALTER TABLE [dbo].[package_product] CHECK CONSTRAINT [FK_package_product_package]
GO
ALTER TABLE [dbo].[package_product]  WITH CHECK ADD  CONSTRAINT [FK_package_product_product] FOREIGN KEY([product_id])
REFERENCES [dbo].[Product] ([Product_ID])
GO
ALTER TABLE [dbo].[package_product] CHECK CONSTRAINT [FK_package_product_product]
GO
ALTER TABLE [dbo].[package_product] ADD  CONSTRAINT [DF__package_p__Is_di__44EA3301]  DEFAULT (1) FOR [displayable]
GO
