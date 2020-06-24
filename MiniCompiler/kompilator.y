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
stat      : write | assign | while | block | cond | return | read 
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
                deeplevel++;
                if (deeplevel == 1)
                    temp = Compiler.NewTemp();
                Compiler.EmitCode("{0}:", temp + "_" + deeplevel.ToString());
            }
            OpenPar expLog ClosePar 
            { 
                Compiler.EmitCode("nielicz{0}:", ++pom2);
                if (deeplevel == 1)
                    temp3 = Compiler.NewTemp();
                Compiler.EmitCode("brfalse {0}", temp3 + "_" + deeplevel.ToString()); 
            }
            stat
            { 
                Compiler.EmitCode("br {0}", temp + "_" + deeplevel.ToString());
                Compiler.EmitCode("{0}:", temp3 + "_" + deeplevel.ToString());
                deeplevel--;
            }
          ;
ifhead     : If OpenPar expLog ClosePar
            {
                Compiler.EmitCode("nielicz{0}:", ++pom2);
                deeplevel++;
                if (deeplevel == 1)
                    temp = Compiler.NewTemp();
                Compiler.EmitCode("brfalse {0}", temp + "_" + deeplevel.ToString());
            }
            ;
if        : ifhead
            stat
            { 
                Compiler.EmitCode("{0}:", temp + "_" + deeplevel.ToString());
                deeplevel--;
            }
          ;
ifelse    : ifhead
            stat
            Else
            {
               deeplevelElse++;
                if (deeplevelElse == 1)
                    temp2 = Compiler.NewTemp();
                Compiler.EmitCode("br {0}", temp2 + "_" + deeplevelElse.ToString());
                Compiler.EmitCode("{0}:", temp + "_" + deeplevel.ToString());
            }
            stat
            {
                Compiler.EmitCode("{0}:", temp2 + "_" + deeplevelElse.ToString());
                deeplevel--;
                deeplevelElse--;
            }
            ;
declare   : Int Ident Semicolon
            {
                if (System.Linq.Enumerable.All(Compiler.symbolTable.Keys, ident => ident != $2))
                {
                    
                    Compiler.EmitCode(".locals init ( int32 {0} )", $2);
                    Compiler.symbolTable.Add($2, "int");
                }
                else
                {
                    Console.WriteLine("line {0,3}:  variable already declared!", @1.StartLine);
                    Compiler.errors++;
                }

            }
            | Double Ident Semicolon
            {
                if (System.Linq.Enumerable.All(Compiler.symbolTable.Keys, ident => ident != $2))
                {
                    Compiler.EmitCode(".locals init ( float64 {0} )", $2);
                    Compiler.symbolTable.Add($2, "double");
                }
                else
                {
                    Console.WriteLine("line {0,3}:  variable already declared!", @1.StartLine);
                    Compiler.errors++;
                }
            }
            | Bool Ident Semicolon
            {
                if (System.Linq.Enumerable.All(Compiler.symbolTable.Keys, ident => ident != $2))
                {
                    Compiler.EmitCode(".locals init ( int32 {0} )", $2);
                    Compiler.symbolTable.Add($2, "bool");
                }
                else
                {
                    Console.WriteLine("line {0,3}:  variable already declared!", @1.StartLine);
                    Compiler.errors++;
                }
            }
            ;
