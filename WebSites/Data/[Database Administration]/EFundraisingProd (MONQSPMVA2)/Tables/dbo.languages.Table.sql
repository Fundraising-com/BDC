USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[languages]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[languages](
	[language_id] [tinyint] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[language_name] [varchar](50) NOT NULL,
	[long_language_code] [char](3) NOT NULL,
	[short_language_code] [char](2) NULL,
 CONSTRAINT [PK_languages] PRIMARY KEY CLUSTERED 
(
	[language_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
