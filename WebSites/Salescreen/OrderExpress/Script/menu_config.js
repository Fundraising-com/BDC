
	var NoOffFirstLineMenus=9;			// Number of first level itemsCOLOR=
	var LowBgColor='';					// Background color when mouse is not over
	var LowSubBgColor='336699';			// Background color when mouse is not over on subs
	var HighBgColor='';					// Background color when mouse is over
	var HighSubBgColor='336699';		// Background color when mouse is over on subs
	var FontLowColor='FFFFFF';			// Font color when mouse is not over
	var FontSubLowColor='99330B';		// Font color subs when mouse is not over
	var FontHighColor='FDBF6A';			// Font color when mouse is over
	var FontSubHighColor='C96700';		// Font color subs when mouse is over
	var BorderColor='';					// Border color
	var BorderSubColor='99330B';		// Border color for subs
	var BorderWidth=1;					// Border width
	var BorderBtwnElmnts=1;				// Border between elements 1 or 0
	var FontFamily="arial, tahoma"		// Font family menu items
	var FontSize=8;						// Font size menu items
	var FontBold=1;						// Bold menu items 1 or 0
	var FontItalic=0;					// Italic menu items 1 or 0
	var MenuTextCentered='left';		// Item text position 'left', 'center' or 'right'
	var MenuCentered='left';			// Menu horizontal position 'left', 'center' or 'right'
	var MenuVerticalCentered='top';		// Menu vertical position 'top', 'middle','bottom' or static
	var ChildOverlap=0;					// horizontal overlap child/ parent
	var ChildVerticalOverlap=0;			// vertical overlap child/ parent
	var StartTop=25;					// Menu offset x coordinate
	var StartLeft=99;					// Menu offset y coordinate
	var VerCorrect=0;					// Multiple frames y correction
	var HorCorrect=0;					// Multiple frames x correction
	var LeftPaddng=3;					// Left padding
	var TopPaddng=2;					// Top padding
	var FirstLineHorizontal=1;			// SET TO 1 FOR HORIZONTAL MENU, 0 FOR VERTICAL
	var MenuFramesVertical=1;			// Frames in cols or rows 1 or 0
	var DissapearDelay=1000;			// delay before menu folds in
	var TakeOverBgColor=1;				// Menu frame takes over background color subitem frame
	var FirstLineFrame='navig';			// Frame where first level appears
	var SecLineFrame='space';			// Frame where sub levels appear
	var DocTargetFrame='space';			// Frame where target documents appear
	var TargetLoc='';					// span id for relative positioning
	var HideTop=0;						// Hide first level when loading new document 1 or 0
	var MenuWrap=0;						// enables/ disables menu wrap 1 or 0
	var RightToLeft=0;					// enables/ disables right to left unfold 1 or 0
	var UnfoldsOnClick=0;				// Level 1 unfolds onclick/ onmouseover
	var WebMasterCheck=0;				// menu tree checking on or off 1 or 0
	var ShowArrow=0;					// Uses arrow gifs when 1
	var KeepHilite=1;					// Keep selected path highligthed
	var Arrws=['tri.gif',5,10,'tridown.gif',10,5,'trileft.gif',5,10];	// Arrow source, width and height

function BeforeStart(){return}
function AfterBuild(){return}
function BeforeFirstOpen(){return}
function AfterCloseAll(){return}


// Menu tree
//	MenuX=new Array(Text to show, Link, background image (optional), number of sub elements, height, width);
//	For rollover images set "Text to show" to:  "rollover:Image1.jpg:Image2.jpg"

Menu1=new Array("Home","javascript:MenuPostBack(2);","",0,20,60);

Menu2=new Array("Setup Account","#","",4,20,117);
	Menu2_1=new Array("Step 1: Campaign information","javascript:MenuPostBack(3);","",0,35,117);	
	Menu2_2=new Array("Step 2: Collection days","javascript:MenuPostBack(4);","",0, 35, 117);
	Menu2_3=new Array("Step 3: Import participants","javascript:MenuPostBack(5);","",0, 35, 117);
	Menu2_4=new Array("Step 4: Prizes","javascript:MenuPostBack(6);","",0, 20, 117);
	//Menu2_5=new Array("step 5: bonus prizes","javascript:MenuPostBack(7);","",0, 40, 117);
if (displayAdvMenu=='1') {
	Menu3=new Array("Data Entry","#","",5,20,90);
		Menu3_1=new Array("Participant Sales","javascript:MenuPostBack(10);","",0,20,117);
		Menu3_2=new Array("Inventory","javascript:MenuPostBack(52);","",0,20,117);
		Menu3_3=new Array("Staff Sales","javascript:MenuPostBack(32);","",0);
		Menu3_4=new Array("Quota","javascript:MenuPostBack(35);","",0);
		Menu3_5=new Array("Magnet","javascript:MenuPostBack(46);","",0);
} else {
	Menu3=new Array("","#","",0);
}

