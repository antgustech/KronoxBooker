namespace Bot
{
    /// <summary>
    /// Creates a new Bot instance and calls it's run method to start.
    /// </summary>
    internal class Start
    {
        private static void Main(string[] args)
        {
            new Bot().Run();
        }
    }
}