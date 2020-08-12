# MiniCompiler
Compiler for my own programming language.
# Language specification:

## Language elements:
Language contains following terminals:
- keywords: **program if else while read write return int double bool true false**
- operators and special symbols: **= || && | & == != > >= < <= + - * / ! ~ ( ) { } ;**
- identifiers and numbers

**Identifier** is string containing letters and digits, starting with letter. Both upper and lowercase letters are allowed and are distinguishable.
**Floating point number** is string of digits meaning integer part of a number, dot and fractional part. Starting zero is allowed only when it is the only digit of integer part.
**Integer number** is string of digits.

Additionally, all white characters (**spaces, tabulators, new lines**) are ignored, but separate elements.

Language allow **comments** starting with **//**. All characters in line after comment symbol are ignored.
**String** is any text in **" "** and can be used only in write instruction.

## Program
Program starts with **program** keyword followed by set of declarations and instructions in brackets.

## Declarations
Single declaration contains type, identifier and semicolon. Allowed types are: **int**, **double** and **bool**. All variables are initialized by default values (zero or false).

Use of undeclared variable causes compilation error with "undeclared variable" message.
All identifiers must be unique.

## Instructions
Language contains 7 instructions:
- block statement **{ code }**
- expression
- conditional statement
- while loop
- read
- write
- return
