using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject sellingUI, sellingOpenUI;
    PlayerMovement pm;
    PlayerGrenade pg;
    bool sellingUIOn,canOpenUI;
    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponent<PlayerMovement>();
        pg = GetComponent<PlayerGrenade>();
        sellingUIOn = false;
        canOpenUI = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        sellingUI.SetActive(false);
        sellingOpenUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        checkOpenBuyingUI();//ドリンクを買えるかチェック
    }

    void checkOpenBuyingUI()
    {

        if (canOpenUI)
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!sellingUIOn)
                {
                    sellingMachineOn();
                }
                else
                {
                    sellingMachineOff();
                }
            }
    }
    void sellingMachineOn()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        sellingUI.SetActive(true);
        sellingOpenUI.SetActive(false);
        sellingUIOn = true;
        pm.canMove = false;
        pg.canShoot = false;
        pg.canControl = false;
    }
    void sellingMachineOff()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        sellingUI.SetActive(false);
        sellingUIOn = false;
        pm.canMove = true;
        pg.canControl = true;
       // pg.canShoot = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Selling Machine")
        {
            sellingOpenUI.SetActive(true);
            canOpenUI = true;
   
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            switch (collision.gameObject.name)
            {
                case "":
                    break;
                case "coin":
                    pg.coinsVal += 500;
                    break;
                default:
                    break;
            }
            Destroy(collision.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Selling Machine")
        {
            sellingOpenUI.SetActive(false);
            canOpenUI = false;
        }
    }
}
