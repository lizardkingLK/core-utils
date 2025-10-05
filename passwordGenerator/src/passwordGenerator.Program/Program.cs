using passwordGenerator.Core;

namespace passwordGenerator.Program;

class Program
{
    static void Main(string[] args) => Generator.Generate(args).Execute();
}
