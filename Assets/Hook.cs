using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hook : MonoBehaviour, ICollideObservable
{
    Transform head, wire;
    Vector2 originPos;
    public Player owner;

    private void Awake()
    {
        head = transform.Find("Head").GetComponent<Transform>();
        wire = transform.Find("Wire").GetComponent<Transform>();
        originPos = transform.position;
        CollideObserver.Instance.SubscribeCollider(this, true);
    }

    private IEnumerator MoveAnimation()
    {
        float t = 0;
        while(true)
        {
            t += Time.deltaTime;
            head.position = Vector2.Lerp(
                head.position, 
                head.position + new Vector3(
                    Math.Sign(Input.mousePosition.x), 
                    Math.Sign(Input.mousePosition.y)
                ), 
                t
            );
            yield return null;
        }
    }
    private IEnumerator HookAnimation()
    {
        float t = 0;
        while (owner.transform.position != head.transform.position)
        {
            t += Time.deltaTime;
            owner.transform.position = Vector2.Lerp(owner.transform.position, head.transform.position, t);
            yield return null;
        }
        Destroy(gameObject);
    }

    Coroutine moveCoroutine;
    public void MoveTo()
    {
        originPos = transform.position;
        moveCoroutine = StartCoroutine(MoveAnimation());
        wire.position = originPos;
        wire.rotation = new(0, 0, Vector2.Angle(wire.position, head.position), 0);
    }

    GameObject ICollideObservable.GetGameObject() => head.gameObject;

    void ICollideObservable.OnCollided(GameObject gameObject)
    {
        if (moveCoroutine != null && gameObject != this.gameObject && gameObject.name != "Player")
        {
            StopCoroutine(moveCoroutine);
            moveCoroutine = null;
            StartCoroutine(HookAnimation());
        }
    }
}
