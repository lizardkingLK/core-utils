using wordSearch.Core;

namespace wordSearch.Program;

class Program
{
    static void Main(string[] arguments)
    => WordSearch.Search(
        Console.IsInputRedirected,
        Console.In,
        arguments);
}
