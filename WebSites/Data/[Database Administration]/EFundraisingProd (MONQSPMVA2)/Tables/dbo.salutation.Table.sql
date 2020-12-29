USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[salutation]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[salutation](
	[salutation_id] [tinyint] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[salutation_desc] [varchar](15) NOT NULL,
 CONSTRAINT [PK_salutation] PRIMARY KEY CLUSTERED 
(
	[salutation_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
