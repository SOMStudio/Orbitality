using System.Collections.Generic;
using Orbitality.Main;
using UnityEngine;

namespace Orbitality.Cursor
{
    public class CursorManager : ExtendedCustomMonoBehaviour, ICursor
    {
        [SerializeField] private GameObject cursorHelpPointPrefab;
        [SerializeField] private bool visualCursorHelp = true;
        private Transform spawnPoint;

        private int minCountPoint = 10;
        private float speedMove = 7.0f;
        private float stepMove = 0.05f;
        private int multiplier = 1;

        private readonly List<Vector3> pointList = new List<Vector3>();
        private readonly Transform[] cursorHelpList = new Transform[10];
        private Vector3 positionMove;
        private Vector3 vectorMove;

        private IGravityController gravityController;

        public Transform SpawnPoint
        {
            set => spawnPoint = value;
        }
        
        public float SpeedMove
        {
            set
            {
                if (value > 0)
                {
                    speedMove = value;
                }
            }
        }

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
                }
                else if (stepMove < 0.01)
                {
                    if (multiplier > 1)
                    {
                        multiplier--;

                        Length = value;
                    }
                }
            }
        }
        
        public int CountPoint()
        {
            return pointList.Count;
        }

        public Vector3 GetPointPosition(int val)
        {
            return pointList[val];
        }

        public Vector3 LastPointPosition => GetPointPosition(CountPoint() - 1);

        public override void Init()
        {
            base.Init();

            InitHelpList();

            gravityController = GameController.Instance;
            vectorMove = (spawnPoint.position - myTransform.position).normalized;
        }

        private void InitHelpList()
        {
            if (visualCursorHelp)
            {
                for (int i = 0; i < minCountPoint; i++)
                {
                    cursorHelpList[i] = Instantiate(cursorHelpPointPrefab, spawnPoint.position, Quaternion.identity)
                        .transform;
                }
            }
        }

        private void UpdatePoint(int number, Vector3 position)
        {
            if (number < pointList.Count)
            {
                pointList[number] = position;
            }
            else
            {
                pointList.Add(position);
            }
        }

        private void UpdateHelpPoint(int number, Vector3 position)
        {
            cursorHelpList[number].position = position;
        }

        public void UpdateCursor()
        {
            positionMove = spawnPoint.position;
            vectorMove = (spawnPoint.position - myTransform.position).normalized;
            int countStep = minCountPoint * multiplier;

            for (int i = 0; i < countStep; i++)
            {
                UpdateStep(i);

                UpdatePoint(i, positionMove);

                if (visualCursorHelp)
                {
                    if ((i + 1) % multiplier == 0)
                    {
                        UpdateHelpPoint(i / multiplier, positionMove);
                    }
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
