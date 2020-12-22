<script language="JavaScript">

/*
Autor: Jorge Ortiz Giraldo
e-mail: jortizg@hotmail.com
website: jortizg.tripod.com
Empresa: WebSys (Medellín - Colombia)
Version 1.0 : Fecha: 31/marzo/2000
Version 1.1 : Fecha: 16/junio/2000
Mejoras introducidas en esta versión:
-Ahora funciona también en Internet Explorer (v5).
-Puede usarse este control varias veces en una misma página.
-Escribe automáticamente las fechas elegidas, en los campos designados.
-Permite asignar un título a la ventana de calendario.
-Resalta en rojo el día de la fecha actual.
-Presenta la fecha actual en un botón que permite elegirla inmediatamente.
Licenciamiento: La propiedad de este script corresponde unica y exclusivamente a su autor, quien concede
a otros usuarios la posibilidad de usarlo libremente si y solo si se conservan estas lineas
de autoría.  Ni el autor ni su empresa ofrecen ninguna garantía sobre el funcionamiento de este
script ni se hacen responsables por fallas ocasionadas por su uso.
*/

nombresMes = Array("","January","February","March","April","May","June","July","August","September","October","November","December");
var anoInicial = 2000;
var anoFinal = 2020;
var ano;
var mes;
var dia;
var campoDeRetorno;
var titulo;
var curyear;
var curday;
var curmonth;
var DateToset;
var DAYSHEADERTYLE = "style=\"COLOR: white; HEIGHT: 1px; BACKGROUND-COLOR: #cc6600\"";
var SELECTDAYSTYLE ="style=\"WIDTH: 14%;FONT-WEIGHT: bold; WIDTH: 14%; COLOR: white; BACKGROUND-COLOR: #cc6600\"";
var REGULARSTYLE = "style=\"WIDTH: 14%;\" align=\"middle\">";

function diasDelMes(ano,mes) {
	
  if ((mes==1)||(mes==3)||(mes==5)||(mes==7)||(mes==8)||(mes==10)||(mes==12)) dias=31
  else if ((mes==4)||(mes==6)||(mes==9)||(mes==11)) dias=30 //F6 - replace 31 by 30
  else if ((((ano % 100)==0) && ((ano % 400)==0)) || (((ano % 100)!=0) && ((ano % 4)==0))) dias = 29
  else dias = 28;
  return dias;
};

function CreateSelectMonth(mesActual) {
  var selectorMes = "";
  selectorMes = "<select name='mes' size='1' onChange='javascript:opener.SetToday(self.document.Calendar.ano[self.document.Calendar.ano.selectedIndex].value,self.document.Calendar.mes[self.document.Calendar.mes.selectedIndex].value,1,false);'>\r\n";
  for (var i=1; i<=12; i++) {
    selectorMes = selectorMes + "  <option value='" + i + "'";
    if (i == mesActual) selectorMes = selectorMes + " selected";
    selectorMes = selectorMes + ">" + nombresMes[i] + "</option>\r\n";
  }
  selectorMes = selectorMes + "</select>\r\n";
  return selectorMes;
}

function CreateSelectYear(anoActual) {
  var selectorAno = "";
  selectorAno = "<select name='ano' size='1' onChange='javascript:opener.SetToday(self.document.Calendar.ano[self.document.Calendar.ano.selectedIndex].value,self.document.Calendar.mes[self.document.Calendar.mes.selectedIndex].value,1,false);'>\r\n";
  for (var i=anoInicial; i<=anoFinal; i++) {
    selectorAno = selectorAno + "  <option value='" + i + "'";
    if (i == anoActual) selectorAno = selectorAno + " selected";
    selectorAno = selectorAno + ">" + i + "</option>\r\n";
  }
  selectorAno = selectorAno + "</select>";
  return selectorAno;
}

