<SCRIPT language="javascript">
	
	

		function getArgs() {
		var args  = new Object();
		var query = location.search.substring(1);
	
		var pairs = query.split('-');
		for (var i = 0;  i < pairs.length; i++) {
			var pos = pairs[i].indexOf('=');
			if(pos == -1) continue;
			var argname = pairs[i].substring(0, pos);
			var value = pairs[i].substring(pos+1);
			args[argname] = unescape(value);
		}
		return args;
	}
 
	var step1_state = 'block'; 
	var step3_state = 'none';
	
	var tmp;
		
	if(!(tmp = GetCookieString("step1_state"))) 
	{
		SetCookie ("step1_state", 'block', expdate);
		SetCookie ("divSearch", 'block', expdate);
		SetCookie ("divSearch2", 'none', expdate);
	}
	else
	{
		if(communicationstep1)
		{
				ShowStep('divSearch', 1,showstep1)
				
					
		}
		else
		{
				
			if(tmp = GetCookieString("step1_state"))
			{
				step1_state=GetCookieString("step1_state");
				eval( "document.all.divSearch.style.display = '"+GetCookieString("divSearch")+"'");
				eval( "document.all.divSearch2.style.display = '"+GetCookieString("divSearch2")+"'");
			}
		}
	}
	
	
	
	if(!(tmp = GetCookieString("step3_state"))) {
		SetCookie ("step3_state", 'none', expdate);
		SetCookie ("divAction", 'none', expdate);
		
		SetCookie ("divAction2", 'block', expdate);
	}
	else
	{
		if(communicationstep3)
		{
			ShowStep('divAction', 3,showstep3)
			
		}
		else
		{
			if(tmp = GetCookieString("step3_state")){
				step3_state=GetCookieString("step3_state");
				eval( "document.all.divAction.style.display = '"+GetCookieString("divAction")+"'");
				eval( "document.all.divAction2.style.display = '"+GetCookieString("divAction2")+"'");
			}
		}
		
	}
		
 	var args = getArgs();
 
 	if(args.step1)
 	{
		if(args.step1 == 'block'){
			step1_state = 'block'
			eval( "document.all.divSearch.style.display = 'block'");
			eval( "document.all.divSearch2.style.display = 'none'");
		} else {
			step1_state = 'none'
			eval( "document.all.divSearch.style.display = 'none'");
			eval( "document.all.divSearch2.style.display = 'block'");
		}
 	}
	
	if(args.step3){
		if(args.step3 == 'block'){
			step3_state = 'block'
			eval( "document.all.divAction.style.display = 'block'");
			eval( "document.all.divAction2.style.display = 'none'");
		} else {
			step3_state = 'none'
			eval( "document.all.divAction.style.display = 'none'");
			eval( "document.all.divAction2.style.display = 'block'");
		}
 	}
		
	function showhide(layer_ref, step) 
	{ 
		
		var expdate = new Date();
		expdate.setTime(expdate.getTime() +  (24 * 60 * 60 * 1000 * 31)); 
		
		if(step == '1'){
			if (step1_state == 'block') { 
				eval( "document.all." + layer_ref + ".style.display = 'none'");
				eval( "document.all." + layer_ref + "2.style.display = 'block'");
				step1_state = 'none'
				SetCookie ("divSearch", "none", expdate);
				SetCookie ("divSearch2", "block", expdate);
			} else { 
				eval( "document.all." + layer_ref + ".style.display = 'block'");
				eval( "document.all." + layer_ref + "2.style.display = 'none'");
				step1_state = 'block'
				
				SetCookie ("divSearch", "block", expdate);
				SetCookie ("divSearch2", "none", expdate);
			}
			SetCookie ("step1_state", step1_state, expdate);
		} else {
			if (step3_state == 'block') { 
				eval( "document.all." + layer_ref + ".style.display = 'none'");
				eval( "document.all." + layer_ref + "2.style.display = 'block'");
				step3_state = 'none'
				
				SetCookie ("divAction", 'none', expdate);
				SetCookie ("divAction2", 'block', expdate);
			} else { 
				eval( "document.all." + layer_ref + ".style.display = 'block'");
				eval( "document.all." + layer_ref + "2.style.display = 'none'");
				step3_state = 'block'
				
				SetCookie ("divAction", 'block', expdate);
				SetCookie ("divAction2", 'none', expdate);
			}
			
			SetCookie ("step3_state", step3_state, expdate);
		}
		}
		
		function ShowStep(layer_ref, step,value) 
		{ 
		
		var expdate = new Date();
		expdate.setTime(expdate.getTime() +  (24 * 60 * 60 * 1000 * 31)); 
		
		if(step == '1'){
			if (!value) { 
				eval( "document.all." + layer_ref + ".style.display = 'none'");
				eval( "document.all." + layer_ref + "2.style.display = 'block'");
				step1_state = 'none'
				SetCookie ("divSearch", "none", expdate);
				SetCookie ("divSearch2", "block", expdate);
			} else { 
				eval( "document.all." + layer_ref + ".style.display = 'block'");
				eval( "document.all." + layer_ref + "2.style.display = 'none'");
				step1_state = 'block'
				
				SetCookie ("divSearch", "block", expdate);
				SetCookie ("divSearch2", "none", expdate);
			}
			SetCookie ("step1_state", step1_state, expdate);
		} 
		else 
		{
			if (!value) 
			{ 
				eval( "document.all." + layer_ref + ".style.display = 'none'");
				eval( "document.all." + layer_ref + "2.style.display = 'block'");
				step3_state = 'none'
				
				SetCookie ("divAction", 'none', expdate);
				SetCookie ("divAction2", 'block', expdate);
			} 
			else 
			{ 
				eval( "document.all." + layer_ref + ".style.display = 'block'");
				eval( "document.all." + layer_ref + "2.style.display = 'none'");
				step3_state = 'block'
				
				SetCookie ("divAction", 'block', expdate);
				SetCookie ("divAction2", 'none', expdate);
			}
			
			SetCookie("step3_state", step3_state, expdate);
		}
		 
	}
	
	
</SCRIPT>
