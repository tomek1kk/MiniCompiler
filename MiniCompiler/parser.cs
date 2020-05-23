// This code was generated by the Gardens Point Parser Generator
// Copyright (c) Wayne Kelly, John Gough, QUT 2005-2014
// (see accompanying GPPGcopyright.rtf)

// GPPG version 1.5.2
// Machine:  DESKTOP-EC4UU67
// DateTime: 23.05.2020 08:52:34
// UserName: tomek
// Input file <kompilator.y - 23.05.2020 08:52:27>

// options: lines gplex

using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Text;
using QUT.Gppg;

namespace GardensPoint
{
public enum Tokens {error=2,EOF=3,Print=4,Exit=5,Assign=6,
    Plus=7,Minus=8,Multiplies=9,Divides=10,Program=11,Return=12,
    Eof=13,Error=14,If=15,Else=16,While=17,Read=18,
    Write=19,Int=20,Double=21,Bool=22,True=23,False=24,
    OpenBracket=25,CloseBracket=26,Semicolon=27,OpenPar=28,ClosePar=29,Ident=30,
    IntNumber=31,RealNumber=32};

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
  private static Rule[] rules = new Rule[27];
  private static State[] states = new State[49];
  private static string[] nonTerms = new string[] {
      "code", "stat", "exp", "term", "factor", "declare", "start", "$accept", 
      "Anon@1", "print", "assign", "Anon@2", };

