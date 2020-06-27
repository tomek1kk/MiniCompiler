%namespace GardensPoint
%union
{
public string  val;
public char    type;
}

%token Assign Plus Minus Multiplies Divides SumLog IlLog
%token Program Return Error
%token If Else While
%token Read Write
%token Int Double Bool 
%token True False
%token OpenBracket CloseBracket Semicolon OpenPar ClosePar Return
%token Equal NotEqual Greater GreaterEqual Less LessEqual And Or Exclamation Neg
%token <val> Ident IntNumber RealNumber String

%type <type> code stat exp term factor declare cond while log assign expLog expRel myAnd myOr

%%
start    : Program OpenBracket declars CloseBracket
           ;
declars  : declare declars | code |;
code     : code stat | stat
          ;
return   : Return Semicolon
          {
            Compiler.EmitCode("ldc.i4 0");
            Compiler.EmitCode("leave EndMain");
          }
          ;
stat      : write | assign Semicolon 
            {
                Compiler.EmitCode("{0}:", Compiler.GetParTemp()); // sprawdzic kolejnosc tych 2
                Compiler.EmitCode("pop");
            }
          | while | block | cond | return | read 
          | error
          {
               Console.WriteLine("  line {0,3}:  syntax error",@1.StartLine);
               ++Compiler.errors;
               yyerrok();
               YYACCEPT;
          }
          ;
block     : OpenBracket code CloseBracket 
          | OpenBracket CloseBracket
          ;
cond      : ifelse | if
          ;
while     : While
            { 
                temp = Compiler.AddWhileTemp();
                Compiler.EmitCode("{0}:", temp);
            }
            OpenPar assign ClosePar 
            { 
                Compiler.EmitCode("{0}:", Compiler.GetParTemp());
                temp = Compiler.AddIfTemp();
                Compiler.EmitCode("brfalse {0}", temp); 
            }
            stat
            { 
                temp = Compiler.GetWhileTemp();
                Compiler.EmitCode("br {0}", temp);
                temp = Compiler.GetIfTemp();
                Compiler.EmitCode("{0}:", temp);
            }
          ;
ifhead    : If OpenPar assign ClosePar
            {
                Compiler.EmitCode("{0}:", Compiler.GetParTemp());
                temp = Compiler.AddIfTemp();
                Compiler.EmitCode("brfalse {0}", temp);
            }
          ;
if        : ifhead
            stat
            { 
                temp = Compiler.GetIfTemp();
                Compiler.EmitCode("{0}:", temp);
            }
          ;
ifelse    : ifhead
            stat
            Else
            {
                temp = Compiler.GetIfTemp();
                temp2 = Compiler.AddElseTemp();
                Compiler.EmitCode("br {0}", temp2);
                Compiler.EmitCode("{0}:", temp);
            }
            stat
            {
                temp2 = Compiler.GetElseTemp();
                Compiler.EmitCode("{0}:", temp2);
            }
            ;
declare   : Int Ident Semicolon
            {
                if (System.Linq.Enumerable.All(Compiler.symbolTable.Keys, ident => ident != "_" + $2))
                {
                    
                    Compiler.EmitCode(".locals init ( int32 _{0} )", $2);
                    Compiler.symbolTable.Add("_" + $2, "int");
                }
                else
                {
                    Console.WriteLine("line {0,3}:  variable already declared!", @1.StartLine);
                    Compiler.errors++;
                }

            }
            | Double Ident Semicolon
            {
                if (System.Linq.Enumerable.All(Compiler.symbolTable.Keys, ident => ident != "_" + $2))
                {
                    Compiler.EmitCode(".locals init ( float64 _{0} )", $2);
                    Compiler.symbolTable.Add("_" + $2, "double");
                }
                else
                {
                    Console.WriteLine("line {0,3}:  variable already declared!", @1.StartLine);
                    Compiler.errors++;
                }
            }
            | Bool Ident Semicolon
            {
                if (System.Linq.Enumerable.All(Compiler.symbolTable.Keys, ident => ident != "_" + $2))
                {
                    Compiler.EmitCode(".locals init ( int32 _{0} )", $2);
                    Compiler.symbolTable.Add("_" + $2, "bool");
                }
                else
                {
                    Console.WriteLine("line {0,3}:  variable already declared!", @1.StartLine);
                    Compiler.errors++;
                }
            }
            ;
