using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int damage;
    [SerializeField] DmgType dmgType;
    [SerializeField] ParticleSystem hitExplosion;
    [SerializeField] int maxDistance = 500;

    Rigidbody _bulletRigidbody;
    public GameObject _owner;
    int _distanceCovered;

    void Awake()
    {
        _bulletRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed);

        _distanceCovered++;
        if (_distanceCovered > maxDistance)
        {
            Instantiate(hitExplosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }


    public void OnShooted(GameObject owner)
    {
        _owner = owner;
    }

    void OnTriggerEnter(Collider other)
    {
        var lifeform = other.GetComponent<LifeForm>();
        if (lifeform)
            lifeform.TakeDamage(new DmgInfo(damage, dmgType), gameObject);

        Instantiate(hitExplosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
