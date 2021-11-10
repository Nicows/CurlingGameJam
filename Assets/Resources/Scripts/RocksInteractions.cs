using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocksInteractions : MonoBehaviour
{
    public CameraShake cameraShake;
    protected AudioSource rockHitSound;
    private bool hasFirstCollided = false;
    
    [Header ("Instanciate Spots")]
    public GameObject rockPrefab;
    private const float MIN_X = -30f;
    private const float MAX_X = 30f;
    private const float MIN_Y = -16f;
    private const float MAX_Y = 16f;

    void Start()
    {
        rockHitSound = GetComponent<AudioSource>();
        InstantiateRocks();
    }


    public void Collision(Rock rock)
    {
        if (hasFirstCollided == false)
        {
            EffectsCollision();
            rock.PlayCollision();
            hasFirstCollided = true;
        }
        else
        {
            hasFirstCollided = false;
        }
    }
    private void EffectsCollision()
    {
        cameraShake.SetShakeCameraIntensity(3f);
        rockHitSound.pitch = Random.Range(0.7f, 0.9f);
        rockHitSound.Play();
        
    }
    public void InstantiateRocks()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject instantiatedObject = Instantiate(rockPrefab, this.transform);
            instantiatedObject.transform.position = new Vector2(Random.Range(MIN_X, MAX_X), Random.Range(MIN_Y, MAX_Y));
        }
    }
}
