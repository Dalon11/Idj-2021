using System.Collections.Generic;

public class MonoCacheList
{
    private List<MonoCache> _LateTick = new List<MonoCache>(101);
    private List<MonoCache> _UpdateTick = new List<MonoCache>(101);
    private List<MonoCache> _FixedUpdateTick = new List<MonoCache>(101);

    public List<MonoCache> LataTick => _LateTick;
    public List<MonoCache> UpdateTick => _UpdateTick;
    public List<MonoCache> FixedUpdateTick => _FixedUpdateTick;
}
