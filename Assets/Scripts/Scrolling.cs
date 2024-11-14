using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// ������ ���������-����������, ������� ������ ���� �������� ��� ����
public class Scrolling : MonoBehaviour
{
    // �������� ���������
    public Vector2 speed = new Vector2(10, 10);

    // ����������� ��������
    public Vector2 direction = new Vector2(-1, 0);

    // �������� ������ ���� ��������� � ������
    public bool isLinkedToCamera = false;

    // 1 � ����������� ���
    public bool isLooping = false;

    // 2 � ������ ����� � ����������
    private List<Transform> backgroundPart;

    public bool isUsingOffset = false;
    public float offsetX;
    public float offsetY;
    void Start()
    {
        // ������ ��� ������������ ����
        if (isLooping)
        {
            // ������������� ���� ����� ���� � ����������
            backgroundPart = new List<Transform>();

            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);

                backgroundPart.Add(child);
            }
            // ���������� �� �������.
            // ����������: �������� ����� ����� �������.
            // �� ������ �������� ��������� ������� ��� ���������
            // ������ ����������� ���������.
            backgroundPart = backgroundPart.OrderBy(
              t => t.position.x
            ).ToList();
        }
    }

    void Update()
    {
        // �����������
        Vector3 movement = new Vector3(
          speed.x * direction.x,
          speed.y * direction.y,
          0);

        movement *= Time.deltaTime;
        transform.Translate(movement);

        // ����������� ������
        if (isLinkedToCamera)
        {
            Camera.main.transform.Translate(movement);
        }

        // 4 - Loop
        if (isLooping)
        {
            // ��������� ������� �������
            Transform firstChild = backgroundPart.FirstOrDefault();

            if (firstChild != null)
            {
                // ���������, ��������� �� ������� (��������) ����� �������
                if (firstChild.position.x < Camera.main.transform.position.x)
                { 
                    // ���� ������� ��� ����� �� ������
                    SpriteRenderer firstChildRenderer = firstChild.GetComponent<SpriteRenderer>();

                    if (firstChildRenderer != null && firstChildRenderer.isVisible == false)
                    {
                        // �������� ��������� ������� �������
                        Transform lastChild = backgroundPart.LastOrDefault();

                        if (lastChild != null)
                        {
                            SpriteRenderer lastChildRenderer = lastChild.GetComponent<SpriteRenderer>();
                            Vector3 lastPosition = lastChild.transform.position;

                            // ���������� bounds ��� ��������� ��������� �������
                            Vector3 lastSize = lastChildRenderer.bounds.size;

                            // ���������� ������ ������ ����� ����������
                            if (isUsingOffset)
                            {
                                firstChild.position = new Vector3(lastPosition.x + lastSize.x + offsetX, offsetY * (Random.value - 0.5f) * 2f, firstChild.position.z);
                            }
                            else
                            {
                                firstChild.position = new Vector3(lastPosition.x + lastSize.x, firstChild.position.y, firstChild.position.z);
                            }
                            // ��������� ������ ������ � ����� ������
                            backgroundPart.Remove(firstChild);
                            backgroundPart.Add(firstChild);
                        }
                    }
                }
            }
        }
    }
}
