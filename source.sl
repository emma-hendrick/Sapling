# A test file to test the lexer and parser
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
#              fn_main
#       =               return
#    x     +               x
#   int   5 5

# We then expect LLVM to generate the following IR for the code
# define i32 @main() {
# entry:
#   %x = add i32 5, 5
#   ret i32 %x
# }