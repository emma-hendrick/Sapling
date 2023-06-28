# A test file to test the lexer, parser, and compiler
int x = 5 + 5;

return x;

# We expect the lexer to return the following:
# type: int
# id: x
# assign: =
# number: 5
# operator: +
# number: 5
# delimeter: ;
# keyword: return
# id: x
# delimiter: ;

# We expect the parser to then create the following tree
#             class_main
#              fn_main
#       =               return
#    x     +               x
#   int   5 5

# We then expect LLVM to generate something similar to the following IR for the code
# ; ModuleID = 'builds/source.bc'
# source_filename = "root"
# target datalayout = "e-m:w-p270:32:32-p271:32:32-p272:64:64-i64:64-f80:128-n8:16:32:64-S128"
# target triple = "x86_64-w64-windows-gnu"

# define i32 @main() {
# main_entry:
#   %x = alloca i32, align 4
#   store i32 10, ptr %x, align 4
#   %loadedValue = load i32, ptr %x, align 4
#   ret i32 %loadedValue
# }