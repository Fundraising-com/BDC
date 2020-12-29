USE [eFundstore]
GO
/****** Object:  Table [dbo].[product_culture]    Script Date: 02/14/2014 16:26:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product_culture](
	[product_id] [int] NOT NULL,
	[culture_code] [nvarchar](5) NOT NULL,
 CONSTRAINT [PK_product_culture] PRIMARY KEY CLUSTERED 
(
	[product_id] ASC,
	[culture_code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
