using UnityEngine;

public class UpdateManager : MonoBehaviour
{
    private Singleton _singleton = Singleton.GetInstance();
    private void Start(){}

    private void LateUpdate()
    {
        for (int i = 0; i < _singleton.MonoCacheList.LataTick.Count; i++)
            _singleton.MonoCacheList.LataTick[i].LateTick();
    }
    private void Update()
    {
        for (int i = 0; i < _singleton.MonoCacheList.UpdateTick.Count; i++)
            _singleton.MonoCacheList.UpdateTick[i].UpdateTick();
    }
    private void FixedUpdate()
    {
        for (int i = 0; i < _singleton.MonoCacheList.FixedUpdateTick.Count; i++)
            _singleton.MonoCacheList.FixedUpdateTick[i].FixedUpdateTick();
    }
}
