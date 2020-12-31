USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[IncidentAction]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[IncidentAction](
	[Instance] [int] IDENTITY(1,1) NOT NULL,
	[IncidentInstance] [int] NOT NULL,
	[ActionInstance] [int] NOT NULL,
	[Comments] [varchar](500) NULL,
	[UserIDCreated] [dbo].[UserID_UDDT] NULL,
	[DateCreated] [datetime] NOT NULL,
	[CustomerRemitHistoryInstance] [int] NULL,
 CONSTRAINT [PK_IncidentAction] PRIMARY KEY CLUSTERED 
(
	[Instance] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
