# LinqOnSteroids
A small library which allows to share code snippets of Linq2Entity Snippet between queries.

This is a very early release and is therefore not recommended for production use.
Most stuff should already work out of the box, but some bugs are to be asumed. 
Please send any problems with the library to me, I will update and fix it as soon as possible.

# Overview
An often encountered problem when using Linq is the inability to share query snippets. 
So often used snippets have to written again and again which leads to duplicate code all over the applications.

LinqOnSteroids tries to solve this problem with the concept of ExpandableQuery, a concept which is lent by [LinqKit](https://github.com/scottksmith95/LINQKit).
An expandable query allows the developer to pass any kind of expression into the query, no matter if the expression is a simple variable access or complex hirarchy of method calls and member accesses. 
Expressions can also be merged and combined together which ultimatly allow the developer to reused query snippets in application, wherever he sees fit.

# Usage
TBD

# Planned Features
* Object Composition
Beeing able to load database entities only partial to improve performance
* Precompiled Queries
The ability to precompile queries so that often used queries don't have to be expanded on every call
* Query composition
Combine multiple queries to be executed in a single query

# Mentions
This project is strongly influenced by [LinqKit](https://github.com/scottksmith95/LINQKit) but takes it a step further. Especially the concept and implementation of the ExpandableQuery is heavily influenced by LinqKit.

# License 
LinqOnSteroids is published under the MIT license. 