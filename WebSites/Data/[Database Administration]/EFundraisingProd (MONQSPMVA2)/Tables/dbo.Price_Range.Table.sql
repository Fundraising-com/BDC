USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Price_Range]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Price_Range](
	[Price_Range_ID] [int] IDENTITY(25,1) NOT FOR REPLICATION NOT NULL,
	[Package_ID] [int] NOT NULL,
	[Minimum_Qty] [int] NOT NULL,
	[Maximum_Qty] [int] NOT NULL,
	[Unit_Price_Sold] [smallmoney] NOT NULL,
 CONSTRAINT [PK_Price_Range] PRIMARY KEY NONCLUSTERED 
(
	[Price_Range_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Price_Range]  WITH NOCHECK ADD  CONSTRAINT [fk_pr_package_id] FOREIGN KEY([Package_ID])
REFERENCES [dbo].[Package] ([Package_Id])
GO
ALTER TABLE [dbo].[Price_Range] CHECK CONSTRAINT [fk_pr_package_id]
GO
