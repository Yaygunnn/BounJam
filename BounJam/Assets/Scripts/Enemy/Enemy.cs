using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Color IceColor;
    Color BaseColor;
    [SerializeField] float IceTime;
    [SerializeField] float speed;
    private Vector3 direction;
    private BaseNode targetNode;
    private BaseRope currentrope;
    void Start()
    {
        BaseColor = spriteRenderer.color;
        spriteRenderer.color = IceColor;
        spriteRenderer.enabled = true;
        Invoke("StartMove", IceTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartMove()
    {
        spriteRenderer.color = BaseColor;

        StartCoroutine(Move());
    }
    IEnumerator Move()
    {
        while (true)
        {
            Vector3 pos = transform.position;
            pos += direction * speed * Time.deltaTime;
            transform.position = pos;

            if (math.dot(direction, targetNode.transform.position - transform.position) < 0)
            {
                transform.position = targetNode.transform.position;
                DecideNewRoad();
            }
            yield return null;
        }
        
    }
    public void Initiate(BaseRope rope)
    {
        BaseNode node = rope.GetNodes()[0];
        direction = (node.transform.position - transform.position).normalized;
        targetNode = node;
        currentrope = rope;
        currentrope.E_RopeCut += Death;
    }

    private void DecideNewRoad()
    {
        currentrope.E_RopeCut -= Death;

    }

    private void Death()
    {
        Destroy(gameObject);
    }
}