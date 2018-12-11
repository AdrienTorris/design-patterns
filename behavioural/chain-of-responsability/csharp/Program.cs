namespace DesignPatterns.Behavioural.ChainOfResponsability
{
    using System;
    using DesignPatterns.Behavioural.ChainOfResponsability.RealWorldExample;
    using DesignPatterns.Behavioural.ChainOfResponsability.Structural;

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

            Handler handler1 = new ConcreteHandler1();
            Handler handler2 = new ConcreteHandler2();
            Handler handler3 = new ConcreteHandler3();

            handler1.SetSuccessor(handler2);
            handler2.SetSuccessor(handler3);

            handler1.HandleRequest();

            Console.WriteLine("Real world example");

            PoSystem poSystem = new PoSystem();

            Console.WriteLine("Handle a purchase of 5000");
            poSystem.ProcessRequest(5000);
            Console.WriteLine("Handle a purchase of 15000");
            poSystem.ProcessRequest(15000);
            Console.WriteLine("Handle a purchase of 70000");
            poSystem.ProcessRequest(70000);
            Console.WriteLine("Handle a purchase of 150000");
            poSystem.ProcessRequest(150000);
        }
    }
}

namespace DesignPatterns.Behavioural.ChainOfResponsability.Structural
{
    using System;

    class Client
    {
    }

    interface IHandler
    {
        void HandleRequest();
    }

    abstract class Handler : IHandler
    {
        protected IHandler successor;

        public void SetSuccessor(IHandler successor)
        {
            this.successor = successor;
        }

        public abstract void HandleRequest();
    }

    sealed class ConcreteHandler1 : Handler
    {
        public override void HandleRequest()
        {
            Console.WriteLine("Request handled by concrete handler 1");

            if (successor != null)
            {
                successor.HandleRequest();
            }
        }
    }

    sealed class ConcreteHandler2 : Handler
    {
        public override void HandleRequest()
        {
            Console.WriteLine("Request handled by concrete handler 2");

            if (successor != null)
            {
                successor.HandleRequest();
            }
        }
    }

    sealed class ConcreteHandler3 : Handler
    {
        public override void HandleRequest()
        {
            Console.WriteLine("Request handled by concrete handler 3");

            if (successor != null)
            {
                successor.HandleRequest();
            }
        }
    }
}

namespace DesignPatterns.Behavioural.ChainOfResponsability.RealWorldExample
{
    using System;

    public abstract class PoApprover
    {
        protected PoApprover successor;

        public void SetSuccessor(PoApprover successor)
        {
            this.successor = successor;
        }

        public abstract void ProcessRequest(decimal price);
    }

    public sealed class Manager : PoApprover
    {
        private const decimal maximumAmmount = 10000;

        public Manager(PoApprover successor)
        {
            this.successor = successor;
        }

        public override void ProcessRequest(decimal price)
        {
            if (price <= maximumAmmount)
            {
                Console.WriteLine("Purchase (" + price + ") approved by " + this.GetType().Name);
            }
            else if (this.successor != null)
            {
                this.successor.ProcessRequest(price);
            }
        }
    }

    public sealed class VicePresident : PoApprover
    {
        private const decimal maximumAmmount = 50000;

        public VicePresident(PoApprover successor)
        {
            this.successor = successor;
        }

        public override void ProcessRequest(decimal price)
        {
            if (price <= maximumAmmount)
            {
                Console.WriteLine("Purchase (" + price + ") approved by " + this.GetType().Name);
            }
            else if (this.successor != null)
            {
                this.successor.ProcessRequest(price);
            }
        }
    }

    public sealed class Ceo : PoApprover
    {
        private const decimal maximumAmmount = 100000;

        public Ceo(PoApprover successor)
        {
            this.successor = successor;
        }

        public override void ProcessRequest(decimal price)
        {
            if (price <= maximumAmmount)
            {
                Console.WriteLine("Purchase (" + price + ") approved by " + this.GetType().Name);
            }
            else
            {
                Console.WriteLine("Plan executive to approve a purchase of " + price);
            }
        }
    }

    public class PoSystem
    {
        protected PoApprover approvalChain = null;

        public PoSystem()
        {
            this.approvalChain =
                new Manager(
                    new VicePresident(
                        new Ceo(null)));
        }

        public void ProcessRequest(decimal price) =>
            this.approvalChain.ProcessRequest(price);
    }
}