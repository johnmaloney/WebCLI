# To Do & Goals

1. Get the CommandRepo Initializer to load a few commands and queries 
2. I want to develop a way to inject new command and queries by injecting code snippets

## Need to Research
1. Need to build out all the IPipe instances and contracts so that 
we can instantiate using reflection and then execute a PipeLine 
with those initialized objects


## What makes up a command or a query
1. Context
2. Environmental Variables
3. Instructions
4. Results



public void AddContextInitializer(string pipeName, string description, 
    Func<IPipe> pipeInitializer, 
    Func<string, object, string[], Dictionary<string, object>, IPipeContext> contextInitializer)
{
    commandDescriptions.Add($"{pipeName} - {description}");

    this.pipelineInitializers.Add(pipeName,
        new PipelineInitializer
        {
            PipeInitializer = () => new PreProcessor().ExtendWith(pipeInitializer()),
            ContextInitializer = contextInitializer
        });
}

public IPipeContext ProduceContext(string commandName, object data, string[] arguments, Dictionary<string, object> options)
{
    var argumentsWithCommand = new List<string>(arguments);
    argumentsWithCommand.Add($"cmd:{commandName}");
                       
    var contextProducer = this.pipelineInitializers[commandName];
    var context = contextProducer.ContextInitializer(commandName, data, argumentsWithCommand.ToArray(), options);
                        
    return context;
}