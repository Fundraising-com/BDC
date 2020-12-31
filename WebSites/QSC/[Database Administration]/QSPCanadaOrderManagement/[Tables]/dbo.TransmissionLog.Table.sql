USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[TransmissionLog]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransmissionLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TransmissionDate] [datetime] NULL,
 CONSTRAINT [PK_TransmissionLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
