using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crocodile : MonoBehaviour
{
                public float moveSpeed = 5f;
            public float minX, maxX, minZ, maxZ;
            public float changeDirectionTime = 2f;
            private float currentTime;
            private Vector3 direction;
             Animator animator;

            void Start()
            {
        animator = GetComponent<Animator>();
        direction = GetRandomDirection();
                currentTime = changeDirectionTime;
               
            }

            void Update()
            {
                transform.localPosition += direction * moveSpeed * Time.deltaTime;

                // check if the object is still within the boundaries
                if (transform.localPosition.x < minX || transform.localPosition.x > maxX || transform.localPosition.z < minZ || transform.localPosition.z > maxZ)
                {
                    // if not, change direction
                    direction = GetRandomDirection();
        }
        else
        {
              animator.SetFloat("x", randomX);
        animator.SetFloat("z", randomZ);
        }
      
        currentTime -= Time.deltaTime;
                if (currentTime <= 0)
                {
                    direction = GetRandomDirection();
                    currentTime = changeDirectionTime;
                }
            }
    float randomX;
    float randomZ;
            Vector3 GetRandomDirection()
            {
                 randomX = Random.Range(-1f, 1f);
                 randomZ = Random.Range(-1f, 1f);
      
                return new Vector3(randomX, 0, randomZ).normalized;
            }
}