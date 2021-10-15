using CommandLine;
using RedditDotNet.ConsoleApp.Verbs;
using System;
using System.Linq;

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
		static void Main(string[] args)
		{
			Parser.Default.ParseArguments(args, GetVerbTypes(typeof(VerbBase))).WithParsed((parsed) =>
			{
				if (parsed is VerbBase verb)
				{
					Console.WriteLine(verb.Run());
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
