using System.Collections;
using System.Collections.Generic;
using Orbitality.Main;
using UnityEngine;

namespace Orbitality.Test
{
    public class CursorManager : ExtendedCustomMonoBehaviour
    {
        [SerializeField] private Transform[] cursorHelpList;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private float speedMove = 7.0f;
        [SerializeField] private float stepMove = 0.1f;
        [SerializeField] private int multiplicator = 2;

        private Vector3 positionMove;
        private Vector3 vectorMove;
        
        
        private IGravityController gravityController;

        public override void Init()
        {
            base.Init();
        
            gravityController = GameController.Instance;
            vectorMove = (spawnPoint.position - myTransform.position).normalized;
        }

        public void UpdateCursor()
        {
            positionMove = spawnPoint.position;
            vectorMove = (spawnPoint.position - myTransform.position).normalized;
            int countStep = cursorHelpList.Length * multiplicator;

            for (int i = 0; i < countStep; i++)
            {
                UpdateStep(i);

                if (i % multiplicator == 0)
                {
                    cursorHelpList[i/multiplicator].position = positionMove;
                }
            }
        }

        private void UpdateStep(int step)
        {
            Vector3 speedVector = vectorMove * speedMove;
            Vector3 dependVector = gravityController.GetDependencyVector(positionMove);

            if (dependVector != Vector3.zero)
            {
                Vector3 speedVectorNew = speedVector + dependVector;
                speedVector = Vector3.Lerp(speedVector, speedVectorNew, stepMove);
                
                vectorMove = speedVector.normalized;
                speedVector = vectorMove * speedMove;
            }
            
            positionMove += speedVector * stepMove;
        }    
    }
}
