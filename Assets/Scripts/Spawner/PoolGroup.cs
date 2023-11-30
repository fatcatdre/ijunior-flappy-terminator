using UnityEngine;
using System.Collections.Generic;

public class PoolGroup : MonoBehaviour
{
    private List<Pool> _pools;

    private void Awake()
    {
        _pools = new List<Pool>(GetComponentsInChildren<Pool>());
    }

    public void ClearAll()
    {
        foreach (Pool pool in _pools)
        {
            pool.gameObject.SetActive(false);
            pool.gameObject.SetActive(true);
        }   
    }
}
