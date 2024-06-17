using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftObject : MonoBehaviour
{
    private float minCount = 1;
    public float maxCount;
    public int gold;
    private SpriteRenderer spriteRenderer;
    Color color;

    private void Start()
    {
        StartCoroutine(AutoClick());
    }

    void OnMouseDown()
    {
        UpdateInfo(true);
    }

    public void GetInit(float getCount, int getGold, int level)
    {
        maxCount = getCount * level;
        gold = getGold * level;
        transform.AddComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(1, 1, 1, 0.1f);
        color = spriteRenderer.color;
    }

    IEnumerator AutoClick() 
    {
        while (true)
        {
            UpdateInfo(false);
            yield return new WaitForSeconds(GameManager.Instance.autoTime);
        }
    }

    private void UpdateInfo(bool check)
    {
        if (check)
        {
            minCount += GameManager.Instance.attack;
        }
        else
        {
            minCount += GameManager.Instance.autoAttack;
        }

        color.a = minCount / maxCount;
        spriteRenderer.color = color;
        GameManager.Instance.currentGold += gold;
        GameManager.Instance.goldText.text = "Gold: " + GameManager.Instance.currentGold.ToString() + "G";
        if (maxCount == minCount)
        {
            GameManager.Instance.SpawnObject();
            Destroy(gameObject);
        }
    }
}
