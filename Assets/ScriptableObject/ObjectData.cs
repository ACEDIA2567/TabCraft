using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Object Data", menuName = "Scriptable/Object", order = 0)]
public class ObjectData : ScriptableObject
{
    public string objectName; // ���� �̸�
    public Sprite sprite;     // �̹���
    public int count;         // Ƚ��
    public int gold;          // ��� ��
}
