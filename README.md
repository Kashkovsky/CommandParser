# CommandParser Csharp
Task solution for Kottans by Dan Kashkovsky

###Extrensibility:

To add a new command:
  1. Create a new Class that inherits Command (abstract). 
  2. Add a new command to the dictionary of CommandBuilder. 
  3. Enjoy.
  
  PS: no need to update Help or any other file.

### How does it work

Program:
ReadArgs() reads a string of user's arguments and splits it into an array.
Process(string[] args) analyzes items of an array from ReadArgs(), for each command item instantiates Flag or passes an arguments to a Flag if it can take them.

Flag:
Flag asks CommandBuilder to create and instance of Command class and asks the instance to Do() it's work.

CommandBuilder (implements ICommandBuilder):
CommandBuilder has a dictionary of all the commands available.
Create(string flag) receives the name of command to create, searches for the apropriate Key in the dictionary and returns corresponding value (a new instance of a Command inheriting class).
GetCommand(string command) - looks for requested command in CommandBuilder's dictionary and in case of success returns value.toString.
GetCommands() sorts entire dictionary and returns the table <name> - <description>

Command classes (inherit abstract class Command):
overrides Command's void Do()

