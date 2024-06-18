# TabCraft

## 게임 설명
### 장르: 클리커
### 조작법: 마우스 클릭
### 게임 방법: 물품들을 클릭하여 제작하는 게임

## 구현 내용
### 1. 아이템 생성
|아이템 사진|
|:---:|
|![image](https://github.com/ACEDIA2567/TabCraft/assets/101154683/71f47aef-09be-48b4-86c6-4fd08713c368)|

```cs
[CreateAssetMenu(fileName = "Object Data", menuName = "Scriptable/Object", order = 0)]
public class ObjectData : ScriptableObject
{
    public string objectName; // 물건 이름
    public Sprite sprite;     // 이미지
    public float count;         // 횟수
    public int gold;          // 골드 량
}
```

### 2. 클릭 시 아이템 횟수를 증가시키고 알파 값도 같이 증가시킴
```cs
private SpriteRenderer spriteRenderer;

public void GetInit(float getCount, int getGold, int level)
{
    maxCount = getCount * level;
    gold = getGold * level;
    transform.AddComponent<BoxCollider2D>();
    spriteRenderer = GetComponent<SpriteRenderer>();
    spriteRenderer.color = new Color(1, 1, 1, 0.1f);
    color = spriteRenderer.color;
}

// 클릭 시
color.a = minCount / maxCount;
spriteRenderer.color = color;
```

### 3. UI 구현
|UI 사진|
|:---:|
|![image](https://github.com/ACEDIA2567/TabCraft/assets/101154683/1e87592b-9ae4-462b-9de6-b8478c19a22e)|

### 4. 강화 구현
**각 강화별로 Text와 골드, 강화 수치를 변경하게 하게 함**
```cs
// 수동 공격 강화
public void AttackUpgrade()
{
    if (UpdateUI(attackText, ref attackGold))
    {
        attack *= 2;
    }
}

// 자동 공격 강화
public void AutoAttackUpgrade()
{
    if (UpdateUI(autoAttackText, ref autoAttackGold))
    {
        autoAttack *= 2;
    }
}

// 자동 시간 강화
public void AutoAttackTimeUpgrade()
{
    if (UpdateUI(autoTimeText, ref autoTimeGold))
    {
        autoTime -= 0.2f;
    }
}

// UI 갱신 및 강화 관련 값 변경
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
```

### 5. 자동과 수동 구현
**코루틴을 이용하여 수동의 메서드 내용을 그대로 사용하여 진행**
```cs
private void Start()
{
    // 오브젝트 시작 시 오토 등록
    StartCoroutine(AutoClick());
}

// 수동으로 마우스 클릭
void OnMouseDown()
{
    UpdateInfo(true);
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
    if (minCount >= maxCount)
    {
        GameManager.Instance.SpawnObject();
        Destroy(gameObject);
    }
}
```
