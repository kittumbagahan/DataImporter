
using System.Collections;

//public enum ETiltDirection { left, right }
//public enum Direction { up, down, left, right }
public abstract class Storybook{

    // non, red, green, blue, purple, orange, yellow, pink, teal, white, black, brown, cyan
    public static EColor GetColor(int index)
    {
        switch (index)
        {
            case 0:
                return EColor.non;
            
            case 1:
                return EColor.red;
              
            case 2:
                return EColor.green;
              
            case 3:
                return EColor.blue;
              
            case 4:
                return EColor.purple;
              
            case 5:
                return EColor.orange;
               
            case 6:
                return EColor.yellow;
               
            case 7:
                return EColor.pink;
               
            case 8:
                return EColor.teal;
               
            case 9:
                return EColor.white;
               
            case 10:
                return EColor.black;
               
            case 11:
                return EColor.brown;
               
            case 12:
                return EColor.cyan;
              
            default:
                return EColor.non;
                
        }
    }

    public static EAlphabet SetEnumAlphabetValue(char c)
    {
        switch (c.ToString().ToLower())
        {
            case "a": return EAlphabet.a;
            case "b": return EAlphabet.b;
            case "c": return EAlphabet.c;
            case "d": return EAlphabet.d;
            case "e": return EAlphabet.e;
            case "f": return EAlphabet.f;
            case "g": return EAlphabet.g;
            case "h": return EAlphabet.h;
            case "i": return EAlphabet.i;
            case "j": return EAlphabet.j;
            case "k": return EAlphabet.k;
            case "l": return EAlphabet.l;
            case "m": return EAlphabet.m;
            case "n": return EAlphabet.n;
            case "o": return EAlphabet.o;
            case "p": return EAlphabet.p;
            case "q": return EAlphabet.q;
            case "r": return EAlphabet.r;
            case "s": return EAlphabet.s;
            case "t": return EAlphabet.t;
            case "u": return EAlphabet.u;
            case "v": return EAlphabet.v;
            case "w": return EAlphabet.w;
            case "x": return EAlphabet.x;
            case "y": return EAlphabet.y;
            case "z": return EAlphabet.z;

            default: return EAlphabet.non;
        }
    }
}
