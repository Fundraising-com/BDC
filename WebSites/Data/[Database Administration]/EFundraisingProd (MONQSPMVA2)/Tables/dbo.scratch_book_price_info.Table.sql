USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[scratch_book_price_info]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[scratch_book_price_info](
	[country_code] [varchar](10) NOT NULL,
	[scratch_book_id] [int] NOT NULL,
	[product_class_id] [tinyint] NOT NULL,
	[effective_date] [datetime] NOT NULL,
	[unit_price] [decimal](15, 4) NULL,
 CONSTRAINT [PK_scratch_book_price_info] PRIMARY KEY CLUSTERED 
(
	[scratch_book_id] ASC,
	[product_class_id] ASC,
	[country_code] ASC,
	[effective_date] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[scratch_book_price_info]  WITH NOCHECK ADD  CONSTRAINT [FK_scratch_book_price_info_country_code] FOREIGN KEY([country_code])
REFERENCES [dbo].[Country] ([Country_Code])
GO
ALTER TABLE [dbo].[scratch_book_price_info] CHECK CONSTRAINT [FK_scratch_book_price_info_country_code]
GO
ALTER TABLE [dbo].[scratch_book_price_info]  WITH NOCHECK ADD  CONSTRAINT [FK_scratch_book_price_info_scratch_book] FOREIGN KEY([scratch_book_id])
REFERENCES [dbo].[scratch_book] ([scratch_book_id])
GO
ALTER TABLE [dbo].[scratch_book_price_info] CHECK CONSTRAINT [FK_scratch_book_price_info_scratch_book]
GO
ALTER TABLE [dbo].[scratch_book_price_info] ADD  CONSTRAINT [DF_scratch_book_price_info_scratch_book_id]  DEFAULT (0) FOR [scratch_book_id]
GO
