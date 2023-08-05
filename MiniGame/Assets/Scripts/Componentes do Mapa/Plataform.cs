using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataform : MonoBehaviour
{
    private Rigidbody2D rig;
    [Range(0f, 50f)]
    [SerializeField]
    private float speed;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
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
    }
}
