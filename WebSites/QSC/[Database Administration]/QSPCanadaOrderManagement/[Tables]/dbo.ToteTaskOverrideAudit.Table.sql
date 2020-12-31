USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[ToteTaskOverrideAudit]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ToteTaskOverrideAudit](
	[ToteInstance] [int] NOT NULL,
	[ToteTaskInstance] [int] NOT NULL,
	[DateChanged] [datetime] NOT NULL,
	[CouponBefore] [int] NULL,
	[UserIDChanged] [char](4) NULL,
	[ReasonCode] [char](1) NOT NULL,
	[DateBefore] [datetime] NULL,
	[CouponAfter] [int] NULL,
 CONSTRAINT [PK_ToteTaskOverrideAudit] PRIMARY KEY CLUSTERED 
(
	[ToteInstance] ASC,
	[ToteTaskInstance] ASC,
	[DateChanged] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
