int x = 2;

# Ensure that this optree is parsed correctly
int y = 5 * x * x - 7 * x + 3;

# Test our ternary operator
return y == 9 ? 0 : -1;