////////////////////////////////////////////////////////////
// This software is solely the property of Karamasoft LLC. /
//   Copyright 2007 Karamasoft LLC. All rights reserved.   /
//                  www.karamasoft.com                     /
////////////////////////////////////////////////////////////

 var usda0=navigator.appVersion.toLowerCase(); var usda1=usda0.indexOf('msie'); var usda2; if (usda1 != -1) { usda2=parseFloat(usda0.substring(usda1+5,usda0.indexOf(';',usda1))); } var usda3=(navigator.userAgent.indexOf('Opera') != -1); var usda4=(usda1 != -1 && !usda3) ? true : false; var usda5=(usda4 && usda2 >= 5) ? true : false; var usda6=(usda4 && usda2 >= 6) ? true : false; var usda7=(!usda4 && document.getElementById) ? true : false; var usd2d='block'; var usd3d='none'; var usd5c='_i'; var UltimatePopups; var UltimatePopupItems; var curPopupObj; function usdj2(usdj3) { if (usda4) { return document.all[usdj3]; } else if (usda7) { return document.getElementById(usdj3); } } function AddEventHandler(obj,usdb23,usdb24) { if (obj.attachEvent) { obj.attachEvent('on'+usdb23,usdb24); } else if (obj.addEventListener) { obj.addEventListener(usdb23,usdb24,true); } } function usd4u(usd5u) { return (usd5u.srcElement) ? usd5u.srcElement : (usd5u.target) ? usd5u.target : null; } function usd8d(usd5u,usd9d) { if (usda4) { return (usd9d.contains(usd5u.srcElement)); } else if (usda7) { var usd0e=usd5u.target; while (usd0e) { if (usd0e == usd9d) { return true; } usd0e=usd0e.parentNode; } return false; } } function usda84(usd5u) { return (usd5u.clientX) ? usd5u.clientX : (usd5u.pageX) ? usd5u.pageX : 0; } function usda85(usd5u) { return (usd5u.clientY) ? usd5u.clientY : (usd5u.pageY) ? usd5u.pageY : 0; } function usdk0(usdk1,usdk2) { if (usda4) { usdk1.insertBefore(usdk2); } else if (usda7) { usdk1.appendChild(usdk2); } } function usd0f(usd1f,usd2f) { if (!usdj2(usd1f)) { var usd4v=document.createElement("iframe"); usd4v.id=usd1f; usd4v.name=usd1f; usd4v.src=(usd2f) ? usd2f : ""; usd4v.style.width="0px"; usd4v.style.height="0px"; usd4v.frameBorder="0"; usd4v.marginHeight="0"; usd4v.marginWidth="0"; usdk0(document.body,usd4v); } return usdj2(usd1f); } function UltimatePopup(usda86,usda87,usda88,usda71,usda72,usda73,usda74) { this.usda86=usda86; this.usda87=usda87; this.usda88=usda88; this.usda71=usda71; this.usda72=usda72; this.usda73=usda73; this.usda74=usda74; this.usda91(); } UltimatePopup.prototype.usda91=function() { if (typeof(UltimatePopups) == 'undefined') { UltimatePopups=new Object; } UltimatePopups[this.usda86]=this; }; UltimatePopup.prototype.PopulatePopup=function() { if (!usdj2(this.usda86)) { this.usda92(); } this.usda93(); }; UltimatePopup.prototype.usda92=function() { var usda95=document.createElement('div'); usda95.id=this.usda86; usda95.name=usda95.id; usda95.className=this.usda88; usda95.style.position='absolute'; usda95.style.zIndex=this.usda71; usdk0(document.body,usda95); if (usda5) { var usda96=usd0f(this.usda86+usd5c,((window.location.protocol == 'https:') ? this.usda87+'blank.htm' : '')); usda96.scrolling="no"; usda96.style.position="absolute"; usda96.style.zIndex=usda95.style.zIndex-1; usda96.style.display=usd3d; } AddEventHandler(document,'mouseup',function(usd5u) { HidePopupOnMouseUp(usd5u); }); return usda95; }; UltimatePopup.prototype.usda93=function() { var usda95=usdj2(this.usda86); this.usda97(usda95); if (this.usda74) { for (var i=0,usdn3=this.usda74.length; i < usdn3; i++) { this.usda98(usda95,this.usda74[i]); this.usda74[i].popupObj=this; } } }; UltimatePopup.prototype.usda97=function(usda95) { while (usda95.childNodes.length > 0) { var usda51=usda95.childNodes[0]; RemoveEventHandler(usda51,'mouseover',usda99); RemoveEventHandler(usda51,'mouseout',usdb10); RemoveEventHandler(usda51,'mouseup',usdb11); usda95.removeChild(usda51); } }; UltimatePopup.prototype.usda98=function(usda95,usdb12) { var usda51=document.createElement('div'); usda51.id=usdb12.usda89; usda51.innerHTML=usdb12.usda79; usda51.className=usdb12.usda80; usda51.usda80=usdb12.usda80; usda51.usda81=usdb12.usda81; if (usda4) { usda51.unselectable='on'; } if (!usdb12.usd2b) { AddEventHandler(usda51,'mouseover',usda99); AddEventHandler(usda51,'mouseout',usdb10); AddEventHandler(usda51,'mouseup',usdb11); } usda95.appendChild(usda51); }; UltimatePopup.prototype.ShowPopup=function(usd5u) { if (curPopupObj && curPopupObj != this) { HidePopupOnMouseUp(usd5u); } curPopupObj=this; curPopupObj.usd2w=(typeof(usd5u) != 'undefined') ? usd4u(usd5u).id : null; usdb13(usd2d); this.usda94(usd5u); }; UltimatePopup.prototype.usda94=function(usd5u) { var usda95=usdj2(this.usda86); usda95.style.left=((this.usda72 != '') ? this.usda72 : usda84(usd5u))+'px'; usda95.style.top=((this.usda73 != '') ? this.usda73 : usda85(usd5u))+'px'; if (usda5) { var usda96=usdj2(this.usda86+usd5c); usda96.style.left=usda95.style.left; usda96.style.top=usda95.style.top; usda96.style.width=usda95.offsetWidth+'px'; usda96.style.height=usda95.offsetHeight+'px'; } }; UltimatePopup.prototype.SetPopupZIndex=function(usda71) { this.usda71=usda71; }; UltimatePopup.prototype.SetPopupLeft=function(usda72) { this.usda72=usda72; }; UltimatePopup.prototype.SetPopupTop=function(usda73) { this.usda73=usda73; }; UltimatePopup.prototype.SetPopupItems=function(usda74) { this.usda74=usda74; }; function HidePopup() { usdb13(usd3d); curPopupObj=null; } function usdb13(usda56) { if (curPopupObj) { var usda95=usdj2(curPopupObj.usda86); usda95.style.display=usda56; usda95.scrollTop=0; if (usda5) { var usda96=usdj2(curPopupObj.usda86+usd5c); usda96.style.display=usda56; } } } function HidePopupOnMouseUp(usd5u) { if (curPopupObj) { var usda95=usdj2(curPopupObj.usda86); if (usda95 && (usda95.style.display == usd2d) && (typeof(usd5u) == 'undefined' || !usd8d(usd5u,usda95)) && (!usda7 || curPopupObj.usd2w != usd4u(usd5u).id)) { HidePopup(); } } } function usda99(usd5u) { var usda35=usdb15(usd5u); usda35.className=usda35.usda81; } function usdb10(usd5u) { var usda35=usdb15(usd5u); usda35.className=usda35.usda80; } function usdb11(usd5u) { var usda35=usdb15(usd5u); var usdb12Obj=UltimatePopupItems[usda35.id]; if (usdb12Obj && usdb12Obj.usda82) { usdb12Obj.usda82(usd5u); } HidePopup(); } function usdb15(usd5u) { var usda35=usd4u(usd5u); while (usda35 && typeof(UltimatePopupItems[usda35.id]) == 'undefined') { usda35=usda35.parentNode; } return usda35; } function UltimatePopupItem(usda89,usda79,usda80,usda81,usda82,usd2b) { this.usda89=usda89; this.usda79=usda79; this.usda80=usda80; this.usda81=usda81; this.usda82=usda82; this.usd2b=usd2b; this.usda90(); } UltimatePopupItem.prototype.usda90=function() { if (typeof(UltimatePopupItems) == 'undefined') { UltimatePopupItems=new Object; } UltimatePopupItems[this.usda89]=this; }; if (typeof(Sys) != 'undefined' && typeof(Sys.Application) != 'undefined') { Sys.Application.notifyScriptLoaded(); } 