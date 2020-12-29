USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[UnAssignLogin]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UnAssignLogin](
	[UnAssignLogin_Id] [int] NOT NULL,
	[User_Name] [varchar](50) NULL,
	[Consultant_Id] [int] NULL,
	[Lead_Id] [int] NULL,
	[Unassignment_TimeStamp] [smalldatetime] NULL,
 CONSTRAINT [PK_UnAssignLogin] PRIMARY KEY NONCLUSTERED 
(
	[UnAssignLogin_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
