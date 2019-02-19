using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_CatMove : MonoBehaviour
{ 
// Update is called once per frame 
    int speed = 10; //스피드 
    void Update()
    {
        float xMove = Input.GetAxis("Horizontal") * speed * Time.deltaTime; //x축으로 이동할 양 
        float yMove = Input.GetAxis("Vertical") * speed * Time.deltaTime; //y축으로 이동할양 
        this.transform.Translate(new Vector3(xMove, yMove, 0));  //이동

    }
}