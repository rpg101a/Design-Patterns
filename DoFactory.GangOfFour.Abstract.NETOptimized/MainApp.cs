using System;
using System.Collections.Generic;

namespace DoFactory.GangOfFour.Abstract.NETOptimized
{
    /// <summary>
    /// MainApp startup class for .NET optimized 
    /// Abstract Factory Design Pattern.
    /// </summary>
    class MainApp
    {
        /// <summary>
        /// Entry point into console application.
        /// </summary>
        public static void Main()
        {
            // Create and run the African animal world
            var africa = new AnimalWorld<Africa>();
            africa.RunFoodChain();

            // Create and run the American animal world
            var america = new AnimalWorld<America>();
            america.RunFoodChain();

            // Wait for user input
            Console.ReadKey();
        }
    }

    /// <summary>
    /// The 'AbstractFactory' interface. 
    /// </summary>
    interface IContinentFactory
    {
        IHerbivore CreateHerbivore();
        ICarnivore CreateCarnivore();
    }

    /// <summary>
    /// The 'ConcreteFactory1' class.
    /// </summary>
    class Africa : IContinentFactory
    {
        public IHerbivore CreateHerbivore()
        {
            return new Wildebeest();
        }

        public ICarnivore CreateCarnivore()
        {
            return new Lion();
        }
    }

    /// <summary>
    /// The 'ConcreteFactory2' class.
    /// </summary>
    class America : IContinentFactory
    {
        public IHerbivore CreateHerbivore()
        {
            return new Bison();
        }

        public ICarnivore CreateCarnivore()
        {
            return new Wolf();
        }
    }

    /// <summary>
    /// The 'AbstractProductA' interface
    /// </summary>
    interface IHerbivore
    {
    }

    /// <summary>
    /// The 'AbstractProductB' interface
    /// </summary>
    interface ICarnivore
    {
        void Eat(IHerbivore h);
    }

    /// <summary>
    /// The 'ProductA1' class
    /// </summary>
    class Wildebeest : IHerbivore
    {
    }

    /// <summary>
    /// The 'ProductB1' class
    /// </summary>
    class Lion : ICarnivore
    {
        public void Eat(IHerbivore h)
        {
            // Eat Wildebeest
            Console.WriteLine(this.GetType().Name +
                " eats " + h.GetType().Name);
        }
    }

    /// <summary>
    /// The 'ProductA2' class
    /// </summary>
    class Bison : IHerbivore
    {
    }

    /// <summary>
    /// The 'ProductB2' class
    /// </summary>
    class Wolf : ICarnivore
    {
        public void Eat(IHerbivore h)
        {
            // Eat Bison
            Console.WriteLine(this.GetType().Name +
                " eats " + h.GetType().Name);
        }
    }

    /// <summary>
    /// The 'Client' interface
    /// </summary>
    interface IAnimalWorld
    {
        void RunFoodChain();
    }

    /// <summary>
    /// The 'Client' class
    /// </summary>
    class AnimalWorld<T> : IAnimalWorld where T : IContinentFactory, new()
    {
        IHerbivore herbivore;
        ICarnivore carnivore;
        T factory;

        /// <summary>
        /// Contructor of Animalworld
        /// </summary>
        public AnimalWorld()
        {
            // Create new continent factory
            factory = new T();

            // Factory creates carnivores and herbivores
            carnivore = factory.CreateCarnivore();
            herbivore = factory.CreateHerbivore();
        }

        /// <summary>
        /// Runs the foodchain: carnivores are eating herbivores.
        /// </summary>
        public void RunFoodChain()
        {
            carnivore.Eat(herbivore);
        }
    }
}
