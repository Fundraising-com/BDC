USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Lead_Activity_copy]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lead_Activity_copy](
	[Lead_Activity_Id] [int] NOT NULL,
	[Lead_Id] [int] NOT NULL,
	[Lead_Activity_Type_Id] [int] NOT NULL,
	[Lead_Activity_Date] [datetime] NOT NULL,
	[Completed_Date] [datetime] NULL,
	[Comments] [text] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
