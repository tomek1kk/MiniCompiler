// This code was generated by the Gardens Point Parser Generator
// Copyright (c) Wayne Kelly, John Gough, QUT 2005-2014
// (see accompanying GPPGcopyright.rtf)

// GPPG version 1.5.2
// Machine:  DESKTOP-EC4UU67
// DateTime: 11.06.2020 13:16:13
// UserName: tomek
// Input file <kompilator.y - 11.06.2020 13:16:10>

// options: lines gplex

using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Text;
using QUT.Gppg;

namespace GardensPoint
{
public enum Tokens {error=2,EOF=3,Assign=4,Plus=5,Minus=6,
    Multiplies=7,Divides=8,Program=9,Return=10,Eof=11,Error=12,
    If=13,Else=14,While=15,Read=16,Write=17,Int=18,
    Double=19,Bool=20,True=21,False=22,OpenBracket=23,CloseBracket=24,
    Semicolon=25,OpenPar=26,ClosePar=27,Equal=28,NotEqual=29,Greater=30,
    GreaterEqual=31,Less=32,LessEqual=33,Ident=34,IntNumber=35,RealNumber=36};

public struct ValueType
#line 7 "kompilator.y"
{
public string  val;
public char    type;
}
#line default
// Abstract base class for GPLEX scanners
[GeneratedCodeAttribute( "Gardens Point Parser Generator", "1.5.2")]
public abstract class ScanBase : AbstractScanner<ValueType,LexLocation> {
  private LexLocation __yylloc = new LexLocation();
  public override LexLocation yylloc { get { return __yylloc; } set { __yylloc = value; } }
  protected virtual bool yywrap() { return true; }
}

// Utility class for encapsulating token information
[GeneratedCodeAttribute( "Gardens Point Parser Generator", "1.5.2")]
public class ScanObj {
  public int token;
  public ValueType yylval;
  public LexLocation yylloc;
  public ScanObj( int t, ValueType val, LexLocation loc ) {
    this.token = t; this.yylval = val; this.yylloc = loc;
  }
}

[GeneratedCodeAttribute( "Gardens Point Parser Generator", "1.5.2")]
public class Parser: ShiftReduceParser<ValueType, LexLocation>
{
#pragma warning disable 649
  private static Dictionary<int, string> aliases;
#pragma warning restore 649
  private static Rule[] rules = new Rule[48];
  private static State[] states = new State[87];
  private static string[] nonTerms = new string[] {
      "code", "stat", "exp", "term", "factor", "declare", "bool", "cond", "while", 
      "start", "$accept", "Anon@1", "write", "assign", "block", "ifelse", "if", 
      "Anon@2", "Anon@3", "ifhead", "Anon@4", "Anon@5", };

