USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Commission_Rate]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Commission_Rate](
	[Consultant_ID] [int] NOT NULL,
	[Commission_Rate_Free] [decimal](15, 4) NOT NULL,
	[Commission_Rate_No_Free] [decimal](15, 4) NOT NULL,
	[Scratch_Book_ID] [int] NOT NULL,
 CONSTRAINT [PK_Commission_Rate] PRIMARY KEY NONCLUSTERED 
(
	[Consultant_ID] ASC,
	[Scratch_Book_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Consultant_ID] ASC,
	[Scratch_Book_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Consultant_ID] ASC,
	[Scratch_Book_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Commission_Rate]  WITH NOCHECK ADD  CONSTRAINT [FK_commission_rate_consultant] FOREIGN KEY([Consultant_ID])
REFERENCES [dbo].[consultant] ([consultant_id])
GO
ALTER TABLE [dbo].[Commission_Rate] CHECK CONSTRAINT [FK_commission_rate_consultant]
GO
ALTER TABLE [dbo].[Commission_Rate]  WITH NOCHECK ADD  CONSTRAINT [FK_commission_rate_scratch_book] FOREIGN KEY([Scratch_Book_ID])
REFERENCES [dbo].[scratch_book] ([scratch_book_id])
GO
ALTER TABLE [dbo].[Commission_Rate] CHECK CONSTRAINT [FK_commission_rate_scratch_book]
GO
ALTER TABLE [dbo].[Commission_Rate] ADD  CONSTRAINT [DF_Commission_Rate_Commission_Rate_Free]  DEFAULT (0) FOR [Commission_Rate_Free]
GO
ALTER TABLE [dbo].[Commission_Rate] ADD  CONSTRAINT [DF_Commission_Rate_Commission_Rate_No_Free]  DEFAULT (0) FOR [Commission_Rate_No_Free]
GO
