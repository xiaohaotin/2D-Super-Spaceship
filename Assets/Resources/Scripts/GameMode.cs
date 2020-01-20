using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    public BulletCharacter bulletTemplate;
    public Transform firpoint;
    public List<BulletCharacter> tempBullets;
    // Start is called before the first frame update
    void Start()
    {
        tempBullets = new List<BulletCharacter>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(20, 40, 200, 100), "散弹"))
        {
            StopAllCoroutines();
            ClearBulletsList();
            //StartCoroutine(FirShotgun())
        }
    }

    public void ClearBulletsList()
    {
        if (tempBullets.Count>0)
        {
            for (int i = (tempBullets.Count -1);i>=0;i--)
            {
                Destroy(tempBullets[i].gameObject);
            }
        }
        tempBullets.Clear();
    }
}
