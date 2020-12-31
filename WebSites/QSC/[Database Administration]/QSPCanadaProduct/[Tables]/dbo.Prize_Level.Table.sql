USE [QSPCanadaProduct]
GO
/****** Object:  Table [dbo].[Prize_Level]    Script Date: 06/07/2017 09:17:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Prize_Level](
	[Prize_Level_Id] [int] NOT NULL,
	[ProgramSectionId] [int] NOT NULL,
	[Product_Code] [varchar](50) NOT NULL,
	[ProductLine] [int] NOT NULL,
	[Level_Code] [varchar](10) NOT NULL,
	[Qty_Required] [int] NOT NULL,
	[Unit_Of_Measure] [varchar](20) NOT NULL,
	[Price_Value] [money] NOT NULL,
	[Start_Date] [smalldatetime] NULL,
	[Product_Name] [varchar](255) NULL,
	[Language_Code] [varchar](50) NULL,
	[Catalog_Code] [varchar](50) NULL,
	[Catalog_Product_Code] [varchar](50) NULL,
 CONSTRAINT [PK_Prize_Level] PRIMARY KEY CLUSTERED 
(
	[Prize_Level_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
