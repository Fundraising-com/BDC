USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[carrier]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[carrier](
	[carrier_id] [tinyint] NOT NULL,
	[description] [varchar](50) NOT NULL,
	[active] [tinyint] NOT NULL
) ON [PRIMARY]
SET ANSI_PADDING OFF
ALTER TABLE [dbo].[carrier] ADD [SCAC] [varchar](4) NULL
ALTER TABLE [dbo].[carrier] ADD  CONSTRAINT [PK_carrier] PRIMARY KEY CLUSTERED 
(
	[carrier_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[carrier] ADD  CONSTRAINT [DF_carrier_active]  DEFAULT (0) FOR [active]
GO
