namespace ToDo
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }
}
