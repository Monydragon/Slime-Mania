using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public SlimeType slimeType;
    public int baseCoins = 1;
    public Vector2 minRangeWalk;
    public Vector2 maxRangeWalk;
    public float speed = 2f;
    public float timeBetweenWalking = 3f;
    public float timer = 0f;
    public Vector2 randomPos;
    public bool isCaptured;
    public bool ableToMove = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ableToMove)
        {
            if (timer <= 0)
            {
                timer = timeBetweenWalking;
                randomPos = new Vector2(Random.Range(minRangeWalk.x, maxRangeWalk.x), Random.Range(minRangeWalk.y, maxRangeWalk.y));
            }
            else
            {
                timer -= Time.deltaTime;
            }

            transform.position = Vector2.MoveTowards(transform.position, randomPos, speed * Time.deltaTime);

        }
    }

    private void OnMouseDrag()
    {
        var screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z);
        var newWorldPos = Camera.main.ScreenToWorldPoint(screenPos);
        newWorldPos = new Vector3(Mathf.Clamp(newWorldPos.x, minRangeWalk.x, maxRangeWalk.x), Mathf.Clamp(newWorldPos.y, minRangeWalk.y, maxRangeWalk.y),transform.position.z);
        transform.position = newWorldPos;
        ableToMove = true;
    }
}