  static Parser() {
    states[0] = new State(new int[]{9,3},new int[]{-10,1});
    states[1] = new State(new int[]{3,2});
    states[2] = new State(-1);
    states[3] = new State(new int[]{23,4});
    states[4] = new State(new int[]{17,11,34,32,18,37,19,40,20,43,15,47,23,55,13,65,2,84},new int[]{-1,5,-2,86,-13,10,-14,31,-6,36,-9,46,-15,54,-8,58,-16,59,-20,60,-17,83});
    states[5] = new State(new int[]{24,6,17,11,34,32,18,37,19,40,20,43,15,47,23,55,13,65,2,84},new int[]{-2,9,-13,10,-14,31,-6,36,-9,46,-15,54,-8,58,-16,59,-20,60,-17,83});
    states[6] = new State(-2,new int[]{-12,7});
    states[7] = new State(new int[]{11,8});
    states[8] = new State(-3);
    states[9] = new State(-4);
    states[10] = new State(-6);
    states[11] = new State(-35,new int[]{-22,12});
    states[12] = new State(new int[]{26,19,35,26,36,27,34,28},new int[]{-3,13,-4,30,-5,29});
    states[13] = new State(new int[]{25,14,5,15,6,22});
    states[14] = new State(-36);
    states[15] = new State(new int[]{26,19,35,26,36,27,34,28},new int[]{-4,16,-5,29});
    states[16] = new State(new int[]{7,17,8,24,25,-38,5,-38,6,-38,27,-38,28,-38,29,-38,30,-38,31,-38,32,-38,33,-38});
    states[17] = new State(new int[]{26,19,35,26,36,27,34,28},new int[]{-5,18});
    states[18] = new State(-41);
    states[19] = new State(new int[]{26,19,35,26,36,27,34,28},new int[]{-3,20,-4,30,-5,29});
    states[20] = new State(new int[]{27,21,5,15,6,22});
    states[21] = new State(-44);
    states[22] = new State(new int[]{26,19,35,26,36,27,34,28},new int[]{-4,23,-5,29});
    states[23] = new State(new int[]{7,17,8,24,25,-39,5,-39,6,-39,27,-39,28,-39,29,-39,30,-39,31,-39,32,-39,33,-39});
    states[24] = new State(new int[]{26,19,35,26,36,27,34,28},new int[]{-5,25});
    states[25] = new State(-42);
    states[26] = new State(-45);
    states[27] = new State(-46);
    states[28] = new State(-47);
    states[29] = new State(-43);
    states[30] = new State(new int[]{7,17,8,24,25,-40,5,-40,6,-40,27,-40,28,-40,29,-40,30,-40,31,-40,32,-40,33,-40});
    states[31] = new State(-7);
    states[32] = new State(new int[]{4,33});
    states[33] = new State(new int[]{26,19,35,26,36,27,34,28},new int[]{-3,34,-4,30,-5,29});
    states[34] = new State(new int[]{25,35,5,15,6,22});
    states[35] = new State(-37);
    states[36] = new State(-8);
    states[37] = new State(new int[]{34,38});
    states[38] = new State(new int[]{25,39});
    states[39] = new State(-32);
    states[40] = new State(new int[]{34,41});
    states[41] = new State(new int[]{25,42});
    states[42] = new State(-33);
    states[43] = new State(new int[]{34,44});
    states[44] = new State(new int[]{25,45});
    states[45] = new State(-34);
    states[46] = new State(-9);
    states[47] = new State(-17,new int[]{-18,48});
    states[48] = new State(new int[]{26,49});
    states[49] = new State(new int[]{26,19,35,26,36,27,34,28,21,81,22,82},new int[]{-7,50,-3,69,-4,30,-5,29});
    states[50] = new State(new int[]{27,51});
    states[51] = new State(-18,new int[]{-19,52});
    states[52] = new State(new int[]{17,11,34,32,18,37,19,40,20,43,15,47,23,55,13,65,2,84},new int[]{-2,53,-13,10,-14,31,-6,36,-9,46,-15,54,-8,58,-16,59,-20,60,-17,83});
    states[53] = new State(-19);
    states[54] = new State(-10);
    states[55] = new State(new int[]{17,11,34,32,18,37,19,40,20,43,15,47,23,55,13,65,2,84},new int[]{-1,56,-2,86,-13,10,-14,31,-6,36,-9,46,-15,54,-8,58,-16,59,-20,60,-17,83});
    states[56] = new State(new int[]{24,57,17,11,34,32,18,37,19,40,20,43,15,47,23,55,13,65,2,84},new int[]{-2,9,-13,10,-14,31,-6,36,-9,46,-15,54,-8,58,-16,59,-20,60,-17,83});
    states[57] = new State(-14);
    states[58] = new State(-11);
    states[59] = new State(-15);
    states[60] = new State(new int[]{17,11,34,32,18,37,19,40,20,43,15,47,23,55,13,65,2,84},new int[]{-2,61,-13,10,-14,31,-6,36,-9,46,-15,54,-8,58,-16,59,-20,60,-17,83});
    states[61] = new State(new int[]{14,62,24,-21,17,-21,34,-21,18,-21,19,-21,20,-21,15,-21,23,-21,13,-21,2,-21});
    states[62] = new State(-22,new int[]{-21,63});
    states[63] = new State(new int[]{17,11,34,32,18,37,19,40,20,43,15,47,23,55,13,65,2,84},new int[]{-2,64,-13,10,-14,31,-6,36,-9,46,-15,54,-8,58,-16,59,-20,60,-17,83});
    states[64] = new State(-23);
    states[65] = new State(new int[]{26,66});
    states[66] = new State(new int[]{26,19,35,26,36,27,34,28,21,81,22,82},new int[]{-7,67,-3,69,-4,30,-5,29});
    states[67] = new State(new int[]{27,68});
    states[68] = new State(-20);
    states[69] = new State(new int[]{28,70,5,15,6,22,29,72,30,74,31,76,32,78,33,80});
    states[70] = new State(new int[]{26,19,35,26,36,27,34,28},new int[]{-3,71,-4,30,-5,29});
    states[71] = new State(new int[]{5,15,6,22,27,-24});
    states[72] = new State(new int[]{26,19,35,26,36,27,34,28},new int[]{-3,73,-4,30,-5,29});
    states[73] = new State(new int[]{5,15,6,22,27,-25});
    states[74] = new State(new int[]{26,19,35,26,36,27,34,28},new int[]{-3,75,-4,30,-5,29});
    states[75] = new State(new int[]{5,15,6,22,27,-26});
    states[76] = new State(new int[]{26,19,35,26,36,27,34,28},new int[]{-3,77,-4,30,-5,29});
    states[77] = new State(new int[]{5,15,6,22,27,-27});
    states[78] = new State(new int[]{26,19,35,26,36,27,34,28},new int[]{-3,79,-4,30,-5,29});
    states[79] = new State(new int[]{5,15,6,22,27,-28});
    states[80] = new State(-29);
    states[81] = new State(-30);
    states[82] = new State(-31);
    states[83] = new State(-16);
    states[84] = new State(new int[]{11,85,24,-12,17,-12,34,-12,18,-12,19,-12,20,-12,15,-12,23,-12,13,-12,2,-12,14,-12});
    states[85] = new State(-13);
    states[86] = new State(-5);

    for (int sNo = 0; sNo < states.Length; sNo++) states[sNo].number = sNo;

    rules[1] = new Rule(-11, new int[]{-10,3});
    rules[2] = new Rule(-12, new int[]{});
    rules[3] = new Rule(-10, new int[]{9,23,-1,24,-12,11});
    rules[4] = new Rule(-1, new int[]{-1,-2});
    rules[5] = new Rule(-1, new int[]{-2});
    rules[6] = new Rule(-2, new int[]{-13});
    rules[7] = new Rule(-2, new int[]{-14});
    rules[8] = new Rule(-2, new int[]{-6});
    rules[9] = new Rule(-2, new int[]{-9});
    rules[10] = new Rule(-2, new int[]{-15});
    rules[11] = new Rule(-2, new int[]{-8});
    rules[12] = new Rule(-2, new int[]{2});
    rules[13] = new Rule(-2, new int[]{2,11});
    rules[14] = new Rule(-15, new int[]{23,-1,24});
    rules[15] = new Rule(-8, new int[]{-16});
    rules[16] = new Rule(-8, new int[]{-17});
    rules[17] = new Rule(-18, new int[]{});
    rules[18] = new Rule(-19, new int[]{});
    rules[19] = new Rule(-9, new int[]{15,-18,26,-7,27,-19,-2});
    rules[20] = new Rule(-20, new int[]{13,26,-7,27});
    rules[21] = new Rule(-17, new int[]{-20,-2});
    rules[22] = new Rule(-21, new int[]{});
    rules[23] = new Rule(-16, new int[]{-20,-2,14,-21,-2});
    rules[24] = new Rule(-7, new int[]{-3,28,-3});
    rules[25] = new Rule(-7, new int[]{-3,29,-3});
    rules[26] = new Rule(-7, new int[]{-3,30,-3});
    rules[27] = new Rule(-7, new int[]{-3,31,-3});
    rules[28] = new Rule(-7, new int[]{-3,32,-3});
    rules[29] = new Rule(-7, new int[]{-3,33});
    rules[30] = new Rule(-7, new int[]{21});
    rules[31] = new Rule(-7, new int[]{22});
    rules[32] = new Rule(-6, new int[]{18,34,25});
    rules[33] = new Rule(-6, new int[]{19,34,25});
    rules[34] = new Rule(-6, new int[]{20,34,25});
    rules[35] = new Rule(-22, new int[]{});
    rules[36] = new Rule(-13, new int[]{17,-22,-3,25});
    rules[37] = new Rule(-14, new int[]{34,4,-3,25});
    rules[38] = new Rule(-3, new int[]{-3,5,-4});
    rules[39] = new Rule(-3, new int[]{-3,6,-4});
    rules[40] = new Rule(-3, new int[]{-4});
    rules[41] = new Rule(-4, new int[]{-4,7,-5});
    rules[42] = new Rule(-4, new int[]{-4,8,-5});
    rules[43] = new Rule(-4, new int[]{-5});
    rules[44] = new Rule(-5, new int[]{26,-3,27});
    rules[45] = new Rule(-5, new int[]{35});
    rules[46] = new Rule(-5, new int[]{36});
    rules[47] = new Rule(-5, new int[]{34});
  }

