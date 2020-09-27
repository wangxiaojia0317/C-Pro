
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text.RegularExpressions;

  
namespace Actor
{ 
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.ReadLine();
        }
    }




    internal interface IActor
    {
        void Execute();

        bool Existed { get; }

        int MessageCount { get; }

        ActorContext Context { get; }
    }

    internal class ActorContext
    {
        public ActorContext(IActor actor)
        {
            this.Actor = actor;
        }

        public IActor Actor { get; private set; }

   
}

    public abstract class Actor<T> : IActor
    {
        protected Actor()
        {
            this.m_context = new ActorContext(this);
        }

        private ActorContext m_context;
        ActorContext IActor.Context
        {
            get
            {
                return this.m_context;
            }
        }

        public bool Existed
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int MessageCount
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Execute()
        {
            throw new NotImplementedException();
        }
    }

}