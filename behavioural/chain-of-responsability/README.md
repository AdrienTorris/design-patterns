Chain Of Responsability

Intent

* You are unable to match the sender of a request to its receiver at compile time

* You want to map requests to multiple receivers

* You want a request-and-forget architecture with a processing pipeline containing many possible request receivers

Benefits

* 1-and-1 mappings between senders and receivers are not needed

* Receivers can be configured dynamically at runtime

* More than one receiver can handle a request

* Senders do not need to keep references to each receiver

Disavantages

* There is a small performance penalty because each request needs to be passed down the chain of receivers

* The pattern increases the chance of unhandled requests

Notes

* The Chain Of Responsability pattern is part of a group of patterns that it shares with the Command, Mediator and Observer patterns. These patterns allows to decouple the sender to the receiver.

* You can combine the Chain Of Responsability with the Command pattern.