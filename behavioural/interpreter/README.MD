Interpreter design pattern

Intent

* You have multiple objectives that exist in a well-defined and well-understood domain

* The objectives can be expressed with a simple language

Benefits

* The domaine specific language provides a very compact representation for queries, commands or business rules in the given domain

* The interpreter hides the complexity of the domain from the rest of the application architecture

Disavantages

* There is a risk that you are writing what appears to be an elegant and compact language expression, but that the low-level implementation of that expression is overly complex and does not perform well