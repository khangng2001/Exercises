using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [Header("Mouse")]
    [SerializeField] private Transform display;
    private Vector3 screenPosition;
    private Vector3 worldPosition;
    [SerializeField] private LayerMask aimLayer;

    [Header("Setting")]
    [SerializeField] private float minRange;
    [SerializeField] private float maxRange;
    private Vector3 locationAimCurrent;
    [SerializeField] private Vector3 offsetAim;
    [SerializeField] private float rotateSpeed;

    [SerializeField] private Transform shootPoint;
    [SerializeField] private ParticleSystem VFX;
    [SerializeField] private List<GameObject> bulletPool;

    private void Start()
    {
        InitBulletPool();
    }

    private void Update()
    {
        GetPositionMouse();
        ClaimAIm();
        DirectionToAimPoint();

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void GetPositionMouse()
    {
        screenPosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, aimLayer))
        {
            worldPosition = hit.point;
        }
    }

    void ClaimAIm()
    {
        float distance = Vector3.Distance(worldPosition, new Vector3(transform.position.x, worldPosition.y, transform.position.z));
        if (distance < maxRange && distance > minRange)
        {
            locationAimCurrent = worldPosition;
        }

        display.position = locationAimCurrent;
    }

    void DirectionToAimPoint()
    {
        Vector3 direction = (locationAimCurrent + offsetAim) - transform.position;
        Vector3 direction2 = transform.position - (locationAimCurrent + offsetAim);
        transform.up = Vector3.Slerp(transform.up, direction, rotateSpeed * Time.deltaTime);
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction, direction), rotateSpeed * Time.deltaTime);
    }

    void InitBulletPool()
    {
        foreach (GameObject bullet in bulletPool)
        {
            bullet.SetActive(false);
        }
    }

    GameObject GetBullet()
    {
        foreach(GameObject bullet in bulletPool)
        {
            if (!bullet.activeSelf)
            {
                return bullet;
            }
        }

        GameObject newBullet = Instantiate(bulletPool[0], bulletPool[0].transform.parent);
        newBullet.SetActive(false);
        bulletPool.Add(newBullet);
        return GetBullet();
    }

    void Shoot()
    {
        RunVXF();
        GameObject bullet = GetBullet();
        BulletController bulletController = bullet.GetComponent<BulletController>();
        if (bulletController != null)
        {
            bulletController.Init(shootPoint.position, locationAimCurrent, offsetAim.y / 2);
        }
        bullet.SetActive(true);
    }

    void RunVXF()
    {
        if (VFX)
        {
            VFX.Play();
        }
    }
}
