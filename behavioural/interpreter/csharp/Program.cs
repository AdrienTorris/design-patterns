namespace DesignPatterns.Behavioural.Interpreter
{
    using System;
    using DesignPatterns.Behavioural.Interpreter.RealWorldExample;
    using DesignPatterns.Behavioural.Interpreter.Structural;

    class Program
    {
        static void Main(string[] args)
        {
			RealWorlExampleMain();

            Console.Read();
        }

        static void RealWorlExampleMain()
        {
            Console.WriteLine("Real world example");
			
			Console.WriteLine("Enter RPN expression:");
			Evaluator evaluator = new Evaluator();
			string enteredRpnExpression = Console.ReadLine();
            RpnExpression rpnExpression = evaluator.Parse(enteredRpnExpression);
            int result = rpnExpression.Interpret();

            Console.WriteLine("Result: "+ result);
			
			Console.Read();
        }
    }
}

namespace DesignPatterns.Behavioural.Interpreter.Structural
{
    using System;

    
}

// Let's build a Reverse Polish Notation interpreter
namespace DesignPatterns.Behavioural.Interpreter.RealWorldExample
{
    using System;
	using System.Collections.Generic;

    public abstract class RpnExpression
	{
		public abstract int Interpret();
	}
	
	sealed class Number : RpnExpression
	{
		private int currentValue = 0;
		
		public Number(int value)
		{
			this.currentValue = value;
		}
		
		public override int Interpret() =>
		   this.currentValue;
	}
	
	sealed class Evaluator
	{
		public RpnExpression Parse(string text)
		{
			Stack<RpnExpression> stack = new Stack<RpnExpression>();
			
			string[] words = text.Split(' ');
			foreach(string word in words)
			{
				RpnExpression leftHandSubexpression = null;
				RpnExpression rightHandSubexpression = null;
				
				switch(word)
				{
					case "+":
						rightHandSubexpression = stack.Pop();
						leftHandSubexpression = stack.Pop();
						stack.Push(new Add(leftHandSubexpression, rightHandSubexpression));
						break;
					case "-":
						rightHandSubexpression = stack.Pop();
						leftHandSubexpression = stack.Pop();
						stack.Push(new Substract(leftHandSubexpression, rightHandSubexpression));
						break;
					case "*":
						rightHandSubexpression = stack.Pop();
						leftHandSubexpression = stack.Pop();
						stack.Push(new Multiply(leftHandSubexpression, rightHandSubexpression));
						break;
					case "/":
						rightHandSubexpression = stack.Pop();
						leftHandSubexpression = stack.Pop();
						stack.Push(new Divide(leftHandSubexpression, rightHandSubexpression));
						break;
					default:
						int number = int.Parse(word);
						stack.Push(new Number(number));
						break;
				}
			}
			
			return stack.Pop();
		}
	}
	
	class Add : RpnExpression
	{
		protected RpnExpression leftHandSubexpression;
		protected RpnExpression rightHandSubexpression;
		
		public Add(RpnExpression leftHandSubexpression,RpnExpression rightHandSubexpression)
		{
			this.leftHandSubexpression=leftHandSubexpression;
			this.rightHandSubexpression=rightHandSubexpression;
		}
		
		public override int Interpret() =>
			this.leftHandSubexpression.Interpret() + this.rightHandSubexpression.Interpret();
	}
	
	class Substract : RpnExpression
	{
		protected RpnExpression leftHandSubexpression;
		protected RpnExpression rightHandSubexpression;
		
		public Substract(RpnExpression leftHandSubexpression,RpnExpression rightHandSubexpression)
		{
			this.leftHandSubexpression=leftHandSubexpression;
			this.rightHandSubexpression=rightHandSubexpression;
		}
		
		public override int Interpret() =>
			this.leftHandSubexpression.Interpret() - this.rightHandSubexpression.Interpret();
	}
	
	class Multiply : RpnExpression
	{
		protected RpnExpression leftHandSubexpression;
		protected RpnExpression rightHandSubexpression;
		
		public Multiply(RpnExpression leftHandSubexpression,RpnExpression rightHandSubexpression)
		{
			this.leftHandSubexpression=leftHandSubexpression;
			this.rightHandSubexpression=rightHandSubexpression;
		}
		
		public override int Interpret() =>
			this.leftHandSubexpression.Interpret() * this.rightHandSubexpression.Interpret();
	}
	
	class Divide : RpnExpression
	{
		protected RpnExpression leftHandSubexpression;
		protected RpnExpression rightHandSubexpression;
		
		public Divide(RpnExpression leftHandSubexpression,RpnExpression rightHandSubexpression)
		{
			this.leftHandSubexpression=leftHandSubexpression;
			this.rightHandSubexpression=rightHandSubexpression;
		}
		
		public override int Interpret() =>
			this.leftHandSubexpression.Interpret() / this.rightHandSubexpression.Interpret();
	}
}