USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_client_by_id_and_sequence_code]    Script Date: 02/14/2014 13:04:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Client_activity
CREATE PROCEDURE [dbo].[efrcrm_get_client_by_id_and_sequence_code] @Client_id int, @Client_sequence_code char(2) AS
begin

SELECT *
FROM client
WHERE Client_id= @Client_id AND client_sequence_code=@Client_sequence_code
end
GO
