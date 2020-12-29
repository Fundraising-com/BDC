USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[brochures_images]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[brochures_images](
	[brochures_images_id] [tinyint] NOT NULL,
	[product_id] [int] NOT NULL,
	[base_filename] [varchar](100) NOT NULL,
	[file_ext] [varchar](5) NOT NULL,
	[number_pages] [tinyint] NOT NULL,
 CONSTRAINT [PK_brochures_images] PRIMARY KEY CLUSTERED 
(
	[brochures_images_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
