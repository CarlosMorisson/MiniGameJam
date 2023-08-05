using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player")]
    public float speed;
    [Tooltip("Genilson")]
    public float jumpForce;
    [HideInInspector]
    public Rigidbody2D rig;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    private bool isGrounded;
    [Header ("Skill De Voltar No Tempo")]
    [SerializeField] private GameObject rastro;
    private bool startCount=true;
    [SerializeField] float coolDown;
    [SerializeField] float coolDownTotal;
    [Header ("Skill De Parar O Tempo")]
    
    private bool Returned;
public virtual void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        coolDown = coolDownTotal;
    }
    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded ||  Returned)
        {
            if(Input.GetKeyDown(KeyCode.Space))
                Jump();
        }
        ChangeLocal();
        Move();
    }

    private void Jump()
    {
        rig.velocity = new Vector2(rig.velocity.x, jumpForce);
        Returned = false;
    }
    private void ChangeLocal()
    {
        if (Input.GetKey(KeyCode.Q) && coolDown>= coolDownTotal)
        {
            coolDown = 0;
            this.transform.localPosition = rastro.transform.localPosition;
            PlayerTemp.instance.playerPositions.Clear();
            rastro.gameObject.SetActive(false);
            Returned = true;
            startCount = true;
        }
        else{
            
            
        }
        if (startCount)
        {
            if(coolDown < coolDownTotal)
            {
                coolDown += Time.deltaTime;
            }
            else
            {
                startCount = false;
                rastro.gameObject.SetActive(true);
            }
        }
        else
        {
           
          
        }
    }
    // Update is called once per frame
    public void Move()
    {
        float movement = Input.GetAxis("Horizontal");
        rig.velocity = new Vector2(movement * speed, rig.velocity.y);
        if (movement > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (movement < 0) ;
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
}