write     : Write expLog Semicolon
            {
                if ($2 == 'd')
                {
                    Compiler.EmitCode("stloc _temp");
                    Compiler.EmitCode("call class [mscorlib]System.Globalization.CultureInfo [mscorlib]System.Globalization.CultureInfo::get_InvariantCulture()");
                    Compiler.EmitCode("ldstr \"{0:0.000000}\"");
                    Compiler.EmitCode("ldloc _temp");
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
               if (!Compiler.symbolTable.ContainsKey($2)) 
               {
                    Console.WriteLine("line {0,3}: error - use of undeclared variable", @1.StartLine);
                    Compiler.errors++;
               }
               else
               {
                    Compiler.EmitCode("call string [mscorlib]System.Console::ReadLine()");
                    if (Compiler.symbolTable[$2] == "bool")
                    {
                        Compiler.EmitCode("call bool [mscorlib]System.Boolean::Parse(string)");
                    }
                    else if (Compiler.symbolTable[$2] == "int")
                    {
                        Compiler.EmitCode("call int32 [mscorlib]System.Int32::Parse(string)");
                    }
                    else
                    {
                       Compiler.EmitCode("call float64 [mscorlib]System.Double::Parse(string)");
                    }
                    Compiler.EmitCode("stloc {0}", $2);
               }
          }
          ;
assign    :  Ident Assign assign 
          {       
               if (!Compiler.symbolTable.ContainsKey($1)) 
               {
                    Console.WriteLine("line {0,3}: error - use of undeclared variable", @1.StartLine);
                    Compiler.errors++;
               }
               else
               {
                    if (Compiler.symbolTable[$1]=="int" && $3 != 'i')
                    {
                        Console.WriteLine("line {0,3}:  semantic error - cannot convert to int (use convert operator)",@1.StartLine);
                        ++Compiler.errors;
                    } 
                    else if (Compiler.symbolTable[$1]=="double" && $3 != 'd' && $3 != 'i')
                    {
                        Console.WriteLine("line {0,3}:  semantic error - cannot convert to double (use convert operator)",@1.StartLine);
                        ++Compiler.errors;
                    }
                    else if (Compiler.symbolTable[$1]=="bool" && $3 != 'b')
                    {
                        Console.WriteLine("line {0,3}:  semantic error - cannot convert to bool (use convert operator)",@1.StartLine);
                        ++Compiler.errors;
                    }
                    else
                    {
                        $$ = $3;
                        if (Compiler.symbolTable[$1]=="double" && $3 =='i')
                        {
                            Compiler.EmitCode("conv.r8");
                            $$ = 'd';
                        }
                        
                        Compiler.EmitCode("ldloc {0}", vari);
                        Compiler.EmitCode("stloc {0}", $1);
                    }
               }
            }
            | Ident Assign expLog Semicolon
            {
               if (!Compiler.symbolTable.ContainsKey($1)) 
               {
                    Console.WriteLine("line {0,3}: error - use of undeclared variable", @1.StartLine);
                    Compiler.errors++;
               }
               else
               {
                    if (Compiler.symbolTable[$1]=="int" && $3 != 'i')
                    {
                        Console.WriteLine("line {0,3}:  semantic error - cannot convert to int (use convert operator)",@1.StartLine);
                        ++Compiler.errors;
                    } 
                    else if (Compiler.symbolTable[$1]=="double" && $3 == 'b')
                    {
                        Console.WriteLine("line {0,3}:  semantic error - cannot convert to double (use convert operator)",@1.StartLine);
                        ++Compiler.errors;
                    }
                    else if (Compiler.symbolTable[$1]=="bool" && $3 != 'b')
                    {
                        Console.WriteLine("line {0,3}:  semantic error - cannot convert to bool (use convert operator)",@1.StartLine);
                        ++Compiler.errors;
                    }
                    else
                    {
                        $$ = $3;
                        if (Compiler.symbolTable[$1]=="double" && $3 =='i')
                        {
                            Compiler.EmitCode("conv.r8");
                            $$ = 'd';
                        }
                        vari = $1;
                        Compiler.EmitCode("stloc {0}", $1);
                        
                    }
               }
            } 
          ;
myAnd     : expRel
            {
                $$ = $1;
                pom++;
                Compiler.EmitCode("brtrue licz{0}", pom);
                Compiler.EmitCode("ldc.i4 0");
                Compiler.EmitCode("br nielicz{0}", pom2 + 1);
                Compiler.EmitCode("licz{0}:", pom);
                Compiler.EmitCode("ldc.i4 1");
            }
            ;
myOr      : expRel
            {
                $$ = $1;
                pom++;
                Compiler.EmitCode("brfalse licz{0}", pom);
                Compiler.EmitCode("ldc.i4 1");
                Compiler.EmitCode("br nielicz{0}", pom2 + 1);
                Compiler.EmitCode("licz{0}:", pom);
                Compiler.EmitCode("ldc.i4 0");
            }
           ;
expLog    : myAnd And expLog
            {
                if ($1 != 'b' || $3 != 'b')
                {
                    Console.WriteLine("line {0,3}:  semantic error - && operator can be used to bool arguments",@1.StartLine);
                    ++Compiler.errors;
                }
                else
                {
                    Compiler.EmitCode("and");
                    $$ = 'b';
                }
            }
            | myOr Or expLog
            {
                if ($1 != 'b' || $3 != 'b')
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
                    Compiler.EmitCode("ceq");
                    Compiler.EmitCode("neg");
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
                    Compiler.EmitCode("ldc.i4 1");
                    Compiler.EmitCode("sub");
                    Compiler.EmitCode("cgt");
                    $$ = 'b';
                }
            }
            | expRel Less exp
            {
                Compiler.EmitCode("clt");
                $$ = 'b';
            }
            | expRel LessEqual exp
            {
                Compiler.EmitCode("ldc.i4 1");
                Compiler.EmitCode("add");
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
log       : log SumLog log
               { $$ = BinaryOpGenCode(Tokens.SumLog, $1, $3, @1.StartLine); }
          | log IlLog log
               { $$ = BinaryOpGenCode(Tokens.IlLog, $1, $3, @1.StartLine); }
          | factor
               { $$ = $1; }
          ;
factor    : OpenPar expLog ClosePar
               { $$ = $2; Compiler.EmitCode("nielicz{0}:", ++pom2); }
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
               if (!Compiler.symbolTable.ContainsKey($1)) 
               {
                    Console.WriteLine("line {0,3}: error - use of undeclared variable", @1.StartLine);
                    Compiler.errors++;
               }
               else
               {
                   Compiler.EmitCode("ldloc {0}", $1);
                   switch(Compiler.symbolTable[$1])
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
string temp3;
int deeplevel = 0;
int deeplevelElse = 0;
string vari;
int pom = 0;
int pom2 = 0;

public Parser(Scanner scanner) : base(scanner) { }

private char BinaryOpGenCode(Tokens t, char type1, char type2, int line)
{
    char type = (type1=='i' && type2=='i') ? 'i' : 'd' ;
    if (type1 != type)
    {
        Compiler.EmitCode("stloc _temp");
        Compiler.EmitCode("conv.r8");
        Compiler.EmitCode("ldloc _temp");
    }
    if (type2 != type)
        Compiler.EmitCode("conv.r8");
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
    return type;
}