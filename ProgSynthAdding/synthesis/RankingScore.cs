using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ProgramSynthesis;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;


namespace ProgSynthAdding
{
    class RankingScore : Feature<double>
    {
        public RankingScore(Grammar grammar) : base(grammar, "Score") { }

        protected override double GetFeatureValueForVariable(VariableNode variable) => 0;

        [FeatureCalculator(nameof(Semantics.Add))]
        public static double Add(double y, double x) => Math.Min(x, y) - 1.0;

        [FeatureCalculator(nameof(Semantics.AddTwo))]
        public static double AddTwo(double y, double x) => 1.0;

        /*[FeatureCalculator(nameof(Semantics.Add))]
        public static double Add(double y, double x)
        {
            double score = y * x + 1.0;

            return score;
        }*/
        int useCount = 0;
        int iter = 0;

        /*[FeatureCalculator(nameof(Semantics.Add), Method = CalculationMethod.FromChildrenNodes)]
        double ScoreAdd(NonterminalNode x, NonterminalNode y)
        {
            if (useCount == 0)
            {
                Console.WriteLine("Ranking Score analysis");
            }


            useCount++;
            

            String program = "" + x + y;

            int xCount = 0;

            foreach(char c in program)
            {
                
                if( c == 'x')
                {
                    xCount++;
              
                }
                
            }
            if (xCount == 4)
            {
                iter = 0;
               
                Console.WriteLine("-------------------------------------------------------------------------");
                Console.WriteLine("NEW PROGRAM: Add("+ x + ", " + y + ")");
                Console.WriteLine("-------------------------------------------------------------------------");
            }
            iter++;
            Console.WriteLine();
            Console.WriteLine("run:" + useCount + " iteration: " + iter);
            Console.WriteLine();

            Console.WriteLine("First parameter: " + x + ", Second parameter: " + y);

            Console.WriteLine("First parameter: " + x + " Value: " + x.GetFeatureValue(this) + 
                           ", Second parameter: " + y + " Value: " + y.GetFeatureValue(this));

            Console.WriteLine("The min value: " + Math.Min(x.GetFeatureValue(this), y.GetFeatureValue(this)));

            double score = (Math.Min(x.GetFeatureValue(this), y.GetFeatureValue(this)) - 1.0);

            Console.WriteLine("score: " + score);

            

            

            Console.WriteLine();         

            return score;
        }*/

        //[FeatureCalculator("x", Method = CalculationMethod.FromLiteral)]
        //public static double X(double x) => 1;   


    }
}
