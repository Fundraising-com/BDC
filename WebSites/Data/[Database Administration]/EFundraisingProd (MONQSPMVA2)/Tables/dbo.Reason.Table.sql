USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Reason]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Reason](
	[Reason_ID] [int] NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[Is_Active] [bit] NOT NULL,
	[ext_adjustment_type_id] [int] NULL,
 CONSTRAINT [PK_Reason] PRIMARY KEY NONCLUSTERED 
(
	[Reason_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
