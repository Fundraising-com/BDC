USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Administrative_Email]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Administrative_Email](
	[Administrative_ID] [int] NULL,
	[Email] [varchar](255) NULL,
	[First_Name] [varchar](50) NULL,
	[Last_Name] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
