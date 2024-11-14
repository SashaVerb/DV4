using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 speed = new Vector2(50, 50);
    private Vector2 movement;

    Rigidbody2D rg;
    private void Awake()
    {
        rg = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        movement = new Vector2(
          speed.x * inputX,
          speed.y * inputY);

        // 5 - ��������
        bool shoot = Input.GetButton("Fire1");
        shoot |= Input.GetButton("Fire2");
        // ���������: ��� ������������� Mac, Ctrl + ������� - ��� ������ ����
        if (shoot)
        {
            Weapon weapon = GetComponent<Weapon>();
            if (weapon != null)
            {
                // ����, ��� ��� ����� �� ����
                weapon.Attack(false);
            }
        }
    }
    void FixedUpdate()
    {
        rg.velocity = movement;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        bool damagePlayer = false;

        // ������������ � ������
        EnemyBrain enemy = collision.gameObject.GetComponent<EnemyBrain>();
        if (enemy != null)
        {
            // ������ �����
            Health enemyHealth = enemy.GetComponent<Health>();
            if (enemyHealth != null) enemyHealth.Damage(enemyHealth.hp);

            damagePlayer = true;
        }

        // ����������� � ������
        if (damagePlayer)
        {
            Health playerHealth = this.GetComponent<Health>();
            if (playerHealth != null) playerHealth.Damage(1);
        }
    }
}
