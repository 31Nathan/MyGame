using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private bool isMoving;
    private Vector2 input;

    private Animator animator;
    public LayerMask solidObjectsLayer;
    public LayerMask interactiblesLayer;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    void Start()
    {

    }

    private void Update()
    {
        if (!isMoving) {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            

            /*
            Cancel Diagonal movement
            if (input.x != 0) {
                input.y = 0;
            }
            */

            if (input != Vector2.zero) {
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                if (isWakable(targetPos)) {
                    StartCoroutine(move(targetPos));
                }
            }
        }
        animator.SetBool("isMoving", isMoving);

        if (Input.GetKeyDown(KeyCode.Z)) {
            Interact();
        }
    }

    private bool isWakable(Vector3 targetPos) {
        return Physics2D.OverlapCircle(targetPos, 0.1f, solidObjectsLayer | interactiblesLayer) == null;
    }

    IEnumerator move(Vector3 targetPos) {
        isMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon) {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;
    }

    void Interact() {
        var facingDir = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        var interactPos = transform.position + facingDir;

        if (Physics2D.OverlapCircle(interactPos, 0.2f, interactiblesLayer) != null) {
            Debug.Log("There is an interactible objet here !");
        }
    }
}

