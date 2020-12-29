USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Commission_Paid]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Commission_Paid](
	[Commission_Year] [smallint] NOT NULL,
	[Commission_Month] [smallint] NOT NULL,
	[Consultant_ID] [int] NOT NULL,
	[Sales_ID] [int] NOT NULL,
	[AR_Status_ID] [int] NULL,
	[Total_Card_Sold] [int] NULL,
	[Sales_Amount] [decimal](15, 4) NULL,
	[Consultant_Commission_Amount] [decimal](15, 4) NULL,
 CONSTRAINT [PK_Commission_Paid] PRIMARY KEY NONCLUSTERED 
(
	[Commission_Year] ASC,
	[Commission_Month] ASC,
	[Consultant_ID] ASC,
	[Sales_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Commission_Paid]  WITH CHECK ADD  CONSTRAINT [fk_AR_Status_ID] FOREIGN KEY([AR_Status_ID])
REFERENCES [dbo].[AR_Status] ([AR_Status_ID])
GO
ALTER TABLE [dbo].[Commission_Paid] CHECK CONSTRAINT [fk_AR_Status_ID]
GO
ALTER TABLE [dbo].[Commission_Paid]  WITH NOCHECK ADD  CONSTRAINT [FK_commission_paid_consultant] FOREIGN KEY([Consultant_ID])
REFERENCES [dbo].[consultant] ([consultant_id])
GO
ALTER TABLE [dbo].[Commission_Paid] CHECK CONSTRAINT [FK_commission_paid_consultant]
GO
ALTER TABLE [dbo].[Commission_Paid] ADD  CONSTRAINT [DF_Commission_Paid_Total_Card_Sold]  DEFAULT (0) FOR [Total_Card_Sold]
GO
ALTER TABLE [dbo].[Commission_Paid] ADD  CONSTRAINT [DF_Commission_Paid_Sales_Amount]  DEFAULT (0) FOR [Sales_Amount]
GO
ALTER TABLE [dbo].[Commission_Paid] ADD  CONSTRAINT [DF_Commission_Paid_Consultant_Commission_Amount]  DEFAULT (0) FOR [Consultant_Commission_Amount]
GO
