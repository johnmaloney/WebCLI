# WebCLI
This is a web based console that can be extended by adding project specific commands

## Goals
1. There is a core library that allows for a module library to add the following items:
    - Commands - loaded from a modules commands file, this will be the text commands entered in the UI
    - Command Metadata - data about the command including samples and documentation
    - Environments - differing environments to execute commands within e.g. prod, staging, dev
    - 

### Commands and Queries
#### Commands
Represent the actions that a user can execute to alter the system or the data stored in the runtime of the WebCLI. 
An example would be the altering of the current environment variables or authentication credentials.

#### Queries
Represent actions that the user would like to execute to retrieve that state of something, this can include
executing SQL, altering a set of text or retrieving data from a service endpoint.

##### Criteria
Crtieria in this application are used to define and generate an instance of a pipeline for the command or query being 
executed. This is data that will be used to build instructions to execute when the command is used in the system.

##### Context
Context is the object that we need to 