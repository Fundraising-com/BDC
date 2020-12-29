USE [eFundweb]
GO
/****** Object:  Table [dbo].[tbd_partner]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbd_partner](
	[Partner_ID] [int] NOT NULL,
	[Country_ID] [int] NOT NULL,
	[Partner_Name] [varchar](200) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
