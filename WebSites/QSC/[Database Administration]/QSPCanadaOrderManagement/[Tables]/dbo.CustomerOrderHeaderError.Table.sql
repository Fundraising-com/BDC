USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[CustomerOrderHeaderError]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerOrderHeaderError](
	[CustomerOrderHeaderErrorID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerOrderHeaderInstance] [int] NOT NULL,
	[TransID] [int] NULL,
	[FieldID] [smallint] NOT NULL,
	[CustomerOrderErrorTypeID] [smallint] NOT NULL,
	[ErrorFlagType] [tinyint] NOT NULL,
	[FocusCreated] [datetime2](7) NOT NULL,
	[FocusModified] [datetime2](7) NULL,
	[CreationDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_CustomerOrderHeaderError] PRIMARY KEY CLUSTERED 
(
	[CustomerOrderHeaderErrorID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CustomerOrderHeaderError]  WITH CHECK ADD  CONSTRAINT [FK_CustomerOrderHeaderError_CustomerOrderDetail] FOREIGN KEY([CustomerOrderHeaderInstance], [TransID])
REFERENCES [dbo].[CustomerOrderDetail] ([CustomerOrderHeaderInstance], [TransID])
GO
ALTER TABLE [dbo].[CustomerOrderHeaderError] CHECK CONSTRAINT [FK_CustomerOrderHeaderError_CustomerOrderDetail]
GO
ALTER TABLE [dbo].[CustomerOrderHeaderError] ADD  CONSTRAINT [DF_CustomerOrderHeaderError_CreationDate]  DEFAULT (getdate()) FOR [CreationDate]
GO
