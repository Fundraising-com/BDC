	
	var fullNameContainer = '';
	var GlobalCountPropertyChanged = 0;
	
	function DecisionValidation()
			{
				alert("RadioButtonValidation");
				/*var oHiddenCtrl=document.getElementById('txtDecision');
				oHiddenCtrl.value="";
				var oVal = document.getElementById('rfvDecision');
				
				for(var i=0;i<2;i++) {
				var oDecisionCtrl = document.getElementById('rdbDecision' + i);
				alert(oDecisionCtrl.checked);
				if(oDecisionCtrl.checked) {
					oHiddenCtrl.value=i + "";
					oVal.IsValid = true;
					oVal.style.visibility = 'hidden';
		
					}					
				}
				
				if(oHiddenCtrl.value == "") {			
				oVal.IsValid = false;
				oVal.style.visibility = 'visible';
			}*/
			}

	function CheckBoxValidation(source) {

			var oSelectedMonth = ""; 
			var oHiddenCtrl = document.getElementById('txtMonthList')
			oHiddenCtrl.value = "";
			var oVal = document.getElementById('rfvMonthList');
				
			for(var i=0;i<12;i++) {
				var oMonthCtrl = document.getElementById(source.id + i);
				
				if(oMonthCtrl.checked) {
					oHiddenCtrl.value += i + " ";
					oVal.IsValid = true;
					oVal.style.visibility = 'hidden';
					
				}					
			}
			if(oHiddenCtrl.value == "") {			
				oVal.IsValid = false;
				oVal.style.visibility = 'visible';
			}
		
	}
	
	
	
	function validateNumber(source,Event)
	{
		var keyCode = Event.keyCode ? Event.keyCode : Event.which ? Event.which : Event.charCode;
		if (((keyCode>=48) && (keyCode<=57)) || keyCode==8)
			return true;
		else
			return false;
	}
	function ResetControls(source, Event) {
			/*alert(source.value);
			
			var keyCode = Event.keyCode ? Event.keyCode : Event.which ? Event.which : Event.charCode;
			if((keyCode == 8) && source.value == "") {
				var oCtrlName = source.id.replace(fullNameContainer + 'txt','');
				var oLabel = document.getElementById(fullNameContainer + 'lbl' + oCtrlName);
				var oValidator = document.getElementById(fullNameContainer + 'rfv' + oCtrlName);
				oValidator.IsValid = false;
				oValidator.style.visibility = 'visible';
				oLabel.style.color = 'red';
			} else if(source.value != "") {
				var oCtrlName = null;
				if(source.id != null)
				{				
					oCtrlName = source.id.replace(fullNameContainer + '_txt','');
				}
				else
				{
					oCtrlName = source.replace('txt','');
				}
				if (oCtrlName.id == 'EvnPhone')
				{
					var oLabel = document.getElementById(fullNameContainer + '_lbl' + oCtrlName);
					var oValidator = document.getElementById(fullNameContainer + '_rfv' + oCtrlName);
					oValidator.style.visibility = 'hidden';
					oLabel.style.color = 'black';
				}
				
			}  */
		
	}

	var isNN = (navigator.appName.indexOf("Netscape")!=-1);
	function setPhone(pCtrlName, index, input,len, e) {
			//pCtrlName = fullNameContainer + "_" + pCtrlName;
			var keyCode = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;
			var oCtrl = document.getElementById(pCtrlName);
			
			oCtrl.value = document.getElementById(pCtrlName + "1").value + "-" + 
							document.getElementById(pCtrlName + "2").value + "-" + 
							document.getElementById(pCtrlName + "3").value;	
			if(keyCode == 8) {
				oCtrl.value = document.getElementById(pCtrlName + "1").value + "-" + 
							document.getElementById(pCtrlName + "2").value + "-" + 
							document.getElementById(pCtrlName + "3").value;		
				if(oCtrl.value == "--")
					oCtrl.value = "";
			}
			var filter = (isNN) ? [0,8,9] : [0,8,9,16,17,18,37,38,39,40,46];
			
			if(input.value.length >= len && !containsElement(filter,keyCode)) {
				input.value = input.value.slice(0, len);
				input.form[(getIndex(input)+1) % input.form.length].focus();
			}
		

		function containsElement(arr, ele) {
			var found = false, index = 0;
			
			while(!found && index < arr.length)
				if(arr[index] == ele)
					found = true;
				else
					index++;
			
			return found;
		}
		
		function getIndex(input) {
			var index = -1, i = 0, found = false;
			
				while (i < input.form.length && index == -1)
					if (input.form[i] == input)
						index = i;
					else 
						i++;
			
			return index;
		}
		return true;
	}

	

