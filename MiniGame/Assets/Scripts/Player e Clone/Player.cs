using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    public static Player instance;
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
    private bool Returned;
    [SerializeField] Image coolDownBar;
    [SerializeField] float coolDownShadow;
    public AudioSource SoundEffect;
    [SerializeField] float coolDownTotalShadow;
    [Header ("Skill De Parar O Tempo")]
    [SerializeField] float coolDownFreezeTime;
    [SerializeField] float coolDownTotalFreezeTime;
    private bool startCountFreeze;
    private ParticleSystem particle;
    public Animator anim;
    public virtual void Start()
    {
        instance = this;
        rig = GetComponent<Rigidbody2D>();
        coolDownShadow = coolDownTotalShadow;
        coolDownFreezeTime = coolDownTotalFreezeTime;
        particle = GetComponentInChildren<ParticleSystem>();
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
        FreezeTime();
    }
    private void FreezeTime()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            
            if(StopTime.instance.stopTime==false)
            {
                coolDownFreezeTime = 0;
                StopTime.instance.Stop();
                StopTime.instance.UpdateBar(coolDownFreezeTime, coolDownTotalFreezeTime);
                startCountFreeze = true;
            }
            else
            {
                StopTime.instance.stopTime = false;
                StopTime.instance.FreezeTimeImage.gameObject.SetActive(false);
                CountFreezeCoolDown();
                startCountFreeze = true;
            }
        }
        if (startCountFreeze)
        {
            CountFreezeCoolDown();
        }
    }
    private void CountFreezeCoolDown()
    {
        if (coolDownFreezeTime < coolDownTotalFreezeTime)
        {
            coolDownFreezeTime += Time.deltaTime;
            StopTime.instance.UpdateBar(coolDownFreezeTime, coolDownTotalFreezeTime);
        }
        else
        {
            startCountFreeze = false;
            StopTime.instance.stopTime = false;
            StopTime.instance.FreezeTimeImage.gameObject.SetActive(false);
        }

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlataformDestroyer"))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
    private void Jump()
    {
        rig.velocity = new Vector2(rig.velocity.x, jumpForce);
        Returned = false;
    }
    private void ChangeLocal()
    {
        if (Input.GetKey(KeyCode.Q) && coolDownShadow>= coolDownTotalShadow)
        {
            coolDownShadow = 0;
            this.transform.localPosition = rastro.transform.localPosition;
            PlayerTemp.instance.playerPositions.Clear();
            rastro.gameObject.SetActive(false);
            Returned = true;
            startCount = true;
            particle.Play();
            SoundEffect.Play();
        }
        if (startCount)
        {
            if(coolDownShadow < coolDownTotalShadow)
            {
                coolDownShadow += Time.deltaTime;
                coolDownBar.fillAmount = coolDownShadow / coolDownTotalShadow;
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

        anim.SetBool("Run", movement != 0);

        if (movement > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (movement < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
}
