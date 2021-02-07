using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField]
    GameObject boomer;
    public float explosionRange = 100;
    public float explosionTime = 1.5f;
    public bool Pin = false;
    // Start is called before the first frame update
    void Start()
    {
        boomer.transform.localScale = Vector3.one * explosionRange;
        boomer.SetActive(false);
        Invoke("Explode", explosionTime); // グレネードが作られてから1.5秒後に爆発させる

    }

    // Update is called once per frame
    void Update()
    {

    }
    void Explode()
    {
        boomer.SetActive(true);
        Destroy(this.gameObject,0.1f);

    }
    private void OnCollisionEnter(Collision other)
    {
        if (Pin) { explosionTime = 0; Explode(); }
        Debug.Log(other.gameObject.name);
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("yes");
            other.gameObject.GetComponent<EnemyAI>().hp -= 10;
        }
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("yes");
            other.gameObject.GetComponent<PlayerEnergy>().shieldVal -= 20;
        }
    }
}



//GetComponent<SphereCollider>().enabled = true;
// // Bit shift the index of the layer(8) to get a bit mask
// int layerMask = 1 << 8;
// // This would cast rays only against colliders in layer 8.
// // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
// layerMask = ~layerMask;

// RaycastHit hit;
// //Debug.Log("doing");
// Collider[] objs = Physics.OverlapSphere(transform.position, explosionRange);
//// Debug.Log(objs.Length);
// //if (objs.Length == 0) return;
// foreach (Collider obj in objs)
// {
//     Debug.DrawRay(transform.position, obj.transform.position);
//     if (Physics.Raycast(transform.position, obj.transform.position, out hit, Mathf.Infinity, layerMask))
//     {
//         if (obj.GetComponent<Rigidbody>())
//         {
//             //if (obj.gameObject.tag == "Enemy")
//             //{
//             //    Debug.Log("yes");
//             //    obj.GetComponent<EnemyAI>().hp -= 10;
//             //}
//             //if (obj.gameObject.tag == "Player")
//             //{
//             //    Debug.Log("yes");
//             //    obj.GetComponent<PlayerEnergy>().shieldVal -= 20;
//             //}
//             obj.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, 5);
//         }
//        // Debug.Log(obj.GetComponent<Rigidbody>());
//     }
//}