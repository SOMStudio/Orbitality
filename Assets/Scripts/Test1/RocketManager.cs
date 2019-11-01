using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Orbitality.Test1
{
    public class RocketManager : MonoBehaviour
    {
        [SerializeField]
        private float speed = 10.0f;

        private Vector3 vectorMove;

        private GameController gameController;
    
        void Start()
        {
            gameController = FindObjectOfType<GameController>();
            vectorMove = transform.forward;
            
            Destroy(this.gameObject, 60.0f);
        }
    
        void Update()
        {
            Vector3 speedVector = vectorMove * speed;
            Vector3 dependVector = gameController.GetDependencyVector(transform);

            if (dependVector != Vector3.zero)
            {
                Vector3 speedVectorNew = speedVector + dependVector;
                speedVector = Vector3.Lerp(speedVector, speedVectorNew, Time.deltaTime);
                
                vectorMove = speedVector.normalized;
                speedVector = vectorMove * speed;
            }
            
            transform.position += speedVector * Time.deltaTime;
        }
        
    }
}

