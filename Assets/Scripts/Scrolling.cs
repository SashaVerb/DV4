using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// Скрипт параллакс-скроллинга, который должен быть прописан для слоя
public class Scrolling : MonoBehaviour
{
    // Скорость прокрутки
    public Vector2 speed = new Vector2(10, 10);

    // Направление движения
    public Vector2 direction = new Vector2(-1, 0);

    // Движения должны быть применены к камере
    public bool isLinkedToCamera = false;

    // 1 – Бесконечный фон
    public bool isLooping = false;

    // 2 – Список детей с рендерером
    private List<Transform> backgroundPart;

    public bool isUsingOffset = false;
    public float offsetX;
    public float offsetY;
    void Start()
    {
        // Только для бесконечного фона
        if (isLooping)
        {
            // Задействовать всех детей слоя с рендерером
            backgroundPart = new List<Transform>();

            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);

                backgroundPart.Add(child);
            }
            // Сортировка по позиции.
            // Примечание: получаем детей слева направо.
            // Мы должны добавить несколько условий для обработки
            // разных направлений прокрутки.
            backgroundPart = backgroundPart.OrderBy(
              t => t.position.x
            ).ToList();
        }
    }

    void Update()
    {
        // Перемещение
        Vector3 movement = new Vector3(
          speed.x * direction.x,
          speed.y * direction.y,
          0);

        movement *= Time.deltaTime;
        transform.Translate(movement);

        // Перемещение камеры
        if (isLinkedToCamera)
        {
            Camera.main.transform.Translate(movement);
        }

        // 4 - Loop
        if (isLooping)
        {
            // Получение первого объекта
            Transform firstChild = backgroundPart.FirstOrDefault();

            if (firstChild != null)
            {
                // Проверить, находится ли ребенок (частично) перед камерой
                if (firstChild.position.x < Camera.main.transform.position.x)
                { 
                    // Если ребенок уже слева от камеры
                    SpriteRenderer firstChildRenderer = firstChild.GetComponent<SpriteRenderer>();

                    if (firstChildRenderer != null && firstChildRenderer.isVisible == false)
                    {
                        // Получить последнюю позицию ребенка
                        Transform lastChild = backgroundPart.LastOrDefault();

                        if (lastChild != null)
                        {
                            SpriteRenderer lastChildRenderer = lastChild.GetComponent<SpriteRenderer>();
                            Vector3 lastPosition = lastChild.transform.position;

                            // Используем bounds для получения реального размера
                            Vector3 lastSize = lastChildRenderer.bounds.size;

                            // Перемещаем первый объект после последнего
                            if (isUsingOffset)
                            {
                                firstChild.position = new Vector3(lastPosition.x + lastSize.x + offsetX, offsetY * (Random.value - 0.5f) * 2f, firstChild.position.z);
                            }
                            else
                            {
                                firstChild.position = new Vector3(lastPosition.x + lastSize.x, firstChild.position.y, firstChild.position.z);
                            }
                            // Поставить первый объект в конец списка
                            backgroundPart.Remove(firstChild);
                            backgroundPart.Add(firstChild);
                        }
                    }
                }
            }
        }
    }
}
