USE [eFundweb]
GO
/****** Object:  Table [dbo].[Group_Type_Desc]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Group_Type_Desc](
	[Group_type_ID] [int] NOT NULL,
	[Language_ID] [int] NOT NULL,
	[Description] [varchar](200) NOT NULL,
 CONSTRAINT [PK_Group_Type_Desc] PRIMARY KEY CLUSTERED 
(
	[Group_type_ID] ASC,
	[Language_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
