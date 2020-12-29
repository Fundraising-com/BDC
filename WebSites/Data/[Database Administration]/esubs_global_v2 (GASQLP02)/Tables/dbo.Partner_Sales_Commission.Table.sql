USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[Partner_Sales_Commission]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Partner_Sales_Commission](
	[Partner_Sales_Commission_ID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[Partner_ID] [int] NOT NULL,
	[Product_Type_ID] [int] NOT NULL,
	[Partner_Commission_Range_ID] [int] NOT NULL,
	[Fixed_Amount] [decimal](15, 4) NOT NULL,
	[Variable_Rate] [decimal](15, 4) NOT NULL,
	[Effective_Date] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
	[pure_variable_rate] [decimal](15, 4) NULL,
	[Store_ID] [int] NOT NULL,
 CONSTRAINT [PK_Partner_Sales_Commission] PRIMARY KEY CLUSTERED 
(
	[Partner_Sales_Commission_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Partner_Sales_Commission]  WITH NOCHECK ADD  CONSTRAINT [FK_Partner_Sales_Commission_Partner_Commission_Range] FOREIGN KEY([Partner_Commission_Range_ID])
REFERENCES [dbo].[Partner_Commission_Range] ([Partner_Commission_Range_ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Partner_Sales_Commission] CHECK CONSTRAINT [FK_Partner_Sales_Commission_Partner_Commission_Range]
GO
ALTER TABLE [dbo].[Partner_Sales_Commission]  WITH CHECK ADD  CONSTRAINT [FK_Partner_Sales_Commission_Store] FOREIGN KEY([Store_ID])
REFERENCES [dbo].[Store] ([Store_ID])
GO
ALTER TABLE [dbo].[Partner_Sales_Commission] CHECK CONSTRAINT [FK_Partner_Sales_Commission_Store]
GO
ALTER TABLE [dbo].[Partner_Sales_Commission] ADD  CONSTRAINT [DF_Partner_Sales_Commission_Fixed_Amount]  DEFAULT ((0.00)) FOR [Fixed_Amount]
GO
ALTER TABLE [dbo].[Partner_Sales_Commission] ADD  CONSTRAINT [DF_Partner_Sales_Commission_Variable_Rate]  DEFAULT ((0.00)) FOR [Variable_Rate]
GO
ALTER TABLE [dbo].[Partner_Sales_Commission] ADD  CONSTRAINT [DF_Partner_Sales_Commission_Effective_Date]  DEFAULT (getdate()) FOR [Effective_Date]
GO
ALTER TABLE [dbo].[Partner_Sales_Commission] ADD  CONSTRAINT [DF_Partner_Sales_Commission_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Partner_Sales_Commission] ADD  DEFAULT ((1)) FOR [Store_ID]
GO