function CreateTableDay(numeroAno,numeroMes,numeroDay,SetSelection) {
  var tabla = "";
  var fechaInicio = new Date();
  fechaInicio.setYear(numeroAno);
  fechaInicio.setMonth(numeroMes-1);
  fechaInicio.setDate(1);
  ajuste = fechaInicio.getDay();
  
  tabla = tabla + "<TR bgcolor=#FFFFFF>";
  tabla = tabla + GetEmptySpace(ajuste);
	
  tabla = tabla + GetDayBefore10(numeroDay,ajuste,SetSelection);
  tabla = tabla + GetDayAfter9(numeroDay,ajuste,SetSelection,numeroAno,numeroMes);

  
  tabla = tabla + GetEmptySpace((GetTotalSquare(numeroAno,numeroMes)-ajuste-diasDelMes(numeroAno,numeroMes)));
  tabla = tabla + "\r\n  </TR>\r\n";

  return tabla;
}
function GetTotalSquare(numeroAno,numeroMes)
{
    var totalSquare = 35;

	if(ajuste+diasDelMes(numeroAno,numeroMes)>=36)
	{
		totalSquare = 42;
		
	}
	return totalSquare;
}
function GetEmptySpace(ajuste)
{
 var tabla1="";

  for (var j=1; j<=ajuste; j++) 
  {
    tabla1 = tabla1 + "<TD style=\"WIDTH: 14%; COLOR: #999999\" align=\"middle\"></TD>";

  }
  return tabla1;
}
function GetDayAfter9(numeroDay,ajuste,SetSelection,numeroAno,numeroMes)
{
	var tabla1 ="";
	for (var i=10; i<=diasDelMes(numeroAno,numeroMes); i++)
	{
		tabla1 = tabla1 + "<TD "; 

		if ((i == numeroDay) ) 
			tabla1 = tabla1 + SELECTDAYSTYLE;
		
		tabla1 = tabla1 + REGULARSTYLE +"<A style=\"COLOR: #003399\" href=\"javascript:opener.ano=self.document.Calendar.ano[self.document.Calendar.ano.selectedIndex].value; opener.mes=self.document.Calendar.mes[self.document.Calendar.mes.selectedIndex].value; opener.dia=" + i + "; opener.escribirFecha(); self.close();\">"+ i + "</A></TD>\r\n";

		if (((i+ajuste) % 7)==0) 
			tabla1 = tabla1 + "\r\n  </TR>\r\n\  <tr bgcolor=#FFFFFF>";
	}
	
	return tabla1;
}
function GetDayBefore10(numeroDay,ajuste,SetSelection)
{
	var tabla1 = "";
	
	for (var i=1; i<10; i++)
	{
		tabla1 = tabla1 + "<TD "; 

		if ((i == numeroDay && SetSelection) ) 
			tabla1 = tabla1 + SELECTDAYSTYLE;
		
		tabla1 = tabla1 + REGULARSTYLE +"<A style=\"COLOR: #003399\" href=\"javascript:opener.ano=self.document.Calendar.ano[self.document.Calendar.ano.selectedIndex].value; opener.mes=self.document.Calendar.mes[self.document.Calendar.mes.selectedIndex].value; opener.dia=" + i + "; opener.escribirFecha(); self.close();\">0"+ i + "</A></TD>\r\n";

	if (((i+ajuste) % 7)==0) 
			tabla1 = tabla1 + "\r\n  </TR>\r\n\  <tr bgcolor=#FFFFFF>";
	}
	

	return tabla1;
}
function SetToday(numeroAno,numeroMes,numeroDay,SetSelection) {
 var html = "<HTML>\r\n<HEAD>\r\n<title>"+titulo+"</title>\r\n</HEAD>\r\n<body bottomMargin=\"0\" leftMargin=\"0\" topMargin=\"0\" rightMargin=\"0\">\r\n<form name=\"Calendar\" id=\"Calendar\">\r\n<TABLE id=\"Cal_CollDay\" style=\"BORDER-RIGHT: #993300 1px solid; BORDER-TOP: #993300 1px solid; FONT-SIZE: 8pt; BORDER-LEFT: #993300 1px solid; WIDTH: 220px; COLOR: #003399; BORDER-BOTTOM: #993300 1px solid; FONT-FAMILY: Verdana; BORDER-COLLAPSE: collapse; HEIGHT: 200px; BACKGROUND-COLOR: #993300\" borderColor=\"#993300\" cellSpacing=\"1\" cellPadding=\"1\" border=\"0\">\r\n<TR>\r\n<TD style=\"BORDER-RIGHT: #993300 1px solid; BORDER-TOP: #993300 1px solid; BORDER-LEFT: #993300 1px solid; BORDER-BOTTOM: #993300 1px solid; HEIGHT: 25px; BACKGROUND-COLOR: #993300\" colSpan=\"7\">\r\n<TABLE cellspacing=0 id=\"Table10\" style=\"FONT-WEIGHT: bold; FONT-SIZE: 10pt; WIDTH: 100%; COLOR: white; FONT-FAMILY: Verdana; BORDER-COLLAPSE: collapse\"  border=\"0\">\r\n<TR>\r\n<TD style=\"FONT-SIZE: 8pt; WIDTH: 15%; COLOR: white\">\r\n</TD>\r\n<TD style=\"WIDTH: 70%\" align=\"middle\">";
 html = html + "<table><tr><td>";
 html = html + CreateSelectMonth(numeroMes);
 html = html + "</td>&nbsp;&nbsp;<td>";
 html = html + CreateSelectYear(numeroAno);
 html = html + "</table></tr></td>";
 html = html + "</TD>\r\n<TD style=\"FONT-SIZE: 8pt; WIDTH: 15%; COLOR: white\" align=\"right\"></TD></TR></TABLE></td></tr><TR>";
 html = html + GetDayHeader();
 html = html + "\n</TR>";
 html = html + CreateTableDay(numeroAno,numeroMes,numeroDay,SetSelection);
  					  
  
  html = html + "</TABLE></form></body></HTML>";
  ventana = open("","calendario","width=220,height=200");
  ventana.document.open();
  ventana.document.writeln(html);
  ventana.document.close();
  ventana.focus();
}

