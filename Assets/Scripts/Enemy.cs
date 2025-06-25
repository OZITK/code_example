using System.Collections;
using UnityEngine;

namespace OZITK
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float health = 200; // Current health
        [SerializeField] private float moveSpeed = 1;
        [Tooltip("Radius of wandering area around starting position")]
        [SerializeField] private float moveRadius = 0.5f;

        private Material material;
        private float maxHealth;
        private Vector3 startPos; // Initial spawn position

        private Color green = new Color(19 / 255f, 154 / 255f, 67 / 255f);
        private Color yellow = new Color(234 / 255f, 196 / 255f, 53 / 255f);
        private Color red = new Color(218 / 255f, 44 / 255f, 56 / 255f);

        private void Awake()
        {
            maxHealth = health;
            startPos = transform.position;
            material = GetComponent<Renderer>().material;
        }

        private void OnEnable()
        {
            StartCoroutine(SimpleWander());
        }

        public void GetHit(float damage)
        {
            if (health <= 0)
                return;

            health -= damage;
            float percent = health / maxHealth * 100;
            if (percent <= 100 && percent > 50)
            {
                material.SetColor("_BaseColor", Color.Lerp(yellow, green, (percent - 50) / 50));
            }
            if (percent <= 50 && percent > 0)
            {
                material.SetColor("_BaseColor", Color.Lerp(red, yellow, percent / 50));
            }
            else if (percent <= 0)
            {
                material.SetColor("_BaseColor", red);
                health = 0;
                // Rotate enemy on death for effect
                transform.RotateAround(transform.position + Vector3.down + Vector3.forward * 0.5f, transform.right, 90f);
                StopAllCoroutines();
            }
        }
        /// <summary>
        /// Coroutine for random wandering within radius
        /// </summary>
        private IEnumerator SimpleWander() //Note: Ideally in EnemiesManager not in every enemy
        {
            Vector3 dest;
            while (health > 0)
            {
                dest = RandomDestination();
                do
                {
                    yield return new WaitForEndOfFrame();
                    transform.position = Vector3.MoveTowards(transform.position, dest, moveSpeed * Time.deltaTime);

                } while (Vector3.Distance(transform.position, dest) > 0.01f);
            }
        }
        /// <summary>
        /// Returns random point within moveRadius of startPos
        /// </summary>
        private Vector3 RandomDestination()
        {
            Vector2 circle2D = Random.insideUnitCircle * moveRadius;
            return startPos + new Vector3(circle2D.x, 0, circle2D.y);
        }
    }
}
