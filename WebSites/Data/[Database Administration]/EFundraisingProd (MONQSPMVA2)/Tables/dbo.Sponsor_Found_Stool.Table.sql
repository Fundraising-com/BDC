USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Sponsor_Found_Stool]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Sponsor_Found_Stool](
	[Stool_ID] [int] IDENTITY(3036,1) NOT NULL,
	[Sales_ID] [int] NOT NULL,
	[User_Name] [varchar](25) NULL,
	[Valeur] [bit] NULL,
	[Modif_Date] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
