USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Credit_Approval_Method]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Credit_Approval_Method](
	[Credit_Approval_Method_ID] [int] NOT NULL,
	[Description] [char](50) NOT NULL,
 CONSTRAINT [PK_Credit_Approval_Method] PRIMARY KEY CLUSTERED 
(
	[Credit_Approval_Method_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
