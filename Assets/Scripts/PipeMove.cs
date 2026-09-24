using UnityEngine;

public class PipeMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float deadZone = -45f;

    private LogicScript logic;

    void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Logic");
        if (go != null)
            logic = go.GetComponent<LogicScript>();
    }

    void Update()
    {
        // Stop moving when game is over
        if (logic != null && logic.gameIsOver)
            return;

        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        if (transform.position.x < deadZone)
            Destroy(gameObject);
    }
}
