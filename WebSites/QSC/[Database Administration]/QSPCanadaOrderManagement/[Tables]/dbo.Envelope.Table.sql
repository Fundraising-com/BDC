USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[Envelope]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Envelope](
	[Instance] [int] NOT NULL,
	[OrderBatchDate] [datetime] NULL,
	[OrderBatchID] [int] NULL,
	[TeacherInstance] [int] NULL,
	[ReportedEnvelopeAmount] [decimal](10, 2) NULL,
	[DateCreated] [datetime] NULL,
	[UserIDCreated] [varchar](50) NULL,
	[DateChanged] [datetime] NULL,
	[UserIDChanged] [varchar](50) NULL,
	[ReportedNumberOfOrderForms] [int] NULL,
	[IsIncentive] [varchar](1) NULL,
 CONSTRAINT [PK_Envelope] PRIMARY KEY CLUSTERED 
(
	[Instance] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
