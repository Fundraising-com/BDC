USE [QSPCanadaProduct]
GO
/****** Object:  Table [dbo].[GiftWithoutPricing]    Script Date: 06/07/2017 09:17:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GiftWithoutPricing](
	[Product_code] [varchar](50) NOT NULL,
	[Product_name] [varchar](255) NULL,
 CONSTRAINT [PK_GiftWithoutPricing] PRIMARY KEY CLUSTERED 
(
	[Product_code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
