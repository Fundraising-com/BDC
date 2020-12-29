USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[payment_exception_type]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[payment_exception_type](
	[payment_id] [int] NOT NULL,
	[exception_type_id] [int] NOT NULL,
	[create_date] [datetime] NOT NULL,
	[validated_date] [datetime] NULL,
	[Is_Corrected] [bit] NOT NULL,
 CONSTRAINT [PK_payment_exception_type] PRIMARY KEY CLUSTERED 
(
	[payment_id] ASC,
	[exception_type_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[payment_exception_type]  WITH CHECK ADD  CONSTRAINT [FK_payment_exception_type_exception_type] FOREIGN KEY([exception_type_id])
REFERENCES [dbo].[exception_type] ([exception_type_id])
GO
ALTER TABLE [dbo].[payment_exception_type] CHECK CONSTRAINT [FK_payment_exception_type_exception_type]
GO
ALTER TABLE [dbo].[payment_exception_type]  WITH CHECK ADD  CONSTRAINT [FK_payment_exception_type_payment] FOREIGN KEY([payment_id])
REFERENCES [dbo].[payment] ([payment_id])
GO
ALTER TABLE [dbo].[payment_exception_type] CHECK CONSTRAINT [FK_payment_exception_type_payment]
GO
ALTER TABLE [dbo].[payment_exception_type] ADD  CONSTRAINT [DF_payment_exception_type_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
ALTER TABLE [dbo].[payment_exception_type] ADD  CONSTRAINT [DF_payment_exception_type_Is_Corrected]  DEFAULT (0) FOR [Is_Corrected]
GO
