using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    public GameObject[] prefabs;

    List<GameObject>[] pools;

    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for(int i = 0; i<pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }

        print(pools.Length);
    }

    public GameObject SpawnObject(int index)
    {
        GameObject pool = null;

        foreach(GameObject item in pools[index])
        {
            if(!item.activeSelf)
            {
                pool = item;
                pool.SetActive(true);
                break;
            }
        }

        if(pool == null)
        {
            pool = Instantiate(prefabs[index], transform);
            pools[index].Add(pool);
        }

        return pool;
    }
}
