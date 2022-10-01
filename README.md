General Design
- The biggest goal with my design were to reduce repeated code as much possible and make the behaviour of each quotation system configurable and extendable.
- This was achieved by moving shared behaviour into common classes and have the client code depend on abstractions. This made it possible to unit test each layer.   
- Also i wanted any errors in the system to be captured neatly with appropriate messages returned to the user and exception details logged for developers/operations teams. 
- I changed the solution from .NetFramework to .NetCore as a couple of the nuget packages were not compatible with framework. I hope this is okay. 

Considerations
- I thought it would be useful to clients to be able to add additional quotation systems to the engine, hence having an add method rather than injecting the systems to the engine constructor. 
- I have a problem naming things to be clear without being overly verbose when unable to do both i stick on the side of descriptive. 
- I did not unit test the ReponseValidators and a couple of the other classes because they were so small i didn't think doing so would add much in terms of this exercise. I would always do so for real code. 


Areas For Improvement
- I know that in the third Quotation system i'm violating the interface segregation principle by giving it the RequestValidation interface which it is not really using. It would be the first thing i would fix. I think some
  type of builder pattern may be suitable for this. 
- The quotationsystem instances need to have their bindings resolved in a more elegant way. 
- Logging in the quotationsystem class to help trace errors. 