function GetDayHeader()
{
 var html1 = "\n<TD " +DAYSHEADERTYLE+ "  align=\"middle\">S</TD>";
 html1 = html1 + "\n<TD " +DAYSHEADERTYLE+ "  align=\"middle\">M</TD>";
 html1 = html1 + "\n<TD " +DAYSHEADERTYLE+ "  align=\"middle\">T</TD>";
 html1 = html1 + "\n<TD " +DAYSHEADERTYLE+ "  align=\"middle\">W</TD>";
 html1 = html1 + "\n<TD " +DAYSHEADERTYLE+ " align=\"middle\">T</TD>";
 html1 = html1 + "\n<TD " +DAYSHEADERTYLE+ "  align=\"middle\">F</TD>";
 html1 = html1 + "\n<TD " +DAYSHEADERTYLE+ "  align=\"middle\">S</TD>";
 return html1;
}
function anoHoy() {
  var fecha = new Date();
  if (navigator.appName == "Netscape") 
	return fecha.getYear() + 1900
  else
  { 
	return fecha.getYear();
  }
}

function mesHoy() {
  var fecha = new Date();
  return fecha.getMonth()+1;
}

function diaHoy() {
  var fecha = new Date();
  return fecha.getDate();
}

function pedirFecha(campoTexto,nombreCampo) {
  ano =  anoHoy();
  mes = mesHoy();
  dia = diaHoy();
  campoDeRetorno = campoTexto;
  titulo = nombreCampo;
  SetToday(ano,mes,dia,true);

}



function Calendar(campoTexto,nombreCampo,DateToSetCalendar) {
  
  if(Date.parse(DateToSetCalendar) == Date.parse(DateToSetCalendar))
  {
	
	DateToset = new Date();
	DateToset.setTime(Date.parse(DateToSetCalendar));
	
  }
  
  else
  {
	 curyear =  anoHoy();
	 curmonth = mesHoy();
	 curday = diaHoy();
	 DateToset = new Date();
     DateToset.setYear(curyear);
     DateToset.setMonth(curmonth);
     DateToset.setDate(curday);
	
  }
   
  ano = GetYear();
  mes = GetMonth();
  dia = GetDay();
  campoDeRetorno = campoTexto;
  titulo = nombreCampo;
  SetToday(ano,mes,dia,true);
  
}
  
function escribirFecha() {
  campoDeRetorno.value = mes + "/" + dia + "/" + ano;
	if(window.document.forms[0].hidChange != "undefined") {
			window.document.forms[0].hidChange.value = 1;
				 			
		}
}
function GetMonth()
{	
	return DateToset.getMonth()+1;
}
function GetYear()
{
  if (navigator.appName == "Netscape") 
	 return DateToset.date() + 1900
   else 
   {
	return DateToset.getYear();
	}
}
function GetDay()
{	
	return DateToset.getDate();
}


  function addZero(vNumber){ 
    return ((vNumber < 10) ? "0" : "") + vNumber 
  } 
        
  function formatDate(vDate, vFormat){ 
    var vDay                      = addZero(vDate.getDate()); 
    var vMonth            = addZero(vDate.getMonth()+1); 
    var vYearLong         = addZero(vDate.getFullYear()); 
    var vYearShort        = addZero(vDate.getFullYear().toString().substring(3,4)); 
    var vYear             = (vFormat.indexOf("yyyy")>-1?vYearLong:vYearShort) 
    var vHour             = addZero(vDate.getHours()); 
    var vMinute           = addZero(vDate.getMinutes()); 
    var vSecond           = addZero(vDate.getSeconds()); 
    var vDateString       = vFormat.replace(/dd/g, vDay).replace(/MM/g, vMonth).replace(/y{1,4}/g, vYear) 
    vDateString           = vDateString.replace(/hh/g, vHour).replace(/mm/g, vMinute).replace(/ss/g, vSecond) 
    return vDateString 
  } 

</script>