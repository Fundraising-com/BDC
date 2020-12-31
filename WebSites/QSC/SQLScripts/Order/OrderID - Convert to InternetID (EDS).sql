SELECT		ioi.InternetOrderID
FROM		Batch b
LEFT JOIN	CustomerOrderHeader coh
				ON	coh.OrderBatchID = b.ID
				AND	coh.OrderBatchDate = b.Date
LEFT JOIN	InternetOrderID ioi
				ON	ioi.CustomerOrderHeaderInstance = coh.Instance
WHERE		b.OrderID = 1036971



ORDER # 1040758 = 50804456
ORDER # 1036971 = 50783285,50783259,50781538,50780903,50780861,50773290
ORDER # 1033450 = 50726193
ORDER # 1045597 = 50850320
ORDER # 1042571 = 50817832
ORDER # 1033450 = 50726193
ORDER # 1056137 = 51033871