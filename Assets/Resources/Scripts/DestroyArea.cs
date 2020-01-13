using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyArea : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    void OnTriggerExit2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
        //if (Layer == "Player")
        //{
        //    gameManager.GameOver();
        //}
        
    }
}
