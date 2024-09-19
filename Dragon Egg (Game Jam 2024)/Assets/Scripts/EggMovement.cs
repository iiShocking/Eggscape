using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EggMovement : MonoBehaviour
{
    [SerializeField] private GameLoopManager gameLoopManager;
    public bool uprightSelf;
    [NonSerialized] public float bounceTime;
    [NonSerialized] public Renderer ren;
    [NonSerialized] public bool inStand = true;
    
    [SerializeField] private bool canMove = true;
    [SerializeField] private float speed;
    [SerializeField] private TMP_Text speedUIText;
    [SerializeField] private TMP_Text interactUIText;
    [SerializeField] private bool shouldTakeDamage = true;
    [SerializeField] private float currentVelocity;
    [SerializeField] private Transform stand;
    private Rigidbody rb;
    private MeshCollider col;
    private bool floating;
    private float startTime;
    private Quaternion startRotation;
    private Quaternion targetRotation;
    private List<GameObject> interactablesInRange = new();
    
    private IInteractable _potentialInteractable;

    [SerializeField] private CheckpointManager checkpointManager;


    private void Awake()
    {
        ren = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 999999f;
        col = rb.GetComponent<MeshCollider>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (_potentialInteractable == null)
            {
                return;
            }
            _potentialInteractable.OnInteract();
        }

        if (bounceTime > 0)
        {
            col.material.bounciness = 1;
            shouldTakeDamage = false;
            bounceTime -= Time.deltaTime;
        }
        else
        {
            col.material.bounciness = 0;
            shouldTakeDamage = true;
        }

        if (interactablesInRange.Count > 0)
        {
            float shortestDistance = 9999f;
            foreach (GameObject interactable in interactablesInRange)
            {
                if ((transform.position - interactable.transform.position).sqrMagnitude < shortestDistance)
                {
                    _potentialInteractable = interactable.GetComponent<IInteractable>();
                }
            }
            DisplayInteractUI();
        }
        else
        {
            HideInteractUI();
        }

        //if (uprightSelf)
        //{
        //    transform.rotation = Quaternion.Slerp(startRotation, targetRotation, Mathf.Sin((Time.time - startTime) * (Mathf.PI / 2) * 5f));
        //    if (Vector3.Angle(transform.up, Vector3.up) < 1f)
        //    {
        //        uprightSelf = false;
        //        rb.angularVelocity = new();
        //    }
        //}
    }

    private void FixedUpdate()
    {
        Vector3 camToEgg = transform.position - Camera.main.transform.position;
        currentVelocity = Mathf.Abs(rb.velocity.x);
        Vector3 moveDirection = new Vector3(camToEgg.x, 0f, camToEgg.z).normalized;
        if (!canMove)
        {
            return;
        }
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(speed * moveDirection * (inStand ? 3f : 1f) + (inStand ? new(0, 3f, 0) : new()));
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-speed * moveDirection * (inStand ? 3f : 1f) + (inStand ? new(0, 3f, 0) : new()));
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-speed * Camera.main.transform.right * (inStand ? 3f : 1f) + (inStand ? new(0, 3f, 0) : new()));
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(speed * Camera.main.transform.right * (inStand ? 3f : 1f) + (inStand ? new(0, 3f, 0) : new()));
        }


        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        if (Input.GetKey(KeyCode.Space) && gameLoopManager.hasWings)
        {
            //GLIDE
            print("GLIDE");
            WingsController.gameobject.SetActive(true);
            WingsController.extend = true;
            rb.AddForce(-Physics.gravity * 0.9f);
        }
        else
        {
            //STOP GLIDING
            WingsController.extend = false;
        }

        if (inStand)
        {
            Vector3 distanceToStand = stand.position - transform.position;
            distanceToStand = new(distanceToStand.x, 0f, distanceToStand.z);
            if (distanceToStand.sqrMagnitude > 0.3f)
            {
                rb.AddForce(-rb.velocity / 2f);
                inStand = false;
            }
        }

        //if (rb.GetAccumulatedForce().sqrMagnitude > 225f)
        //{
        //    rb.AddForce(-rb.GetAccumulatedForce().normalized * (rb.GetAccumulatedForce().magnitude - 15f));
        //    Debug.DrawLine(transform.position, transform.position + rb.GetAccumulatedForce(), Color.red);
        //}
        //else
        //{
        //    Debug.DrawLine(transform.position, transform.position + rb.GetAccumulatedForce());
        //}

        if (rb.velocity.sqrMagnitude > 225f)
        {
            rb.velocity = rb.velocity.normalized * 15f;
        }

        //Vector3 horizontalVelocity = new(rb.velocity.x, 0, rb.velocity.z);
        //if (horizontalVelocity.sqrMagnitude > 25f)
        //{
        //    Vector3 cappedHorizontalVelocity = horizontalVelocity.normalized * 5f;
        //    rb.velocity = new(cappedHorizontalVelocity.x, rb.velocity.y, cappedHorizontalVelocity.z);
        //}

        if (floating)
        {
            rb.AddForce(-Physics.gravity * 1.03f);
        }
        
        UpdateUI();
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.GetContact(0);
        Vector3 normalVelocity = Vector3.Dot(collision.relativeVelocity, contact.normal) * contact.normal;
        Debug.DrawLine(contact.point, contact.point + collision.relativeVelocity, Color.blue, 2f);
        Debug.DrawLine(contact.point, contact.point + normalVelocity, new(1f, 0f, 1f), 2f);
        if (normalVelocity.sqrMagnitude > 169f)
        {
            if (shouldTakeDamage)
            {
                StartCoroutine(Broken());
            }
            else if (bounceTime <= 0)
            {
                print("Phew!");
                ToggleDamageable();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            //_potentialInteractable = other.GetComponent<IInteractable>();
            //DisplayInteractUI();

            interactablesInRange.Add(other.gameObject);
        }

        if (other.CompareTag("Checkpoint"))
        {
            checkpointManager.AssignCheckpoint(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            //_potentialInteractable = null;
            //HideInteractUI();

            interactablesInRange.Remove(other.gameObject);
        }
    }

    public void ToggleDamageable() => shouldTakeDamage = !shouldTakeDamage;
    private IEnumerator Broken()
    {
        ren.material.SetColor("_Color", Color.red);
        canMove = false;
        bounceTime = 0;
        yield return new WaitForSeconds(2);
        checkpointManager.RespawnAtCheckpoint();
        ren.material.SetColor("_Color", new(0.8584906f, 0.6741052f, 0.4486828f));
        canMove = true;
        shouldTakeDamage = false;
    }

    private void UpdateUI()
    {
        speedUIText.text = Mathf.Round(rb.velocity.magnitude) + " m/s";
    }

    private void DisplayInteractUI()
    {
        interactUIText.text = "Press E to interact with " + _potentialInteractable.GetName();
        interactUIText.gameObject.SetActive(true);
    }
    
    private void HideInteractUI() => interactUIText.gameObject.SetActive(false);

    public IEnumerator Float(float time)
    {
        floating = true;
        yield return new WaitForSeconds(time);
        floating = false;
    }

    public IEnumerator Bouncy()
    {
        col.material.bounciness = 1;
        yield return new WaitForSeconds(10);
        col.material.bounciness = 0;
    }

    public void Flap()
    {
        startTime = Time.time;
        startRotation = transform.rotation;
        targetRotation = Quaternion.AngleAxis(Vector3.Angle(transform.up, Vector3.up), Vector3.Cross(transform.up, Vector3.up)) * transform.rotation;
        uprightSelf = true;
        StartCoroutine(AddForceInFixedUpdate());
    }

    public void ChangeSpeed(float targetSpeed, float duration) => StartCoroutine(ChangeMovementSpeed(targetSpeed, duration));

    private IEnumerator AddForceInFixedUpdate()
    {
        yield return new WaitForFixedUpdate();
        rb.AddForce(10f * Vector3.up, ForceMode.Impulse);
        rb.angularVelocity = new();
        rb.AddTorque(Vector3.Angle(transform.up, Vector3.up) / 100f * Vector3.Cross(transform.up, Vector3.up).normalized, ForceMode.Impulse);
    }

    private IEnumerator ChangeMovementSpeed(float targetSpeed, float duration)
    {
        speed = targetSpeed;
        yield return new WaitForSeconds(duration);
        speed = 6;
    }
}