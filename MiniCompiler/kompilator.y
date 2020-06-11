
// Uwaga: W wywołaniu generatora gppg należy użyć opcji /gplex

%namespace GardensPoint

%union
{
public string  val;
public char    type;
}

%token Assign Plus Minus Multiplies Divides 
%token Program Return Eof Error 
%token If Else While
%token Read Write
%token Int Double Bool
%token True False
%token OpenBracket CloseBracket Semicolon OpenPar ClosePar
%token Equal NotEqual Greater GreaterEqual Less LessEqual
%token <val> Ident IntNumber RealNumber

%type <type> code stat exp term factor declare bool cond while

%%
start    : Program OpenBracket code CloseBracket 
           {
               //Compiler.EmitCode("// linia {0,3} :  "+Compiler.source[lineno-1],lineno);
               Compiler.EmitCode("ldstr \"\\nEnd of execution\\n\"");
               Compiler.EmitCode("call void [mscorlib]System.Console::WriteLine(string)");
               Compiler.EmitCode("");
               YYACCEPT;
           }
               Eof 
           ;
code     : code stat { ++lineno; }
          | stat { ++lineno; }
          ;
stat      : write | assign | declare | while | block | cond
          | error
          {
               Console.WriteLine("  line {0,3}:  syntax error",lineno);
               ++Compiler.errors;
               yyerrok();
          }
          | error Eof
          {
               Console.WriteLine("  line {0,3}:  syntax error",lineno);
               ++Compiler.errors;
               yyerrok();
               YYACCEPT;
          }
          ;
block     : OpenBracket code CloseBracket
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
            OpenPar bool ClosePar 
            { 
                if (deeplevel == 1)
                    temp2 = Compiler.NewTemp();
                Compiler.EmitCode("brfalse {0}", temp2 + "_" + deeplevel.ToString()); 
            }
            stat
            { 
                Compiler.EmitCode("br {0}", temp + "_" + deeplevel.ToString());
                Compiler.EmitCode("{0}:", temp2 + "_" + deeplevel.ToString());
                deeplevel--;
            }
          ;
ifhead     : If OpenPar bool ClosePar
            {
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
               
                if (deeplevel == 1)
                    temp2 = Compiler.NewTemp();
                Compiler.EmitCode("br {0}", temp2 + "_" + deeplevel.ToString());
                Compiler.EmitCode("{0}:", temp + "_" + deeplevel.ToString());
            }
            stat
            {
                Compiler.EmitCode("{0}:", temp2 + "_" + deeplevel.ToString());
                deeplevel--;
            }
            ;
bool      : exp Equal exp 
            {
                Compiler.EmitCode("ceq");
            }
            | exp NotEqual exp
            {
                Compiler.EmitCode("ceq");
                Compiler.EmitCode("neg");
            }
            | exp Greater exp
            {
                Compiler.EmitCode("cgt");
            }
            | exp GreaterEqual exp
            {
                
            }
            | exp Less exp
            {
                Compiler.EmitCode("clt");
            }
            | exp LessEqual
            {
            }
            | True 
            | False
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
                    Console.WriteLine("line {0,3}:  variable already declared!", lineno);
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
                    Console.WriteLine("line {0,3}:  variable already declared!", lineno);
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
                    Console.WriteLine("line {0,3}:  variable already declared!", lineno);
                    Compiler.errors++;
                }
            }
            ;
write     : Write
            {
               //Compiler.EmitCode("// linia {0,3} :  "+Compiler.source[lineno-1],lineno);
               Compiler.EmitCode("ldstr \"  Write: {0}\"");
            }
            exp Semicolon
            {
               Compiler.EmitCode("box [mscorlib]System.{0}",$3=='i'?"Int32":"Double");
               Compiler.EmitCode("ldstr \"{0}\"",$3=='i'?"i":"r");
               Compiler.EmitCode("call void [mscorlib]System.Console::WriteLine(string,object,object)");
               Compiler.EmitCode("");
            }
          ;
assign    : Ident Assign exp Semicolon
            {
               if (!Compiler.symbolTable.ContainsKey($1)) 
               {
                    Console.WriteLine("line {0,3}: error - use of undeclared variable", lineno);
               }
               else
               {
                    if (Compiler.symbolTable[$1]=="int" && $3 != 'i')
                    {
                        Console.WriteLine("line {0,3}:  semantic error - cannot convert double to int",lineno);
                        ++Compiler.errors;
                    }   
                    else
                    {
                        Compiler.EmitCode("stloc {0}", $1);
                    }
               }
            } 
          ;
exp       : exp Plus term
               { $$ = BinaryOpGenCode(Tokens.Plus, $1, $3); }
          | exp Minus term
               { $$ = BinaryOpGenCode(Tokens.Minus, $1, $3); }
          | term
               { $$ = $1; }
          ;

term      : term Multiplies factor
               { $$ = BinaryOpGenCode(Tokens.Multiplies, $1, $3); }
          | term Divides factor
               { $$ = BinaryOpGenCode(Tokens.Divides, $1, $3); }
          | factor
               { $$ = $1; }
          ;

factor    : OpenPar exp ClosePar
               { $$ = $2; }
          | IntNumber
          {
               Compiler.EmitCode("ldc.i4 {0}",int.Parse($1));
               $$ = 'i'; 
          }
          | RealNumber
          {
               double d = double.Parse($1,System.Globalization.CultureInfo.InvariantCulture) ;
               Compiler.EmitCode(string.Format(System.Globalization.CultureInfo.InvariantCulture,"ldc.r8 {0}",d));
               $$ = 'd'; 
          }
          | Ident
          {
               Compiler.EmitCode("ldloc {0}", $1);
               $$ = Compiler.symbolTable[$1] == "int" ? 'i' : 'd';
          }
          ;

%%

int lineno = 1;
string temp;
string temp2;
int deeplevel = 0;

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