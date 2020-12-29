USE [eFundstore]
GO
/****** Object:  Table [dbo].[credit_card]    Script Date: 02/14/2014 16:26:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[credit_card](
	[credit_card_id] [int] IDENTITY(1,1) NOT NULL,
	[online_user_id] [int] NOT NULL,
	[credit_card_type_id] [tinyint] NOT NULL,
	[credit_card] [varbinary](30) NOT NULL,
	[last_digits] [char](4) NOT NULL,
	[year_expire] [smallint] NOT NULL,
	[month_expire] [tinyint] NOT NULL,
	[date_created] [datetime] NOT NULL,
	[removed] [bit] NOT NULL,
 CONSTRAINT [PK_credit_cards] PRIMARY KEY NONCLUSTERED 
(
	[credit_card_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[credit_card] ADD  CONSTRAINT [DF_credit_cards_date_created]  DEFAULT (getdate()) FOR [date_created]
GO
ALTER TABLE [dbo].[credit_card] ADD  CONSTRAINT [DF_credit_cards_removed]  DEFAULT (0) FOR [removed]
GO
