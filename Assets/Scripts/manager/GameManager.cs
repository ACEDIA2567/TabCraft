using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private List<ObjectData> datas;
    [SerializeField] private GameObject CraftObject;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Transform spawnPoint;
    public TextMeshProUGUI goldText;

    public int attack = 1;
    private int attackGold = 200;
    public TextMeshProUGUI attackText;

    public int autoAttack = 1;
    private int autoAttackGold = 300;
    public TextMeshProUGUI autoAttackText;

    public float autoTime = 2.0f;
    private int autoTimeGold = 500;
    public TextMeshProUGUI autoTimeText;

    private ObjectData data;
    public int attackValue = 1;
    [SerializeField] private TextMeshProUGUI levelText;
    public int currentLevel = 1;
    public int currentGold = 0;
    public int currentExp = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        SpawnObject();
    }

    public void ExpUp()
    {
        currentExp++;
        if(currentExp == 10)
        {
            currentLevel++;
            levelText.text = "Level: " + currentLevel;
            currentExp = 0;
        }
    }

    public void SpawnObject()
    {   
        data = Instantiate(datas[Random.Range(0, datas.Count)]);
        GameObject clone = Instantiate(CraftObject, spawnPoint.position, Quaternion.identity);
        clone.GetComponent<SpriteRenderer>().sprite = data.sprite;
        if(clone.TryGetComponent<CraftObject>(out CraftObject craftObject))
        {
            nameText.text = data.objectName;
            craftObject.GetInit(data.count, data.gold, currentLevel);
        }
        ExpUp();
    }

    public void AttackUpgrade()
    {
        if (UpdateUI(attackText, ref attackGold))
        {
            attack *= 2;
        }
    }

    public void AutoAttackUpgrade()
    {
        if (UpdateUI(autoAttackText, ref autoAttackGold))
        {
            autoAttack *= 2;
        }
    }

    public void AutoAttackTimeUpgrade()
    {
        if (UpdateUI(autoTimeText, ref autoTimeGold))
        {
            autoTime -= 0.2f;
        }
    }

    public bool UpdateUI(TextMeshProUGUI upgradetext, ref int upgradeGold)
    {
        if (currentGold > upgradeGold)
        {
            currentGold -= upgradeGold;
            upgradeGold *= 2;
            upgradetext.text = upgradeGold.ToString() + "G";
            goldText.text = "Gold: " + currentGold.ToString() + "G";
            return true;
        }
        return false;
    }
}
