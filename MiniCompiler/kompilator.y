
// Uwaga: W wywołaniu generatora gppg należy użyć opcji /gplex

%namespace GardensPoint

%union
{
public string  val;
public char    type;
}

%token Print Exit
%token Assign Plus Minus Multiplies Divides 
%token Program Return  Eof Error 
%token If Else While
%token Read Write
%token Int Double Bool
%token True False
%token OpenBracket CloseBracket Semicolon OpenPar ClosePar
%token <val> Ident IntNumber RealNumber

%type <type> code stat exp term factor declare

%%
start    : Program OpenBracket code CloseBracket 
                {
               Compiler.EmitCode("// linia {0,3} :  "+Compiler.source[lineno-1],lineno);
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

stat      : print | assign | declare
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
declare   : Int Ident Semicolon
            {
                Compiler.EmitCode(".locals init ( int32 i{0} )", $2);
                Compiler.EmitCode("ldc.i4 0");
                Compiler.EmitCode("stloc i{0}", $2);
            }
            | Double Ident Semicolon
            {
                Compiler.EmitCode(".locals init ( float64 f{0} )", $2);
                Compiler.EmitCode("ldc.r8 0");
                Compiler.EmitCode("stloc f{0}", $2);
            }
            | Bool Ident Semicolon
            {
                Compiler.EmitCode(".locals init ( int32 b{0} )", $2);
                Compiler.EmitCode("ldc.i4 0");
                Compiler.EmitCode("stloc b{0}", $2);
            }
            ;
print     : Print
               {
               Compiler.EmitCode("// linia {0,3} :  "+Compiler.source[lineno-1],lineno);
               Compiler.EmitCode("ldstr \"  Result: {0}{1}\"");
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
               if ( $1[0]=='@' && $3!='i' )
                   {
                   Console.WriteLine("  line {0,3}:  semantic error - cannot convert real to int",lineno);
                   ++Compiler.errors;
                   }
               else
                   {
                   if ( $1[0]=='$' && $3!='r' )
                       Compiler.EmitCode("conv.r8");
                   Compiler.EmitCode("stloc _{0}{1}", $1[0]=='@'?'i':'r', $1[1]);
                   Compiler.EmitCode("");
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
               $$ = 'r'; 
               }
          | Ident
               {
               Compiler.EmitCode("ldloc i{0}", $1);
               $$ = $1[0]=='@'?'i':'r';
               }
          ;

%%

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