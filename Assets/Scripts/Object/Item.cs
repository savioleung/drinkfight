using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    float speed = 3f;
    Transform playerPos;
    private void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        var dis = Vector3.Distance(playerPos.position, this.transform.position);
        if (dis < 5)
        {
            transform.position = Vector3.Lerp(transform.position, playerPos.position, Time.deltaTime*10);
        }
        transform.Rotate(0, speed, 0);    
    }

}
