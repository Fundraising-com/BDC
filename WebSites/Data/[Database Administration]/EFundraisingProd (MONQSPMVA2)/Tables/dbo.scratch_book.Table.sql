USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[scratch_book]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[scratch_book](
	[scratch_book_id] [int] NOT NULL,
	[product_class_id] [tinyint] NULL,
	[supplier_id] [tinyint] NULL,
	[package_id] [int] NOT NULL,
	[description] [varchar](100) NOT NULL,
	[raising_potential] [decimal](15, 4) NULL,
	[product_code] [varchar](20) NOT NULL,
	[current_description] [varchar](100) NULL,
	[is_active] [bit] NOT NULL,
	[is_displayable] [bit] NOT NULL,
	[total_qty] [int] NULL,
	[fixed_profit] [decimal](18, 0) NULL,
	[replicated] [bit] NULL,
	[SAPMaterialNo] [int] NULL,
	[InHouse] [bit] NULL,
 CONSTRAINT [PK_scratch_book] PRIMARY KEY CLUSTERED 
(
	[scratch_book_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[scratch_book]  WITH CHECK ADD  CONSTRAINT [FK_scratch_book_Package] FOREIGN KEY([package_id])
REFERENCES [dbo].[Package] ([Package_Id])
GO
ALTER TABLE [dbo].[scratch_book] CHECK CONSTRAINT [FK_scratch_book_Package]
GO
ALTER TABLE [dbo].[scratch_book] ADD  CONSTRAINT [DF_scratch_book_package_id]  DEFAULT (0) FOR [package_id]
GO
ALTER TABLE [dbo].[scratch_book] ADD  CONSTRAINT [DF_scratch_book_is_displayable]  DEFAULT (1) FOR [is_displayable]
GO
