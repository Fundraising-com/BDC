DROP TABLE #duppayments
DROP TABLE #allpayments

SELECT	*
INTO	#allpayments
FROM	Payment
WHERE	Order_ID IN (SELECT OrderID from QSPCanadaOrderManagement..Batch WHERE OrderID BETWEEN 800000 AND 900000)
AND		cheque_number <> 'CASH'

--run this update until 0 rows returned to strip all leading zeros
update #allpayments
set Cheque_Number = RIGHT(Cheque_Number, LEN(Cheque_Number)-1)
where LEFT(cheque_number, 1) = '0'

SELECT	*
FROM	#allpayments

SELECT		Order_ID,
			Cheque_Number,
			--Payment_Amount,
			count(*) as Cnt
INTO		#DupPayments
FROM		#AllPayments
WHERE		Order_ID NOT IN (800954,800955,800956,801536)
GROUP BY	Order_ID,
			Cheque_Number
			--Payment_Amount
HAVING		COUNT(Order_ID) > 1
ORDER BY	Order_ID

SELECT	*
FROM	#DupPayments

SELECT		*,
			ap.Payment_ID,
			','
FROM		#DupPayments dp
JOIN		#AllPayments ap
				ON	ap.Cheque_Number = dp.Cheque_Number
				AND	ap.Order_ID = dp.Order_ID
				--AND	ap.Payment_Amount = dp.Payment_Amount
--where		ap.last_updated_by = '646'--'ChequeLoad'
ORDER BY	dp.Order_ID,
			dp.Cheque_Number
