
<!-- 
// Définition des variables globale. 
var objNum=0; // Numéros de l'objet courrant 
var cursorPosXStart,cursorPosYStart; // Position du curseur au départ du déplacement 
var objPosLeftStart,objPosTopStart; // Position de l'objet au départ du déplacement 
  
function init() 
{ // démarrage appelé dans la ligne body 
     
    // capture d'événènements appuyer, relacher 
    if (document.captureEvents) 
    { // pour Netscape 4 
        document.captureEvents(Event.MOUSEDOWN | Event.MOUSEUP); 
    } 
    document.onmousedown = startDrag; 
    document.onmouseup = endDrag; 
} 
  
  
function startDrag(e) 
{ 
    objNum= whichObj(e); // recherche l'objet selectionné 
     
    if(objNum!=null) // Si l'objet est trouvé 
    { 
     // enregistrement de la position du curseur ; Si (netscape) ? vrai : faux; 
     cursorPosXStart = (navigator.appName.substring(0,3) == "Net") ? e.pageX : event.clientX; 
        cursorPosYStart = (navigator.appName.substring(0,3) == "Net") ? e.pageY : event.clientY; 
  
        // enregistrement de la position de l'image 
         objPosLeftStart=parseInt(document.images[objNum].style.left); 
     objPosTopStart=parseInt(document.images[objNum].style.top); 
  
        // capture de l'événènement déplacement du curseur 
         if (document.captureEvents) 
        { // pour Netscape 4 
            document.captureEvents(Event.MOUSEMOVE); 
        } 
         document.onmousemove= moveIt; 
    } 
    return false; // retourne faux pour que l'explorateur ne tienne pas compte du déplacement 
} 
  
  
function moveIt(e) 
{ 
    // enregistrement de la position du curseur ; Si (netscape) ? vrai : faux; 
    var cursorPosX = (navigator.appName.substring(0,3) == "Net") ? e.pageX : event.clientX; 
    var cursorPosY = (navigator.appName.substring(0,3) == "Net") ? e.pageY : event.clientY; 
  
    // déplacement de l'image 
    document.images[objNum].style.left=objPosLeftStart+cursorPosX-cursorPosXStart; 
    document.images[objNum].style.top=objPosTopStart+cursorPosY-cursorPosYStart; 
     
    return false; 
} 
  
  
function endDrag(e) 
{ 
    // suppression du numéros de l'image 
    objNum = null; 
     
    // Arret de la capture de l'événènement déplacement du curseur 
       if (document.captureEvents) 
    { // pour Netscape 4 
        document.releaseEvents(Event.MOUSEMOVE); 
    } 
       document.onmousemove= ""; 
  
} 
  
  
  
function whichObj(e) 
{ 
    n=0; 
    // enregistrement de la position du curseur ; Si (netscape) ? vrai : faux; 
    var cursorPosX = (navigator.appName.substring(0,3) == "Net") ? e.pageX : event.clientX; 
    var cursorPosY = (navigator.appName.substring(0,3) == "Net") ? e.pageY : event.clientY; 
  
    while (document.images[n]) 
    { // tant que l'image existe 
  
        // Initialise des attributs de l'image trouver 
        document.images[n].style.position="absolute"; 
        document.images[n].style.zIndex=1000-n; 
        document.images[n].style.cursor="default"; 
         
        if(document.images[n].style.left=="" || document.images[n].style.left==null) 
        { // Si la position de l'image n'est pas définit, on le fait 
            document.images[n].style.left=0; 
            document.images[n].style.top=0; 
  
        } 
  
        // enregistrement de la position de l'image 
        objPosLeft=parseInt(document.images[n].style.left); 
        objPosTop=parseInt(document.images[n].style.top); 
  
        // position de l'objet sur lequel il est cliqué 
        if ((cursorPosX > objPosLeft) && 
        (cursorPosX < objPosLeft + document.images[n].width) && 
        (cursorPosY > objPosTop) && 
        (cursorPosY < objPosTop + document.images[n].height)) 
        { // Si le curseur est dans la zone d'affichage de l'image 
            return n; 
        } 
        else n++; 
    } 
    return null; 
} 

init();
  
// --> 
