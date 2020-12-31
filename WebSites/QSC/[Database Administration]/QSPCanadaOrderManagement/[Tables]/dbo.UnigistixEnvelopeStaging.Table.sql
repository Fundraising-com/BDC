USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[UnigistixEnvelopeStaging]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UnigistixEnvelopeStaging](
	[EnvelopeID] [int] NOT NULL,
	[TeacherInstance] [int] NULL,
	[TeacherMiddle] [varchar](10) NULL,
	[TeacherLastName] [varchar](50) NULL,
	[Classroom] [varchar](10) NULL,
 CONSTRAINT [PK_UnigistixEnvelopeStagingTable] PRIMARY KEY CLUSTERED 
(
	[EnvelopeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
