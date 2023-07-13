[Home](../home.md) - [Sapling 0.1.2 Documentation](./doc.md)

# Sapling Programming Language 0.1.2 Expressions
| Expression Type   | Description | Example  |
| ----------------- | ----------- | ----------- |
| Identifier        | This type of expression uses an identifier to access a value stored in that variable. | x, y, my_variable |
| Literal           | This expression just directly represents its value. | true, -20, "Hello World" |
| Ternary           | This type of expression allows us to base its value on a condition. We get one value if the condition returns true, and another if the condition returns false. | true ? 1 : 0, my_bool ? v_if_true : v_if_false |
| Optree            | An optree allows us to perform operations on expressions, and also allows us to compare expressions. | 5 + 5, 3 * 4 * 9, x + y * z, 5 == 7 - 2 |
| Paren Expression  | Parenthesis can be useful to deal with complicated order of operation issues, or just to make code more readable. They simply evaluate to their inner expression. | (my_expression) |
| Method Expression | This type of expression allows us to call a method defined elsewhere in our code, with specified parameters and get its return value. | my_func(x), my_func(310, false) |
| More Complicated  | This is an example of a more complicated expression, which combines multiple of the above expressions. | (y - 3) == my_func(4) ? 35 : x |

# Disclaimer: Methods and Classes
While methods and classes in Sapling are technically expressions, it is generally not useful to think of them as such. 

Classes define a structure which can be used to create new instances of complicated structs and associated methods. 

Methods simply define a collection of statements that will be executed on some parameters to return a value.

Methods are implemented in Sapling 0.1.2 in a very limited manner. Classes are not yet implemented.