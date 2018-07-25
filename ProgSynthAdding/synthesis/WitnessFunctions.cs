using Microsoft.ProgramSynthesis;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Learning;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace ProgSynthAdding
{
    public class WitnessFunctions : DomainLearningLogic
    {
        public WitnessFunctions(Grammar grammar) : base(grammar) { }

         // deduce the spec of the second parameter of Add (0) using the ExampleSpec
        [WitnessFunction(nameof(Semantics.Add), 0)]
        public DisjunctiveExamplesSpec WitnessAddendOne(GrammarRule rule, 
            DisjunctiveExamplesSpec spec)
        {
            var result = new Dictionary<State, IEnumerable<object>>();

            foreach (var example in spec.DisjunctiveExamples)
            {
                State inputState = example.Key;

                var outputs = example.Value;

                var possibleAddends = new List<int>();

                foreach (int output in outputs)
                {
                    int outputNumber = output;

                    for (int i = 1; i < outputNumber; i++)
                    {
                        possibleAddends.Add(i);
                    }                    
                }
                result[inputState] = possibleAddends.Cast<object>();
            }
            return new DisjunctiveExamplesSpec(result);
        }

        // deduce the spec of the first parameter of Add (0) using the ExampleSpec
        [WitnessFunction(nameof(Semantics.Add), 1, DependsOnParameters = new int[] {0})]
        public DisjunctiveExamplesSpec WitnessAddendTwo(GrammarRule rule,
            DisjunctiveExamplesSpec spec, ExampleSpec ySpec)
        {
            var result = new Dictionary<State, IEnumerable<object>>();

            foreach (var examples in spec.DisjunctiveExamples)
            {
                State inputState = examples.Key;

                var outputs = examples.Value;

                var possibleAddends = new List<int>();

                foreach (int output in outputs)
                {
                    int outputNumber = output;
                    
                    var yExamples = ySpec.DisjunctiveExamples.Values;

                    foreach(var example in yExamples)
                    {
                        foreach(int paramOne in example)
                        {
                            int addend = output - paramOne;

                            possibleAddends.Add(addend);                                                        
                        }
                    }                                  
                }
                result[inputState] = possibleAddends.Cast<object>();
            }
            return new DisjunctiveExamplesSpec(result);
        }

        // deduce the spec of the second parameter of Add (0) using the ExampleSpec
        [WitnessFunction(nameof(Semantics.Add), 0)]
        public DisjunctiveExamplesSpec WitnessTwoAddendOne(GrammarRule rule,
            DisjunctiveExamplesSpec spec)
        {
            var result = new Dictionary<State, IEnumerable<object>>();

            foreach (var example in spec.DisjunctiveExamples)
            {
                State inputState = example.Key;

                var outputs = example.Value;

                var possibleAddends = new List<int>();

                foreach (int output in outputs)
                {
                    int outputNumber = output;

                    for (int i = 1; i < outputNumber; i++)
                    {
                        possibleAddends.Add(i);
                    }
                }
                result[inputState] = possibleAddends.Cast<object>();
            }
            return new DisjunctiveExamplesSpec(result);
        }

        // deduce the spec of the first parameter of Add (0) using the ExampleSpec
        [WitnessFunction(nameof(Semantics.Add), 1, DependsOnParameters = new int[] { 0 })]
        public DisjunctiveExamplesSpec WitnessTwoAddendTwo(GrammarRule rule,
            DisjunctiveExamplesSpec spec, ExampleSpec ySpec)
        {
            var result = new Dictionary<State, IEnumerable<object>>();

            foreach (var examples in spec.DisjunctiveExamples)
            {
                State inputState = examples.Key;

                var outputs = examples.Value;

                var possibleAddends = new List<int>();

                foreach (int output in outputs)
                {
                    int outputNumber = output;

                    var yExamples = ySpec.DisjunctiveExamples.Values;

                    foreach (var example in yExamples)
                    {
                        foreach (int paramOne in example)
                        {
                            int addend = output - paramOne;

                            possibleAddends.Add(addend);
                        }
                    }
                }
                result[inputState] = possibleAddends.Cast<object>();
            }
            return new DisjunctiveExamplesSpec(result);
        }
    }    
}