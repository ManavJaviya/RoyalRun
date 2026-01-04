using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector2 movement;
    new Rigidbody rigidbody;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float xClamp = 3f;
    [SerializeField] float zClamp = 2f;
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();   
    }

    void FixedUpdate()
    {
        HandelMovement();   
    }
    public void Move(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>(); 
    }

    public void HandelMovement()
    {
        Vector3 currentPos = rigidbody.position;
        Vector3 moveDir = new Vector3(movement.x, 0, movement.y);
        Vector3 newPos = currentPos + moveDir * moveSpeed * Time.fixedDeltaTime;

        newPos.x = Mathf.Clamp(newPos.x, -xClamp, xClamp);
        newPos.z = Mathf.Clamp(newPos.z, -zClamp, zClamp);
        rigidbody.MovePosition(newPos);
    }

}
