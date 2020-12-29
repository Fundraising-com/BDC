USE [fastfundraising]
GO
/****** Object:  Table [dbo].[FC]    Script Date: 02/14/2014 16:35:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FC](
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
	[profit_raised] [decimal](12, 2) NULL,
 CONSTRAINT [PK_FC] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[FC] ADD  DEFAULT (NULL) FOR [profit_raised]
GO
