# SharpE2
This is a C# expression engine. The engine can convert string based simple mathematical expression to a value or a simplified expression.

This class library works as follow:

"1 + 2 + 3" = "6"

"2 * 10 % 3" = "2"

# Installation
### Using Visual Studio
Checkout the project and open SharpE2.csproj. You will be able to build the project with the target configuration of your choice.

### Using MS Build
TBD

# Usage
Link/reference the .dll to your project and include `using SharpE2;`

```csharp
E2Simplifier simplifier = new E2Simplifier();

string output = simplifier.Simplify("5 + 6 * 3 + 5 ^ 5");

/* output will be 3148 as a string */
```

# Supported Syntax
### Values
- Decimal
- Octal
- Hexadecimal
- Binary
### Operators
The current supported operators are:
- addition (+)
- subtraction (-)
- multiplication (*)
- division (/)
- exponent (^)
- modulo (%)
### Variables
Variables name are supported. The current standard is any letters and the '_' character.

For example:
- x
- _abc
- x_x
- ABC
