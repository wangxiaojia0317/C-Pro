
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 协变与逆变
{
    class Program
    {
        static void Main(string[] args)
        {
            IMyList<Animal> myAnimals = new MyList<Animal>();
            IMyList<Dog> myDogs =new  MyList<Animal>();
        }
    }


    /*
     in .Net
        T is modified for in(Contravariant) as para and T can't be a return value 
        however,it is inverese with out
         
    */

    public class Dog : Animal { }
    public abstract class Animal
    { }

    public interface IMyList<in T>
    {
       //GetElement();
        void ChangeT(T t);
    }

    public class MyList<T> : IMyList<T>
    {
        public void ChangeT(T t)
        {
            throw new NotImplementedException();
        }
        
        
    }
}
