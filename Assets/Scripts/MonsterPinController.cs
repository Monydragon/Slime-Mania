using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPinController : MonoBehaviour
{
    public List<GameObject> ContainedSlimes = new List<GameObject>();
    public Vector2 minPinRange = new Vector2(-8.3f, 0.5f);
    public Vector2 maxPinRange = new Vector2(-4.45f, 4.45f);
    public Vector2 defaultArenaMinRange = new Vector2(-8.3f, -4.45f);
    public Vector2 defaultArenaMaxRange = new Vector2(8.3f, 4.45f);

    private void OnEnable()
    {
        EventManager.onSlimeConvertToPoints += EventManager_onSlimeConvertToPoints;
    }

    private void EventManager_onSlimeConvertToPoints()
    {
        Debug.Log($"Slimes to Convert: {ContainedSlimes.Count}");

        foreach (GameObject obj in ContainedSlimes)
        {
            if (obj != null)
            {
                var currentSlime = obj.GetComponent<SlimeController>();
                if (currentSlime != null)
                {
                    if (GameManager.instance.SlimeTypeMultiplier == currentSlime.slimeType)
                    {
                        GameManager.instance.Score += currentSlime.baseScore * GameManager.instance.MultiplierForSlimeType;
                    }
                    else
                    {
                        GameManager.instance.Score += currentSlime.baseScore * GameManager.instance.StandardMultiplier;
                    }
                    EventManager.ScoreChanged();
                    Destroy(obj);
                }
            }

        }
        ContainedSlimes.Clear();

    }

    private void OnDisable()
    {
        EventManager.onSlimeConvertToPoints -= EventManager_onSlimeConvertToPoints;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Slime")
        {
            var slime = collision.GetComponent<SlimeController>();
            if(slime != null)
            {
                slime.isCaptured = true;
                slime.minRangeWalk = minPinRange;
                slime.maxRangeWalk = maxPinRange;
            }
            if (!ContainedSlimes.Contains(collision.gameObject))
            {
                ContainedSlimes.Add(collision.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Slime")
        {
            var slime = collision.GetComponent<SlimeController>();
            if (slime != null)
            {
                slime.isCaptured = false;
                slime.minRangeWalk = defaultArenaMinRange;
                slime.maxRangeWalk = defaultArenaMaxRange;
            }
            if (!ContainedSlimes.Contains(collision.gameObject))
            {
                ContainedSlimes.Remove(collision.gameObject);
            }
        }
    }
}
