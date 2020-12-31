USE [QSPCanadaCommon]
GO
/****** Object:  Table [dbo].[CAccountCode]    Script Date: 06/07/2017 09:29:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CAccountCode](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Class] [varchar](4) NOT NULL,
	[AccountCode] [varchar](6) NOT NULL,
	[Name] [varchar](50) NULL,
 CONSTRAINT [PK_CAccountCode] PRIMARY KEY CLUSTERED 
(
	[Class] ASC,
	[AccountCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[CAccountCode]  WITH CHECK ADD  CONSTRAINT [fk_CAccountCode_CAccountClass] FOREIGN KEY([Class])
REFERENCES [dbo].[CAccountClass] ([AccountClass])
GO
ALTER TABLE [dbo].[CAccountCode] CHECK CONSTRAINT [fk_CAccountCode_CAccountClass]
GO
