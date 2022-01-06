public class Singleton
{
    private static Singleton _instance = null;
    private MonoCacheList _monoCacheList = new MonoCacheList();
    public MonoCacheList MonoCacheList => _monoCacheList;

    private bool _isPause = false;
    public bool IsPause { get => _isPause; set => _isPause = value; }

    private Singleton(){}
    public static Singleton GetInstance() => _instance ??= new Singleton();
}
