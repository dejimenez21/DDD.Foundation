# Djimenez.DDD.Foundation
Djimenez.DDD.Foundation is a NuGet package providing a set of foundational classes for implementing Domain-Driven Design (DDD). 
The package includes base classes for Entities, Value Objects, and Enumerations. It also introduces a mechanism for error 
handling through the Result and Error classes.

## Content
The package contains the following classes, grouped by their folders:

### Domain
- `Entity`: An abstract base class for DDD entities with a unique identifier of type `Guid`.
- `ValueObject`: An abstract base class for DDD value objects. It overrides `Equals`, `GetHashCode`, and operators for correct equality comparison.
- `Enumeration`: An abstract base class for creating enumeration classes that allow you to handle enumerated values in a DDD context.

### Results
- `Error`: Represents an error with a status code, an error code, and a message. Errors can be nested using the `Reasons` property.
- `Result`: A class for returning operation results which can either be successful or fail with an `Error`.
- `Result<TValue>`: A derivative of `Result` for operations returning a value.

## Installation
To install the package, run the following command in the Package Manager Console:

```shell
Install-Package Djimenez.DDD.Foundation
```

or, if you prefer to use the dotnet CLI:

```shell
dotnet add package Djimenez.DDD.Foundation
```

## Usage

Here are some basic examples on how to use the classes in the package.

### Entity
```csharp
public class User : Entity
{
    public User(Guid id)
    {
        Id = id;
    }
}

var user1 = new User(Guid.NewGuid());
var user2 = new User(user1.Id);
Console.WriteLine(user1 == user2); // Prints: True
```

### ValueObjext
```csharp
public class Address : ValueObject
{
    public string Street { get; }
    public string City { get; }

    public Address(string street, string city)
    {
        Street = street;
        City = city;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Street;
        yield return City;
    }
}

var address1 = new Address("Main St", "NYC");
var address2 = new Address("Main St", "NYC");
Console.WriteLine(address1 == address2); // Prints: True
```

### Enumeration
```csharp
public class Color : Enumeration
{
    public static readonly Color Red = new Color("Red");
    public static readonly Color Blue = new Color("Blue");

    private Color(string name) : base(name) { }
}

var red = Color.FromName<Color>("Red");
Console.WriteLine(red); // Prints: Red
```

### Error and Result
```csharp
Error error = new Error(404, "not_found", "User not found.");
Result failResult = Result.Fail(error);

if (failResult.IsFailed)
{
    Console.WriteLine(failResult.Error.Message); // Prints: User not found.
}

Result<User> successResult = Result.Success(new User(Guid.NewGuid()));

if (successResult.IsSuccess)
{
    Console.WriteLine(successResult.Value.Id); // Prints: The Id of the user.
}

```

> These are just basic examples. Depending on your DDD model, you can extend these classes according to your needs.

## License

This project is licensed under the terms of the MIT license.
