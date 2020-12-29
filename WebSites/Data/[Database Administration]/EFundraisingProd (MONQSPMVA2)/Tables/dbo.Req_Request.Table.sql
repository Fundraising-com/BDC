USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Req_Request]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Req_Request](
	[Request_Id] [int] NOT NULL,
	[Language_Id] [int] NULL,
	[Request_Type_ID] [int] NULL,
	[Project_Type_ID] [int] NULL,
	[Priority_Id] [int] NULL,
	[Project_Name] [varchar](60) NULL,
	[Summary_Description] [text] NULL,
	[Request_Date] [smalldatetime] NULL,
	[Decision_Required_Date] [datetime] NULL,
	[Impact_Description] [text] NULL,
	[Mis_Impact_Description] [text] NULL,
	[Decision_Description] [text] NULL,
	[Decision_Id] [int] NULL,
	[Decision_Date] [smalldatetime] NULL,
	[Project_Sheduled_Start_Date] [smalldatetime] NULL,
	[Project_Sheduled_End_Date] [smalldatetime] NULL,
	[Project_Start_Date] [smalldatetime] NULL,
	[Project_End_Date] [smalldatetime] NULL,
	[Manager_ID] [int] NULL,
	[Employee_Id] [int] NULL,
	[MIS_ID] [int] NULL,
 CONSTRAINT [PK_Req_Request] PRIMARY KEY NONCLUSTERED 
(
	[Request_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Req_Request]  WITH NOCHECK ADD  CONSTRAINT [FK_Req_Request_Req_Decision] FOREIGN KEY([Decision_Id], [Language_Id])
REFERENCES [dbo].[Req_Decision] ([Decision_Id], [Language_Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Req_Request] CHECK CONSTRAINT [FK_Req_Request_Req_Decision]
GO
ALTER TABLE [dbo].[Req_Request]  WITH NOCHECK ADD  CONSTRAINT [FK_Req_Request_Req_Priority] FOREIGN KEY([Priority_Id], [Language_Id])
REFERENCES [dbo].[Req_Priority] ([Priority_Id], [Language_Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Req_Request] CHECK CONSTRAINT [FK_Req_Request_Req_Priority]
GO
ALTER TABLE [dbo].[Req_Request]  WITH NOCHECK ADD  CONSTRAINT [FK_Req_Request_Req_Project_Type] FOREIGN KEY([Project_Type_ID], [Language_Id])
REFERENCES [dbo].[Req_Project_Type] ([Project_Type_ID], [Language_Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Req_Request] CHECK CONSTRAINT [FK_Req_Request_Req_Project_Type]
GO
ALTER TABLE [dbo].[Req_Request]  WITH NOCHECK ADD  CONSTRAINT [FK_Req_Request_Req_Request_Type] FOREIGN KEY([Request_Type_ID], [Language_Id])
REFERENCES [dbo].[Req_Request_Type] ([Request_Type_ID], [Language_Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Req_Request] CHECK CONSTRAINT [FK_Req_Request_Req_Request_Type]
GO
