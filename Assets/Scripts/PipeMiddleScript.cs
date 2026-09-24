using UnityEngine;

public class PipeMiddleScript : MonoBehaviour
{
    public LogicScript logic;

    void Start()
    {
        if (logic == null)
        {
            GameObject go = GameObject.FindGameObjectWithTag("Logic");
            if (go != null) logic = go.GetComponent<LogicScript>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Only give points when the PLAYER passes through the score zone
        if (collision.CompareTag("Player"))
        {
            logic.addScore(1);
            logic.PlayClip(logic.pointClip);
        }
    }
}
