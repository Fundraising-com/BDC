USE [fastfundraising]
GO
/****** Object:  Table [dbo].[TEMP_FC_WITH_8000_ACCOUNT]    Script Date: 02/14/2014 16:35:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TEMP_FC_WITH_8000_ACCOUNT](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[ext_id] [int] NOT NULL,
	[email_address] [varchar](50) NULL,
	[active] [smallint] NOT NULL,
	[login] [nvarchar](25) NULL,
	[url] [varchar](50) NULL,
	[city] [varchar](50) NULL,
	[state] [varchar](50) NULL,
	[image_url] [varchar](50) NULL,
	[phone] [varchar](50) NULL,
	[esubs_parnter_id] [int] NULL,
	[SAPAccountNo] [int] NULL,
 CONSTRAINT [PK_TEMP_FC_WITH_8000_ACCOUNT] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
