using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganAir : MonoBehaviour
{
    private GameObject playerObject;
    private Rigidbody2D rig;
    [Range(2f, 10f)]
    public float speed;
    private Vector2 vector;
    [SerializeField] private ParticleSystem particles;
    private void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player"); 
        rig = playerObject.GetComponent<Rigidbody2D>();
        vector = this.gameObject.transform.localScale;
        particles = GetComponentInChildren<ParticleSystem>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            rig.velocity = new Vector2(rig.velocity.x, speed);
        }
    }
    private void Update()
    {
        if (StopTime.instance.stopTime)
        {
            this.gameObject.transform.localScale = new Vector2(0, 0);
            particles.Stop();
        }
        else
        {
            this.gameObject.transform.localScale = vector;
            particles.Play();
        }
            
    }
}
