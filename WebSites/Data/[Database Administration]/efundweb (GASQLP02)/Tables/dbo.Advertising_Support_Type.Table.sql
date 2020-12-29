USE [eFundweb]
GO
/****** Object:  Table [dbo].[Advertising_Support_Type]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Advertising_Support_Type](
	[Advertising_Support_Type_ID] [int] NOT NULL,
	[Description] [char](50) NULL,
	[Comments] [varchar](255) NULL,
 CONSTRAINT [PK_Advertising_Support_Type] PRIMARY KEY CLUSTERED 
(
	[Advertising_Support_Type_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
