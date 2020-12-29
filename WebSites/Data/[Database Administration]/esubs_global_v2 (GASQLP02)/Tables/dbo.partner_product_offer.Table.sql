USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[partner_product_offer]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[partner_product_offer](
	[partner_id] [int] NOT NULL,
	[product_offer_id] [int] NOT NULL,
	[create_date] [datetime] NOT NULL,
 CONSTRAINT [PK_partner_product_offer] PRIMARY KEY CLUSTERED 
(
	[partner_id] ASC,
	[product_offer_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[partner_product_offer] ADD  DEFAULT (getdate()) FOR [create_date]
GO
