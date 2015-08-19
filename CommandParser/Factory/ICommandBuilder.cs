namespace CommandParser
{
    //Abstract factory
    interface ICommandBuilder
    {   
        Command Create(string flag);
    }
}
