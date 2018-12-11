namespace DesignPatterns.Behavioural.Command
{
    using System;
    using DesignPatterns.Behavioural.Command.RealWorldExample;
    using DesignPatterns.Behavioural.Command.Structural;

    class Program
    {
        static void Main(string[] args)
        {
            StructuralExampleMain();

            Console.Read();
        }

        static void StructuralExampleMain()
        {
            Console.WriteLine("Structural example");
			

            Console.WriteLine("Real world example");
			
        }
    }
}

namespace DesignPatterns.Behavioural.Command.Structural
{
    using System;

    
}

namespace DesignPatterns.Behavioural.Command.RealWorldExample
{
    using System;

    public sealed class MathLib
	{
		protected int currentValue = 0;
		
		public int CurrentValue 
		{
			get
			{
				return this.currentValue;
			}
		}
			
		public void Add(int argument) =>
			this.currentValue += argument;
			
		public void Substract(int argument) =>
			this.currentValue -= argument;
			
		public void Multiply(int argument) =>
			this.currentValue *= argument;
			
		public void Divide(int argument) =>
			this.currentValue /= argument;
	}
	
	public abstract class AbstractOperation
	{
		public abstract void Execute();
		
		public abstract AbstractOperation Inverse( get; }
	}
	
	public class Operation : AbstractOperation
	{
		protected MathLib mathlib;
		
		protected string operation = null;
		
		protected int argument;
		
		public Operation(MathLib mathlib, string operation, int argument)
		{
			this.mathlib = mathlib;
			this.operation = operation;
			this.argument = argument;
		}
		
		public override void Execute()
		{
			switch(this.operation)
			{
				case "+":
					this.mathlib.Add(this.argument);
					break;
				case "-":
					this.mathlib.Substract(this.argument);
					break;
				case "*":
					this.mathlib.Multiply(this.argument);
					break;
				case "/":
					this.mathlib.Divide(this.argument);
					break;
				default:
					throw new InvalidOperationException("Invalid math operation");
			}
		}
		
		public override AbstractOperation Inverse
		{
			get
			{
				switch(this.operation)
				{
					case "+":
						return new Operation(this.mathlib, "-", this.argument);
				}
			}
		}
	}
}