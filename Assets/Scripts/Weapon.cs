using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //--------------------------------
    // 1 � ���������� ���������
    //-------------------------------
    /// <summary>
    /// ������ ������� ��� ��������
    /// </summary>
    public Transform shotPrefab;

    /// <summary>
    /// ����� ����������� � ��������
    /// </summary>
    public float shootingRate = 0.25f;
    //--------------------------------
    // 2 - �����������
    //--------------------------------
    private float shootCooldown;

    void Start()
    {
        shootCooldown = 0f;
    }

    void Update()
    {
        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        }
    }
    //--------------------------------
    // 3 - �������� �� ������� �������
    //--------------------------------

    /// <summary>
    /// �������� ����� ������, ���� ��� ��������
    /// </summary>
    public void Attack(bool isEnemy)
    {
        if (CanAttack)
        {
            shootCooldown = shootingRate;

            // �������� ����� �������
            var shotTransform = Instantiate(shotPrefab, transform.position, transform.rotation) as Transform;

            // �������� �����
            Shot shot = shotTransform.gameObject.GetComponent<Shot>();
            if (shot != null)
            {
                shot.isEnemyShot = isEnemy;
            }

            // �������� ���, ����� ������� ������ ��� ��������� �� ����
            OneDirectionMove move = shotTransform.gameObject.GetComponent<OneDirectionMove>();
            if (move != null)
            {
                move.direction = this.transform.right; // � ���������� ������������ ��� ����� ������ �� �������
            }
        }
    }

    /// <summary>
    /// ������ �� ������ ��������� ����� ������?
    /// </summary>
    public bool CanAttack
    {
        get
        {
            return shootCooldown <= 0f;
        }
    }

}
