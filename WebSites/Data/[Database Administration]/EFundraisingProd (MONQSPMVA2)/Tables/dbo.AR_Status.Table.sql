USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[AR_Status]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AR_Status](
	[AR_Status_ID] [int] NOT NULL,
	[Description] [varchar](50) NULL,
	[Commission_On_Hold] [bit] NULL,
	[Commission_Is_Payable] [bit] NULL,
	[Commission_Is_Credited] [bit] NULL,
 CONSTRAINT [PK_AR_Status] PRIMARY KEY NONCLUSTERED 
(
	[AR_Status_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
