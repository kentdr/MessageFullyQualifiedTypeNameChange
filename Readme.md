### Option One

Option One is the "preferred option".  It uses a shared Net Standard 2.0 class library for the message objects.  This will allow NServiceBus to "just work" as it will agree on the fully qualified class name. The downsides to this approach is the Receiver will need to be modified to reference the .NET Standard class library.  All endpoints using the shared library will be forced to use a [Conventions approach](https://docs.particular.net/nservicebus/messaging/conventions) due to the different versions of NServiceBus being used.

### Option Two

Option two uses an [Inbound Mutator](https://docs.particular.net/nservicebus/pipeline/message-mutators?) to change the `NServiceBus.EnclosedMessageTypes` header on all messages as they are being processed by the transport.  

This will properly orient the parser to use the correct class when it attempts to deserialize the message. This approach alleviates the need to have a shared library and does not require the use of Conventions. It does require that the Receiver program be modified	slightly to register and implement the Inbound Mutator.

### Option Three

Option Three can be considered the "least preferred" option.  It uses an Outbound Mutator exactly how Option Two uses an Inbound Mutator. 

In this case, the Receiver project does not have to change at all because the header is being managed when messages are sent. Unfortunatly, the fully qualified type information becomes a "magic string" and will need to be copied to every sender that needs to send these messages. Additionally, the string will need to be updated if Receiver ever changes its internals, any messages sent prior to being updated will need to be manually changed and requeued.
