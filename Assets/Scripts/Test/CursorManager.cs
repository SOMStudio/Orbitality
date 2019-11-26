using System;
using System.Collections;
using System.Collections.Generic;
using Orbitality.Main;
using UnityEngine;

namespace Orbitality.Test
{
    public class CursorManager : ExtendedCustomMonoBehaviour
    {
        [SerializeField] private GameObject cursorPointPrefab;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private int minCountPoint = 10;
        [SerializeField] private float speedMove = 7.0f;
        [SerializeField] private float stepMove = 0.05f;
        [SerializeField] private int multiplier = 1;

        private readonly Transform[] cursorHelpList = new Transform[10];
        private Vector3 positionMove;
        private Vector3 vectorMove;

        private IGravityController gravityController;

        public float SpeedMove {
            set
            {
                if (value > 0)
                {
                    speedMove = value;
                }
            }
        }

        public Vector3 LastPointPosition => positionMove;

        public float Length
        {
            get { return stepMove * minCountPoint * multiplier * speedMove; }
            set
            {
                stepMove = value / (minCountPoint * multiplier * speedMove);

                if (stepMove > 0.1)
                {
                    multiplier++;

                    Length = value;
                } else if (stepMove < 0.01)
                {
                    if (multiplier > 1)
                    {
                        multiplier--;

                        Length = value;
                    }
                }
            }
        }

        public override void Init()
        {
            base.Init();
            
            InitHelpList();
            
            gravityController = GameController.Instance;
            vectorMove = (spawnPoint.position - myTransform.position).normalized;
        }
        
        private void InitHelpList()
        {
            for (int i = 0; i < minCountPoint; i++)
            {
                cursorHelpList[i] = Instantiate(cursorPointPrefab, spawnPoint.position, Quaternion.identity).transform;
            }
        }
        
        public void UpdateCursor()
        {
            positionMove = spawnPoint.position;
            vectorMove = (spawnPoint.position - myTransform.position).normalized;
            int countStep = minCountPoint * multiplier;
            
            for (int i = 0; i < countStep; i++)
            {
                UpdateStep(i);
                
                if (i % multiplier == 0)
                {
                    cursorHelpList[i/multiplier].position = positionMove;
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
