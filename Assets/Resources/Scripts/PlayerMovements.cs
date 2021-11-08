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
    public Rigidbody2D rbPlayer;
    public GameObject firstRock;

    [Header("Kick")]
    public float minForceKick = 5f;
    public float maxForceKick = 20f;
    public float currentForceKick = 5f;


    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        InputMovements();
        CheckForceKick();
    }
    private void InputMovements()
    {
        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rbPlayer.velocity = direction * movementSpeed;
    }
    public Vector3 GetPosition()
    {
        return rbPlayer.transform.position;
    }
    public void resetPosition()
    {
        rbPlayer.position = Vector3.zero;
    }

    private void CheckForceKick()
    {
        if(Input.GetKey(KeyCode.Space)){
            if(currentForceKick < maxForceKick){
                currentForceKick += 10 * Time.deltaTime;
            }
        }
        if(Input.GetKeyUp(KeyCode.Space)) KickRock();
    }

    private void KickRock()
    {
        float attackOffset = 20f;
        Vector3 attackDir = (UtilsClass.GetMouseWorldPosition() - GetPosition()).normalized;
        Vector3 targetPosition = GetPosition() + attackDir * attackOffset;

        Collider2D[] hitRocks = Physics2D.OverlapCircleAll(attackPoint.position, kickRange, rockLayer);
        foreach (Collider2D rock in hitRocks)
        {
            targetPosition = rock.transform.position;
            attackDir = (rock.transform.position - GetPosition()).normalized;
            rock.GetComponent<Rigidbody2D>().AddForce(attackDir * currentForceKick, ForceMode2D.Impulse);
        }
        currentForceKick = minForceKick;

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, kickRange);
    }
}
