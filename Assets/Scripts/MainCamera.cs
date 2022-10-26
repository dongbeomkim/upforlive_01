using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    Transform target;
    float speed = 1f;

    public Vector2 center;
    public Vector2 size;

    float height;
    float width;

    private void Awake()
    {
        
    }

    void Start()
    {
        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;   
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center,size);
    }

    void LateUpdate()
    {
        if(GameManager.Instance != null)
        {
            target = GameManager.Instance.Player.transform;

            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);

            float Lx = size.x * 0.5f - width;
            float clampX = Mathf.Clamp(transform.position.x, -Lx + center.x, Lx + center.x);

            float Ly = size.y * 0.5f - height;
            float clampY = Mathf.Clamp(transform.position.y, -Ly + center.y, Ly + center.y);

            transform.position = new Vector3(clampX, clampY, -10f);
        }
        else
        {
            
        }
        
    }
}
