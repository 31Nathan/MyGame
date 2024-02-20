using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private bool isMoving;
    private Vector2 input;

    private Animator animator;
    public LayerMask solidObjectsLayer;

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
    }

    private bool isWakable(Vector3 targetPos) {
        return Physics2D.OverlapCircle(targetPos, 0.1f, solidObjectsLayer) == null;
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
}

