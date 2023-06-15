using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Move : MonoBehaviour
{
    public Transform[] points;
    public Transform player;
    public float speed;
    private float[] distances;
    private float delayTime;
    private Vector3 direction;

    private void Start()
    {
        distances = new float[points.Length];
    }

    private void Update()
    {
        delayTime += Time.deltaTime;
        if (delayTime >= 2)
        {
            List<Transform> validPoints = new List<Transform>(points);

        // Loại bỏ các điểm có collider chạm vào "Wall"
        foreach (Transform point in points)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, 0.1f);
            foreach (Collider2D col in colliders)
            {
                if (col.CompareTag("Wall"))
                {
                    validPoints.Remove(point);
                    break;
                }
            }
            Debug.DrawLine(point.position + Vector3.left * 0.1f, point.position + Vector3.right * 0.1f, Color.red);
        }

        // Tính khoảng cách từ ghost đến player cho các điểm còn lại
        
            float[] distances = new float[validPoints.Count];
            for (int i = 0; i < validPoints.Count; i++)
            {
                distances[i] = Vector3.Distance(validPoints[i].position, player.position);
            }

            // Tìm khoảng cách ngắn nhất và chọn điểm tương ứng
            float minDistance = Mathf.Min(distances);
            int closestIndex = Array.IndexOf(distances, minDistance);

            // Lấy hướng di chuyển của ghost
            direction = validPoints[closestIndex].position - transform.position;
            delayTime = 0;
        }
        transform.position += direction.normalized * speed * Time.deltaTime;
    }
}