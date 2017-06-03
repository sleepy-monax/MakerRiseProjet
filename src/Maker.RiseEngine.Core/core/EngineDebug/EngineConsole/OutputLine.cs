namespace Maker.RiseEngine.Core.EngineDebug.EngineConsole
{
    enum OutputLineType
    {
        Command,
        Output,
        Output_Info,
        Output_Warning,
        Output_Error
    }

    class OutputLine
    {
        public string Output { get; set; } = "";
        public OutputLineType Type { get; set; }

        public OutputLine(string output, OutputLineType type)
        {
            Output = output;
            Type = type;
        }

        public override string ToString()
        {
            return Output;
        }
    }
}