write     : Write assign Semicolon
            {
                Compiler.EmitCode("{0}:", Compiler.GetParTemp());
                if ($2 == 'd')
                {
                    Compiler.EmitCode("stloc __temp");
                    Compiler.EmitCode("call class [mscorlib]System.Globalization.CultureInfo [mscorlib]System.Globalization.CultureInfo::get_InvariantCulture()");
                    Compiler.EmitCode("ldstr \"{0:0.000000}\"");
                    Compiler.EmitCode("ldloc __temp");
                    Compiler.EmitCode("box [mscorlib]System.Double");
                    Compiler.EmitCode("call string [mscorlib]System.String::Format(class [mscorlib]System.IFormatProvider, string, object)");
                    Compiler.EmitCode("call void [mscorlib]System.Console::Write(string)");
                }
                else if ($2 == 'b')
                {
                    Compiler.EmitCode("call void [mscorlib]System.Console::Write(bool)");
                }
                else
                {
                    Compiler.EmitCode("box [mscorlib]System.Int32");
                    Compiler.EmitCode("call void [mscorlib]System.Console::Write(object)");
                }
            }
            | Write String Semicolon
            {
                Compiler.EmitCode("ldstr {0}", $2);
                Compiler.EmitCode("call void [mscorlib]System.Console::Write(string)");
            }
          ;
read      : Read Ident Semicolon
            {
               if (!Compiler.symbolTable.ContainsKey("_" + $2)) 
               {
                    Console.WriteLine("line {0,3}: error - use of undeclared variable", @1.StartLine);
                    Compiler.errors++;
               }
               else
               {
                    Compiler.EmitCode("call string [mscorlib]System.Console::ReadLine()");
                    if (Compiler.symbolTable["_" + $2] == "bool")
                    {
                        Compiler.EmitCode("call bool [mscorlib]System.Boolean::Parse(string)");
                    }
                    else if (Compiler.symbolTable["_" + $2] == "int")
                    {
                        Compiler.EmitCode("call int32 [mscorlib]System.Int32::Parse(string)");
                    }
                    else
                    {
                       Compiler.EmitCode("call float64 [mscorlib]System.Double::Parse(string)");
                    }
                    Compiler.EmitCode("stloc {0}", "_" + $2);
               }
          }
          ;
assign    :  Ident Assign assign 
          {       
               if (!Compiler.symbolTable.ContainsKey("_" + $1)) 
               {
                    Console.WriteLine("line {0,3}: error - use of undeclared variable", @1.StartLine);
                    Compiler.errors++;
               }
               else
               {
                    if (Compiler.symbolTable["_" + $1]=="int" && $3 != 'i')
                    {
                        Console.WriteLine("line {0,3}:  semantic error - cannot convert to int (use convert operator)",@1.StartLine);
                        ++Compiler.errors;
                    } 
                    else if (Compiler.symbolTable["_" + $1]=="double" && $3 != 'd' && $3 != 'i')
                    {
                        Console.WriteLine("line {0,3}:  semantic error - cannot convert to double (use convert operator)",@1.StartLine);
                        ++Compiler.errors;
                    }
                    else if (Compiler.symbolTable["_" + $1]=="bool" && $3 != 'b')
                    {
                        Console.WriteLine("line {0,3}:  semantic error - cannot convert to bool (use convert operator)",@1.StartLine);
                        ++Compiler.errors;
                    }
                    else
                    {
                        $$ = $3;
                        if (Compiler.symbolTable["_" + $1]=="double" && $3 =='i')
                        {
                            Compiler.EmitCode("conv.r8");
                            $$ = 'd';
                        }
                        Compiler.EmitCode("dup");
                        Compiler.EmitCode("stloc {0}", "_" + $1);
                    }
               }
            }
            | expLog 
            { 
                $$ = $1;
            }
          ;
myAnd     : expRel
            {
                $$ = $1;
            }
            ;
myOr      : expRel
            {
                $$ = $1;
            }
           ;
