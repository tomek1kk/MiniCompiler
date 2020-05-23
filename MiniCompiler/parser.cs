// This code was generated by the Gardens Point Parser Generator
// Copyright (c) Wayne Kelly, John Gough, QUT 2005-2014
// (see accompanying GPPGcopyright.rtf)

// GPPG version 1.5.2
// Machine:  DESKTOP-EC4UU67
// DateTime: 23.05.2020 17:58:33
// UserName: tomek
// Input file <kompilator.y - 23.05.2020 17:58:26>

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
  private static Rule[] rules = new Rule[46];
  private static State[] states = new State[89];
  private static string[] nonTerms = new string[] {
      "code", "stat", "exp", "term", "factor", "declare", "bool", "cond", "while", 
      "start", "$accept", "Anon@1", "write", "assign", "if", "Anon@2", "Anon@3", 
      "Anon@4", "Anon@5", "Anon@6", "Anon@7", };

  static Parser() {
    states[0] = new State(new int[]{9,3},new int[]{-10,1});
    states[1] = new State(new int[]{3,2});
    states[2] = new State(-1);
    states[3] = new State(new int[]{23,4});
    states[4] = new State(new int[]{17,11,34,32,18,37,19,40,20,43,13,53,15,62,2,72},new int[]{-1,5,-2,74,-13,10,-14,31,-6,36,-8,46,-15,47,-9,61});
    states[5] = new State(new int[]{24,6,17,11,34,32,18,37,19,40,20,43,13,53,15,62,2,72},new int[]{-2,9,-13,10,-14,31,-6,36,-8,46,-15,47,-9,61});
    states[6] = new State(-2,new int[]{-12,7});
    states[7] = new State(new int[]{11,8});
    states[8] = new State(-3);
    states[9] = new State(-4);
    states[10] = new State(-6);
    states[11] = new State(-33,new int[]{-21,12});
    states[12] = new State(new int[]{26,19,35,26,36,27,34,28},new int[]{-3,13,-4,30,-5,29});
    states[13] = new State(new int[]{25,14,5,15,6,22});
    states[14] = new State(-34);
    states[15] = new State(new int[]{26,19,35,26,36,27,34,28},new int[]{-4,16,-5,29});
    states[16] = new State(new int[]{7,17,8,24,25,-36,5,-36,6,-36,27,-36,28,-36,29,-36,30,-36,31,-36,32,-36,33,-36});
    states[17] = new State(new int[]{26,19,35,26,36,27,34,28},new int[]{-5,18});
    states[18] = new State(-39);
    states[19] = new State(new int[]{26,19,35,26,36,27,34,28},new int[]{-3,20,-4,30,-5,29});
    states[20] = new State(new int[]{27,21,5,15,6,22});
    states[21] = new State(-42);
    states[22] = new State(new int[]{26,19,35,26,36,27,34,28},new int[]{-4,23,-5,29});
    states[23] = new State(new int[]{7,17,8,24,25,-37,5,-37,6,-37,27,-37,28,-37,29,-37,30,-37,31,-37,32,-37,33,-37});
    states[24] = new State(new int[]{26,19,35,26,36,27,34,28},new int[]{-5,25});
    states[25] = new State(-40);
    states[26] = new State(-43);
    states[27] = new State(-44);
    states[28] = new State(-45);
    states[29] = new State(-41);
    states[30] = new State(new int[]{7,17,8,24,25,-38,5,-38,6,-38,27,-38,28,-38,29,-38,30,-38,31,-38,32,-38,33,-38});
    states[31] = new State(-7);
    states[32] = new State(new int[]{4,33});
    states[33] = new State(new int[]{26,19,35,26,36,27,34,28},new int[]{-3,34,-4,30,-5,29});
    states[34] = new State(new int[]{25,35,5,15,6,22});
    states[35] = new State(-35);
    states[36] = new State(-8);
    states[37] = new State(new int[]{34,38});
    states[38] = new State(new int[]{25,39});
    states[39] = new State(-30);
    states[40] = new State(new int[]{34,41});
    states[41] = new State(new int[]{25,42});
    states[42] = new State(-31);
    states[43] = new State(new int[]{34,44});
    states[44] = new State(new int[]{25,45});
    states[45] = new State(-32);
    states[46] = new State(-9);
    states[47] = new State(new int[]{24,-13,17,-13,34,-13,18,-13,19,-13,20,-13,13,-13,15,-13,2,-13,14,-14},new int[]{-16,48});
    states[48] = new State(new int[]{14,49});
    states[49] = new State(new int[]{23,50});
    states[50] = new State(new int[]{17,11,34,32,18,37,19,40,20,43,13,53,15,62,2,72},new int[]{-1,51,-2,74,-13,10,-14,31,-6,36,-8,46,-15,47,-9,61});
    states[51] = new State(new int[]{24,52,17,11,34,32,18,37,19,40,20,43,13,53,15,62,2,72},new int[]{-2,9,-13,10,-14,31,-6,36,-8,46,-15,47,-9,61});
    states[52] = new State(-15);
    states[53] = new State(new int[]{26,54});
    states[54] = new State(new int[]{26,19,35,26,36,27,34,28,21,87,22,88},new int[]{-7,55,-3,75,-4,30,-5,29});
    states[55] = new State(new int[]{27,56});
    states[56] = new State(new int[]{23,57});
    states[57] = new State(-20,new int[]{-20,58});
    states[58] = new State(new int[]{17,11,34,32,18,37,19,40,20,43,13,53,15,62,2,72},new int[]{-1,59,-2,74,-13,10,-14,31,-6,36,-8,46,-15,47,-9,61});
    states[59] = new State(new int[]{24,60,17,11,34,32,18,37,19,40,20,43,13,53,15,62,2,72},new int[]{-2,9,-13,10,-14,31,-6,36,-8,46,-15,47,-9,61});
    states[60] = new State(-21);
    states[61] = new State(-10);
    states[62] = new State(-16,new int[]{-17,63});
    states[63] = new State(new int[]{26,64});
    states[64] = new State(new int[]{26,19,35,26,36,27,34,28,21,87,22,88},new int[]{-7,65,-3,75,-4,30,-5,29});
    states[65] = new State(new int[]{27,66});
    states[66] = new State(new int[]{23,67});
    states[67] = new State(-17,new int[]{-18,68});
    states[68] = new State(new int[]{17,11,34,32,18,37,19,40,20,43,13,53,15,62,2,72},new int[]{-1,69,-2,74,-13,10,-14,31,-6,36,-8,46,-15,47,-9,61});
    states[69] = new State(new int[]{17,11,34,32,18,37,19,40,20,43,13,53,15,62,2,72,24,-18},new int[]{-19,70,-2,9,-13,10,-14,31,-6,36,-8,46,-15,47,-9,61});
    states[70] = new State(new int[]{24,71});
    states[71] = new State(-19);
    states[72] = new State(new int[]{11,73,24,-11,17,-11,34,-11,18,-11,19,-11,20,-11,13,-11,15,-11,2,-11});
    states[73] = new State(-12);
    states[74] = new State(-5);
    states[75] = new State(new int[]{28,76,5,15,6,22,29,78,30,80,31,82,32,84,33,86});
    states[76] = new State(new int[]{26,19,35,26,36,27,34,28},new int[]{-3,77,-4,30,-5,29});
    states[77] = new State(new int[]{5,15,6,22,27,-22});
    states[78] = new State(new int[]{26,19,35,26,36,27,34,28},new int[]{-3,79,-4,30,-5,29});
    states[79] = new State(new int[]{5,15,6,22,27,-23});
    states[80] = new State(new int[]{26,19,35,26,36,27,34,28},new int[]{-3,81,-4,30,-5,29});
    states[81] = new State(new int[]{5,15,6,22,27,-24});
    states[82] = new State(new int[]{26,19,35,26,36,27,34,28},new int[]{-3,83,-4,30,-5,29});
    states[83] = new State(new int[]{5,15,6,22,27,-25});
    states[84] = new State(new int[]{26,19,35,26,36,27,34,28},new int[]{-3,85,-4,30,-5,29});
    states[85] = new State(new int[]{5,15,6,22,27,-26});
    states[86] = new State(-27);
    states[87] = new State(-28);
    states[88] = new State(-29);

    for (int sNo = 0; sNo < states.Length; sNo++) states[sNo].number = sNo;

    rules[1] = new Rule(-11, new int[]{-10,3});
    rules[2] = new Rule(-12, new int[]{});
    rules[3] = new Rule(-10, new int[]{9,23,-1,24,-12,11});
    rules[4] = new Rule(-1, new int[]{-1,-2});
    rules[5] = new Rule(-1, new int[]{-2});
    rules[6] = new Rule(-2, new int[]{-13});
    rules[7] = new Rule(-2, new int[]{-14});
    rules[8] = new Rule(-2, new int[]{-6});
    rules[9] = new Rule(-2, new int[]{-8});
    rules[10] = new Rule(-2, new int[]{-9});
    rules[11] = new Rule(-2, new int[]{2});
    rules[12] = new Rule(-2, new int[]{2,11});
    rules[13] = new Rule(-8, new int[]{-15});
    rules[14] = new Rule(-16, new int[]{});
    rules[15] = new Rule(-8, new int[]{-15,-16,14,23,-1,24});
    rules[16] = new Rule(-17, new int[]{});
    rules[17] = new Rule(-18, new int[]{});
    rules[18] = new Rule(-19, new int[]{});
    rules[19] = new Rule(-9, new int[]{15,-17,26,-7,27,23,-18,-1,-19,24});
    rules[20] = new Rule(-20, new int[]{});
    rules[21] = new Rule(-15, new int[]{13,26,-7,27,23,-20,-1,24});
    rules[22] = new Rule(-7, new int[]{-3,28,-3});
    rules[23] = new Rule(-7, new int[]{-3,29,-3});
    rules[24] = new Rule(-7, new int[]{-3,30,-3});
    rules[25] = new Rule(-7, new int[]{-3,31,-3});
    rules[26] = new Rule(-7, new int[]{-3,32,-3});
    rules[27] = new Rule(-7, new int[]{-3,33});
    rules[28] = new Rule(-7, new int[]{21});
    rules[29] = new Rule(-7, new int[]{22});
    rules[30] = new Rule(-6, new int[]{18,34,25});
    rules[31] = new Rule(-6, new int[]{19,34,25});
    rules[32] = new Rule(-6, new int[]{20,34,25});
    rules[33] = new Rule(-21, new int[]{});
    rules[34] = new Rule(-13, new int[]{17,-21,-3,25});
    rules[35] = new Rule(-14, new int[]{34,4,-3,25});
    rules[36] = new Rule(-3, new int[]{-3,5,-4});
    rules[37] = new Rule(-3, new int[]{-3,6,-4});
    rules[38] = new Rule(-3, new int[]{-4});
    rules[39] = new Rule(-4, new int[]{-4,7,-5});
    rules[40] = new Rule(-4, new int[]{-4,8,-5});
    rules[41] = new Rule(-4, new int[]{-5});
    rules[42] = new Rule(-5, new int[]{26,-3,27});
    rules[43] = new Rule(-5, new int[]{35});
    rules[44] = new Rule(-5, new int[]{36});
    rules[45] = new Rule(-5, new int[]{34});
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
      case 11: // stat -> error
#line 41 "kompilator.y"
               {
               Console.WriteLine("  line {0,3}:  syntax error",lineno);
               ++Compiler.errors;
               yyerrok();
               }
#line default
        break;
      case 12: // stat -> error, Eof
#line 47 "kompilator.y"
               {
               Console.WriteLine("  line {0,3}:  syntax error",lineno);
               ++Compiler.errors;
               yyerrok();
               YYAccept();
               }
#line default
        break;
      case 14: // Anon@2 -> /* empty */
#line 54 "kompilator.y"
                    { Compiler.EmitCode("br elseend"); }
#line default
        break;
      case 15: // cond -> if, Anon@2, Else, OpenBracket, code, CloseBracket
#line 54 "kompilator.y"
                                                                                            { Compiler.EmitCode("elseend:"); }
#line default
        break;
      case 16: // Anon@3 -> /* empty */
#line 56 "kompilator.y"
                  { Compiler.EmitCode("while:"); }
#line default
        break;
      case 17: // Anon@4 -> /* empty */
#line 56 "kompilator.y"
                                                                                     { Compiler.EmitCode("brfalse endwhile"); }
#line default
        break;
      case 18: // Anon@5 -> /* empty */
#line 58 "kompilator.y"
            { Compiler.EmitCode("br while"); }
#line default
        break;
      case 19: // while -> While, Anon@3, OpenPar, bool, ClosePar, OpenBracket, Anon@4, code, 
               //          Anon@5, CloseBracket
#line 58 "kompilator.y"
                                                            { Compiler.EmitCode("endwhile:"); }
#line default
        break;
      case 20: // Anon@6 -> /* empty */
#line 60 "kompilator.y"
                                                 { Compiler.EmitCode("brfalse endif"); }
#line default
        break;
      case 21: // if -> If, OpenPar, bool, ClosePar, OpenBracket, Anon@6, code, CloseBracket
#line 60 "kompilator.y"
                                                                                                           { Compiler.EmitCode("endif:"); }
#line default
        break;
      case 22: // bool -> exp, Equal, exp
#line 63 "kompilator.y"
            {
                Compiler.EmitCode("ceq");
            }
#line default
        break;
      case 23: // bool -> exp, NotEqual, exp
#line 67 "kompilator.y"
            {
                Compiler.EmitCode("ceq");
                Compiler.EmitCode("neg");
            }
#line default
        break;
      case 24: // bool -> exp, Greater, exp
#line 72 "kompilator.y"
            {
                Compiler.EmitCode("cgt");
            }
#line default
        break;
      case 25: // bool -> exp, GreaterEqual, exp
#line 76 "kompilator.y"
            {
                
            }
#line default
        break;
      case 26: // bool -> exp, Less, exp
#line 80 "kompilator.y"
            {
                Compiler.EmitCode("clt");
            }
#line default
        break;
      case 27: // bool -> exp, LessEqual
#line 84 "kompilator.y"
            {
            }
#line default
        break;
      case 30: // declare -> Int, Ident, Semicolon
#line 90 "kompilator.y"
            {
                Compiler.EmitCode(".locals init ( int32 i{0} )", ValueStack[ValueStack.Depth-2].val);

            }
#line default
        break;
      case 31: // declare -> Double, Ident, Semicolon
#line 95 "kompilator.y"
            {
                Compiler.EmitCode(".locals init ( float64 f{0} )", ValueStack[ValueStack.Depth-2].val);
                Compiler.EmitCode("ldc.r8 0");
                Compiler.EmitCode("stloc f{0}", ValueStack[ValueStack.Depth-2].val);
            }
#line default
        break;
      case 32: // declare -> Bool, Ident, Semicolon
#line 101 "kompilator.y"
            {
                Compiler.EmitCode(".locals init ( int32 b{0} )", ValueStack[ValueStack.Depth-2].val);
                Compiler.EmitCode("ldc.i4 0");
                Compiler.EmitCode("stloc b{0}", ValueStack[ValueStack.Depth-2].val);
            }
#line default
        break;
      case 33: // Anon@7 -> /* empty */
#line 108 "kompilator.y"
               {
               //Compiler.EmitCode("// linia {0,3} :  "+Compiler.source[lineno-1],lineno);
               Compiler.EmitCode("ldstr \"  Result: {0}{1}\"");
               }
#line default
        break;
      case 34: // write -> Write, Anon@7, exp, Semicolon
#line 113 "kompilator.y"
               {
               Compiler.EmitCode("box [mscorlib]System.{0}",ValueStack[ValueStack.Depth-2].type=='i'?"Int32":"Double");
               Compiler.EmitCode("ldstr \"{0}\"",ValueStack[ValueStack.Depth-2].type=='i'?"i":"r");
               Compiler.EmitCode("call void [mscorlib]System.Console::WriteLine(string,object,object)");
               Compiler.EmitCode("");
               }
#line default
        break;
      case 35: // assign -> Ident, Assign, exp, Semicolon
#line 121 "kompilator.y"
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
      case 36: // exp -> exp, Plus, term
#line 139 "kompilator.y"
               { CurrentSemanticValue.type = BinaryOpGenCode(Tokens.Plus, ValueStack[ValueStack.Depth-3].type, ValueStack[ValueStack.Depth-1].type); }
#line default
        break;
      case 37: // exp -> exp, Minus, term
#line 141 "kompilator.y"
               { CurrentSemanticValue.type = BinaryOpGenCode(Tokens.Minus, ValueStack[ValueStack.Depth-3].type, ValueStack[ValueStack.Depth-1].type); }
#line default
        break;
      case 38: // exp -> term
#line 143 "kompilator.y"
               { CurrentSemanticValue.type = ValueStack[ValueStack.Depth-1].type; }
#line default
        break;
      case 39: // term -> term, Multiplies, factor
#line 147 "kompilator.y"
               { CurrentSemanticValue.type = BinaryOpGenCode(Tokens.Multiplies, ValueStack[ValueStack.Depth-3].type, ValueStack[ValueStack.Depth-1].type); }
#line default
        break;
      case 40: // term -> term, Divides, factor
#line 149 "kompilator.y"
               { CurrentSemanticValue.type = BinaryOpGenCode(Tokens.Divides, ValueStack[ValueStack.Depth-3].type, ValueStack[ValueStack.Depth-1].type); }
#line default
        break;
      case 41: // term -> factor
#line 151 "kompilator.y"
               { CurrentSemanticValue.type = ValueStack[ValueStack.Depth-1].type; }
#line default
        break;
      case 42: // factor -> OpenPar, exp, ClosePar
#line 155 "kompilator.y"
               { CurrentSemanticValue.type = ValueStack[ValueStack.Depth-2].type; }
#line default
        break;
      case 43: // factor -> IntNumber
#line 157 "kompilator.y"
               {
               Compiler.EmitCode("ldc.i4 {0}",int.Parse(ValueStack[ValueStack.Depth-1].val));
               CurrentSemanticValue.type = 'i'; 
               }
#line default
        break;
      case 44: // factor -> RealNumber
#line 162 "kompilator.y"
               {
               double d = double.Parse(ValueStack[ValueStack.Depth-1].val,System.Globalization.CultureInfo.InvariantCulture) ;
               Compiler.EmitCode(string.Format(System.Globalization.CultureInfo.InvariantCulture,"ldc.r8 {0}",d));
               CurrentSemanticValue.type = 'r'; 
               }
#line default
        break;
      case 45: // factor -> Ident
#line 168 "kompilator.y"
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

#line 175 "kompilator.y"

int lineno=1;

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
