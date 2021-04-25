using UnityEngine;

public static class MoveController
{
    public static void Move(Transform transform, float horizontal, float movementSpeed)
    {
        if (horizontal < -0.01)
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        else if (horizontal > 0.01)
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);

        //rb.AddForce(horizontal * Vector2.right * Time.deltaTime * speed, ForceMode2D.Impulse);
        transform.Translate(horizontal * movementSpeed * Time.deltaTime, 0, 0, transform);
    }
}