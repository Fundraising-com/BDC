USE [EFRCommon]
GO
/****** Object:  Table [dbo].[product]    Script Date: 02/14/2014 16:26:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[product](
	[product_id] [int] NOT NULL,
	[parent_product_id] [int] NULL,
	[scratch_book_id] [int] NOT NULL,
	[name] [varchar](100) NOT NULL,
	[raising_potential] [decimal](15, 4) NULL,
	[product_code] [varchar](20) NOT NULL,
	[enabled] [bit] NOT NULL,
	[create_date] [datetime] NULL,
	[is_inner] [bit] NULL,
 CONSTRAINT [PK_product] PRIMARY KEY CLUSTERED 
(
	[product_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
