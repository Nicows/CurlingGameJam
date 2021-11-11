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
    public CameraShake cameraShake;
    private Animator animator;
    // private bool isZoomed = true;

    [Header("Movements")]
    [SerializeField] private float movementSpeed = 20f;
    [SerializeField] private float sprintSpeed = 40f;
    public ParticleSystem particleSprint;

    [Header("Kick")]
    public float kickRange = 5f;
    public float minForceKick = 10f;
    public float maxForceKick = 100f;
    public float currentForceKick;
    public float addForceKickOnHold = 75f;
    public ParticleSystem particleKick;
    private AudioSource kickSound;

    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
        kickSound = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        currentForceKick = minForceKick;
    }

    void Update()
    {
        InputMovements();
        CheckForceKick();
        // CheckZoom();
    }
    private void InputMovements()
    {
        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (Input.GetKey(KeyCode.LeftShift))
            rbPlayer.velocity = direction * sprintSpeed;
        else
            rbPlayer.velocity = direction * movementSpeed;
    }
    public void CheckZoom()
    {
        // if (Input.GetKeyDown(KeyCode.E)) {
        //     if(isZoomed){
        //         Cinemachine.CinemachineVirtualCamera cvc = cameraShake.GetComponent<Cinemachine.CinemachineVirtualCamera>();
        //         cvc.m_Lens.OrthographicSize = 40;
        //         isZoomed = false;
        //     }else {
        //         Cinemachine.CinemachineVirtualCamera cvc = cameraShake.GetComponent<Cinemachine.CinemachineVirtualCamera>();
        //         cvc.m_Lens.OrthographicSize = 30;
        //         isZoomed = true;
        //     }
            
        // }
    }
    public Vector3 GetPosition()
    {
        return rbPlayer.transform.position;
    }
    public void resetPosition()
    {
        rbPlayer.position = new Vector3(-40f, 0f, 0f);
    }

    private void CheckForceKick()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (currentForceKick < maxForceKick)
            {
                currentForceKick += addForceKickOnHold * Time.deltaTime;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space)) KickRock();
    }

    private void KickRock()
    {

        float attackOffset = 20f;
        Vector3 attackDir = (UtilsClass.GetMouseWorldPosition() - GetPosition()).normalized;
        Vector3 targetPosition = GetPosition() + attackDir * attackOffset;

        Collider2D[] hitRocks = Physics2D.OverlapCircleAll(attackPoint.position, kickRange, rockLayer);
        if (hitRocks.Length > 0)
        {
            cameraShake.SetShakeCameraIntensity(7f);
            kickSound.pitch = Random.Range(0.8f, 1.2f);
            kickSound.Play();
            particleKick.Play();
            animator.Play("PlayerKick");
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
