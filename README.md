# CLinq

CLinq, short for Composable Linq, is a library which brings the ability to compose Linq2Entity queries at runtime and therefore the reusability of queries and the possibility to create complex queries at runtime, instead of always having to predefine them.

## Overview

An often encountered problem when using Linq is the inability to share query snippets.
Often used snippets have to written again and again which leads to duplicate code all over the applications.
It also means that for usecases, where the query depends on the state of the application, all required variations of a query that go above the state of variable and very simple control flows need to be predefined and applied at runtime.

CLinq to solve this problem with the concept of a ComposableQuery, which is lent by [LinqKit](https://github.com/scottksmith95/LINQKit)'s ExpandableQuery.
A composable query allows the developer to pass any kind of expression into the query, no matter if the expression is a simple variable access or complex hirarchy of method calls and member accesses.
Expressions can also be merged and combined together which ultimatly allow the developer to reused query snippets in application, wherever he sees fit.

## Example

### The Problem

Imagine a method which returns if an employee has too much overtime worked up and needs to relax more:

```csharp
bool IsOverworked(int employeeId)
{
    return employees.Where(e => e.Id == employeeId)
                    .First(e => e.Overtime >= 100);
}
```

This works and is perfectly fine, until you want want to build a page which displays a list of all overworked employees. Now you write another method like this:

```csharp
IQueryable<Employee> GetAllOverworkedEmployees()
{
    return employees.Where(e => e.Overtime >= 100);
}
```

Now we have another method which uses the same condition `e.Overtime >= 100` which confronts us with the same problems of duplicate code we've always know.
Changing the definition of overworked requires us to change every line of code, which checks for overworked.
How much easier would it be if we could just create a method

```csharp
bool IsOverworked(Employee e) => e.Overtime >= 100
```

and call it in the above examples

```csharp
bool IsOverworked(int employeeId)
{
    return employees.Where(e => e.Id == employeeId)
                    .First(e => IsOverworked(e));
}
```

```csharp
IQueryable<Employee> GetAllOverworkedEmployees()
{
    return employees.Where(e => IsOverworked(e));
}
```

But since Linq builds its queries based on [Expression Trees](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/expression-trees/) which get translated into SQL code (or whatever you need to query your data store), we cannot just pass a method to the query, since Linq would have no way of knowing how to translate that into SQL.

### The Solution

This is where CLinq comes into play.
CLinq allows us to define expressions, plug them into an existing Linq query and compose more complex queries based on smaller building blocks.

The first step is therefore to define an expression which can later be passed into the expression tree of the query.
So instead of the above method `IsOverworked` we define the following expression.

```csharp
Expression<Func<Employee, bool>> IsOverworkedExpression = e => e.Overtime >= 100;
```

Before we can use this query, we need to mark the used query as composable, which we do with the extension method `AsComposable` which can be used on every kind of `IQueryable`.

The next step is to bring this all together.
To use the expression and build a query out from it, we have to tell the composable query how that expression has to be called, and mainly on which parameters.
Basically we need to `Pass` the employee to the expression, before beeing able to make any calculation on it.
Doing all of this, and our methods from above look like this:

```csharp
bool IsOverworked(int employeeId)
{
    return employees.AsComposable()
                    .Where(e => e.Id == employeeId)
                    .First(e => IsOverworkedExpression.Pass(e));
}
```

```csharp
IQueryable<Employee> GetAllOverworkedEmployees()
{
    return employees.AsComposable()
                    .Where(e => IsOverworkedExpression.Pass(e));
}
```

And this is all that needs to be done.
In the background, CLinq now takes the composable query, analyzes it for all occurences of `Pass` and projects the underlying expression into the linq query before compiling it into SQL.

More complex scenarios are also possible.
Check out [Usage](#Usage) for more information.

## Usage

Make sure to have read the above example, before continuing this section.
Not all of the following examples make complete sense from a technical or business related standpoint, but are used to describe the basic concepts of CLinq.

### Expressions

You're completely free on how you want to define your expressions.
It's also possible to return expressions from methods, and even modify them based on your provided parameters.

#### Paremetrizing Expressions

You can define methods to return your expression and use the method parameters in the expression.
This even works with default parameter values

```csharp
Expression<Func<Employee, bool>> IsOverworkedExpression(int overworkedThreshold = 100)
{
    return e => e.Overtime > overtimeThresshold;
}
```

Now you can use this expression like before `e => IsOverworkedExpression().Pass(e)` or you can define your own threshold on the fly `e => IsOverworkedExpression(500).Pass(e)`, which leads to a different query everytime you call it.

#### Different Expressions based on state

You can als return completely different expression based on the parameters.
Say, you don't want to think about overworked employees during a company wide crisis:

```csharp
Expression<Func<Employee, bool>> IsOverworkedExpression(bool crisisMode)
{
    if (crisisMode)
        return e => false;
    else
        return e => e.Overtime > overtimeThresshold;
}
```

Of course you cannot only change your expressions based on parameters but basically on everything state related in your application.
E.g. you could return a different expression when the application is in Developer mode.
Some randomization during the dev mode for example?

```csharp
Expression<Func<Employee, bool>> IsOverworkedExpression()
{
    if (new Random().Next() % 2 == 0)
        return e => false;
    else
        return e => true;
}
```

#### Expressions with multiple parameters

Expressions are not limited to single parameters.

```csharp
Expression<Func<Employee, Employee, bool>> AreEmployeesTeamMembersExpression()
{
    return (e1, e2) => e1.TeamId == e2.TeamId;
}
```

This can be used straight forward, and as you can see you can even pass the provided employee into the expression

```csharp
IQueryable<Employee> GetTeamMembersOf(Employee employee)
{
    return employees.AsComposable()
                    .Where(e => AreEmployeesTeamMembersExpression().Pass(employee, e));
}
```

#### Combining Expressions

It's also possible to combine multiple expressions at runtime.
Lets assume the following expressions.

```csharp
Expression<Func<Employee, IEnumerable>> IsTeamMemberAndOverworked(Employee employee)
{
    return e => AreEmployeesTeamMembersExpression().Pass(e, employee)
                && IsOverworkedExpression().Pass(e);
}
```

Or maybe you want to Pass one expression into another expression?
Lets assume the following expression to get the superior of an employee:

```csharp
Expression<Func<Employee, Employee>> GetSuperiorExpression()
{
    return e => e.Superior;
}
```

Then we can create the following expression to check if a superior is overworked:

```csharp
Expression<Func<Employee, bool>> IsSuperiorOverworkedExpression()
{
    return e => IsOverworkedExpression().Pass(GetSuperiorExpression().Pass(e));
}
```

#### Calling methods to mofidy expression

It's even possible to call normal methods inside an expression.
In [Paremetrizing Expressions](#Paremetrizing Expressions) we passed a paremeter to the expression to change the behavior of the expression.
But we could also call a method at runtime which defines this overworkedThreshold at runtime

```csharp
Expression<Func<Employee, bool>> IsOverworkedExpression()
{
    return e => e.Overtime > Configuration.GetOverworkedThreshold();
}
```

The Method `Configuration.GetOverworkedThreshold()` will be evaluated before the expression is projected into the Linq query, and the returned value is used inside the query.

[comment]: # (## Under the hood)

## Hints and Drawbacks

[comment]: # (## Limitations)

## Performance Implications

When using CLinq one has to thing about the performance implications.
Using Linq has never been the fastest method to query databases, as the translation from an expression tree to an SQL query needs time.
CLinq introduces another step to this process, since the expression tree itself first needs to be modified, before it can be translated to SQL.
The time required for this depends on the complexity of the query itself, and how long it takes to evaluate all the methods that are used inside the query.

Generaly though, one can say that in use cases where Linq2Entity is fast enough, CLinq does not degrade the performance in a meaningful way and can most times be used without issues.

## Compiled Queries

One of the possibilities to mitigate performance issues while using Linq2Entity is the use of [Compiled Queries](https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/ef/language-reference/compiled-queries-linq-to-entities).
CLinq is fully compatible to Compiled Queries, but there is a small pitfall in play here.
When modifying a query at runtime

```csharp
Expression<Func<object, bool>> SampleExpression()
{
    if (Configuration.SomeValue)
        return o => true;
    else
        return o => false;
}
```

the compiled query is cached depending on the value of the parameter at the moment of query compilation, meaning later usages of the query will always use the originally cached query.
This can not be avoided should be kept in mind when working with compiled queries.

## Feedback and Feature Requests

I'm always glad about feedback.
If you come arround a bug using CLinq, please provide me a bug report and I fix as soon as possible.
Also, if there is some scenario which is not working at the moment with CLinq, provide a feature request and I'll see if it's possible to implement this scenario.

## Mentions

This project is strongly influenced by [LinqKit](https://github.com/scottksmith95/LINQKit) but takes it a step further. Especially the concept and implementation of the ComposableQuery is heavily influenced by LinqKit.

## License

CLinq is published under the MIT license.
