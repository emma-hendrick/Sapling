[Home](../home.md) - [Sapling 0.1.2 Documentation](./doc.md)

# Sapling Programming Language 0.1.2 Statements
| Statement Type  | Description                                                      | Example                             |
| --------------- | ---------------------------------------------------------------- | ----------------------------------- |
| Assign Property | Assign an expression to an identifier.                           | int x = 5; str my_name = "Michael"; |
| Assign Method   | Assign a method to an identifier.                                | method s = {return "s";}            |
| Call Method     | Call a method using its identifier.                              | s(); printf("Hello World!");        |
| Assign Class    | Assign a class to an identifier.                                 | Not yet implemented                 |
| Return          | Return a value from a method or from the top level of a program. | return my_expression;               |

# Disclaimer: Methods and Classes
While methods and classes in Sapling are technically expressions, it is generally not useful to think of them as such. 

Classes define a structure which can be used to create new instances of complicated structs and associated methods. 

Methods simply define a collection of statements that will be executed on some parameters to return a value.

Methods are implemented in Sapling 0.1.2 in a very limited manner. Classes are not yet implemented.