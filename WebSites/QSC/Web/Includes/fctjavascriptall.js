	function Refresh()
	{
		document.forms(0).submit();
	}
	function Reset(div)
	{
		var elements = eval("document.all." + div+".all");
		for(i=0;i < elements.length;i++)				
		{
			
			if(elements[i].type == "text")
			{
				elements[i].value = '';
			}
			else if(elements[i].type == "select-one")
			{
				elements[i].selectedIndex = 0;
			}
			
		}
	}
	function Open(url)
	{
		window.open(url,'',"toolbar = no,status=no,scrollbars=yes,resizable=no, width=668, height=550");
	}
	
	function OpenBig(url)
	{
		window.open(url,'',"toolbar = no,status=no,scrollbars=yes,resizable=yes, width=800, height=550");
	}
	
	function OpenCustom(url, width, height)
	{
		window.open(url,'',"toolbar = no,status=no,scrollbars=yes,resizable=yes, width=" + width + ", height=" + height);
	}
	
	function openErrorWindow(var1){
	    toggleDivConfirmVisibility();
	    
		if (document.all)
			var xMax = screen.width, yMax = screen.height;
		else
			if (document.layers)
				var xMax = window.outerWidth, yMax = window.outerHeight;
			else
				var xMax = 1024, yMax=768;

		var xOffset = (xMax - 550)/2, yOffset = (yMax - 250)/2;

		loadwindow(var1,550,250)
		
	
	}
		if (document.all){
			var xMax = document.body.offsetWidth;
			var yMax = document.body.offsetHeight;


			var xOffset = (xMax - 550)/2, yOffset = (yMax - 350)/2;
		}
		


var dragapproved=false
var minrestore=0
var initialwidth,initialheight
var ie5=document.all&&document.getElementById
var ns6=document.getElementById&&!document.all

function iecompattest(){
return (!window.opera && document.compatMode && document.compatMode!="BackCompat")? document.documentElement : document.body
}

function drag_drop(e){
if (ie5&&dragapproved&&event.button==1){
document.getElementById("dwindow").style.left=tempx+event.clientX-offsetx+"px"
document.getElementById("dwindow").style.top=tempy+event.clientY-offsety+"px"
}
else if (ns6&&dragapproved){
document.getElementById("dwindow").style.left=tempx+e.clientX-offsetx+"px"
document.getElementById("dwindow").style.top=tempy+e.clientY-offsety+"px"
}
}

function initializedrag(e){
offsetx=ie5? event.clientX : e.clientX
offsety=ie5? event.clientY : e.clientY
document.getElementById("dwindowcontent").style.display="none" //extra
tempx=parseInt(document.getElementById("dwindow").style.left)
tempy=parseInt(document.getElementById("dwindow").style.top)

dragapproved=true
document.getElementById("dwindow").onmousemove=drag_drop
}

function loadwindow(url,width,height){
if (document.getElementById("dpleasewait"))
	document.getElementById("dpleasewait").style.display='none'
if (!ie5&&!ns6)
window.open(url,"","width=width,height=height,scrollbars=1")
else{

document.getElementById("dwindow").style.display=''
document.getElementById("dwindow").style.width=initialwidth=width+"px"
document.getElementById("dwindow").style.height=initialheight=height+"px"
document.getElementById("dwindow").style.left=xOffset+"px"
document.getElementById("dwindow").style.top=yOffset+"px"
document.getElementById("cframe").src=url

}
}


function maximize(){
if (minrestore==0){
minrestore=1 //maximize window
document.getElementById("maxname").setAttribute("src","restore.gif")
document.getElementById("dwindow").style.width=ns6? window.innerWidth-20+"px" : iecompattest().clientWidth+"px"
document.getElementById("dwindow").style.height=ns6? window.innerHeight-20+"px" : iecompattest().clientHeight+"px"
}
else{
minrestore=0 //restore window
document.getElementById("maxname").setAttribute("src","max.gif")
document.getElementById("dwindow").style.width=initialwidth
document.getElementById("dwindow").style.height=initialheight
}
document.getElementById("dwindow").style.left=ns6? window.pageXOffset+"px" : iecompattest().scrollLeft+"px"
document.getElementById("dwindow").style.top=ns6? window.pageYOffset+"px" : iecompattest().scrollTop+"px"
}

function closeit(){
document.getElementById("dwindow").style.display="none"
}

function stopdrag(){
dragapproved=false;
document.getElementById("dwindow").onmousemove=null;
document.getElementById("dwindowcontent").style.display="" //extra
}

function pleasewait(){
document.getElementById("dpleasewait").style.display=''
document.getElementById("dpleasewait").style.left=xOffset+"px"
document.getElementById("dpleasewait").style.top=yOffset+"px"
}

function toggleDivConfirmVisibility(){

 var div = document.getElementById("divConfirm");
 if ( div != null){
	var display = div.style.visibility;
	if ( display != "hidden" ){
		div.style.visibility = "hidden";
	}
	else{
		div.style.visibility = "inherit";
	}	
 }
}