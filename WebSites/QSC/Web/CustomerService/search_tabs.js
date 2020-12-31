<script language="javascript">
	
	function GetCookieVal (offset) {
		var endstr = document.cookie.indexOf (";", offset);
		if (endstr == -1)
			endstr = document.cookie.length;
		return parseInt(unescape(document.cookie.substring(offset, endstr)));
	}
	
	function GetCookieStringVal (offset) {
		var endstr = document.cookie.indexOf (";", offset);
		if (endstr == -1)
			endstr = document.cookie.length;

		return unescape(document.cookie.substring(offset, endstr));
	}
	
	function GetCookie (name) {
		var arg = name + "=";
		var alen = arg.length;
		var clen = document.cookie.length;
		var i = 0;
		while (i < clen) {
			var j = i + alen;
			if (document.cookie.substring(i, j) == arg)
				return GetCookieVal (j);
			i = document.cookie.indexOf(" ", i) + 1;
			if (i == 0) break; 
		}
		
		return null;
	}  
	
	function GetCookieString (name) {
		var arg = name + "=";
		var alen = arg.length;
		var clen = document.cookie.length;
		var i = 0;
		while (i < clen) {
			var j = i + alen;
			if (document.cookie.substring(i, j) == arg)
				return GetCookieStringVal (j);
			i = document.cookie.indexOf(" ", i) + 1;
			if (i == 0) break; 
		}
		
		return null;
	}  	
	
	function SetCookie (name, value) {
		var argv = SetCookie.arguments;
		var argc = SetCookie.arguments.length;
		var expires = (argc > 2) ? argv[2] : null;
		var path = (argc > 3) ? argv[3] : null;
		var domain = (argc > 4) ? argv[4] : null;
		var secure = (argc > 5) ? argv[5] : false;
		document.cookie = name + "=" + escape (value) +
		((expires == null) ? "" : ("; expires=" + expires.toGMTString())) +
		((path == null) ? "" : ("; path=" + path)) +
		((domain == null) ? "" : ("; domain=" + domain)) +
		((secure == true) ? "; secure" : "");
	}
	
	
	
			var maxH =495;		
			var group_visible;
			var subs_visible;
			var ship_visible;
			var cc_visible;
			var group_h;
			var subs_h;
			var ship_h;
			var cc_h;
			var expdate;
			
		var expdate = new Date();
		var num;
		expdate.setTime(expdate.getTime() +  (24 * 60 * 60 * 1000 * 31)); 
		if(!(num = GetCookie("num"))) {
			num=0
		}
		num++;
		SetCookie ("num", num, expdate);
	//	SetCookie("group_visible", group_visible, expdate)
		if (num == 1 || newsession == true) {
			
			group_visible=0
			cc_visible=0
			subs_visible=1
			ship_visible=0
			group_h=0
			cc_h=0
			subs_h=maxH
			ship_h=0
			
			expdate = new Date();
			expdate.setTime(expdate.getTime() +  (24 * 60 * 60 * 1000 * 31)); 
			SetCookie("group_visible", group_visible, expdate)
			SetCookie("subs_visible", subs_visible, expdate)
			SetCookie("ship_visible", ship_visible, expdate)
			SetCookie("cc_visible", cc_visible, expdate)
			SetCookie("group_h", group_h, expdate)
			SetCookie("subs_h", subs_h, expdate)
			SetCookie("ship_h", ship_h, expdate)
			SetCookie("cc_h", cc_h, expdate)
			eval("document.all.group_content.style.height = '"+maxH+"px';");
			
		} else {
			group_visible=GetCookie("group_visible")
			subs_visible=GetCookie("subs_visible")
			ship_visible=GetCookie("ship_visible")
			cc_visible=GetCookie("cc_visible")
			group_h=GetCookie("group_h")
			subs_h=GetCookie("subs_h")
			ship_h=GetCookie("ship_h")		
			cc_h=GetCookie("cc_h")	
		}
	
	
	if(group_h>0){	
		eval("document.all.group_content.style.display = 'block';");
		eval("document.all.group_search.style.display = 'block';");
		eval("document.all.group_content.style.height = '"+group_h+"px';");
	}
	else {
		//eval("document.all.group_content.style.height = '0px';");
		eval("document.all.group_content.style.display = 'none';");
		eval("document.all.group_search.style.display = 'none';");
	}
	
	if(subs_h>0){
		eval("document.all.subs_content.style.display = 'block';");
		eval("document.all.subs_search.style.display = 'block';");
		eval("document.all.subs_content.style.height = '"+subs_h+"px';");	
	}else{	
		//eval("document.all.subs_content.style.height = '0px';");	
		eval("document.all.subs_content.style.display = 'none';");
		eval("document.all.subs_search.style.display = 'none';");
	}
	
	if(ship_h>0){
		eval("document.all.ship_content.style.display = 'block';");
		eval("document.all.ship_search.style.display = 'block';");
		eval("document.all.ship_content.style.height = '"+ship_h+"px';");	
	}else{
		//eval("document.all.ship_content.style.height = '0px';");	
		eval("document.all.ship_content.style.display = 'none';");
		eval("document.all.ship_search.style.display = 'none';");
	}
		
		
	if(cc_h>0){
		eval("document.all.creditcard_content.style.display = 'block';");
		eval("document.all.creditcard_search.style.display = 'block';");
		eval("document.all.creditcard_content.style.height = '"+cc_h+"px';");	
	}else{
		//eval("document.all.creditcard_content.style.height = '0px';");	
		eval("document.all.creditcard_content.style.display = 'none';");
		eval("document.all.creditcard_search.style.display = 'none';");
	}
	
	function show(layer_ref){
			
			if(group_visible==1){
				moveOut('group_content', group_h)
				group_visible=0
				SetCookie("group_h", 0, expdate)
			}
			else if(subs_visible==1){
				moveOut('subs_content', subs_h)
				subs_visible=0
				SetCookie("subs_h", 0, expdate)
			}
			else if(ship_visible==1){
				moveOut('ship_content', ship_h)
				ship_visible=0
				SetCookie("ship_h", 0, expdate)
			
			}
			else if(cc_visible==1){
				moveOut('creditcard_content', cc_h)
				cc_visible=0
				SetCookie("cc_h", 0, expdate)
			
			}
			if(layer_ref == 'group_content'){
				eval( "document.all." + layer_ref + ".style.display = 'block'");	
				eval( "document.all.group_search.style.display = 'block'");	
				moveIn('group_content', group_h)		
				group_visible=1;
				SetCookie("group_h", maxH, expdate)
			} if(layer_ref == 'subs_content'){
				eval( "document.all." + layer_ref + ".style.display = 'block'");	
				eval( "document.all.subs_search.style.display = 'block'");		
				moveIn('subs_content', subs_h)		
				subs_visible=1;
				SetCookie("subs_h", maxH, expdate)
			} if(layer_ref == 'ship_content'){
				eval( "document.all." + layer_ref + ".style.display = 'block'");	
				eval( "document.all.ship_search.style.display = 'block'");		
				ship_visible=1;
				moveIn('ship_content', ship_h)
				SetCookie("ship_h", maxH, expdate)
			} if(layer_ref == 'creditcard_content'){
				eval( "document.all." + layer_ref + ".style.display = 'block'");	
				eval( "document.all.creditcard_search.style.display = 'block'");	
				cc_visible=1;
				moveIn('creditcard_content', cc_h)
				SetCookie("cc_h", maxH, expdate)
			}
			
					expdate = new Date();
			expdate.setTime(expdate.getTime() +  (24 * 60 * 60 * 1000 * 31)); 
			SetCookie("group_visible", group_visible, expdate)
			SetCookie("subs_visible", subs_visible, expdate)
			SetCookie("ship_visible", ship_visible, expdate)
			SetCookie("cc_visible", cc_visible, expdate)
			
		
			
	}
	
	function wait(hv, v, layer_ref){
		string="retrieve("+hv+","+v+",'"+layer_ref+"');";
		setTimeout(string,1);
	}
	function wait2(hv, v, layer_ref){
		string="extend("+hv+","+v+",'"+layer_ref+"');";
		setTimeout(string,1);
	}

