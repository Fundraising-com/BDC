USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Kit_Type]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Kit_Type](
	[Kit_Type_ID] [int] NOT NULL,
	[Description] [varchar](50) NULL,
	[Delivery_Time] [datetime] NULL,
	[Comments] [text] NULL,
	[Is_Default] [bit] NULL,
	[is_active] [bit] NULL,
	[create_date] [datetime] NULL,
 CONSTRAINT [PK_Kit_Type] PRIMARY KEY CLUSTERED 
(
	[Kit_Type_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Kit_Type] ADD  CONSTRAINT [DF_Kit_Type_Is_Default]  DEFAULT (0) FOR [Is_Default]
GO
ALTER TABLE [dbo].[Kit_Type] ADD  CONSTRAINT [DF_Kit_Type_is_active]  DEFAULT (1) FOR [is_active]
GO
ALTER TABLE [dbo].[Kit_Type] ADD  CONSTRAINT [DF_Kit_Type_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
