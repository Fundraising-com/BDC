USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[product_desc]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[product_desc](
	[product_desc_id] [smallint] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[product_id] [int] NOT NULL,
	[language_id] [tinyint] NOT NULL,
	[product_name] [varchar](100) NOT NULL,
	[product_short_desc] [varchar](300) NOT NULL,
	[product_long_desc] [varchar](1000) NOT NULL,
	[product_small_img] [varchar](25) NULL,
	[product_large_img] [varchar](25) NULL,
	[available_online] [bit] NOT NULL,
 CONSTRAINT [PK_product_desc] PRIMARY KEY CLUSTERED 
(
	[product_id] ASC,
	[language_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_product_desc] UNIQUE NONCLUSTERED 
(
	[product_desc_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[product_desc]  WITH CHECK ADD  CONSTRAINT [FK_product_desc_languages] FOREIGN KEY([language_id])
REFERENCES [dbo].[languages] ([language_id])
GO
ALTER TABLE [dbo].[product_desc] CHECK CONSTRAINT [FK_product_desc_languages]
GO
ALTER TABLE [dbo].[product_desc]  WITH NOCHECK ADD  CONSTRAINT [FK_product_desc_products] FOREIGN KEY([product_id])
REFERENCES [dbo].[scratch_book] ([scratch_book_id])
GO
ALTER TABLE [dbo].[product_desc] CHECK CONSTRAINT [FK_product_desc_products]
GO
ALTER TABLE [dbo].[product_desc] ADD  CONSTRAINT [DF_product_desc_available_online]  DEFAULT (1) FOR [available_online]
GO
