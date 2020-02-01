using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject Enemy1;
    // Start is called before the first frame update
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        StopAllCoroutines();
        Enemy1.gameObject.GetComponent<GameMode>().ClearBulletsList();
        StartCoroutine(Enemy1.gameObject.GetComponent<GameMode>().FireTurbine());
    }
}
