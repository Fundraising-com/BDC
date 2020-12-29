USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[EFO_Supporter_Email_Sent]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EFO_Supporter_Email_Sent](
	[Supporter_Email_Sent_ID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[Email_Type_ID] [int] NOT NULL,
	[Supporter_ID] [int] NOT NULL,
	[Date_Sent] [datetime] NULL,
 CONSTRAINT [PK_EFO_Supporter_Email_Sent] PRIMARY KEY NONCLUSTERED 
(
	[Supporter_Email_Sent_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
