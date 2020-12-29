IF @@servername NOT IN ('GASQLT02', 'MONQSPMVA2')
BEGIN
	RAISERROR('Wrong server!', 20, 1) WITH LOG
END
USE [eFundraisingProd]
GO

/****** Object:  Table [dbo].[cost_of_goods]    Script Date: 12/11/2018 8:28:26 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'cost_of_goods')
BEGIN
	CREATE TABLE [dbo].[cost_of_goods](
		[id] [int] IDENTITY(1,1) NOT NULL,
		[scratch_book_id] [int] NOT NULL,
		[quantity_from] [smallint] NOT NULL,
		[quantity_to] [smallint] NOT NULL,
		[effective_start_date] [datetime] NOT NULL,
		[effective_end_date] [datetime] NOT NULL,
		[cost] [float] NOT NULL,
      [country] nvarchar(2) NOT NULL,
	 CONSTRAINT [PK_cost_of_goods] PRIMARY KEY CLUSTERED 
	(
		[id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	ALTER TABLE [dbo].[cost_of_goods]  WITH CHECK ADD  CONSTRAINT [FK_cost_of_goods_scratch_book] FOREIGN KEY([scratch_book_id])
	REFERENCES [dbo].[scratch_book] ([scratch_book_id])

	ALTER TABLE [dbo].[cost_of_goods] CHECK CONSTRAINT [FK_cost_of_goods_scratch_book]
END