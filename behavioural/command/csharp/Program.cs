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
			
			RealWorlExampleMain();

            Console.Read();
        }

        static void StructuralExampleMain()
        {
            Console.WriteLine("Structural example");
			
        }

        static void RealWorlExampleMain()
        {
            Console.WriteLine("Real world example");
			
			InteractiveShell interactiveShell = new InteractiveShell();
			interactiveShell.Run();
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
	using System.Collections.Generic;

    public sealed class MathLib
	{
		private int currentValue = 0;
		
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
		// Execute the operation
		public abstract void Execute();
		
		// Gets the inverse of the operation
		public abstract AbstractOperation Inverse { get; }
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
					case "-":
						return new Operation(this.mathlib, "+", this.argument);
					case "*":
						return new Operation(this.mathlib, "/", this.argument);
					case "/":
						return new Operation(this.mathlib, "*", this.argument);
					default:
						throw new InvalidOperationException("Invalid math operation");
				}
			}
		}
	}
	
	public class InteractiveShell
	{
		public void Run()
		{
			MathLib mathLib = new MathLib();
			
			Calculator calculator = new Calculator();
			
			Console.WriteLine("Please input a math operation:");
			Console.WriteLine("     + <number>: add number");
			Console.WriteLine("     - <number>: substract number");
			Console.WriteLine("     * <number>: multiply number");
			Console.WriteLine("     / <number>: divide number");
			Console.WriteLine("     ~ <steps>: undo steps");
			Console.WriteLine("     q: quit");
			
			string input = Console.ReadLine();
			while(input!="q")
			{
				if(input.Length >= 2)
				{
					string op = input.Substring(0,1);
					int arg = int.Parse(input.Substring(1));
					
					// Handle undo command
					if(op=="~")
					{
						for(int i=0;i<arg;i++)
						{
							calculator.Undo();
						}
					}else{
						AbstractOperation operation = new Operation(mathLib, op, arg);
						
						calculator.Invoke(operation);
					}
				}
				
				Console.WriteLine("Result: {0}", mathLib.CurrentValue);
				input = Console.ReadLine();
			}
		}
	}
	
	public class Calculator
	{
		protected Stack<AbstractOperation> undoStack = new Stack<AbstractOperation>();
		
		public void Invoke(AbstractOperation operation)
		{
			operation.Execute();
			undoStack.Push(operation);
		}
		
		// Undo the last operation
		public void Undo()
		{
			var operation = undoStack.Pop();
			operation.Inverse.Execute();
		}
	}
}
