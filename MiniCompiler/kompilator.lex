
%using QUT.Gppg;
%namespace GardensPoint

IntNumber   [0-9]+
RealNumber  [0-9]+\.[0-9]+
Ident       ("@"|"$")[a-z]
PrintErr    "print"("@"|"$"|[a-z0-9])[a-z0-9]*

%%

"print"       { return (int)Tokens.Print; }
"exit"        { return (int)Tokens.Exit; }
{IntNumber}   { yylval.val=yytext; return (int)Tokens.IntNumber; }
{RealNumber}  { yylval.val=yytext; return (int)Tokens.RealNumber; }
{Ident}       { yylval.val=yytext; return (int)Tokens.Ident; }
"="           { return (int)Tokens.Assign; }
"+"           { return (int)Tokens.Plus; }
"-"           { return (int)Tokens.Minus; }
"*"           { return (int)Tokens.Multiplies; }
"/"           { return (int)Tokens.Divides; }
"("           { return (int)Tokens.OpenPar; }
")"           { return (int)Tokens.ClosePar; }
"\r"          { return (int)Tokens.Endl; }
<<EOF>>       { return (int)Tokens.Eof; }
" "           { }
"\t"          { }
{PrintErr}    { return (int)Tokens.Error; }
.             { return (int)Tokens.Error; }