using CommandLine;
using RedditDotNet.ConsoleApp.Verbs;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RedditDotNet.ConsoleApp
{
    /// <summary>
    /// Reddit command line application
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main function
        /// </summary>
        /// <param name="args">command line arguments</param>
        static async Task Main(string[] args)
        {
            await Parser.Default
                .ParseArguments(args, GetVerbTypes(typeof(VerbBase)))
                .WithParsedAsync(async (parsed) =>
                {
                    if (parsed is VerbBase verb)
                    {
                        Console.WriteLine(await verb.Run());
                    }
                    else
                    {
                        Console.Error.WriteLine($"Unexpected verb: {parsed.GetType()}");
                    }
                });
        }

        /// <summary>
        /// Get all types that are assignable from a given base type that are not abstract
        /// </summary>
        /// <param name="baseType">Base type to get types for</param>
        /// <returns>Array of types</returns>
        static Type[] GetVerbTypes(Type baseType)
        {
            var assembly = baseType.Assembly;
            return assembly.GetTypes().Where(_ => !_.IsAbstract && baseType.IsAssignableFrom(_)).ToArray();
        }
    }
}
