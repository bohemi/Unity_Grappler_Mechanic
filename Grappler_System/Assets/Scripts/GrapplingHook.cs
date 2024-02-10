using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    [SerializeField] private Transform player_position = null;
    [SerializeField] private float roap_reach_speed = 0.0f;
    [SerializeField] private float max_connection_distance = 0.0f;

    private void Update()
    {
        player_position.position.Normalize();
        transform.position.Normalize();

        if (player_position.position.x >= transform.position.x &&
            player_position.position.x <= transform.position.x + max_connection_distance)
        {
            Debug.DrawLine(transform.position, player_position.position, Color.green);
        }
    }
}
