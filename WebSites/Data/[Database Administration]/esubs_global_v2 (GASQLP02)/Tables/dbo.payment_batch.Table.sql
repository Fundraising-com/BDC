USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[payment_batch]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[payment_batch](
	[payment_batch_id] [int] IDENTITY(1,1) NOT NULL,
	[filename] [varchar](1024) NULL,
	[createdate] [datetime] NOT NULL,
	[confirmation_date] [datetime] NULL,
	[cancelled_date] [datetime] NULL,
 CONSTRAINT [PK_batch] PRIMARY KEY CLUSTERED 
(
	[payment_batch_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[payment_batch] ADD  CONSTRAINT [DF_payment_batch_createdate]  DEFAULT (getdate()) FOR [createdate]
GO
