namespace Konzit.Core.Singleton
{
    public class KonzitSingleton<T> where T : class, new()
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new();
                    return _instance;
                }
                else
                {
                    lock( _instance )
                    {
                        return _instance;
                    }
                }
            }
        }
    }

}
