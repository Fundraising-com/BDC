USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[tbd_unsub_excel]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbd_unsub_excel](
	[Email] [nvarchar](255) NULL,
	[Method] [nvarchar](255) NULL,
	[Source ] [nvarchar](255) NULL,
	[Transaction date] [smalldatetime] NULL
) ON [PRIMARY]
GO
