
%using QUT.Gppg;
%namespace GardensPoint

IntNumber   ([1-9][0-9]*)|0
RealNumber  ([1-9][0-9]*\.[0-9]+)|(0\.[0-9]+)
Ident		(("@"|"$")[a-z])|(([a-zA-Z])[0-9a-zA-Z]*)
PrintErr    "print"("@"|"$"|[a-z0-9])[a-z0-9]*

%%
"print"       { return (int)Tokens.Print; }
"exit"        { return (int)Tokens.Exit; }
"program"	  { return (int)Tokens.Program; }
"if"		  { return (int)Tokens.If; }
"else"  	  { return (int)Tokens.Else; }
"while"	      { return (int)Tokens.While; }
"read"	      { return (int)Tokens.Read; }
"write"  	  { return (int)Tokens.Write; }
"return"	  { return (int)Tokens.Return; }
"int"   	  { return (int)Tokens.Int; }
"double"	  { return (int)Tokens.Double; }
"bool"	      { return (int)Tokens.Bool; }
"true"  	  { return (int)Tokens.True; }
"false"	      { return (int)Tokens.False; }
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
"{"			  { return (int)Tokens.OpenBracket; }
"}"			  { return (int)Tokens.CloseBracket; }
";"			  { return (int)Tokens.Semicolon; }
"\r"          { }
<<EOF>>       { return (int)Tokens.Eof; }
" "           { }
"\t"          { }
{PrintErr}    { return (int)Tokens.Error; }
.             { return (int)Tokens.Error; }
