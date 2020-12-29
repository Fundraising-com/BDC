USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[direct_mail_letter]    Script Date: 02/14/2014 16:26:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[direct_mail_letter](
	[direct_mail_letter_id] [int] IDENTITY(1,1) NOT NULL,
	[direct_mail_id] [int] NOT NULL,
	[letter_bar_code_1] [varchar](256) NOT NULL,
	[letter_bar_code_2] [varchar](256) NOT NULL,
	[letter_type] [int] NOT NULL,
	[create_date] [datetime] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
