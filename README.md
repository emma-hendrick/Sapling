# Sapling Programming Language V. 0.1.1

Hello all, welcome to the sapling programming language. Once this is in a working capacity it is intended to be an object based language which allows for effortless error handling.

Sapling is a general-purpose, statically-typed, pure, functional programming language.

## Release Notes

### 0.0.0

Setting up Classes for all Tokens which will be used within Sapling

### 0.0.1

Compilation of some testing code works; AST not yet implemented

### 0.1.0

Parser, AST, and Code Generation implemented for most simple expressions and statements.
Things we can parse:
- Method Assignment
- Methods
- Class Assignment
- Classes
- Property Assignment
- Expressions
- Literal Expressions
- Identifier Expressions
- Paren Expressions
- Ternary Expressions
- Optrees
- Operators
- Return Statements

Things we can generate code for:
- Property Assignment
- Expressions
- Literal Expressions
- Identifier Expressions
- Paren Expressions
- Ternary Expressions
- Optrees
- Operators
- Return Statements

Compilation is also functional!!

### 0.1.1

- We can now use BIFs within the Sapling Programming Language.   
    These currently include printf and getchar.
- Fixed some small bugs within the optree, parser, and token precedence.

### 0.1.2

Mostly just patches and bug fixing. 
- Nested expressions now evaluate correctly (although the only way to negate an expression is still to multiply it by -1). 
- Improved Sapling Compiler error messages slightly, although they are still pretty bad.
- Added BIF flush_stdin to allow the user to get more than one char in the lifetime of a program.
- Sapling run and test output is now printed correctly.
- The Sapling Programming Language now has 6 basic commands - documented in the sapling guide.

---

**Enjoy!**

### [Installation Guide](Guide/installation.md)
### [Sapling Guide](Guide/home.md)