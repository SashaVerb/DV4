using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    // 1 – Переменная дизайнера

    /// <summary>
    /// Причиненный вред
    /// </summary>
    public int damage = 1;

    /// <summary>
    /// Снаряд наносит повреждения игроку или врагам?
    /// </summary>
    public bool isEnemyShot = false;
    
    void Start()
    {
        // Ограниченное время жизни, чтобы избежать утечек
        Destroy(gameObject, 20); // 20 секунд
    }
}
