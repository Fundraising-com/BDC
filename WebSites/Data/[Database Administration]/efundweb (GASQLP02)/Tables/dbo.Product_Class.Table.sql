USE [eFundweb]
GO
/****** Object:  Table [dbo].[Product_Class]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Product_Class](
	[Product_Class_ID] [int] NOT NULL,
	[Division_ID] [int] NOT NULL,
	[Description] [varchar](50) NULL,
	[Product_Code] [varchar](10) NULL,
	[Display_Name] [varchar](100) NULL,
	[Product_Class_Web_Desc] [varchar](2000) NULL,
	[Product_Class_Web_Profit] [varchar](50) NULL,
	[Product_Class_Image] [varchar](50) NULL,
	[Product_Class_Title_Image] [varchar](50) NULL,
	[Is_Displayable] [bit] NOT NULL,
	[Accounting_Class_ID] [int] NULL,
 CONSTRAINT [PK_Product_Class] PRIMARY KEY CLUSTERED 
(
	[Product_Class_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Product_Class] ADD  DEFAULT (1) FOR [Is_Displayable]
GO
