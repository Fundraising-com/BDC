USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Efr_Lead_Activity]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Efr_Lead_Activity](
	[Lead_Activity_Id] [int] NOT NULL,
	[Lead_Id] [int] NOT NULL,
	[Lead_Activity_Type_Id] [int] NOT NULL,
	[Lead_Activity_Date] [smalldatetime] NOT NULL,
	[Completed_Date] [smalldatetime] NULL,
	[Comments] [text] NULL,
 CONSTRAINT [PK_Efr_Lead_Activity] PRIMARY KEY NONCLUSTERED 
(
	[Lead_Activity_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Efr_Lead_Activity]  WITH NOCHECK ADD  CONSTRAINT [FK_efr_lead_activity_lead] FOREIGN KEY([Lead_Id])
REFERENCES [dbo].[lead] ([lead_id])
GO
ALTER TABLE [dbo].[Efr_Lead_Activity] CHECK CONSTRAINT [FK_efr_lead_activity_lead]
GO
ALTER TABLE [dbo].[Efr_Lead_Activity]  WITH NOCHECK ADD  CONSTRAINT [fk_efrla_lead_activity_type_id] FOREIGN KEY([Lead_Activity_Type_Id])
REFERENCES [dbo].[Lead_Activity_Type] ([Lead_Activity_Type_Id])
GO
ALTER TABLE [dbo].[Efr_Lead_Activity] CHECK CONSTRAINT [fk_efrla_lead_activity_type_id]
GO
