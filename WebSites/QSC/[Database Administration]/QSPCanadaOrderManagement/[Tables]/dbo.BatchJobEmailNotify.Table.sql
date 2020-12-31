USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[BatchJobEmailNotify]    Script Date: 06/07/2017 09:18:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BatchJobEmailNotify](
	[BatchJobID] [int] NOT NULL,
	[Name] [nvarchar](60) NOT NULL,
	[EmailAddress] [nvarchar](60) NOT NULL,
 CONSTRAINT [PK_BatchJobEmailNotify] PRIMARY KEY CLUSTERED 
(
	[BatchJobID] ASC,
	[Name] ASC,
	[EmailAddress] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
