//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class BirdScript : MonoBehaviour
//{
//    public Rigidbody2D myRigidbody;
//    public float flapStrength;
//    public LogicScript logic;
//    public bool birdIsAlive = true;

//    void Start()
//    {
//        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
//    }

//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.Space) && birdIsAlive)
//        {
//            myRigidbody.linearVelocity = Vector2.up * flapStrength;

//            // play flap sound
//            logic.PlayClip(logic.flapClip);
//        }
//    }

//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//        if (!birdIsAlive) return; // avoid double-triggering

//        birdIsAlive = false;

//        // play hit and die sounds (will mix). If you want only die, remove one.
//        logic.PlayClip(logic.hitClip);
//        logic.PlayClip(logic.dieClip);

//        logic.gameOver();

//        // optionally disable controls/physics or call other death routines here
//        // e.g. GetComponent<Collider2D>().enabled = false;
//    }
//}
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    [Header("Refs")]
    public Rigidbody2D rb;
    public LogicScript logic;

    [Header("Gameplay")]
    public float flapStrength = 8f;
    public bool birdIsAlive = true;

    [Header("Debug")]
    public bool debugMode = false; // turn on to see collision logs

    void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        if (logic == null)
        {
            GameObject g = GameObject.FindGameObjectWithTag("Logic");
            if (g != null) logic = g.GetComponent<LogicScript>();
        }
    }

    void Update()
    {
        if (!birdIsAlive) return;

        bool flap = Input.GetKeyDown(KeyCode.Space) ||
                    Input.GetMouseButtonDown(0) ||
                    (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began);

        if (flap)
        {
            // correct API: velocity
            rb.linearVelocity = Vector2.up * flapStrength;
            logic?.PlayClip(logic.flapClip);
        }
    }

    // Helper: checks tag on object and all parents
    private bool HasTagInHierarchy(GameObject go, string tag)
    {
        if (go == null) return false;
        Transform t = go.transform;
        while (t != null)
        {
            // safe access: don't use CompareTag if tag may be missing in project
            if (t.gameObject.tag == tag) return true;
            t = t.parent;
        }
        return false;
    }

    private bool IsScoreZone(GameObject go) => HasTagInHierarchy(go, "ScoreZone");
    private bool IsPipe(GameObject go) => HasTagInHierarchy(go, "Pipe");
    private bool IsGround(GameObject go) => HasTagInHierarchy(go, "Ground");

    private void LogHit(string kind, GameObject obj)
    {
        if (!debugMode) return;
        string parent = obj.transform.parent ? obj.transform.parent.name : "null";
        string rootTag = obj.transform.root ? obj.transform.root.tag : "null";
        Debug.Log($"{kind}: name='{obj.name}' tag='{obj.tag}' parent='{parent}' rootTag='{rootTag}' layer='{LayerMask.LayerToName(obj.layer)}'");
    }

    // Trigger (Is Trigger = ON)
    private void OnTriggerEnter2D(Collider2D other)
    {
        LogHit("[TRIGGER]", other.gameObject);

        // ignore score zone
        if (IsScoreZone(other.gameObject)) return;

        if (IsPipe(other.gameObject) || IsGround(other.gameObject))
        {
            Die("trigger");
        }
    }

    // Solid collision (Is Trigger = OFF)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        LogHit("[COLLISION]", collision.gameObject);

        // check the collider contact points' GameObjects too (in case child colliders used)
        GameObject hitObj = collision.collider != null ? collision.collider.gameObject : collision.gameObject;

        if (IsScoreZone(hitObj)) return;

        if (IsPipe(hitObj) || IsGround(hitObj))
        {
            Die("collision");
        }
    }

    private void Die(string cause)
    {
        if (!birdIsAlive) return;
        birdIsAlive = false;

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.gravityScale = 0f;
            rb.angularVelocity = 0f;
        }

        logic?.PlayClip(logic.hitClip);
        logic?.PlayClip(logic.dieClip);
        logic?.gameOver();

        // hide bird
        gameObject.SetActive(false);

        if (debugMode) Debug.Log($"[BirdScript] Died via {cause}");
    }
}







