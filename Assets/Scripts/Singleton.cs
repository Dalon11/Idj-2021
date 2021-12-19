public class Singleton
{
    private static Singleton _instance = null;
    private MonoCacheList _monoCacheList = new MonoCacheList();
    public MonoCacheList MonoCacheList => _monoCacheList;

    private Singleton(){}
    public static Singleton GetInstance() => _instance ??= new Singleton();
}
