using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController cc;
    [SerializeField]
    private Transform camTran;
    [SerializeField]
    private float moveSpeed = 10.0f;
    [SerializeField]
    private float jumpPower = 100.0f;
    private const float Y_ANGLE_MIN = -50.0f;
    private const float Y_ANGLE_MAX = 75.0f;
    float mouseX;
    float mouseY;
    float g = 5f;
    private float horizontal;
    Vector3 movement = Vector3.zero;
    public bool canMove,highSpeed;

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        horizontal = transform.eulerAngles.y;
        cc = this.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            TurnCam();
            playerMovement();
        }
        else
        {
            movement.y -= g * Time.deltaTime;//重力
            cc.Move(movement * Time.deltaTime);//移動を反映
        }
    }

    void TurnCam()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY -= Input.GetAxis("Mouse Y");
        mouseY = Mathf.Clamp(mouseY, Y_ANGLE_MIN, Y_ANGLE_MAX);
        camTran.Rotate(mouseY, 0, 0);
        horizontal = (horizontal + mouseX) % 360f;
        //キャラの回転
        transform.rotation = Quaternion.AngleAxis(horizontal, Vector3.up);
        //カメラの回転
        camTran.rotation = Quaternion.Euler(mouseY, horizontal, 0);


    }
    /// <summary>
    /// プレイヤーの移動関数
    /// </summary>
    void playerMovement()
    {

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 25.0f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = 15.0f;
        }
        if (Input.GetKey(KeyCode.W))
        {
            cc.Move(this.gameObject.transform.forward * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            cc.Move(this.gameObject.transform.forward * -1f * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            cc.Move(this.gameObject.transform.right * -1 * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            cc.Move(this.gameObject.transform.right * moveSpeed * Time.deltaTime);
        }
        if (CheckGrounded())//地面にいる場合
        {
            g = 5f;
            if (Input.GetKeyDown(KeyCode.Space))//スペース押したら
            {
                g *= 50;
                movement.y = jumpPower;//ジャンプ
            }
        }
        else
        {
            g++;
        }
        movement.y -= g * Time.deltaTime;//重力
        cc.Move(movement * Time.deltaTime);//移動を反映

    }
    //rayを下に照射、地面に当たったら着地状態
    public bool CheckGrounded()
    {
        var controller = GetComponent<CharacterController>();
        if (controller.isGrounded) { return true; }
        var ray = new Ray(this.transform.position , Vector3.down);
        var tolerance = 2f;
       // Debug.Log(Physics.Raycast(ray, tolerance));
        return Physics.Raycast(ray, tolerance);
    }
}
