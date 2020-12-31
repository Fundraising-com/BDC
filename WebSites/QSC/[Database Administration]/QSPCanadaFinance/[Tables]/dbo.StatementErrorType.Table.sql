USE [QSPCanadaFinance]
GO
/****** Object:  Table [dbo].[StatementErrorType]    Script Date: 06/07/2017 09:16:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[StatementErrorType](
	[StatementErrorTypeID] [int] IDENTITY(1,1) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[Error] [varchar](1000) NOT NULL,
 CONSTRAINT [PK__StatementErrorTy__79B300FB] PRIMARY KEY CLUSTERED 
(
	[StatementErrorTypeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