if (displayAdvMenu=='1') {
	Menu4=new Array("Search/Edit","#","",6,20,90);
		Menu4_1=new Array("Participants","javascript:MenuPostBack(8);","",0,20,117);
		Menu4_2=new Array("Participant Schedule","javascript:MenuPostBack(36);","",0);
		Menu4_3=new Array("Teachers","javascript:MenuPostBack(9);","",0);
		Menu4_4=new Array("Staff","javascript:MenuPostBack(31);","",0);
		Menu4_5=new Array("Periods","javascript:MenuPostBack(34);","",0);
		Menu4_6=new Array("Campaign Product","javascript:MenuPostBack(48);","",0);
} else {
	Menu4=new Array("","#","",0);
}

if (displayAdvMenu=='1') {
	Menu5=new Array("Reporting","#","",20,20,92);
		Menu5_1=new Array("Accounting form","javascript:MenuPostBack(11);","",0,20,170);
		Menu5_2=new Array("Inventory form","javascript:MenuPostBack(53);","",0,20);
		Menu5_3=new Array("Master School Summary","javascript:MenuPostBack(12);","",0,20);		
		Menu5_4=new Array("Top seller","javascript:MenuPostBack(14);","",0,20);
		Menu5_5=new Array("Top classroom","javascript:MenuPostBack(15);","",0,20);
		Menu5_6=new Array("Staff order","javascript:MenuPostBack(16);","",0,20);
		Menu5_7=new Array("Prizes list","javascript:MenuPostBack(17);","",0,20);
		Menu5_8=new Array("Prizes tickets","javascript:MenuPostBack(18);","",0,20);
		Menu5_9=new Array("Blank Prizes tickets","javascript:MenuPostBack(43);","",0,20);
		Menu5_10=new Array("Premium prizes list","javascript:MenuPostBack(19);","",0,20);
		Menu5_11=new Array("Premium prizes tickets","javascript:MenuPostBack(20);","",0,20);
		Menu5_12=new Array("Blank Premium prizes tickets","javascript:MenuPostBack(44);","",0,20);
		Menu5_13=new Array("Print permission slip","javascript:MenuPostBack(21);","",0,20);
		Menu5_14=new Array("Events list","javascript:MenuPostBack(22);","",0,20);
		Menu5_15=new Array("Events tickets","javascript:MenuPostBack(23);","",0,20);
		Menu5_16=new Array("Quota Letters","javascript:MenuPostBack(41);","",0,20);
		Menu5_17=new Array("Quota Report","javascript:MenuPostBack(42);","",0,20);
		Menu5_18=new Array("Prizes Inventory","javascript:MenuPostBack(45);","",0,20);
		Menu5_19=new Array("MagNet List","javascript:MenuPostBack(50);","",0,20);
		Menu5_20=new Array("Prizes Summary Tickets","javascript:MenuPostBack(54);","",0,20);
		
} else {
	Menu5=new Array("","#","",0);
}

if (QSPFormRole =='1') 
{
	Menu6=new Array("Admin","#","",6,20,90);
		Menu6_1=new Array("Campaigns Selection","javascript:MenuPostBack(33);","",0,20,120);
		Menu6_2=new Array("Campaign Creation","javascript:MenuPostBack(39);","",0, 20);
		Menu6_3=new Array("Users","javascript:MenuPostBack(38)","",0,20);
		Menu6_4=new Array("Prizes","javascript:MenuPostBack(37);","",0, 20);
		Menu6_5=new Array("FM Products","javascript:MenuPostBack(49);","",0, 20);
		Menu6_6=new Array("Data Administration","javascript:MenuPostBack(47);","",0, 20);	
} else if (QSPFormRole>='2') {
	Menu6=new Array("Admin","#","",7,20,90);
		Menu6_1=new Array("Campaigns Selection","javascript:MenuPostBack(33);","",0,20,120);
		Menu6_2=new Array("Campaign Creation","javascript:MenuPostBack(39);","",0, 20);
		Menu6_3=new Array("Users","javascript:MenuPostBack(38)","",0,20);
		Menu6_4=new Array("Prizes","javascript:MenuPostBack(37);","",0, 20);
		Menu6_5=new Array("FM Products","javascript:MenuPostBack(49);","",0, 20);
		Menu6_6=new Array("QSP Products","javascript:MenuPostBack(51);","",0, 20);
		Menu6_7=new Array("Data Administration","javascript:MenuPostBack(47);","",0, 20);	
} else {
	Menu6=new Array("Campaign Selection","javascript:MenuPostBack(33)","",0,20,130);
}

Menu7=new Array("Contact Us","mailto:qsp_IT@custhelp.com","",0,20,88);

if (QSPFormRole>='1') 
{                      
	Menu8=new Array("Help","javascript:window.open('helpfile/QSPForm_FM_help.pdf')","",0,20,52);
} else {
	Menu8=new Array("Help","javascript:window.open('helpfile/QSPForm_help.pdf')","",0,20,52);
}

Menu9=new Array("Sign Out","logout.aspx","",0,20,70);