USE [eFundweb]
GO
/****** Object:  Table [dbo].[Scratch_Book]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Scratch_Book](
	[Scratch_Book_ID] [int] NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[Raising_Potential] [numeric](15, 4) NULL,
	[Product_Code] [varchar](15) NOT NULL,
	[Current_Description] [varchar](50) NULL,
	[Product_Class_ID] [int] NULL,
	[Supplier_ID] [int] NULL,
	[Is_Active] [bit] NOT NULL,
	[Package_ID] [int] NOT NULL,
	[Order_Taker_ID] [int] NULL,
	[Small_Image] [varchar](50) NULL,
	[Front_Image] [varchar](50) NULL,
	[Back_Image] [varchar](50) NULL,
	[Scratch_Booh_Web_Desc] [varchar](2000) NULL,
	[Is_Displayable] [bit] NOT NULL,
	[Total_Qty] [int] NULL,
 CONSTRAINT [PK_Scratch_Book] PRIMARY KEY CLUSTERED 
(
	[Scratch_Book_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Scratch_Book] ADD  CONSTRAINT [DF__Scratch_B__Packa__2645B050]  DEFAULT (0) FOR [Package_ID]
GO
ALTER TABLE [dbo].[Scratch_Book] ADD  CONSTRAINT [DF__Scratch_B__Is_Di__2739D489]  DEFAULT (1) FOR [Is_Displayable]
GO
