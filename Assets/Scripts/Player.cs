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

        // 5 - Стрельба
        bool shoot = Input.GetButton("Fire1");
        shoot |= Input.GetButton("Fire2");
        // Замечание: Для пользователей Mac, Ctrl + стрелка - это плохая идея
        if (shoot)
        {
            Weapon weapon = GetComponent<Weapon>();
            if (weapon != null)
            {
                // ложь, так как игрок не враг
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

        // Столкновение с врагом
        EnemyBrain enemy = collision.gameObject.GetComponent<EnemyBrain>();
        if (enemy != null)
        {
            // Смерть врага
            Health enemyHealth = enemy.GetComponent<Health>();
            if (enemyHealth != null) enemyHealth.Damage(enemyHealth.hp);

            damagePlayer = true;
        }

        // Повреждения у игрока
        if (damagePlayer)
        {
            Health playerHealth = this.GetComponent<Health>();
            if (playerHealth != null) playerHealth.Damage(1);
        }
    }
}
