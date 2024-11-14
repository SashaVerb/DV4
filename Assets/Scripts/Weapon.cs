using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //--------------------------------
    // 1 – Переменные дизайнера
    //-------------------------------
    /// <summary>
    /// Префаб снаряда для стрельбы
    /// </summary>
    public Transform shotPrefab;

    /// <summary>
    /// Время перезарядки в секундах
    /// </summary>
    public float shootingRate = 0.25f;
    //--------------------------------
    // 2 - Перезарядка
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
    // 3 - Стрельба из другого скрипта
    //--------------------------------

    /// <summary>
    /// Создайте новый снаряд, если это возможно
    /// </summary>
    public void Attack(bool isEnemy)
    {
        if (CanAttack)
        {
            shootCooldown = shootingRate;

            // Создайте новый выстрел
            var shotTransform = Instantiate(shotPrefab, transform.position, transform.rotation) as Transform;

            // Свойство врага
            Shot shot = shotTransform.gameObject.GetComponent<Shot>();
            if (shot != null)
            {
                shot.isEnemyShot = isEnemy;
            }

            // Сделайте так, чтобы выстрел всегда был направлен на него
            OneDirectionMove move = shotTransform.gameObject.GetComponent<OneDirectionMove>();
            if (move != null)
            {
                move.direction = this.transform.right; // в двухмерном пространстве это будет справа от спрайта
            }
        }
    }

    /// <summary>
    /// Готово ли оружие выпустить новый снаряд?
    /// </summary>
    public bool CanAttack
    {
        get
        {
            return shootCooldown <= 0f;
        }
    }

}
