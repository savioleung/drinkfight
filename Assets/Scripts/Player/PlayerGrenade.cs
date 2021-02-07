using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGrenade : MonoBehaviour
{
    //アイテム欄設定
    static int maxItemNum = 6;
    //各アイテムのステイタス
    public int[] itemStatus = new int[6];
    //プレイヤー使用中アイテム
    int usingItem = 0;
    //アイテム画像
    [SerializeField]
    private RawImage[] itemImage = new RawImage[maxItemNum];
    //アイテム背景画像
    [SerializeField]
    private RawImage[] itemBackGround = new RawImage[7];
    /***アイテム画像テクスチャ
     * 0.空
     * 1.ボトル
     * 2.空ボトル
     * 3.缶
     * 4.空缶
     * 5.瓶
     * 6.空ビン
     * ***/
    [SerializeField]
    private Texture[] itemTexture = new Texture[7];

    [SerializeField]
    private Text coinsUI;
    public int coinsVal = 500;
    //グレネード発射設定
    //発射するグレネードオブジェクト
    public GameObject grenade;
    //発射するポイント
    public Transform shootPoint;
    //発射する可能性
    public bool canShoot, canControl;
    float getEnergy = 20;
    //発射する力
    float grenadeForce = 8;
    //プレイヤーが持ってるオブジェクト
    public GameObject playerG;
    private void Start()
    {

        itemSlotSelect(usingItem);
        //アイテムをないものとリセット
        for (int i = 0; i < maxItemNum; i++)
        {
            itemStatus[i] = 0;
        }

        canShoot = false;
        canControl = true;
        playerG.SetActive(false);

    }
    // Update is called once per frame
    void Update()
    {

        itemChange();
        if (canControl)
        {
            #region スロット変更
            var key = Input.inputString;
            switch (key)
            {
                case "1":
                    usingItem = 0;
                    break;
                case "2":
                    usingItem = 1;
                    break;
                case "3":
                    usingItem = 2;
                    break;
                case "4":
                    usingItem = 3;
                    break;
                case "5":
                    usingItem = 4;
                    break;
                case "6":
                    usingItem = 5;
                    break;
                default:
                    break;
            }
            itemSlotSelect(usingItem);
            nowItemControl(usingItem);
            #endregion

        }
        coinsUI.text = coinsVal + "G";
    }
    public void itemSlotSelect(int itemNum)
    {
        for (int i = 0; i < maxItemNum; i++)
        {
            itemBackGround[i].color = new Color(0.65f, 1, 1);
        }
        itemBackGround[itemNum].color = new Color(0.85f, 1, 0);
    }
    public void itemChange()
    {
        for (int i = 0; i < maxItemNum; i++)
        {
            itemImage[i].texture = itemTexture[itemStatus[i]];

        }
    }
    void nowItemControl(int item)
    {
        if (itemStatus[item] == 0)
        {
            canShoot = false;
            playerG.SetActive(false);
        }
        else
        {
            playerG.SetActive(true);
            switch (itemStatus[item])
            {
                //ドリンク
                case 1:
                case 3:
                case 5:
                    canShoot = false;
                    if (Input.GetKeyDown(KeyCode.R))
                    {
                        itemStatus[item]++;
                        switch (itemStatus[item])
                        {
                            case 1:
                                getEnergy = 20;
                                break;
                            case 3:
                                getEnergy = 30;

                                break;
                            case 5:
                                getEnergy = 10;
                                break;
                        }
                        GetComponent<PlayerEnergy>().EnergeVal += 20;
                    }
                    break;
                //ドリンク（空）
                case 2:
                case 4:
                case 6:
                    canShoot = true;
                    #region 発射
                    if (canShoot)
                    {
                        if (Input.GetMouseButton(0))
                        {
                            grenadeForce += 1 * Time.deltaTime * 2;
                        }
                        if (Input.GetMouseButtonUp(0))
                        {
                            GameObject grenadeClone = Instantiate(grenade, shootPoint.position, shootPoint.rotation);
                            grenadeClone.GetComponent<Rigidbody>().AddForce(shootPoint.forward * 10, ForceMode.Impulse);
                            switch (itemStatus[item])
                            {
                                case 2:
                                    grenadeClone.GetComponent<Grenade>().explosionRange = 100;
                                    break;
                                case 4:
                                    grenadeClone.GetComponent<Grenade>().explosionRange = 150;
                                    grenadeClone.GetComponent<Grenade>().explosionTime = 3;
                                    break;
                                case 6:
                                    grenadeClone.GetComponent<Grenade>().explosionRange = 50;
                                    grenadeClone.GetComponent<Grenade>().explosionTime = 99;
                                    grenadeClone.GetComponent<Grenade>().Pin = true;
                                    break;
                            }
                            // Debug.Log(grenadeForce);
                            grenadeForce = 8;
                            canShoot = false;
                            itemStatus[item] = 0;
                        }
                    }
                    #endregion
                    break;
                default:
                    break;
            }
        }
    }
}
