USE [QSPCanadaFinance]
GO
/****** Object:  Table [dbo].[GLAccount]    Script Date: 06/07/2017 09:16:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GLAccount](
	[GLAccountID] [int] IDENTITY(1,1) NOT NULL,
	[GLAccountStatusID] [int] NOT NULL,
	[GLAccountSystemID] [int] NOT NULL,
	[Description] [varchar](1000) NOT NULL,
	[Account] [varchar](50) NOT NULL,
	[Division] [varchar](50) NULL,
	[Product] [varchar](50) NULL,
	[Department] [varchar](50) NULL,
	[Project] [varchar](50) NULL,
	[Source] [varchar](50) NULL,
	[Geographic] [varchar](50) NULL,
	[Other] [varchar](50) NULL,
	[Affiliate] [varchar](50) NULL,
	[Entity] [varchar](50) NULL,
	[LangMarket] [varchar](50) NULL,
	[DistChannel] [varchar](50) NULL,
	[Segment] [varchar](50) NULL,
	[CreateDate] [datetime] NOT NULL,
	[BusinessUnitID] [int] NOT NULL,
 CONSTRAINT [PK_GLAccount] PRIMARY KEY CLUSTERED 
(
	[GLAccountID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[GLAccount]  WITH CHECK ADD  CONSTRAINT [FK_GLAccount_GLAccountStatus] FOREIGN KEY([GLAccountStatusID])
REFERENCES [dbo].[GLAccountStatus] ([GLAccountStatusID])
GO
ALTER TABLE [dbo].[GLAccount] CHECK CONSTRAINT [FK_GLAccount_GLAccountStatus]
GO
ALTER TABLE [dbo].[GLAccount] ADD  CONSTRAINT [DF_GLAccount_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
