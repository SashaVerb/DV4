using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class Health : MonoBehaviour
{
    /// <summary>
    /// ����� ����������
    /// </summary>
    public int hp = 1;

    /// <summary>
    /// ���� ��� �����?
    /// </summary>
    public bool isEnemy = true;

    /// <summary>
    /// ������� ���� � ��������� ������ �� ������ ���� ���������
    /// </summary>
    /// <param name="damageCount"></param>
    public void Damage(int damageCount)
    {
        hp -= damageCount;

        if (hp <= 0)
        {
            // ������!
            if (isEnemy)
            {
                GetComponent<EnemyBrain>().Restart();
                transform.position += new Vector3(20, 0, 0);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        // ��� �������?
        Shot shot = otherCollider.gameObject.GetComponent<Shot>();
        if (shot != null)
        {
            // ��������� �������������� ����
            if (shot.isEnemyShot != isEnemy)
            {
                Damage(shot.damage);

                // ���������� �������
                Destroy(shot.gameObject); // ������ �������� � ������� ������, ����� �� ������ ������� ������.      }
            }
        }
    }
}
