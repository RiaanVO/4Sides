using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private float _timer;
    private int _bulletType;
    private Vector3 _bulletPos;
    private GameObject bullet;

    public float fireRate;

    [Header ("Bullets")]
    public GameObject bullet1;
    public GameObject bullet2;
    public GameObject bullet3;
    public GameObject bullet4;

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
                bullet = bullet1;
                break;
            case 2:
                bullet = bullet2;
                break;
            case 3:
                bullet = bullet3;
                break;
            case 4:
                bullet = bullet4;
                break;
            default:
                bullet = bullet1;
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