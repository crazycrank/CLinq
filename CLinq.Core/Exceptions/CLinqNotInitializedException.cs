namespace CLinq.Core.Exceptions
{
    public class CLinqNotInitializedException : CLinqException
    {
        public CLinqNotInitializedException() 
            : base("CLinq has not yet been initialized. Call CLinqInitializer.Initialize() once.")
        { }
    }
}