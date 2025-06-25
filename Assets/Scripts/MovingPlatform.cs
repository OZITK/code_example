using UnityEngine;

namespace OZITK
{
    public class MovingPlatform : MonoBehaviour
    {
        [Tooltip("Movement distance vector")]
        [SerializeField] private Vector3 moveOffset;
        [SerializeField] private float moveSpeed;

        private Vector3 startPos;
        private Vector3 endPos => startPos + moveOffset;
        private bool invertMove; // Direction toggle flag
        private Transform player;

        private void Start()
        {
            startPos = transform.position;
        }

        /// <summary>
        /// Move platform, update player position if on platform
        /// </summary>
        private void LateUpdate()
        {
            Vector3 newPos = Vector3.MoveTowards(transform.position, (invertMove ? startPos : endPos), moveSpeed * Time.deltaTime);
            if (player != null)
                player.position += newPos - transform.position;
            transform.position = newPos;

            if (transform.position == startPos)
            {
                invertMove = false;
            }
            if (transform.position == endPos)
            {
                invertMove = true;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                player = other.transform.parent;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                player = null;
            }
        }
    }
}
