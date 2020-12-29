USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Area_Manager]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Area_Manager](
	[Area_Manager_ID] [int] NOT NULL,
	[Area_Manager_Name] [varchar](25) NULL,
 CONSTRAINT [PK_Area_Manager] PRIMARY KEY NONCLUSTERED 
(
	[Area_Manager_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
