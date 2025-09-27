using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{

    [Header("Angle")]
    //theta
    [SerializeField]
    float deg_Angle;
    [SerializeField]
    float speed;

    [SerializeField] Transform player;   // 캐릭터(중심점)

    [Header("Cricle")]
    [SerializeField]
    int iter = 0;
    [SerializeField]
    float radius;

    [Header("Skill")]
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    Transform bulletHolder;

    [SerializeField]
    int interval;
    [SerializeField]
    int start_deg_Angle;
    [SerializeField]
    int end_deg_Angle;


    float currentAngle;

    float c_Angle;

    [SerializeField]
    public int angle_distance = 50;

    void Update()
    {
        //MoveAngle(deg_Angle);

        if(Input.GetMouseButtonDown(0))
        {
            VolleyShot();
        }
    }


    private void FixedUpdate()
    {
        MoveCircle(radius);
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = new Vector2(0f, 0f);
        }
    }



    private void MoveAngle(float _deg_Angle)
    {
        var rad_Angle = DegreesToRadians(_deg_Angle);
        Vector2 duration = new Vector2(Mathf.Cos(rad_Angle), Mathf.Sin(rad_Angle));
        transform.Translate(duration * speed * Time.deltaTime);
    }

    private void MoveCircle(float _radius)
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 dir = mouseWorldPos - player.position;

        float baseAngle = Mathf.Atan2(dir.y, dir.x);

        currentAngle += speed * Mathf.Deg2Rad * Time.deltaTime;

        float finalAngle = baseAngle + currentAngle;

        Vector2 offset = new Vector2(Mathf.Cos(finalAngle), Mathf.Sin(finalAngle)) * radius;

        transform.position = (Vector2)player.position + offset;
    }

    private void VolleyShot()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 dir = mouseWorldPos - player.position;

        float baseAngle = Mathf.Atan2(dir.y, dir.x);
        
        c_Angle += speed * Mathf.Deg2Rad * Time.deltaTime;

        float finalAngle = baseAngle + c_Angle;

        float angleDeg = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        if (angleDeg < 0) angleDeg += 360f;

        int s_angle = (((int)angleDeg - 20) % 360 + 360) % 360;
        int e_angle = (((int)angleDeg + 20) % 360 + 360) % 360;

        if (e_angle < s_angle) e_angle += 360;

        Debug.Log(s_angle);
        Debug.Log(e_angle);

        for (int i = s_angle; i < e_angle; i += interval)
        {
            int angle = i % 360;

            var bullet = Instantiate(bulletPrefab, bulletHolder);
            var rad_Angle = DegreesToRadians(angle);
            Vector2 direction = new Vector2(Mathf.Cos(rad_Angle), Mathf.Sin(rad_Angle));
            bullet.transform.position = this.transform.position;
            // Debug.Log(direction);
            bullet.GetComponent<Bullet>().direction = direction;

            bullet.name = $"butllet_{i}";
        }
    }

    private float DegreesToRadians(float _angle)
    {
        return _angle * Mathf.Deg2Rad;
    }

}