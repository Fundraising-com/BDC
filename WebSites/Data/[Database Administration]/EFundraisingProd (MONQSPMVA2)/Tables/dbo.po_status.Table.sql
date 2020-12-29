USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[po_status]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[po_status](
	[po_status_id] [tinyint] NOT NULL,
	[description] [varchar](50) NOT NULL,
	[active] [bit] NULL,
 CONSTRAINT [PK_po_status] PRIMARY KEY CLUSTERED 
(
	[po_status_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UC_po_status_description] UNIQUE NONCLUSTERED 
(
	[description] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[po_status] ADD  DEFAULT ((1)) FOR [active]
GO
