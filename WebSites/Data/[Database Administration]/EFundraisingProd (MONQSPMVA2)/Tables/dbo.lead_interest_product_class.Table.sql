USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[lead_interest_product_class]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[lead_interest_product_class](
	[lead_id] [int] NOT NULL,
	[product_class_id] [tinyint] NOT NULL
) ON [PRIMARY]
GO
