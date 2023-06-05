# A test file to test the lexer and parser
int x = 5 + 5;

# We expect the lexer to return the following:
# type: int
# id: x
# assign: =
# number: 5
# operator: +
# number: 5
# delimeter: ;

# We expect the parser to then create the following tree
#       =
#    x     +
#   int   5 5

# We then expect LLVM to generate the following IR for the code
#
#
#
#