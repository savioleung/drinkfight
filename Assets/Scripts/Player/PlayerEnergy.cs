using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEnergy : MonoBehaviour
{
    [SerializeField]
    private GameObject damageUI;
    float i = 0;
    [SerializeField]
    private Slider shieldSlider;
    [SerializeField]
    private Slider EnergeSlider;
    [Range(0.0f, 100.0f)]
    public float shieldVal = 20;
    [Range(0.0f, 100.0f)]
    public float EnergeVal = 100;
    private void Start()
    {
        damageUI.SetActive(true);
    }
    private void Update()
    {

        shieldSlider.value = shieldVal;
        EnergeSlider.value = EnergeVal;
        if (Input.GetKey(KeyCode.T) && shieldVal < 100 && EnergeVal > 0)
        {
            ShieldCharge();
        }
        if (shieldVal <= 0)
        {
            Debug.Log("you just die");
        }
        shieldVal = valblock(shieldVal);
        EnergeVal = valblock(EnergeVal);
        i -= 1 * Time.deltaTime;
        damageUI.GetComponent<Image>().color = new Color(1, 0, 0, i);

    }
    void ShieldCharge()
    {
        EnergeVal -= 1;
        shieldVal += 2;
    }
    float valblock(float n)
    {
        if (n <= 0)
        {
            return 0;
        }
        else if (n >= 100)
        {
            return 100;
        }
        else { return n; }
    }
    public void getDamage(float damage)
    {
        i = 0.5f;
        shieldVal -= damage;

    }

}
