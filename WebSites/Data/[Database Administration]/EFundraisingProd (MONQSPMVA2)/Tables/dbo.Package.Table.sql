USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Package]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Package](
	[Package_Id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[Description] [varchar](50) NULL,
	[Comments] [text] NULL,
	[Package_Image] [varchar](50) NULL,
	[Package_Profit] [varchar](50) NULL,
	[Package_Web_Desc] [text] NULL,
	[Package_Title_Image] [varchar](50) NULL,
	[Is_Displayable] [bit] NOT NULL,
	[product_class_id] [int] NULL,
	[profit_min] [decimal](15, 4) NULL,
	[profit_max] [decimal](15, 4) NULL,
	[profit_default] [decimal](15, 4) NULL,
 CONSTRAINT [PK_Package] PRIMARY KEY NONCLUSTERED 
(
	[Package_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Package]  WITH NOCHECK ADD  CONSTRAINT [FK_Package_product_class] FOREIGN KEY([product_class_id])
REFERENCES [dbo].[product_class] ([product_class_id])
NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[Package] NOCHECK CONSTRAINT [FK_Package_product_class]
GO
ALTER TABLE [dbo].[Package] ADD  CONSTRAINT [DF_Package_Is_Displayable]  DEFAULT (0) FOR [Is_Displayable]
GO
