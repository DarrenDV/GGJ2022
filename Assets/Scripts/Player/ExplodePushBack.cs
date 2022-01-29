using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodePushBack : MonoBehaviour
{
    [SerializeField] private float radius = 100000f;
    [SerializeField] private float force = 10000000f;

    public void Update()
    {
        Detonate();

        if (Input.GetKeyDown(KeyCode.C))
        {
            Detonate();
            //Debug.Log("Aids");
        }
    }

    public void Detonate()
    {
        Vector2 explosionPos = this.transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPos, radius);
        foreach(Collider2D collider in colliders)
        {
            
            if(collider.tag == "MeleeEnemy")
            {
                Debug.Log("hot");
                Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
                AddExplosionForce2D(rb, this.transform.position, force, radius);

                if (rb != null)
                {
                    

                }
            }
        }
    }

    void AddExplosionForce2D(Rigidbody2D rb, Vector3 explosionOrigin, float explosionForce, float explosionRadius)
    {
        Vector3 direction = transform.position - explosionOrigin;
        float forceFalloff = 1 - (direction.magnitude / explosionRadius);
        rb.AddForce(direction.normalized * (forceFalloff <= 0 ? 0 : explosionForce) * forceFalloff);
    }
}
