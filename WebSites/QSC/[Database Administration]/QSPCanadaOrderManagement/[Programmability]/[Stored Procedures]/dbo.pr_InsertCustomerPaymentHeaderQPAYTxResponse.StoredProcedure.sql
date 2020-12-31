USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_InsertCustomerPaymentHeaderQPAYTxResponse]    Script Date: 06/07/2017 09:20:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================

-- Author: <Author,,Name>

-- Create date: <Create Date,,>

-- Description: <Description,,>

-- =============================================

CREATE PROCEDURE [dbo].[pr_InsertCustomerPaymentHeaderQPAYTxResponse]

-- Add the parameters for the stored procedure here

@customerpaymentheaderinstance int , 

@QPAYTx int

AS

BEGIN


SET NOCOUNT ON;

insert CustomerPaymentHeaderQPAYTxResponse

(CustomerPaymentHeaderInstance, BPPSTxID) values( @customerpaymentheaderinstance, @QPAYTx)


END
GO
