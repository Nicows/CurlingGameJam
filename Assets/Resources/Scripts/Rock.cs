using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    private ParticleSystem particuleRockHit;

    private void Start()
    {
        particuleRockHit = GetComponentInChildren<ParticleSystem>();
    }
    public void PlayCollision()
    {
        particuleRockHit.Play();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Rock")
        {
            GetComponentInParent<RocksInteractions>().Collision(this);
        }
    }
}
