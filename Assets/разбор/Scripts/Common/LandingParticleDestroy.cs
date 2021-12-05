using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingParticleDestroy : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(DestroyingParticle());
    }

    IEnumerator DestroyingParticle()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        Destroy(gameObject);
    }
}
