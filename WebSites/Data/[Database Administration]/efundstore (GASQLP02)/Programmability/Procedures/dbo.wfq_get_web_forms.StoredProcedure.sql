USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[wfq_get_web_forms]    Script Date: 02/14/2014 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[wfq_get_web_forms](@intWebForm_id AS INTEGER)
AS
	SELECT 
	control_type_id,
	assembly_name,
	namespace,
	class_name,
	display_attribute,
	binding_name,
	event_handler_name,
	auto_post_back
	FROM controls_types
	WHERE control_type_id IN
		(
		SELECT control_type_id FROM questions q
			INNER JOIN web_forms_questions wfq
			ON wfq.question_id = q.question_id
			WHERE wfq.web_form_id = @intWebForm_id
		)

	SELECT 
		web_form_id,
		web_form_desc,
		web_form_type_id,
		lead_status_id,
		datestamp, 
		stored_proc_to_call
	FROM web_forms
		WHERE web_form_id = @intWebForm_id
	
	SELECT
		q.question_id,
		q.question_name,
		q.question_description,
		q.control_type_id,
		q.field_name,
		q.default_value,
		q.min_lenght,
		q.max_lenght,
		q.nbr_values,
		q.datestamp,
		wfq.is_required,
		wfq.question_order,
		q.stored_proc_to_call,
		q.anwsers_type, 
		q.field_value,
		qpt.parameter_target
	FROM questions q, web_forms_questions wfq,  question_params_target qpt		
		WHERE q.question_id = wfq.question_id
		AND wfq.web_form_id = 1
		AND qpt.web_form_id = 1
		AND qpt.question_id = q.question_ID
	ORDER BY wfq.question_order ASC
GO
