USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[scratch_book_commission]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[scratch_book_commission](
	[scratch_book_commission_id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[scratch_book_id] [int] NULL,
	[commission_rate] [decimal](15, 4) NULL,
	[commission_rate_ca] [decimal](15, 4) NULL,
	[create_date] [datetime] NOT NULL,
 CONSTRAINT [PK_scratch_book_commission] PRIMARY KEY CLUSTERED 
(
	[scratch_book_commission_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[scratch_book_commission] ADD  CONSTRAINT [DF_scratch_book_commission_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
