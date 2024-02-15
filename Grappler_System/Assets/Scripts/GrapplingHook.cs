using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    [SerializeField] private GameObject player = null;
    [SerializeField] private float hook_speed = 0.04f;
    [SerializeField] private float max_connection_distance = 0.0f;
    [SerializeField] private GameObject[] hooks = null;
    private int which_hook_collided = -1;

    private void Update()
    {
        ClickedOnHooks();
        MoveTowardsHook(ClickedOnHooks(), which_hook_collided);
    }

    private bool ClickedOnHooks()
    {
        Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        for (int i = 0; i < hooks.Length; i++)
        {
            bool bMouse_overlapping = hooks[i].GetComponent<Collider2D>().OverlapPoint(point);

            if (bMouse_overlapping)
            {
                hooks[i].transform.position.Normalize();
                which_hook_collided = i;
                break;
            }
            else if (i == hooks.Length - 1)
            {
                which_hook_collided = -1;
                return false;
            }
        }

        return true;
    }

    void MoveTowardsHook(bool can_move_to_hooks, int which_hook_collided)
    {
        if (!can_move_to_hooks) { return; }

        if (Vector2.Distance(hooks[which_hook_collided].transform.position, player.transform.position) <=
            max_connection_distance)
        {
            Debug.DrawLine(hooks[which_hook_collided].transform.position, player.transform.position, Color.green);

            Vector2 force_direction = hooks[which_hook_collided].transform.position - player.transform.position;
            force_direction.Normalize();

            player.GetComponent<Rigidbody2D>().AddRelativeForce(force_direction * hook_speed, ForceMode2D.Impulse);

            if (player.GetComponent<Collider2D>().IsTouching(hooks[which_hook_collided].GetComponent<Collider2D>()))
            {
                Debug.Log("Dead!!");
            }
        }
    }
}
