[Home](./home.md) - [Installation](./installation.md)

# Table of Contents

- [How to start coding in Sapling](#how-to-start-coding-in-sapling)

    - [Documentation by Sapling Version](#documentation-by-sapling-version)

    - [Expressions](#expressions)

        - [What is an expression?](#what-is-an-expression)

        - [Expressions by Sapling Version](#expressions-by-sapling-version)

    - [Statements](#statements)

        - [What is a statement?](#what-is-a-statement)

        - [Statements by Sapling Version](#statements-by-sapling-version)

    - [BIFs](#bifs)

        - [What is a BIF?](#what-is-a-bif)

        - [What if the BIF I need doesn't exist](#what-if-the-bif-i-need-does-not-exist)

        - [Bifs by Sapling Version](#bifs-by-sapling-version)

# How to start coding in Sapling
Sapling, like many other programming languages is built out of many small and simple building blocks. Understanding these building blocks is key to understanding a programming language.

## Documentation by Sapling Version
- [0.1.2](./0.1.2/doc.md)

## Expressions

### What is an expression?
An expression in a programming language represents some kind of value. 5 could be an expression. 2 + 2 would also be an expression (which would of course, evaluate to 4). Even "Hello World" is an expression, albeit, a different type.

In general, think to yourself, does this represent some kind of data? If it does you know it is an expression. Otherwise, it would be an expression (covered below).

### Expressions by Sapling Version
- [0.1.2](./0.1.2/expressions.md)

## Statements

### What is a statement?
A statement in a programming language represents an instruction to do something. Some types of statements you might see in a programming language could be an instruction for the computer to print a value to the terminal, to store a value with an identifier, or to return a value from the program.

One tricky exception to this could be a function which does something and then returns a value. This can function as either a statement or an expression. If the value is simply discarded, you can think of these as statements. Otherwise, it is best to think of them as expressions.

### Statements by Sapling Version
- [0.1.2](./0.1.2/statements.md)

## BIFs

### What is a BIF?
A bif is a built in function in Sapling that does a task that would usually be impossible for a user to program in themselves. These can include things such as printing to the terminal, getting user input from the terminal, flushing the input buffer, or many other things.

### What if the BIF I need does not exist
If the BIF you need does not exist feel free to create an issue on our github page documenting the BIF which you need and what you need it for, and if it does not exist, and there is no other way to solve your problem, we will work on adding it in a future Sapling release.

### BIFs by Sapling Version
- [0.1.2](./0.1.2/bifs.md)