  static Parser() {
    states[0] = new State(new int[]{11,3},new int[]{-7,1});
    states[1] = new State(new int[]{3,2});
    states[2] = new State(-1);
    states[3] = new State(new int[]{25,4});
    states[4] = new State(new int[]{4,11,30,32,20,37,21,40,22,43,2,46},new int[]{-1,5,-2,48,-10,10,-11,31,-6,36});
    states[5] = new State(new int[]{26,6,4,11,30,32,20,37,21,40,22,43,2,46},new int[]{-2,9,-10,10,-11,31,-6,36});
    states[6] = new State(-2,new int[]{-9,7});
    states[7] = new State(new int[]{13,8});
    states[8] = new State(-3);
    states[9] = new State(-4);
    states[10] = new State(-6);
    states[11] = new State(-14,new int[]{-12,12});
    states[12] = new State(new int[]{28,19,31,26,32,27,30,28},new int[]{-3,13,-4,30,-5,29});
    states[13] = new State(new int[]{27,14,7,15,8,22});
    states[14] = new State(-15);
    states[15] = new State(new int[]{28,19,31,26,32,27,30,28},new int[]{-4,16,-5,29});
    states[16] = new State(new int[]{9,17,10,24,27,-17,7,-17,8,-17,29,-17});
    states[17] = new State(new int[]{28,19,31,26,32,27,30,28},new int[]{-5,18});
    states[18] = new State(-20);
    states[19] = new State(new int[]{28,19,31,26,32,27,30,28},new int[]{-3,20,-4,30,-5,29});
    states[20] = new State(new int[]{29,21,7,15,8,22});
    states[21] = new State(-23);
    states[22] = new State(new int[]{28,19,31,26,32,27,30,28},new int[]{-4,23,-5,29});
    states[23] = new State(new int[]{9,17,10,24,27,-18,7,-18,8,-18,29,-18});
    states[24] = new State(new int[]{28,19,31,26,32,27,30,28},new int[]{-5,25});
    states[25] = new State(-21);
    states[26] = new State(-24);
    states[27] = new State(-25);
    states[28] = new State(-26);
    states[29] = new State(-22);
    states[30] = new State(new int[]{9,17,10,24,27,-19,7,-19,8,-19,29,-19});
    states[31] = new State(-7);
    states[32] = new State(new int[]{6,33});
    states[33] = new State(new int[]{28,19,31,26,32,27,30,28},new int[]{-3,34,-4,30,-5,29});
    states[34] = new State(new int[]{27,35,7,15,8,22});
    states[35] = new State(-16);
    states[36] = new State(-8);
    states[37] = new State(new int[]{30,38});
    states[38] = new State(new int[]{27,39});
    states[39] = new State(-11);
    states[40] = new State(new int[]{30,41});
    states[41] = new State(new int[]{27,42});
    states[42] = new State(-12);
    states[43] = new State(new int[]{30,44});
    states[44] = new State(new int[]{27,45});
    states[45] = new State(-13);
    states[46] = new State(new int[]{13,47,26,-9,4,-9,30,-9,20,-9,21,-9,22,-9,2,-9});
    states[47] = new State(-10);
    states[48] = new State(-5);

    for (int sNo = 0; sNo < states.Length; sNo++) states[sNo].number = sNo;

    rules[1] = new Rule(-8, new int[]{-7,3});
    rules[2] = new Rule(-9, new int[]{});
    rules[3] = new Rule(-7, new int[]{11,25,-1,26,-9,13});
    rules[4] = new Rule(-1, new int[]{-1,-2});
    rules[5] = new Rule(-1, new int[]{-2});
    rules[6] = new Rule(-2, new int[]{-10});
    rules[7] = new Rule(-2, new int[]{-11});
    rules[8] = new Rule(-2, new int[]{-6});
    rules[9] = new Rule(-2, new int[]{2});
    rules[10] = new Rule(-2, new int[]{2,13});
    rules[11] = new Rule(-6, new int[]{20,30,27});
    rules[12] = new Rule(-6, new int[]{21,30,27});
    rules[13] = new Rule(-6, new int[]{22,30,27});
    rules[14] = new Rule(-12, new int[]{});
    rules[15] = new Rule(-10, new int[]{4,-12,-3,27});
    rules[16] = new Rule(-11, new int[]{30,6,-3,27});
    rules[17] = new Rule(-3, new int[]{-3,7,-4});
    rules[18] = new Rule(-3, new int[]{-3,8,-4});
    rules[19] = new Rule(-3, new int[]{-4});
    rules[20] = new Rule(-4, new int[]{-4,9,-5});
    rules[21] = new Rule(-4, new int[]{-4,10,-5});
    rules[22] = new Rule(-4, new int[]{-5});
    rules[23] = new Rule(-5, new int[]{28,-3,29});
    rules[24] = new Rule(-5, new int[]{31});
    rules[25] = new Rule(-5, new int[]{32});
    rules[26] = new Rule(-5, new int[]{30});
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
               Compiler.EmitCode("// linia {0,3} :  "+Compiler.source[lineno-1],lineno);
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
      case 9: // stat -> error
#line 41 "kompilator.y"
               {
               Console.WriteLine("  line {0,3}:  syntax error",lineno);
               ++Compiler.errors;
               yyerrok();
               }
#line default
        break;
      case 10: // stat -> error, Eof
#line 47 "kompilator.y"
               {
               Console.WriteLine("  line {0,3}:  syntax error",lineno);
               ++Compiler.errors;
               yyerrok();
               YYAccept();
               }
#line default
        break;
      case 11: // declare -> Int, Ident, Semicolon
#line 55 "kompilator.y"
            {
                Compiler.EmitCode(".locals init ( int32 i{0} )", ValueStack[ValueStack.Depth-2].val);
                Compiler.EmitCode("ldc.i4 0");
                Compiler.EmitCode("stloc i{0}", ValueStack[ValueStack.Depth-2].val);
            }
#line default
        break;
      case 12: // declare -> Double, Ident, Semicolon
#line 61 "kompilator.y"
            {
                Compiler.EmitCode(".locals init ( float64 f{0} )", ValueStack[ValueStack.Depth-2].val);
                Compiler.EmitCode("ldc.r8 0");
                Compiler.EmitCode("stloc f{0}", ValueStack[ValueStack.Depth-2].val);
            }
#line default
        break;
      case 13: // declare -> Bool, Ident, Semicolon
#line 67 "kompilator.y"
            {
                Compiler.EmitCode(".locals init ( int32 b{0} )", ValueStack[ValueStack.Depth-2].val);
                Compiler.EmitCode("ldc.i4 0");
                Compiler.EmitCode("stloc b{0}", ValueStack[ValueStack.Depth-2].val);
            }
#line default
        break;
      case 14: // Anon@2 -> /* empty */
#line 74 "kompilator.y"
               {
               Compiler.EmitCode("// linia {0,3} :  "+Compiler.source[lineno-1],lineno);
               Compiler.EmitCode("ldstr \"  Result: {0}{1}\"");
               }
#line default
        break;
      case 15: // print -> Print, Anon@2, exp, Semicolon
#line 79 "kompilator.y"
               {
               Compiler.EmitCode("box [mscorlib]System.{0}",ValueStack[ValueStack.Depth-2].type=='i'?"Int32":"Double");
               Compiler.EmitCode("ldstr \"{0}\"",ValueStack[ValueStack.Depth-2].type=='i'?"i":"r");
               Compiler.EmitCode("call void [mscorlib]System.Console::WriteLine(string,object,object)");
               Compiler.EmitCode("");
               }
#line default
        break;
      case 16: // assign -> Ident, Assign, exp, Semicolon
#line 87 "kompilator.y"
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
                   Compiler.EmitCode("stloc _{0}{1}", ValueStack[ValueStack.Depth-4].val[0]=='@'?'i':'r', ValueStack[ValueStack.Depth-4].val[1]);
                   Compiler.EmitCode("");
                   }
               }