expLog    : expLog 
            {                
                Compiler.AddParTemp(); 
                Compiler.EmitCode("brtrue licz{0}", ++pom);
                Compiler.EmitCode("ldc.i4 0");
                Compiler.EmitCode("br {0}", Compiler.CheckParTemp());
                Compiler.EmitCode("licz{0}:", pom);
                Compiler.EmitCode("ldc.i4 1");  
            } 
            And myAnd
            {
                if ($1 != 'b' || $4 != 'b')
                {
                    Console.WriteLine("line {0,3}:  semantic error - && operator can be used to bool arguments",@1.StartLine);
                    ++Compiler.errors;
                }
                else
                {
                    Compiler.EmitCode("and");
                    Compiler.EmitCode("{0}:", Compiler.GetParTemp());
                    $$ = 'b';
                }
            }
            | expLog
            {
                Compiler.AddParTemp(); 
                Compiler.EmitCode("brfalse licz{0}", ++pom);
                Compiler.EmitCode("ldc.i4 1");
                Compiler.EmitCode("br {0}", Compiler.CheckParTemp());
                Compiler.EmitCode("licz{0}:", pom);
                Compiler.EmitCode("ldc.i4 0");
            }
            Or myOr
            {
                if ($1 != 'b' || $4 != 'b')
                {
                    Console.WriteLine("line {0,3}:  semantic error - || operator can be used to bool arguments",@1.StartLine);
                    ++Compiler.errors;
                }
                else
                {
                    Compiler.EmitCode("or");
                    $$ = 'b';
                }
            }
            | expRel { $$ = $1; }
            ;
expRel    : expRel Equal exp
            {
                if (($1 == 'b' && $3 != 'b') || ($1 != 'b' && $3 == 'b'))
                {
                    Console.WriteLine("line {0,3}:  semantic error - == operator cannot be used to these arguments",@1.StartLine);
                    ++Compiler.errors;
                }
                else
                {
                    if ($1 != 'b' || $3 != 'b')
                        CheckTypes($1, $3);
                    Compiler.EmitCode("ceq");
                    $$ = 'b';
                }
            }
            | expRel NotEqual exp
            {
                if (($1 == 'b' && $3 != 'b') || ($1 != 'b' && $3 == 'b'))
                {
                    Console.WriteLine("line {0,3}:  semantic error - != operator cannot be used to these arguments",@1.StartLine);
                    ++Compiler.errors;
                }
                else
                {
                    if ($1 != 'b' || $3 != 'b')
                        CheckTypes($1, $3);
                    Compiler.EmitCode("ceq");
                    Compiler.EmitCode("ldc.i4 1");
                    Compiler.EmitCode("xor");
                    $$ = 'b';
                }
            }
            | expRel Greater exp
            {
                if ($1 == 'b' || $3 == 'b')
                {
                    Console.WriteLine("line {0,3}:  semantic error - > operator cannot be used to bool arguments",@1.StartLine);
                    ++Compiler.errors;
                }
                else
                {
                    CheckTypes($1, $3);
                    Compiler.EmitCode("cgt");
                    $$ = 'b';
                }
            }
            | expRel GreaterEqual exp
            {
                if ($1 == 'b' || $3 == 'b')
                {
                    Console.WriteLine("line {0,3}:  semantic error - >= operator cannot be used to bool arguments",@1.StartLine);
                    ++Compiler.errors;
                }
                else
                {
                    if ($3 == 'd')
                        Compiler.EmitCode("ldc.r8 1");
                    else
                        Compiler.EmitCode("ldc.i4 1");
                    Compiler.EmitCode("sub");
                    CheckTypes($1, $3);
                    Compiler.EmitCode("cgt");
                    $$ = 'b';
                }
            }
            | expRel Less exp
            {
                CheckTypes($1, $3);
                Compiler.EmitCode("clt");
                $$ = 'b';
            }
            | expRel LessEqual exp
            {
                 if ($3 == 'd')
                     Compiler.EmitCode("ldc.r8 1");
                 else
                     Compiler.EmitCode("ldc.i4 1");
                Compiler.EmitCode("add");
                CheckTypes($1, $3);
                Compiler.EmitCode("clt");
                $$ = 'b';
            }
            | exp { $$ = $1; }
            ;
exp       : exp Plus term
               { $$ = BinaryOpGenCode(Tokens.Plus, $1, $3, @1.StartLine); }
          | exp Minus term
               { $$ = BinaryOpGenCode(Tokens.Minus, $1, $3, @1.StartLine); }
          | term
               { $$ = $1; }
          ;

term      : term Multiplies log
               { $$ = BinaryOpGenCode(Tokens.Multiplies, $1, $3, @1.StartLine); }
          | term Divides log
               { $$ = BinaryOpGenCode(Tokens.Divides, $1, $3, @1.StartLine); }
          | log
               { $$ = $1; }
          ;
log       : log SumLog factor
               { $$ = BinaryOpGenCode(Tokens.SumLog, $1, $3, @1.StartLine); }
          | log IlLog factor
               { $$ = BinaryOpGenCode(Tokens.IlLog, $1, $3, @1.StartLine); }
          | factor
               { $$ = $1; }
          ;
