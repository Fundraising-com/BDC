USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Partner_Sales_Commission]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Partner_Sales_Commission](
	[Partner_Sales_Commission_ID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[Partner_ID] [int] NOT NULL,
	[Product_Class_ID] [int] NOT NULL,
	[Variable_Rate] [decimal](15, 4) NOT NULL,
	[Effective_Date] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Partner_Sales_Commission] PRIMARY KEY CLUSTERED 
(
	[Partner_Sales_Commission_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Partner_Sales_Commission]  WITH CHECK ADD  CONSTRAINT [FK_Partner_Sales_Commission_product_class] FOREIGN KEY([Product_Class_ID])
REFERENCES [dbo].[product_class] ([product_class_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Partner_Sales_Commission] CHECK CONSTRAINT [FK_Partner_Sales_Commission_product_class]
GO
ALTER TABLE [dbo].[Partner_Sales_Commission] ADD  CONSTRAINT [DF_Partner_Sales_Commission_Variable_Rate]  DEFAULT ((0.00)) FOR [Variable_Rate]
GO
ALTER TABLE [dbo].[Partner_Sales_Commission] ADD  CONSTRAINT [DF_Partner_Sales_Commission_Effective_Date]  DEFAULT (getdate()) FOR [Effective_Date]
GO
ALTER TABLE [dbo].[Partner_Sales_Commission] ADD  CONSTRAINT [DF_Partner_Sales_Commission_Active]  DEFAULT ((1)) FOR [Active]
GO
