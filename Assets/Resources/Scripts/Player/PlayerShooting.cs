using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private float _timer;
    private int _bulletType;
    private Vector3 _bulletPos;

    public float fireRate;

    public GameObject bullet;
   
    void Awake()
    {
        fireRate = 0.5f;
    }

    void Update()
    {
        _timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && _timer >= fireRate)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        switch (_bulletType)
        {
            case 1:
                bullet = Resources.Load("Prefabs/Bullet") as GameObject;
                break;
            default:
                bullet = Resources.Load("Prefabs/Bullet") as GameObject;
                break;
        }

        _timer = 0f;
        Instantiate(bullet, transform.position, transform.rotation);
        
    }

    public void UpdateBullet(int bulletType)
    {
        _bulletType = bulletType;
    }
}