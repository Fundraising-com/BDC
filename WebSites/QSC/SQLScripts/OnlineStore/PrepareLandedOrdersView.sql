select top 99 t.LastWorkflowStepTypeID, *
from core.CustomerOrder co
join core.Tote t on t.ToteID = co.ToteIDContract
where CustomerOrderStateID = 39
and co.FormCode in ('0737', '0745')

SELECT		DISTINCT t.ToteID, c.ContractID
FROM		core.Tote t
JOIN		core.Contract c ON c.ContractID = t.ContractID
LEFT JOIN	core.SuperToteTote stt ON stt.ToteID = t.ToteID
WHERE		stt.SuperToteToteID IS NULL --Only include Totes that have not yet been aggregated
AND			t.WorkflowID IN (40, 41) --Only include Landed Mag and Landed Gift
AND			t.ToteID NOT IN ( --Exclude totes whose contract has a Landed Mag or Landed Gift CustomerOrder not ready to export
	
	SELECT		DISTINCT t2.ToteID
	FROM		core.Tote t2
	LEFT JOIN	core.CustomerOrder co2 ON co2.ToteIDContract = t2.ToteID
	JOIN		core.Contract c2 ON c2.ContractID = t2.ContractID
	LEFT JOIN	core.SuperToteTote stt2 ON stt2.ToteID = t2.ToteID
	AND			t2.WorkflowID IN (40, 41) --Only include Landed Mag and Landed Gift
	AND			stt2.SuperToteToteID IS NULL --Only include Totes that have not yet been aggregated
	AND			(co2.CustomerOrderStateID NOT IN (38) OR co2.CustomerOrderID IS NULL)
	
)