  protected override void Initialize() {
    this.InitSpecialTokens((int)Tokens.error, (int)Tokens.EOF);
    this.InitStates(states);
    this.InitRules(rules);
    this.InitNonTerminals(nonTerms);
  }

  protected override void DoAction(int action)
  {
#pragma warning disable 162, 1522
    switch (action)
    {
      case 2: // Anon@1 -> /* empty */
#line 26 "kompilator.y"
                {
               //Compiler.EmitCode("// linia {0,3} :  "+Compiler.source[lineno-1],lineno);
               Compiler.EmitCode("ldstr \"\\nEnd of execution\\n\"");
               Compiler.EmitCode("call void [mscorlib]System.Console::WriteLine(string)");
               Compiler.EmitCode("");
               YYAccept();
               }
#line default
        break;
      case 4: // code -> code, stat
#line 35 "kompilator.y"
                     { ++lineno; }
#line default
        break;
      case 5: // code -> stat
#line 36 "kompilator.y"
                 { ++lineno; }
#line default
        break;
      case 12: // stat -> error
#line 40 "kompilator.y"
               {
               Console.WriteLine("  line {0,3}:  syntax error",lineno);
               ++Compiler.errors;
               yyerrok();
               }
#line default
        break;
      case 13: // stat -> error, Eof
#line 46 "kompilator.y"
               {
               Console.WriteLine("  line {0,3}:  syntax error",lineno);
               ++Compiler.errors;
               yyerrok();
               YYAccept();
               }
#line default
        break;
      case 17: // Anon@2 -> /* empty */
#line 58 "kompilator.y"
            { 
                temp = Compiler.NewTemp();
                Compiler.EmitCode("{0}:", temp);
            }
#line default
        break;
      case 18: // Anon@3 -> /* empty */
#line 63 "kompilator.y"
            { 
                temp2 = Compiler.NewTemp();
                Compiler.EmitCode("brfalse {0}", temp2); 
            }
#line default
        break;
      case 19: // while -> While, Anon@2, OpenPar, bool, ClosePar, Anon@3, stat
#line 68 "kompilator.y"
            { 
                Compiler.EmitCode("br {0}", temp);
                Compiler.EmitCode("{0}:", temp2);
            }
#line default
        break;
      case 20: // ifhead -> If, OpenPar, bool, ClosePar
#line 74 "kompilator.y"
            {
                temp = Compiler.NewTemp();
                Compiler.EmitCode("brfalse {0}", temp);
            }
#line default
        break;
      case 21: // if -> ifhead, stat
#line 81 "kompilator.y"
            { 
                Compiler.EmitCode("{0}:", temp);
            }
#line default
        break;
      case 22: // Anon@4 -> /* empty */
#line 88 "kompilator.y"
            {
                temp2 = Compiler.NewTemp();
                Compiler.EmitCode("br {0}", temp2);
                Compiler.EmitCode("{0}:", temp);
            }
#line default
        break;
      case 23: // ifelse -> ifhead, stat, Else, Anon@4, stat
#line 94 "kompilator.y"
            {
                Compiler.EmitCode("{0}:", temp2);
            }
#line default
        break;
      case 24: // bool -> exp, Equal, exp
#line 99 "kompilator.y"
            {
                Compiler.EmitCode("ceq");
            }
#line default
        break;
      case 25: // bool -> exp, NotEqual, exp
#line 103 "kompilator.y"
            {
                Compiler.EmitCode("ceq");
                Compiler.EmitCode("neg");
            }
#line default
        break;
      case 26: // bool -> exp, Greater, exp
#line 108 "kompilator.y"
            {
                Compiler.EmitCode("cgt");
            }
#line default
        break;
      case 27: // bool -> exp, GreaterEqual, exp
#line 112 "kompilator.y"
            {
                
            }
#line default
        break;
      case 28: // bool -> exp, Less, exp
#line 116 "kompilator.y"
            {
                Compiler.EmitCode("clt");
            }
#line default
        break;
      case 29: // bool -> exp, LessEqual
#line 120 "kompilator.y"
            {
            }
#line default
        break;
      case 32: // declare -> Int, Ident, Semicolon
#line 126 "kompilator.y"
            {
                Compiler.EmitCode(".locals init ( int32 i{0} )", ValueStack[ValueStack.Depth-2].val);

            }
#line default
        break;
      case 33: // declare -> Double, Ident, Semicolon
#line 131 "kompilator.y"
            {
                Compiler.EmitCode(".locals init ( float64 f{0} )", ValueStack[ValueStack.Depth-2].val);
                Compiler.EmitCode("ldc.r8 0");
                Compiler.EmitCode("stloc f{0}", ValueStack[ValueStack.Depth-2].val);
            }
#line default
        break;
      case 34: // declare -> Bool, Ident, Semicolon
#line 137 "kompilator.y"
            {
                Compiler.EmitCode(".locals init ( int32 b{0} )", ValueStack[ValueStack.Depth-2].val);
                Compiler.EmitCode("ldc.i4 0");
                Compiler.EmitCode("stloc b{0}", ValueStack[ValueStack.Depth-2].val);
            }
#line default
        break;
      case 35: // Anon@5 -> /* empty */
#line 144 "kompilator.y"
               {
               //Compiler.EmitCode("// linia {0,3} :  "+Compiler.source[lineno-1],lineno);
               Compiler.EmitCode("ldstr \"  Write: {0}\"");
               }
#line default
        break;
      case 36: // write -> Write, Anon@5, exp, Semicolon
#line 149 "kompilator.y"
               {
               Compiler.EmitCode("box [mscorlib]System.{0}",ValueStack[ValueStack.Depth-2].type=='i'?"Int32":"Double");
               Compiler.EmitCode("ldstr \"{0}\"",ValueStack[ValueStack.Depth-2].type=='i'?"i":"r");
               Compiler.EmitCode("call void [mscorlib]System.Console::WriteLine(string,object,object)");
               Compiler.EmitCode("");
               }
#line default
        break;
      case 37: // assign -> Ident, Assign, exp, Semicolon
#line 157 "kompilator.y"
               {
               if ( ValueStack[ValueStack.Depth-4].val[0]=='@' && ValueStack[ValueStack.Depth-2].type!='i' )
                   {
                   Console.WriteLine("  line {0,3}:  semantic error - cannot convert real to int",lineno);
                   ++Compiler.errors;
                   }
               else
                   {
                   if ( ValueStack[ValueStack.Depth-4].val[0]=='$' && ValueStack[ValueStack.Depth-2].type!='r' )
                       Compiler.EmitCode("conv.r8");

                    Compiler.EmitCode("stloc i{0}", ValueStack[ValueStack.Depth-4].val);
                   //Compiler.EmitCode("stloc _{0}{1}", $1[0]=='@'?'i':'r', $1[1]);
                   Compiler.EmitCode("");
                   }
               }
#line default
        break;
      case 38: // exp -> exp, Plus, term
#line 175 "kompilator.y"
               { CurrentSemanticValue.type = BinaryOpGenCode(Tokens.Plus, ValueStack[ValueStack.Depth-3].type, ValueStack[ValueStack.Depth-1].type); }
#line default
        break;
      case 39: // exp -> exp, Minus, term
#line 177 "kompilator.y"
               { CurrentSemanticValue.type = BinaryOpGenCode(Tokens.Minus, ValueStack[ValueStack.Depth-3].type, ValueStack[ValueStack.Depth-1].type); }
#line default
        break;
      case 40: // exp -> term
#line 179 "kompilator.y"
               { CurrentSemanticValue.type = ValueStack[ValueStack.Depth-1].type; }
#line default
        break;
      case 41: // term -> term, Multiplies, factor
#line 183 "kompilator.y"
               { CurrentSemanticValue.type = BinaryOpGenCode(Tokens.Multiplies, ValueStack[ValueStack.Depth-3].type, ValueStack[ValueStack.Depth-1].type); }
#line default
        break;
      case 42: // term -> term, Divides, factor
#line 185 "kompilator.y"
               { CurrentSemanticValue.type = BinaryOpGenCode(Tokens.Divides, ValueStack[ValueStack.Depth-3].type, ValueStack[ValueStack.Depth-1].type); }
#line default
        break;
      case 43: // term -> factor
#line 187 "kompilator.y"
               { CurrentSemanticValue.type = ValueStack[ValueStack.Depth-1].type; }
#line default
        break;
      case 44: // factor -> OpenPar, exp, ClosePar
#line 191 "kompilator.y"
               { CurrentSemanticValue.type = ValueStack[ValueStack.Depth-2].type; }
#line default
        break;
      case 45: // factor -> IntNumber
#line 193 "kompilator.y"
               {
               Compiler.EmitCode("ldc.i4 {0}",int.Parse(ValueStack[ValueStack.Depth-1].val));
               CurrentSemanticValue.type = 'i'; 
               }
#line default
        break;
      case 46: // factor -> RealNumber
#line 198 "kompilator.y"
               {
               double d = double.Parse(ValueStack[ValueStack.Depth-1].val,System.Globalization.CultureInfo.InvariantCulture) ;
               Compiler.EmitCode(string.Format(System.Globalization.CultureInfo.InvariantCulture,"ldc.r8 {0}",d));
               CurrentSemanticValue.type = 'r'; 
               }
#line default
        break;
      case 47: // factor -> Ident
#line 204 "kompilator.y"
               {
               Compiler.EmitCode("ldloc i{0}", ValueStack[ValueStack.Depth-1].val);
               CurrentSemanticValue.type = ValueStack[ValueStack.Depth-1].val[0]=='@'?'i':'r';
               }
#line default
        break;
    }
#pragma warning restore 162, 1522
  }

