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

    private ObjectData data;
    public int attackValue = 1;
    public int currentLevel = 1;
    private int currentExp = 0;
    public int currentGold = 0;

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
}
