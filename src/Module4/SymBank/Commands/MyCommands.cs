namespace SymBank.Commands
{
    public static class MyCommands
    {
        public static readonly OpenTextEditorCommand OpenTextEditor =
            new OpenTextEditorCommand();

        public static readonly ExitAppCommand ExitApp =
            new ExitAppCommand();
    }
}
