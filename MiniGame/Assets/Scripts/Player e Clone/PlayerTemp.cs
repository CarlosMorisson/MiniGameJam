using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTemp : MonoBehaviour
{
    public static PlayerTemp instance;
    public Transform playerTransform;
    public float followDelay = 1f; // The delay in seconds. Adjust this value to control the follower's delay.
    public float catchUpSpeed = 10f; // The speed at which the follower catches up when the player stops.
    public float speed;
    private Rigidbody2D followerRigidbody;
    public List<Vector2> playerPositions = new List<Vector2>();

    private void Start()
    {
        instance = this;
    }
    private void OnEnable()
    {
        this.transform.position = playerTransform.position;
        followerRigidbody = GetComponent<Rigidbody2D>();

        StartCoroutine(UpdatePlayerPositions());
    }

    private void FixedUpdate()
    {
        if (playerPositions.Count > 0)
        {
            Vector2 targetPosition = playerPositions[0];
            if (playerPositions.Count > 10)
            {
                targetPosition = playerPositions[10];
            }

            followerRigidbody.MovePosition(Vector2.Lerp(followerRigidbody.position, targetPosition,   speed* Time.fixedDeltaTime));
        }
    }

    private IEnumerator UpdatePlayerPositions()
    {
        while (true)
        {
            playerPositions.Insert(0, playerTransform.position);
            if (playerPositions.Count > 11)
            {
                playerPositions.RemoveAt(playerPositions.Count - 1);
            }
            yield return new WaitForSeconds(.05f);
        }
    }
}
