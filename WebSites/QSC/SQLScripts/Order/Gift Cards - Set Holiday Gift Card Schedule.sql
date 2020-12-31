SELECT	*
FROM	SystemOptions

UPDATE	QSPCanadaCommon..SystemOptions
SET		TextValue = '2008-12-08'
WHERE	KeyValue = 'First_Mail_Date_Xmas_Gift_Card'

UPDATE	QSPCanadaCommon..SystemOptions
SET		TextValue = '2008-12-19'
WHERE	KeyValue = 'Last_Mail_Date_Xmas_Gift_Card'