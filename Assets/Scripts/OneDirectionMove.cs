using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneDirectionMove : MonoBehaviour
{
    // 1 - ����������

    /// <summary>
    /// �������� �������
    /// </summary>
    public Vector2 speed = new Vector2(10, 10);

    /// <summary>
    /// ����������� ��������
    /// </summary>
    public Vector2 direction = new Vector2(-1, 0);

    private Vector2 movement;
    private Rigidbody2D rg;

    private void Awake()
    {
        rg = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 2 - �����������
        movement = new Vector2(
          speed.x * direction.x,
          speed.y * direction.y);
    }

    void FixedUpdate()
    {
        // ��������� �������� � Rigidbody
        rg.velocity = movement;
    }

}
