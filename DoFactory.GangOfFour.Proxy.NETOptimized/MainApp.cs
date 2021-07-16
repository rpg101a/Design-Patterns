using System;
using System.Runtime.Remoting;

namespace DoFactory.GangOfFour.Proxy.NETOptimized
{
    /// <summary>
    /// MainApp startup class for .NET optimized 
    /// Proxy Design Pattern.
    /// </summary>
    class MainApp
    {
        /// <summary>
        /// Entry point into console application.
        /// </summary>
        static void Main()
        {
            // Create math proxy
            var proxy = new MathProxy();

            // Do the math
            Console.WriteLine("4 + 2 = " + proxy.Add(4, 2));
            Console.WriteLine("4 - 2 = " + proxy.Sub(4, 2));
            Console.WriteLine("4 * 2 = " + proxy.Mul(4, 2));
            Console.WriteLine("4 / 2 = " + proxy.Div(4, 2));

            // Wait for user
            Console.ReadKey();
        }
    }

    /// <summary>
    /// The 'Subject' interface
    /// </summary>
    public interface IMath
    {
        double Add(double x, double y);
        double Sub(double x, double y);
        double Mul(double x, double y);
        double Div(double x, double y);
    }

    /// <summary>
    /// The 'RealSubject' class
    /// </summary>
    class Math : MarshalByRefObject, IMath
    {
        public double Add(double x, double y) { return x + y; }
        public double Sub(double x, double y) { return x - y; }
        public double Mul(double x, double y) { return x * y; }
        public double Div(double x, double y) { return x / y; }
    }

    /// <summary>
    /// The remote 'Proxy Object' class
    /// </summary>
    class MathProxy : IMath
    {
        Math math;

        // Constructor
        public MathProxy()
        {
            // Create Math instance in a different AppDomain
            var ad =  AppDomain.CreateDomain("MathDomain", null, null);

            var o = ad.CreateInstance(
                "DoFactory.GangOfFour.Proxy.NETOptimized",
                "DoFactory.GangOfFour.Proxy.NETOptimized.Math");
            math = (Math)o.Unwrap();
        }

        public double Add(double x, double y)
        {
            return math.Add(x, y);
        }

        public double Sub(double x, double y)
        {
            return math.Sub(x, y);
        }

        public double Mul(double x, double y)
        {
            return math.Mul(x, y);
        }

        public double Div(double x, double y)
        {
            return math.Div(x, y);
        }
    }
}
