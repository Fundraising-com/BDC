USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Local_Sponsor_Activity]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Local_Sponsor_Activity](
	[Local_Sponsor_Activity_ID] [int] NOT NULL,
	[Local_Sponsor_Activity_Type_ID] [int] NOT NULL,
	[Sales_ID] [int] NOT NULL,
	[Sponsor_Consultant_ID] [int] NOT NULL,
	[Local_Sponsor_Activity_Date] [smalldatetime] NOT NULL,
	[Completed_Date] [smalldatetime] NULL,
	[Comments] [varchar](255) NULL,
	[Brand_ID] [int] NULL,
	[Local_Sponsor_ID] [int] NULL,
 CONSTRAINT [PK_Local_Sponsor_Activity] PRIMARY KEY NONCLUSTERED 
(
	[Local_Sponsor_Activity_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Local_Sponsor_Activity]  WITH CHECK ADD  CONSTRAINT [fk_LSA_Local_Sponsor_Activity_Type_ID] FOREIGN KEY([Local_Sponsor_Activity_Type_ID])
REFERENCES [dbo].[Local_Sponsor_Activity_Type] ([Local_Sponsor_Activity_Type_Id])
GO
ALTER TABLE [dbo].[Local_Sponsor_Activity] CHECK CONSTRAINT [fk_LSA_Local_Sponsor_Activity_Type_ID]
GO
ALTER TABLE [dbo].[Local_Sponsor_Activity]  WITH CHECK ADD  CONSTRAINT [fk_LSA_Sponsor_Consultant_ID] FOREIGN KEY([Sponsor_Consultant_ID])
REFERENCES [dbo].[Sponsor_Consultant] ([Sponsor_Consultant_ID])
GO
ALTER TABLE [dbo].[Local_Sponsor_Activity] CHECK CONSTRAINT [fk_LSA_Sponsor_Consultant_ID]
GO
ALTER TABLE [dbo].[Local_Sponsor_Activity] ADD  CONSTRAINT [DF_Local_Sponsor_Activity_Sponsor_Consultant_ID]  DEFAULT (0) FOR [Sponsor_Consultant_ID]
GO
