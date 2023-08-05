using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataform : MonoBehaviour
{
    public static Plataform instance;
    private Rigidbody2D rig;
    [Range(0f, 50f)]
    [SerializeField]
    public float speed;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        instance = this;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlataformDestroyer"))
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        rig.velocity = new Vector2(0, speed);
        if (StopTime.instance.stopTime)

            speed = 0;
        else
            speed = 50;
    }
}
