using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class remove : MonoBehaviour
{
    public putWall pw;
    public removeWall rw;
    public int index;
    public GameObject wall;

    private SpriteRenderer thisSprite;
    public bool flag;

    void Awake()
    {
        index = -1;
        flag = false;
        thisSprite = GetComponent<SpriteRenderer>();
    }

    void OnMouseOver()
    {
        if (rw.canRemove)
        {
            thisSprite.color = new Color(0.55f, 0.55f, 0.55f, 1);
        }
    }

    private void OnMouseExit()
    {
        if (rw.canRemove)
        {
            thisSprite.color = new Color(1, 1, 1, 1);
        }
    }

    void OnMouseDown()
    {
        StartCoroutine("destroy");
    }

    IEnumerator destroy()
    {
        if (rw.canRemove || flag)
        {
            if (pw.wallList.Exists(x => x.gameObject))
            {
                index = pw.wallList.FindIndex(x => x == wall.gameObject);
                print("제거된 벽의 index : " + index);
                rw.remove(index);
                yield return new WaitForSeconds(0.1f);
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("cat_Boom")){
            if (N_CardSystem.isBoom && !flag)
            {
                flag = true;
                StartCoroutine("destroy");
            }
        }
    }

}
