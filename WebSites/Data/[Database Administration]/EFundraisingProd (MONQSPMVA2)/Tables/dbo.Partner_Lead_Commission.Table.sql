USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Partner_Lead_Commission]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Partner_Lead_Commission](
	[Partner_Lead_Commission_ID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[Partner_ID] [int] NOT NULL,
	[Channel_Code] [varchar](4) NOT NULL,
	[Partner_Commission_Range_ID] [int] NOT NULL,
	[Fixed_Amount] [decimal](15, 4) NOT NULL,
	[Effective_Date] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Partner_Lead_Commission] PRIMARY KEY CLUSTERED 
(
	[Partner_Lead_Commission_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Partner_Lead_Commission]  WITH NOCHECK ADD  CONSTRAINT [FK_Partner_Lead_Commission_Lead_Channel] FOREIGN KEY([Channel_Code])
REFERENCES [dbo].[Lead_Channel] ([Channel_Code])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Partner_Lead_Commission] CHECK CONSTRAINT [FK_Partner_Lead_Commission_Lead_Channel]
GO
ALTER TABLE [dbo].[Partner_Lead_Commission]  WITH NOCHECK ADD  CONSTRAINT [FK_Partner_Lead_Commission_Partner_Commission_Range] FOREIGN KEY([Partner_Commission_Range_ID])
REFERENCES [dbo].[Partner_Commission_Range] ([Partner_Commission_Range_ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Partner_Lead_Commission] CHECK CONSTRAINT [FK_Partner_Lead_Commission_Partner_Commission_Range]
GO
ALTER TABLE [dbo].[Partner_Lead_Commission] ADD  CONSTRAINT [DF_Partner_Lead_Commission_Fixed_Amount]  DEFAULT ((0.00)) FOR [Fixed_Amount]
GO
ALTER TABLE [dbo].[Partner_Lead_Commission] ADD  CONSTRAINT [DF_Partner_Lead_Commission_Effective_Date]  DEFAULT (getdate()) FOR [Effective_Date]
GO
ALTER TABLE [dbo].[Partner_Lead_Commission] ADD  CONSTRAINT [DF_Partner_Lead_Commission_Active]  DEFAULT ((1)) FOR [Active]
GO
