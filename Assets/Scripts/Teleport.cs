using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Transform player = other.transform;
            Rigidbody rigidbody = player.GetComponent<Rigidbody>();
            RigidbodyFirstPersonController firstPersonController =
                player.GetComponent<RigidbodyFirstPersonController>();
            Vector3 transformForward = transform.forward;

            SetVelocity(rigidbody, transformForward);
            SetDirection(player, firstPersonController, transformForward);
        }
    }

    private void SetDirection(Transform player, RigidbodyFirstPersonController firstPersonController,
        Vector3 transformForward)
    {
        Vector3 axisOfRotation = Vector3.Cross(transformForward, player.forward).normalized;
        float angleBetween = Vector3.Angle(transformForward, player.forward);

        player.position = target.position;
        Vector3 newForward = Quaternion.AngleAxis(angleBetween, axisOfRotation) * target.forward;
        float angle = Vector3.Angle(player.forward, newForward);
        int sign = (int) Mathf.Sign(Vector3.Cross(player.forward, newForward).y);

        player.forward = newForward;

        firstPersonController.mouseLook.AddYRotationOffset(sign * angle);
    }

    private void SetVelocity(Rigidbody rigidbody, Vector3 transformForward)
    {
        var velocityNormalized = rigidbody.velocity.normalized;
        Vector3 axisOfRotation = Vector3.Cross(transformForward, velocityNormalized).normalized;
        float angleBetween = Vector3.Angle(transformForward, velocityNormalized);

        Vector3 newVelocityDirection = Quaternion.AngleAxis(angleBetween, axisOfRotation) * target.forward;

        rigidbody.velocity = newVelocityDirection * rigidbody.velocity.magnitude;
    }
}