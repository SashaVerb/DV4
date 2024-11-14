using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    // 1 � ���������� ���������

    /// <summary>
    /// ����������� ����
    /// </summary>
    public int damage = 1;

    /// <summary>
    /// ������ ������� ����������� ������ ��� ������?
    /// </summary>
    public bool isEnemyShot = false;
    
    void Start()
    {
        // ������������ ����� �����, ����� �������� ������
        Destroy(gameObject, 20); // 20 ������
    }
}
