USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Proposal]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Proposal](
	[Proposal_ID] [int] NOT NULL,
	[Fax_Name] [varchar](200) NULL,
	[Email_Name] [varchar](200) NULL,
	[Description] [varchar](200) NULL,
 CONSTRAINT [PK_Proposal] PRIMARY KEY NONCLUSTERED 
(
	[Proposal_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
