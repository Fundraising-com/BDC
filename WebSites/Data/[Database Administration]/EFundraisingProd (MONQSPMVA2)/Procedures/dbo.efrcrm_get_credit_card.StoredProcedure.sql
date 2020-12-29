USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_credit_card]    Script Date: 02/14/2014 13:04:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[efrcrm_get_credit_card] @sale_id int, @passphrase nvarchar(20)
   
AS
begin

-- display ciphertext
--SELECT @encrypted_str AS CipherText;
DECLARE @decrypted_str VARBINARY(MAX)
select @decrypted_str = credit_card_no from sale where sales_id = @sale_id

SET @decrypted_str = DecryptByPassPhrase(@passphrase, @decrypted_str);

-- display decrypted text
SELECT CONVERT(NVARCHAR(200), @decrypted_str) AS PlainText;


end
GO
