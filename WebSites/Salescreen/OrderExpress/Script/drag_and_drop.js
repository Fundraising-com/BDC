
<!-- 
// On peux d�placer tous les �l�ments juste changeant sont id. 
  
  
// D�finition des variables globale. 
var objNum=0; // Num�ros de l'objet courrant 
var cursorPosXStart,cursorPosYStart; // Position du curseur au d�part du d�placement 
var objPosLeftStart,objPosTopStart; // Position de l'objet au d�part du d�placement 
  
function DragAndDropInit() 
{ // d�marrage appel� dans la ligne body 
     
    // capture d'�v�n�nements appuyer, relacher 
    if (document.captureEvents) 
    { // pour Netscape 4 
        document.captureEvents(Event.MOUSEDOWN | Event.MOUSEUP); 
    } 
    document.onmousedown = startDrag; 
    document.onmouseup = endDrag; 
    //alert('adsdsa');
} 
  
  
function startDrag(e) 
{ 
    //alert('startDrag');
    objNum= whichObj(e); // recherche l'objet selectionn� 
    
    if(objNum!=null) // Si l'objet est trouv� 
    { 
         // enregistrement de la position du curseur ; Si (netscape) ? vrai : faux; 
         cursorPosXStart = (navigator.appName.substring(0,3) == "Net") ? e.pageX : event.clientX; 
         cursorPosYStart = (navigator.appName.substring(0,3) == "Net") ? e.pageY : event.clientY; 
  
         // enregistrement de la position de l'objet 
         objPosLeftStart=parseInt(document.getElementById(objNum).style.left); 
         objPosTopStart=parseInt(document.getElementById(objNum).style.top); 
  
        // capture de l'�v�n�nement d�placement du curseur 
         if (document.captureEvents) 
        { // pour Netscape 4 
            document.captureEvents(Event.MOUSEMOVE); 
        } 
         document.onmousemove= moveIt; 
    } 
    return false; // retourne faux pour que l'explorateur ne tienne pas compte du d�placement 
} 
  
  
function moveIt(e) 
{ 
    // enregistrement de la position du curseur ; Si (netscape) ? vrai : faux; 
    var cursorPosX = (navigator.appName.substring(0,3) == "Net") ? e.pageX : event.clientX; 
    var cursorPosY = (navigator.appName.substring(0,3) == "Net") ? e.pageY : event.clientY; 
  
    // d�placement de l'objet courrant 
    document.getElementById(objNum).style.left=objPosLeftStart+cursorPosX-cursorPosXStart; 
    document.getElementById(objNum).style.top=objPosTopStart+cursorPosY-cursorPosYStart; 
     
    return false; 
} 
  
  
function endDrag(e) 
{ 
    // suppression du num�ros de l'objet courrant 
    objNum = null; 
  
    // Arret de la capture de l'�v�n�nement d�placement du curseur 
       if (document.captureEvents) 
    { // pour Netscape 4 
        document.releaseEvents(Event.MOUSEMOVE); 
    } 
       document.onmousemove= ""; 
  
} 
  
  
function whichObj(e) 
{ 
    n=0; 
    var id="tvBusinessRule"; // debut du nom des IDs des �l�ments qui doivent bouger, vous pouvez le changer. 
     
    // enregistrement de la position du curseur ; Si (netscape) ? vrai : faux; 
    var cursorPosX = (navigator.appName.substring(0,3) == "Net") ? e.pageX : event.clientX; 
    var cursorPosY = (navigator.appName.substring(0,3) == "Net") ? e.pageY : event.clientY; 
    
    for (n=1; n < document.all.length; n++){
    
    //while (document.getElementById(id+n)) 
    //{ // tant que l'ID existe ("move0","move1","move2",...) 
        id = document.all[n].id;
        if(id.length != '')
        {
            var oCtl = document.getElementById(id);
            if ((oCtl.tagName != 'body') && (oCtl.tagName.toLowerCase() != 'form'))
            {
                // Initialise des attributs de l'objet trouver 
                document.getElementById(id).style.position="absolute"; 
                document.getElementById(id).style.zIndex=1000-n; 
                document.getElementById(id).style.cursor="default"; 
          
                if(document.getElementById(id).style.left=="" || document.getElementById(id).style.left==null) 
                { // Si la position de l'objet n'est pas d�finit, on le fait 
                    document.getElementById(id).style.left=0; 
                    document.getElementById(id).style.top=0; 
                } 
                 
                // enregistrement de la position de l'objet 
                objPosLeft=parseInt(document.getElementById(id).style.left); 
                objPosTop=parseInt(document.getElementById(id).style.top); 
          
                if ((cursorPosX > objPosLeft) && 
                (cursorPosX < objPosLeft + document.getElementById(id).offsetWidth) && 
                (cursorPosY > objPosTop) && 
                (cursorPosY < objPosTop + document.getElementById(id).offsetHeight)) 
                { // Si le curseur est dans la zone d'affichage 
                    alert('id=' + id + ';tagName=' + oCtl.tagName);
                    return id; 
                } 
            
            
            }
            
            //else n++; 
        }
        
    }
     
    return null; 
} 

// --> 
