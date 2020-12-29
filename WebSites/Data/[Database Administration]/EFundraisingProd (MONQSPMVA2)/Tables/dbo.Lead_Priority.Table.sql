USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Lead_Priority]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Lead_Priority](
	[Lead_Priority_Id] [int] NOT NULL,
	[Description] [varchar](50) NULL,
 CONSTRAINT [PK_Lead_Priority] PRIMARY KEY CLUSTERED 
(
	[Lead_Priority_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
