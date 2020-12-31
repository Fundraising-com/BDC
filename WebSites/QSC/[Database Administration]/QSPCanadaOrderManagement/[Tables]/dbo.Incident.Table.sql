USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[Incident]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Incident](
	[IncidentInstance] [int] IDENTITY(200000,1) NOT NULL,
	[ProblemCodeInstance] [int] NOT NULL,
	[CustomerOrderHeaderInstance] [int] NOT NULL,
	[TransID] [int] NOT NULL,
	[CommunicationChannelInstance] [int] NOT NULL,
	[CommunicationSourceInstance] [int] NOT NULL,
	[StatusInstance] [int] NOT NULL,
	[ReferToIncidentInstance] [int] NULL,
	[Comments] [varchar](500) NULL,
	[UserIDCreated] [dbo].[UserID_UDDT] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_Incident] PRIMARY KEY CLUSTERED 
(
	[IncidentInstance] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
