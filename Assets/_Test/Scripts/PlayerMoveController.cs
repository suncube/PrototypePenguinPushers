using UnityEngine;
using System.Collections;

public class PlayerMoveController : MonoBehaviour
{
    [SerializeField]
    private PlayerSettings m_Settings;
    [SerializeField]
    private Rigidbody Rigidbody;

    void FixedUpdate()
    {
        Processing();
    }

    private void Processing()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = transform.forward * moveVertical;
        Rigidbody.AddForce(movement * m_Settings.Speed);
      
    }

    private void Rotate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        transform.rotation *= Quaternion.Euler(0f, moveHorizontal * m_Settings.RotateSpeed, 0f);
    }
}