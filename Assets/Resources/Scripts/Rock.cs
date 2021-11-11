using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    private ParticleSystem particuleRockHit;
    private const float PLAYER_DISTANCE_GENERATE_LINE_RENDERER = 5f;
    private Transform player;
    private LineRenderer lineRenderer;

    private void Awake()
    {
        particuleRockHit = GetComponentInChildren<ParticleSystem>();
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lineRenderer = GetComponentInChildren<LineRenderer>();
    }
    private void Update()
    {
        // if (Vector3.Distance(this.transform.position, player.position) <= PLAYER_DISTANCE_GENERATE_LINE_RENDERER) SetUpLine();
        // else lineRenderer.gameObject.SetActive(false);
    }
    public void PlayCollision()
    {
        particuleRockHit.Play();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Rock")
        {
            GameObject.FindObjectOfType<RocksInteractions>().Collision(this);
        }
    }

    private void SetUpLine()
    {
        lineRenderer.gameObject.SetActive(true);
        float lineLength = 15f;
        Vector3 startLinePos = transform.position;
        Vector3 endLinePos = (transform.position - player.position) * lineLength;

        lineRenderer.SetPosition(0, startLinePos);
        lineRenderer.SetPosition(1, endLinePos);

    }
}
