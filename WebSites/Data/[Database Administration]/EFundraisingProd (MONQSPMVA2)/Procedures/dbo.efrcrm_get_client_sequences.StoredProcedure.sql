USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_client_sequences]    Script Date: 02/14/2014 13:04:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Client_sequence
CREATE PROCEDURE [dbo].[efrcrm_get_client_sequences] AS
begin

select Client_sequence_code, Description, Is_active from Client_sequence

end
GO
