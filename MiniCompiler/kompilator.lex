
%using QUT.Gppg;
%namespace GardensPoint

IntNumber   ([1-9][0-9]*)|0
RealNumber  ([1-9][0-9]*\.[0-9]+)|(0\.[0-9]+)
Ident		([a-zA-Z])[0-9a-zA-Z]*
String      \"[^'\"'\n]*[^'\'\n]*\"
PrintErr    "print"("@"|"$"|[a-z0-9])[a-z0-9]*
Comment     "//"[^\n]*

%%
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
{Comment}     { }
{IntNumber}   { yylval.val=yytext; return (int)Tokens.IntNumber; }
{RealNumber}  { yylval.val=yytext; return (int)Tokens.RealNumber; }
{Ident}       { yylval.val=yytext; return (int)Tokens.Ident; }
{String}      { yylval.val=yytext; return (int)Tokens.String; }
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
"=="		  { return (int)Tokens.Equal; }
"!="		  { return (int)Tokens.NotEqual; }
">"			  { return (int)Tokens.Greater; }
">="		  { return (int)Tokens.GreaterEqual; }
"<"			  { return (int)Tokens.Less; }
"<="		  { return (int)Tokens.LessEqual; }
"&&"		  { return (int)Tokens.And; }
"!"		      { return (int)Tokens.Exclamation; }
"||"		  { return (int)Tokens.Or; }
"~"			  { return (int)Tokens.Neg; }
"|"			  { return (int)Tokens.SumLog; }
"&"			  { return (int)Tokens.IlLog; }
"(int)"		  { return (int)Tokens.IntConv; }
"(double)"    { return (int)Tokens.DoubleConv; }
"\r"          { }
<<EOF>>       { return (int)Tokens.Eof; }
" "           { }
"\t"          { }
{PrintErr}    { return (int)Tokens.Error; }
.             { return (int)Tokens.Error; }

%{
  yylloc = new LexLocation(tokLin, tokCol, tokELin, tokECol);
%}