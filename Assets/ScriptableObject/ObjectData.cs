using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Object Data", menuName = "Scriptable/Object", order = 0)]
public class ObjectData : ScriptableObject
{
    public string objectName; // 물건 이름
    public Sprite sprite;     // 이미지
    public int count;         // 횟수
    public int gold;          // 골드 량
}
