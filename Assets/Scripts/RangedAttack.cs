using UnityEngine;

public class RangedAttack : MonoBehaviour

{
    public Transform RangedWeapon;

    Vector2 direction;

    public GameObject Projectile;

    public float ProjectileSpeed;

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePos - (Vector2)RangedWeapon.position;
        FaceMouse();

        if (Input.GetMouseButtonDown(0))
        {
            shoot();
        }

    }
    void FaceMouse()
    {
        RangedWeapon.transform.right = direction;
    }
    void shoot()
    {
        GameObject ProjectileInst = Instantiate(Projectile, RangedWeapon.position, RangedWeapon.rotation);
        ProjectileInst.GetComponent<Rigidbody2D>().AddForce(ProjectileInst.transform.right * ProjectileSpeed);
    }
}
