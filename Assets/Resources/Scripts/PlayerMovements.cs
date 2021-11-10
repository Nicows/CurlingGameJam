using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovements : MonoBehaviour
{
    [Header("Attack Components")]
    public Transform attackPoint;
    private Rigidbody2D rbPlayer;
    public LayerMask rockLayer;

    [Header ("Movements")]
    [SerializeField] private float movementSpeed = 2f;
    // [SerializeField] private float maxSpeed = 2f;
    public ParticleSystem particleSprint;

    [Header("Kick")]
    public float kickRange = 3f;
    public float minForceKick = 10f;
    public float maxForceKick = 40f;
    public float currentForceKick;
    public float addForceKickOnHold = 30f;
    public ParticleSystem particleKick;
    private AudioSource kickSound;
    
    public CameraShake cameraShake;

    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
        kickSound = GetComponent<AudioSource>();
        currentForceKick = minForceKick;
    }

    void Update()
    {
        InputMovements();
        CheckForceKick();
    }
    private void InputMovements()
    {
        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        // if(Input.GetAxis("Horizontal") == 0f && Input.GetAxis("Vertical") == 0f){
        //     // Debug.Log("noinput");
        // }else if(rbPlayer.velocity.magnitude >= maxSpeed){
        //     rbPlayer.velocity = direction * movementSpeed;
        // }else if(rbPlayer.velocity.magnitude < maxSpeed){
        //     // rbPlayer.velocity += direction * movementSpeed * 5 * Time.deltaTime;
        // }

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
        if(Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)){
            if(currentForceKick < maxForceKick){
                currentForceKick += addForceKickOnHold * Time.deltaTime;
            }
        }
        if(Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0)) KickRock();
    }

    private void KickRock()
    {
        
        float attackOffset = 20f;
        Vector3 attackDir = (UtilsClass.GetMouseWorldPosition() - GetPosition()).normalized;
        Vector3 targetPosition = GetPosition() + attackDir * attackOffset;

        Collider2D[] hitRocks = Physics2D.OverlapCircleAll(attackPoint.position, kickRange, rockLayer);
        if(hitRocks.Length > 0){
            cameraShake.SetShakeCameraIntensity(7f);
            kickSound.pitch = Random.Range(0.8f, 1.2f);
            kickSound.Play();
            particleKick.Play();
        }
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
