public abstract class SingletonHT<T> where T : SingletonHT<T>, new()
{
    private static T ms_instance = default(T);

    public static T Instance
    {
        get
        {
            if (ms_instance == null)
            {
                ms_instance = new T();
            }
            return ms_instance;
        }
    }

}