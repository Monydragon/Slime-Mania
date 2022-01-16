using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPinController : MonoBehaviour
{
    public List<GameObject> ContainedSlimes = new List<GameObject>();

    private void OnEnable()
    {
        EventManager.onSlimeConvertToPoints += EventManager_onSlimeConvertToPoints;
    }

    private void EventManager_onSlimeConvertToPoints()
    {
        foreach (GameObject obj in ContainedSlimes)
        {
            if (obj != null)
            {
                EventManager.SlimeConvert(obj);
                var currentSlime = obj.GetComponent<SlimeController>();
                if (currentSlime != null)
                {
                    if (GameManager.Instance.SlimeTypeMultiplier == currentSlime.slimeType)
                    {
                        GameManager.Instance.Coins += currentSlime.baseCoins * GameManager.Instance.MultiplierForSlimeType;
                    }
                    else
                    {
                        GameManager.Instance.Coins += currentSlime.baseCoins * GameManager.Instance.StandardMultiplier;
                    }
                    EventManager.CoinsChanged();
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
            }
            if (!ContainedSlimes.Contains(collision.gameObject))
            {
                ContainedSlimes.Remove(collision.gameObject);
            }
        }
    }
}
