USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[partner_commission]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[partner_commission](
	[partner_id] [int] NOT NULL,
	[product_class_id] [tinyint] NOT NULL,
	[commission_rate] [decimal](15, 4) NOT NULL,
 CONSTRAINT [PK_partner_commission] PRIMARY KEY CLUSTERED 
(
	[partner_id] ASC,
	[product_class_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
