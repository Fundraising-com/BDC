USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[product_class_desc]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[product_class_desc](
	[product_class_id] [int] NOT NULL,
	[language_id] [tinyint] NOT NULL,
	[product_class_desc] [varchar](50) NOT NULL,
	[min_requirements] [varchar](100) NULL,
 CONSTRAINT [PK_product_class_desc] PRIMARY KEY CLUSTERED 
(
	[product_class_id] ASC,
	[language_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
