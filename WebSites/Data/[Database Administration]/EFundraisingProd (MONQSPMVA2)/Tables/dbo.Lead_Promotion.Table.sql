USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Lead_Promotion]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lead_Promotion](
	[Lead_Promotion_Id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[Lead_Id] [int] NOT NULL,
	[Promotion_Id] [int] NOT NULL,
	[Entry_Date] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_Lead_Promotion] PRIMARY KEY CLUSTERED 
(
	[Lead_Promotion_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Lead_Promotion]  WITH NOCHECK ADD  CONSTRAINT [FK_lead_promotion_lead] FOREIGN KEY([Lead_Id])
REFERENCES [dbo].[lead] ([lead_id])
GO
ALTER TABLE [dbo].[Lead_Promotion] CHECK CONSTRAINT [FK_lead_promotion_lead]
GO
