USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[AR_Activity]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AR_Activity](
	[AR_Activity_ID] [int] NOT NULL,
	[AR_Activity_Type_ID] [int] NOT NULL,
	[Sales_ID] [int] NOT NULL,
	[AR_Consultant_ID] [int] NOT NULL,
	[AR_Activity_Date] [smalldatetime] NOT NULL,
	[Completed_Date] [smalldatetime] NULL,
	[Comments] [varchar](255) NULL,
 CONSTRAINT [PK_AR_Activity] PRIMARY KEY NONCLUSTERED 
(
	[AR_Activity_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[AR_Activity]  WITH CHECK ADD  CONSTRAINT [FK_AR_Activity_AR_Activity_Type] FOREIGN KEY([AR_Activity_Type_ID])
REFERENCES [dbo].[AR_Activity_Type] ([AR_Activity_Type_Id])
GO
ALTER TABLE [dbo].[AR_Activity] CHECK CONSTRAINT [FK_AR_Activity_AR_Activity_Type]
GO
ALTER TABLE [dbo].[AR_Activity]  WITH NOCHECK ADD  CONSTRAINT [FK_ar_activity_consultant] FOREIGN KEY([AR_Consultant_ID])
REFERENCES [dbo].[consultant] ([consultant_id])
GO
ALTER TABLE [dbo].[AR_Activity] CHECK CONSTRAINT [FK_ar_activity_consultant]
GO
ALTER TABLE [dbo].[AR_Activity]  WITH NOCHECK ADD  CONSTRAINT [FK_ar_activity_sale] FOREIGN KEY([Sales_ID])
REFERENCES [dbo].[sale] ([sales_id])
GO
ALTER TABLE [dbo].[AR_Activity] CHECK CONSTRAINT [FK_ar_activity_sale]
GO
