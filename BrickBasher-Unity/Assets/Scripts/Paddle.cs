/**** 
 * Created by: Bob Baloney
 * Date Created: April 20, 2022
 * 
 * Last Edited by: Jacob Sharp
 * Last Edited: April 28, 2022
 * 
 * Description: Paddle controler on Horizontal Axis
****/

/*** Using Namespaces ***/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float speed = 10; //speed of paddle


    // Update is called once per frame
    void Update()
    {
        // move the paddle according to keyboard input
        float moveDir = Input.GetAxis("Horizontal");
        Vector3 newPos = transform.position;
        newPos.x += moveDir * speed * Time.deltaTime;
        transform.position = newPos;
    }//end Update()
}
