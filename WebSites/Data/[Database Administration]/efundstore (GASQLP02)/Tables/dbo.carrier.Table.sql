USE [eFundstore]
GO
/****** Object:  Table [dbo].[carrier]    Script Date: 02/14/2014 16:26:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[carrier](
	[carrier_id] [tinyint] NOT NULL,
	[description] [varchar](50) NOT NULL,
 CONSTRAINT [PK_carrier] PRIMARY KEY CLUSTERED 
(
	[carrier_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