  protected override string TerminalToString(int terminal)
  {
    if (aliases != null && aliases.ContainsKey(terminal))
        return aliases[terminal];
    else if (((Tokens)terminal).ToString() != terminal.ToString(CultureInfo.InvariantCulture))
        return ((Tokens)terminal).ToString();
    else
        return CharToString((char)terminal);
  }

#line 211 "kompilator.y"

int lineno=1;
string temp;
string temp2;
int whilecounter=1;

public Parser(Scanner scanner) : base(scanner) { }

private char BinaryOpGenCode(Tokens t, char type1, char type2)
    {
    char type = ( type1=='i' && type2=='i' ) ? 'i' : 'r' ;
    if ( type1!=type )
        {
        Compiler.EmitCode("stloc temp");
        Compiler.EmitCode("conv.r8");
        Compiler.EmitCode("ldloc temp");
        }
   // if ( type2!=type )
   //     Compiler.EmitCode("conv.r8");
    switch ( t )
        {
        case Tokens.Plus:
            Compiler.EmitCode("add");
            break;
        case Tokens.Minus:
            Compiler.EmitCode("sub");
            break;
        case Tokens.Multiplies:
            Compiler.EmitCode("mul");
            break;
        case Tokens.Divides:
            Compiler.EmitCode("div");
            break;
        default:
            Console.WriteLine($"  line {lineno,3}:  internal gencode error");
            ++Compiler.errors;
            break;
        }
    return type;
    }
#line default
}
}