#line default
        break;
      case 17: // exp -> exp, Plus, term
#line 103 "kompilator.y"
               { CurrentSemanticValue.type = BinaryOpGenCode(Tokens.Plus, ValueStack[ValueStack.Depth-3].type, ValueStack[ValueStack.Depth-1].type); }
#line default
        break;
      case 18: // exp -> exp, Minus, term
#line 105 "kompilator.y"
               { CurrentSemanticValue.type = BinaryOpGenCode(Tokens.Minus, ValueStack[ValueStack.Depth-3].type, ValueStack[ValueStack.Depth-1].type); }
#line default
        break;
      case 19: // exp -> term
#line 107 "kompilator.y"
               { CurrentSemanticValue.type = ValueStack[ValueStack.Depth-1].type; }
#line default
        break;
      case 20: // term -> term, Multiplies, factor
#line 111 "kompilator.y"
               { CurrentSemanticValue.type = BinaryOpGenCode(Tokens.Multiplies, ValueStack[ValueStack.Depth-3].type, ValueStack[ValueStack.Depth-1].type); }
#line default
        break;
      case 21: // term -> term, Divides, factor
#line 113 "kompilator.y"
               { CurrentSemanticValue.type = BinaryOpGenCode(Tokens.Divides, ValueStack[ValueStack.Depth-3].type, ValueStack[ValueStack.Depth-1].type); }
#line default
        break;
      case 22: // term -> factor
#line 115 "kompilator.y"
               { CurrentSemanticValue.type = ValueStack[ValueStack.Depth-1].type; }
#line default
        break;
      case 23: // factor -> OpenPar, exp, ClosePar
#line 119 "kompilator.y"
               { CurrentSemanticValue.type = ValueStack[ValueStack.Depth-2].type; }
#line default
        break;
      case 24: // factor -> IntNumber
#line 121 "kompilator.y"
               {
               Compiler.EmitCode("ldc.i4 {0}",int.Parse(ValueStack[ValueStack.Depth-1].val));
               CurrentSemanticValue.type = 'i'; 
               }
#line default
        break;
      case 25: // factor -> RealNumber
#line 126 "kompilator.y"
               {
               double d = double.Parse(ValueStack[ValueStack.Depth-1].val,System.Globalization.CultureInfo.InvariantCulture) ;
               Compiler.EmitCode(string.Format(System.Globalization.CultureInfo.InvariantCulture,"ldc.r8 {0}",d));
               CurrentSemanticValue.type = 'r'; 
               }
#line default
        break;
      case 26: // factor -> Ident
#line 132 "kompilator.y"
               {
               Compiler.EmitCode("ldloc xi{0}", ValueStack[ValueStack.Depth-1].val);
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

#line 139 "kompilator.y"

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
    if ( type2!=type )
        Compiler.EmitCode("conv.r8");
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
