using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(EdgeCollider2D))]
public class Tail : MonoBehaviour {

    Transform player;

    public Transform Snake;

    public float pointSpacing = 1.0105f;
    public float invicibleTime = 3f;
    public bool invicible = false;

    private List<Vector2> points;

    private LineRenderer line;
    private EdgeCollider2D coll;

    public float baseSnakeWidth = 0.1f;
    [HideInInspector]
    public float snakeWidth;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        coll = GetComponent<EdgeCollider2D>();

        StartCoroutine(MakeInvicible(invicibleTime));

        points = new List<Vector2>();

        player = GetComponentInParent<Transform>();

        coll.offset = new Vector2(-player.position.x, -player.position.y);

        snakeWidth = baseSnakeWidth;
    }

    void Update()
    {
        line.startWidth = snakeWidth;
        line.endWidth = snakeWidth;

        if(!invicible)
            if (Vector3.Distance(points.Last(), Snake.position) > pointSpacing)
                setPoint();
    }

    void setPoint()
    {
        if(points.Count > 2)
        {
            coll.points = points.ToArray<Vector2>();
        }

        points.Add(Snake.position);

        line.numPositions = points.Count;
        line.SetPosition(points.Count - 1, Snake.position);

    }

    IEnumerator MakeInvicible(float time)
    {
        invicible = true;
        yield return new WaitForSeconds(time);
        setPoint();
        invicible = false;
    }

}
