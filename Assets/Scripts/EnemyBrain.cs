using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    private bool hasSpawn;
    private OneDirectionMove move;
    private Weapon[] weapons;

    private Collider2D collider;
    private SpriteRenderer spriteRenderer;
    void Awake()
    {
        collider = GetComponent<Collider2D>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        // Получить оружие только один раз
        weapons = GetComponentsInChildren<Weapon>();

        // Отключить скрипты, чтобы деактивировать объекты при отсутствии спавна
        move = GetComponent<OneDirectionMove>();
    }

    // 1 - Отключить все
    void Start()
    {
        hasSpawn = false;

        // Отключить
        // -- коллайдеры
        collider.enabled = false;
        // -- Перемещение
        move.enabled = false;
        // -- стрельбу
        foreach (Weapon weapon in weapons)
        {
            weapon.enabled = false;
        }
    }

    void Update()
    {
        // 2 - Проверить, начался ли спавн врагов.
        if (hasSpawn == false)
        {
            if (spriteRenderer.isVisible)
            {
                Spawn();
            }
        }
        else
        {
            // автоматическая стрельба
            foreach (Weapon weapon in weapons)
            {
                weapon.Attack(true);
            }

            // 4 – Выход за рамки камеры? Уничтожить игровой объект.
            if (spriteRenderer.isVisible == false)
            {
                Destroy(gameObject);
            }
        }
    }

    // 3 - Самоактивация.
    private void Spawn()
    {
        hasSpawn = true;

        // Включить все
        // -- Коллайдеры
        collider.enabled = true;
        // -- Перемещение
        move.enabled = true;
        // -- Стрельбу
        foreach (Weapon weapon in weapons)
        {
            weapon.enabled = true;
        }
    }

}
