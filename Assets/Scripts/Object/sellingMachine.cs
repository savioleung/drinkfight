using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using UnityEngine.UI;

public class sellingMachine : MonoBehaviour
{
    PlayerGrenade pg;

    // Start is called before the first frame update
    void Start()
    {
        pg = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerGrenade>();
    }

    int checkEmptyBox() {
        for(int i=0;i<6; i++)
        {
            if (pg.itemStatus[i]==0)
            {
                return i;

            }
        }
        return -7;
    }
    //ボトルのドリンクを買うボタン
    public void bottleOnClick()
    {
        if (checkEmptyBox() != -7&& pg.coinsVal >= 150)
        {
            pg.itemStatus[checkEmptyBox()] = 1;
            pg.coinsVal -= 150;
        }
    }
    //缶のドリンクを買うボタン
    public void canOnClick()
    {
        if (checkEmptyBox() != -7&&pg.coinsVal >= 160)
        {
            pg.itemStatus[checkEmptyBox()] = 3;
            pg.coinsVal -= 160;
        }
    }
    //瓶のドリンクを買うボタン
    public void binOnClick()
    {
        if (checkEmptyBox() != -7&&pg.coinsVal >= 170)
        {
            pg.itemStatus[checkEmptyBox()] = 5;
            pg.coinsVal -= 170;
        }
    }
}
