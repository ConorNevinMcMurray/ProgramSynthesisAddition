using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.ProgramSynthesis;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compiler;
using Microsoft.ProgramSynthesis.Learning;
using Microsoft.ProgramSynthesis.Learning.Strategies;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Diagnostics;


namespace ProgSynthAdding
{
    class Program
    {               
        static void Main(string[] args)
        {
            //parse grammar file 
            var grammar = CompileGrammar();
            //configure the prose engine 
            var prose = ConfigureSynthesis(grammar.Value);

            //create the example
            var input = State.CreateForExecution(grammar.Value.InputSymbol, 1);
            int output = 4;
            var examples = new Dictionary<State, object> { { input, output } };
            
            var spec = new ExampleSpec(examples);          
                                 
            var scoreFeature = new RankingScore(grammar.Value);

            //IEnumerable<ProgramNode> best = learnedSet.TopK(scoreFeature, k: 1);

            var learnedSet = prose.LearnGrammar(spec);

            var programs = learnedSet.RealizedPrograms;

            //var bestPrograms = prose.LearnGrammarTopK(spec, scoreFeature, 1, null);
            
            //var programs = bestPrograms.RealizedPrograms;

            //run the first synthesized program in the same input and check if 
            //the output is correct
            //var programs = learnedSet.RealizedPrograms;

            var outputProgram = programs.First().Invoke(input) as int?;
            var fristProgram = programs.First().ToString();

            


            Console.WriteLine("using:");
            Console.WriteLine("An input example of: " + 1);
            Console.WriteLine("An output example of: " + outputProgram);
            Console.WriteLine("The following programs are synthesized: ");
            Console.WriteLine("");


           
            int programNumber = 0;

            foreach (var program in programs)
            {
                programNumber++;
                
                Console.WriteLine(program);  
                
                Console.WriteLine("");
                
            }
           
            Console.WriteLine(programNumber + " programs are synthesized");
            
            Console.ReadLine();
        }

        

        // with grammar parameter 
        public static SynthesisEngine ConfigureSynthesis(Grammar grammar)
        {
            var witnessFunctions = new WitnessFunctions(grammar);
            var deductiveSynthesis = new DeductiveSynthesis(witnessFunctions);
            var compBased = new ComponentBasedSynthesis();
            var synthesisExtrategies = new ISynthesisStrategy[] {  deductiveSynthesis };
            var synthesisConfig = new SynthesisEngine.Config { Strategies = synthesisExtrategies };
            var prose = new SynthesisEngine(grammar, synthesisConfig);
            return prose;
        }

        // comment or uncomment to choose approproate grammar (sorry for rudimental selection process)

        //private const string GrammarPath = @"../../../../ProgSynthAdding/synthesis/grammar/additionOriginal.grammar";

        //private const string GrammarPath = @"../../../../ProgSynthAdding/synthesis/grammar/additionLetFail.grammar";

        private const string GrammarPath = @"../../../../ProgSynthAdding/synthesis/grammar/additionLetWorking.grammar";


        private static Result<Grammar> CompileGrammar()
        {
            return DSLCompiler.
                Compile(new CompilerOptions()
                {
                    InputGrammarText = File.ReadAllText(GrammarPath),
                    References = CompilerReference.FromAssemblyFiles(typeof(Semantics).GetTypeInfo().Assembly)
                });
        }
    }
}
