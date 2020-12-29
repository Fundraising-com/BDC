USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[AR_Consultant]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AR_Consultant](
	[AR_Consultant_ID] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Email] [varchar](50) NULL,
	[Phone_Ext] [varchar](3) NULL,
	[Is_Active] [bit] NOT NULL,
	[Nt_Login] [varchar](50) NULL,
 CONSTRAINT [PK_AR_Consultant] PRIMARY KEY NONCLUSTERED 
(
	[AR_Consultant_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[AR_Consultant] ADD  CONSTRAINT [DF_AR_Consultant_Is_Active]  DEFAULT (0) FOR [Is_Active]
GO
