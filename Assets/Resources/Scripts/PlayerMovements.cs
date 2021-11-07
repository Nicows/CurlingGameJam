using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovements : MonoBehaviour
{
    [Header("Attack Components")]
    public Transform attackPoint;
    public float kickRange = 1.5f;
    public LayerMask rockLayer;

    [SerializeField] private float movementSpeed = 2f;
    [SerializeField] private float maxSpeed = 2f;
    private Rigidbody2D rbPlayer;
    public GameObject firstRock;

    // Start is called before the first frame update
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        InputMovements();

    }
    private void InputMovements()
    {
        if (Input.GetKeyDown(KeyCode.Space)) KickRock();

        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        // rbPlayer.AddForce(direction.normalized * movementSpeed * Time.deltaTime, ForceMode2D.Force);
        rbPlayer.velocity = direction.normalized * movementSpeed * Time.deltaTime * 800;
        // if(!LimitSpeed()){
        //     GetComponent<Rigidbody2D>().velocity = direction.normalized * movementSpeed * Time.deltaTime;
        // }

    }


    public void KickRock()
    {

        Collider2D[] hitRocks = Physics2D.OverlapCircleAll(attackPoint.position, kickRange, rockLayer);

        foreach (Collider2D rock in hitRocks)
        {
            Debug.Log("Rock : " + rock.name + " kicked");
            rock.GetComponent<Rigidbody2D>().AddForce(Vector2.right, ForceMode2D.Impulse);

        }

    }
    private void AssistKickDirection()
    {
        CreateSceneParameters parameters = new CreateSceneParameters(LocalPhysicsMode.Physics2D);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, kickRange);
    }
}
