namespace CLinq.Core
{
    public static class CLinqInitializer
    {
        public static void Initialize()
        {
            Extensions.Factory = new QueryComposerFactory();
        }
    }
}
