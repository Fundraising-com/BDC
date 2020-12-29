//
//	August 1, 2005		Louis Turmel	Class implementation
//

 
using System;
using System.Collections;

namespace GA.BDC.Core.Utilities.Images
{

	public class ColorsHelper {
	
		public ColorsHelper() {
                
        }

		public static string GetInvertColorObject(string hexColorValue) {
			if(hexColorValue != null) {
				string colorValue = hexColorValue.Replace("#","");
				string r, g, b;
				r = colorValue.Substring(0,2);
				g = colorValue.Substring(2,2);
				b = colorValue.Substring(4,2);
				int rI, gI, bI;
				rI = GetIntFromHex(r);
				gI = GetIntFromHex(g);
				bI = GetIntFromHex(b);
				
				rI = GetInvertNumber(rI, 255);
				gI = GetInvertNumber(gI, 255);
				bI = GetInvertNumber(bI, 255);
				
				r = NumToHex(rI);
				g = NumToHex(gI);
				b = NumToHex(bI);
				if(r.Length < 2)
					r = "0" + r;
				if(g.Length < 2)
					g = "0" + g;
				if(b.Length < 2)
					b = "0" + b;

				return "#" + r + g + b;
			} else
				return "#ffffff";
		}

        public static string Invert(System.Drawing.Color color) {
            System.Drawing.Color c = new System.Drawing.Color();
           	
			return string.Empty;
        }

        public static ArrayList GenerateColorList(int NumberOfDesiredColors) {
            ArrayList oColors = new ArrayList();
            double oMax = Math.Pow(256,3);
            double oC = oMax / (double)NumberOfDesiredColors;
            double oIncrement = 256;//Math.Round(Math.Pow(oC, (double)1/(double)3), 6);
            for(double r=0;r<256;r+=oIncrement) {
            	for(double g=0;g<256;g+=oIncrement) {
                        for(double b=0;b<256;b+=oIncrement) {                                       
                                oColors.Add("#" + GetHex((int)r) + GetHex((int)g) + GetHex((int)b));
                        }
                }
            }
            oColors.Sort();                   
            return oColors;
        }

		//--ok
		public static int GetIntFromHex(string hexValue) {
			return Int32.Parse(hexValue,System.Globalization.NumberStyles.HexNumber ); 	
		}

        private static string GetHex(int value) {
            string oValue = NumToHex(value);
            if(oValue.Length == 1)
                oValue = "0" + oValue;
            return oValue;
        }

        private static string GetHexa(int value) {
            string oValue = NumToHex(value);
            if(oValue.Length < 6) {
                while(oValue.Length < 6)
                        oValue = "0" + oValue;
            }
            return oValue;
        }

        public static string NumToHex(int value) {           
            string oHex = string.Empty;
            if(value >= 16)
                oHex += NumToHex(value / 16);
            oHex += GetHexChar(value % 16);
            return oHex;
        }

        private static int GetInvertNumber(int value, int rangeLength) {
       		return rangeLength - value;
        }

        private static string GetHexChar(int value) {
        	string oValue = value.ToString();
        	switch(value) {
            	case 10:
                    	oValue = "A";
                    	break;
            	case 11:
                    	oValue = "B";
                    	break;
            	case 12:
                    	oValue = "C";
                    	break;
            	case 13:
                    	oValue = "D";
                    	break;
            	case 14:
                    	oValue = "E";
                    	break;
            	case 15:
                    	oValue = "F";
                    	break;
        	}
        	return oValue;
        }

		private static int GetIntFromHexChar(char hexValue) 
		{
			int intValue = 0;
			switch(hexValue.ToString().ToLower()) 
			{
				case "a":
					intValue = 10;
					break;
				case "b":
					intValue = 11;
					break;
				case "c":
					intValue = 12;
					break;
				case "d":
					intValue = 13;
					break;
				case "e":
					intValue = 14;
					break;
				case "f":
					intValue = 15;
					break;
				default:
					intValue = int.Parse(hexValue.ToString());
					break;
			}
			return intValue;
		}
	}
}
