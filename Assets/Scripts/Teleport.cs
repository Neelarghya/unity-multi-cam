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

            Vector3 axisOfRotation = Vector3.Cross(transform.forward, player.forward).normalized;

            float angleBetween = Vector3.Angle(transform.forward, player.forward);

            player.position = target.position;
            Vector3 newForward = Quaternion.AngleAxis(angleBetween, axisOfRotation) * target.forward;
            float angle = Vector3.Angle(player.forward, newForward);
            int sign = (int) Mathf.Sign(Vector3.Cross(player.forward, newForward).y);

            player.forward = newForward;
            rigidbody.velocity = player.forward * rigidbody.velocity.magnitude;

            firstPersonController.mouseLook.AddYRotationOffset(sign * angle);
        }
    }
}