factor    : OpenPar
          {
            Compiler.AddParTemp();
          }
            assign
            ClosePar
          { 
            $$ = $3; 
            Compiler.EmitCode("{0}:", Compiler.GetParTemp());
          }
          | Minus factor
          {
            if ($2 == 'b')
            {
                 Console.WriteLine("line {0,3}:  expression must be int or double to use - operator",@1.StartLine);
                 Compiler.errors++;
            }
            else
            {
                $$ = $2;
                Compiler.EmitCode("neg");
            }
          }
          | Exclamation factor
          {
            if ($2 == 'b')
            {
                Compiler.EmitCode("ldc.i4 1");
                Compiler.EmitCode("xor");
                $$ = 'b';
            }
            else
            {
                 Console.WriteLine("line {0,3}:  expression must be bool to use ! operator",@1.StartLine);
                 Compiler.errors++;
            }
          }
          | Neg factor
          {
            if ($2 != 'i')
            {
                 Console.WriteLine("line {0,3}:  expression must be int to use ~ operator",@1.StartLine);
                 Compiler.errors++;
            }
            else
            {
                $$ = $2;
                Compiler.EmitCode("not");
            }
          }
          | OpenPar Int ClosePar factor
          {
            if ($4 == 'd')
            {
                $$ = 'i';
                Compiler.EmitCode("conv.i4");
            }
            else if ($4 == 'i')
            {
                $$ = 'i';
            }
            else if ($4 == 'b')
            {
                $$ = 'i';
            }
            else
            {
                 Console.WriteLine("line {0,3}:  type not recognized",@1.StartLine);
                 Compiler.errors++;
            }
          }
          | OpenPar Double ClosePar factor
          {
            if ($4 == 'd')
            {
                $$ = 'd';
            }
            else if ($4 == 'i')
            {
                Compiler.EmitCode("conv.r8");
                $$ = 'd';
            }
            else if ($4 == 'b')
            {
                Compiler.EmitCode("conv.r8");
                $$ = 'd';
            }
            else
            {
                 Console.WriteLine("line {0,3}:  type not recognized",@1.StartLine);
                 Compiler.errors++;
            }
          }
          | IntNumber
          {
               Compiler.EmitCode("ldc.i4 {0}",int.Parse($1));
               $$ = 'i'; 
          }
          | RealNumber
          {
               double d = double.Parse($1,System.Globalization.CultureInfo.InvariantCulture);
               Compiler.EmitCode(string.Format(System.Globalization.CultureInfo.InvariantCulture,"ldc.r8 {0}",d));
               $$ = 'd'; 
          }
          | True
          {
                Compiler.EmitCode("ldc.i4 1");
                $$ = 'b';
          }
          | False
          {
                Compiler.EmitCode("ldc.i4 0");
                $$ = 'b';
          }
          | Ident
          {
               if (!Compiler.symbolTable.ContainsKey("_" + $1)) 
               {
                    Console.WriteLine("line {0,3}: error - use of undeclared variable", @1.StartLine);
                    Compiler.errors++;
               }
               else
               {
                   Compiler.EmitCode("ldloc {0}", "_" + $1);
                   switch(Compiler.symbolTable["_" + $1])
                   {
                        case "int":
                            $$ = 'i';
                            break;
                        case "double":
                            $$ = 'd';
                            break;
                        case "bool":
                            $$ = 'b';
                            break;
                        default:
                            Console.WriteLine("line {0,3}:  unrecognized type",@1.StartLine);
                            Compiler.errors++;
                            break;
                   }
               }
          }
          ;

%%

string temp;
string temp2;
int pom = 0;

public Parser(Scanner scanner) : base(scanner) { }

private void CheckTypes(char type1, char type2)
{
    char type = (type1=='i' && type2=='i') ? 'i' : 'd' ;
    if (type1 != type)
    {
        Compiler.EmitCode("stloc __temp");
        Compiler.EmitCode("conv.r8");
        Compiler.EmitCode("ldloc __temp");
    }
    if (type2 != type)
        Compiler.EmitCode("conv.r8");
}

private char BinaryOpGenCode(Tokens t, char type1, char type2, int line)
{
    CheckTypes(type1, type2);
    switch (t)
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
        case Tokens.SumLog:
            Compiler.EmitCode("or");
            break;
        case Tokens.IlLog:
            Compiler.EmitCode("and");
            break;
        default:
            Console.WriteLine($"line {0}:  token not recognized", line);
            ++Compiler.errors;
            break;
    }
    return (type1=='i' && type2=='i') ? 'i' : 'd';
}