USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Lead_Activity_Type]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Lead_Activity_Type](
	[Lead_Activity_Type_Id] [int] NOT NULL,
	[Description] [varchar](50) NULL,
	[Priority] [int] NULL,
 CONSTRAINT [PK_Lead_Activity_Type] PRIMARY KEY CLUSTERED 
(
	[Lead_Activity_Type_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
