USE [QSPCanadaCommon]
GO
/****** Object:  Table [dbo].[FM_Phone]    Script Date: 06/07/2017 09:29:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FM_Phone](
	[FMID] [varchar](4) NOT NULL,
	[Type] [int] NOT NULL,
	[PhoneListID] [int] NULL,
	[PhoneNumber] [varchar](50) NULL,
	[BestTimeToCall] [varchar](2000) NULL,
 CONSTRAINT [PK_FM_Phone] PRIMARY KEY CLUSTERED 
(
	[FMID] ASC,
	[Type] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
