USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Payment_status]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Payment_status](
	[Payment_status_id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[Description] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Payment_status] PRIMARY KEY CLUSTERED 
(
	[Payment_status_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
