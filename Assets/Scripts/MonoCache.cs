using UnityEngine;

public class MonoCache : MonoBehaviour
{
    //Через этот класс реализуеться список для апдейт менеджера.
    private Singleton _singleton = Singleton.GetInstance();

    protected void AddLateTick() => _singleton.MonoCacheList.LataTick.Add(this);
    protected void AddUpdate() => _singleton.MonoCacheList.UpdateTick.Add(this);
    protected void AddFixedUpdate() => _singleton.MonoCacheList.FixedUpdateTick.Add(this);

    protected void RemoveLateTick() => _singleton.MonoCacheList.LataTick.Remove(this);
    protected void RemoveUpdate() => _singleton.MonoCacheList.UpdateTick.Remove(this);
    protected void RemoveFixedUpdate() => _singleton.MonoCacheList.FixedUpdateTick.Remove(this);

    public virtual void LateTick(){}
    public virtual void UpdateTick(){}
    public virtual void FixedUpdateTick(){}
}
