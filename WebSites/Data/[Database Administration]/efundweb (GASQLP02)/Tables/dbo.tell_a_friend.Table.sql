USE [eFundweb]
GO
/****** Object:  Table [dbo].[tell_a_friend]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tell_a_friend](
	[tell_a_friend_id] [int] IDENTITY(1,1) NOT NULL,
	[culture_code] [varchar](50) NULL,
	[from_name] [varchar](256) NULL,
	[from_email] [varchar](256) NULL,
	[to_name] [varchar](256) NULL,
	[to_email] [varchar](256) NULL,
	[subject] [varchar](256) NULL,
	[message] [varchar](8000) NULL,
	[bounced] [bit] NULL,
	[datesent] [datetime] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