function retrieve(hv, v, layer_ref){
	eval(hv +"-= 50")
	eval("document.all."+layer_ref+".style.height ='"+v+"px'");			
	moveOut(layer_ref, hv)
}

function extend(hv, v, layer_ref){
	eval(hv +"+= 50")
	eval("document.all."+layer_ref+".style.height ='"+v+"px'");			
	moveIn(layer_ref, hv)
}


	function moveOut(layer_ref,i) {
		
			if(layer_ref == 'group_content'){
				eval( "document.all.group_search.style.display = 'none'");
				if(group_h > 0){
					wait('\'group_h\'', group_h, 'group_content')
				}
				else {
					eval("document.all."+layer_ref+".style.height ='0px'");			
					eval( "document.all." + layer_ref + ".style.display = 'none'");	
						
					group_h=0
				}
			
			} else 	if(layer_ref == 'subs_content'){
			
				eval( "document.all.subs_search.style.display = 'none'");
				if(subs_h > 0){
					wait('\'subs_h\'', subs_h, 'subs_content')
				}
				else {
					eval("document.all."+layer_ref+".style.height ='0px'");
					eval( "document.all." + layer_ref + ".style.display = 'none'");	
						
					subs_h=0
				}
			
			} else 	if(layer_ref == 'ship_content'){
				eval( "document.all.ship_search.style.display = 'none'");
				if(ship_h > 0){
					wait('\'ship_h\'', ship_h, 'ship_content')
				}
				else {
					ship_h=0
					eval("document.all."+layer_ref+".style.height ='0px'");
					eval( "document.all." + layer_ref + ".style.display = 'none'");	
				//		
				}
			
			} else 	if(layer_ref == 'creditcard_content'){
				eval( "document.all.creditcard_search.style.display = 'none'");	
				if(cc_h > 0){
					wait('\'cc_h\'', cc_h, 'creditcard_content')
				}
				else {
					cc_h=0
					eval("document.all."+layer_ref+".style.height ='0px'");
					eval( "document.all." + layer_ref + ".style.display = 'none'");	
				//	
				}
			
			}
						
	
	}

	function moveIn(layer_ref,i) {
		
			if(layer_ref == 'group_content'){
			
				if(group_h <= maxH){
					wait2('\'group_h\'', group_h, 'group_content')
				}
				else {
					eval( "document.all." + layer_ref + ".style.display = 'block'");	
					
				}
			
			} else 	if(layer_ref == 'subs_content'){
			
				if(subs_h <= maxH){
					wait2('\'subs_h\'', subs_h, 'subs_content')
				}
				else {
					eval( "document.all." + layer_ref + ".style.display = 'block'");	
				}
			
			} else 	if(layer_ref == 'ship_content'){
			
				if(ship_h <= maxH){
					wait2('\'ship_h\'', ship_h, 'ship_content')
				}
				else {
					eval( "document.all." + layer_ref + ".style.display = 'block'");	
				}
			
			} else 	if(layer_ref == 'creditcard_content'){
			
				if(cc_h <= maxH){
					wait2('\'cc_h\'', cc_h, 'creditcard_content')
				}
				else {
					eval( "document.all." + layer_ref + ".style.display = 'block'");	
				}
			
			}
			
	
		
	}
</script>