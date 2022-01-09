using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSlimeController : MonoBehaviour
{
    public int numberOfClicksToDisable = 3;
    public List<GameObject> slimePrefabs;
    public float speed = 3f;
    public float timeToWaitForAnim = 2.45f;

    private int currentClickedAmount;
    private bool lockedOnTarget;
    private Transform target;
    private Animator m_Animator;

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!lockedOnTarget)
        {
            target = GetClosestSlime(GameManager.instance.SlimeTransforms.ToArray());
            if(target != null)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
        }
    }

    Transform GetClosestSlime(Transform[] targets)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in targets)
        {
            if(t == transform)
            {
                continue;
            }
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var slime = collision.transform.GetComponent<SlimeController>();
        if (slime != null)
        {
            lockedOnTarget = true;
            slime.ableToMove = false;
            StartCoroutine(Explode(collision.gameObject));
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
        var bomb = collision.transform.GetComponent<BombSlimeController>();
        if (bomb != null)
        {
            lockedOnTarget = true;
            StartCoroutine(Explode(collision.gameObject));
            gameObject.GetComponent<Collider2D>().enabled = false;
        }

    }

    public IEnumerator Explode(GameObject obj)
    {
        if (m_Animator != null)
        {
            m_Animator.SetTrigger("Explode");
        }
        if (obj != null)
        {
            yield return new WaitForSeconds(timeToWaitForAnim);
            if(obj != null)
            {
                EventManager.SlimeDestroy(obj);
            }
        }
        EventManager.SlimeDestroy(gameObject);

        yield return null;

    }

    private void OnMouseDown()
    {
        currentClickedAmount++;

        if(currentClickedAmount >= numberOfClicksToDisable)
        {
            var randSlime = Random.Range(0, slimePrefabs.Count);
            Instantiate(slimePrefabs[randSlime], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
