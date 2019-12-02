using System;
using System.Collections;
using Orbitality.Cursor;
using Orbitality.Main;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Orbitality.Enemy.AI
{
    public class AiManager : ExtendedCustomMonoBehaviour, IAiInput
    {
        [SerializeField] private float rotateSpeed = 5f;
        [SerializeField] private float stepChangeRotateSpeed = 0.51f;
        [SerializeField] private float maxRotateSpeed = 5.0f;
        [SerializeField] private float minRotateSpeed = 2.0f;

        [SerializeField] private float changeLengthCursorSpeed = 0.2f;
        [SerializeField] private float stepChangeLengthCursorSpeed = 0.021f;
        [SerializeField] private float maxLengthCursorSpeed = 0.2f;
        [SerializeField] private float minLengthCursorSpeed = 0.1f;

        [SerializeField] private float distanceForShoot = 1;

        [SerializeField] private Transform[] obstacleList;
        
        [SerializeField] private Transform target;
        private ICursor cursorManager;

        [SerializeField] private AiState aiActiveState;
        [SerializeField] private int rotateDirection = 0;
        [SerializeField] private int increaseDirection = 0;

        private float prevDistanceToTarget;
        private float minDistance;

        public int RotateAxis
        {
            get { return rotateDirection; }
        }

        public Vector3 RotateVector
        {
            get { return rotateDirection * rotateSpeed * new Vector3(0, 1, 0); }
        }

        public int IncreaseAxis
        {
            get { return increaseDirection; }
        }

        public float IncreaseLength
        {
            get { return cursorManager.Length * changeLengthCursorSpeed * increaseDirection; }
        }

        public event Action Shot;

        private void Awake()
        {
            aiActiveState = AiState.searching_target;

            prevDistanceToTarget = 100;
        }

        public override void Init()
        {
            base.Init();
            
            StartCoroutine(AiIntelligence());
            
            minDistance = DistanceFromObjectToTarget();
        }

        public void SetCursor(ICursor val)
        {
            this.cursorManager = val;
        }

        private IEnumerator AiIntelligence()
        {
            do
            {
                switch (aiActiveState)
                {
                    case AiState.pouse_no_target:

                        break;
                    case AiState.searching_target:
                        if (target == null)
                        {
                            aiActiveState = AiState.pouse_no_target;
                        }
                        else
                        {
                            if (Random.Range(0, 100) < 50)
                            {
                                aiActiveState = Random.Range(0, 100) > 50 ? AiState.turning_right : AiState.turning_left;
                            }
                            else
                            {
                                aiActiveState = Random.Range(0, 100) > 50 ? AiState.cursor_increase : AiState.cursore_decrease;
                            }
                        }
                        break;
                    case AiState.turning_left:
                        if (rotateDirection != -1)
                        {
                            rotateDirection = -1;
                        }
                        break;
                    case AiState.turning_right:
                        if (rotateDirection != 1)
                        {
                            rotateDirection = 1;
                        }
                        break;
                    case AiState.cursor_increase:
                        if (increaseDirection != 1)
                        {
                            increaseDirection = 1;
                        }
                        break;
                    case AiState.cursore_decrease:
                        if (increaseDirection != -1)
                        {
                            increaseDirection = -1;
                        }
                        break;
                }
                
                yield return new WaitForSeconds(0.1f);
                
                if (rotateDirection != 0 || increaseDirection != 0)
                {
                    float newDistanceToTarget = DistanceFromCursorToTarget();
                    if (prevDistanceToTarget < newDistanceToTarget)
                    {
                        if (rotateDirection != 0)
                        {
                            if (ReduceRotateSpeed())
                            {
                                ReversRotateDirection();
                            }
                            else
                            {
                                rotateDirection = 0;
                                rotateSpeed = maxRotateSpeed;
                                aiActiveState = AiState.searching_target;
                            }
                        }

                        if (increaseDirection != 0)
                        {
                            if (cursorManager.Length > minDistance / 2)
                            {
                                if (ReduceLengthCursorSpeed())
                                {
                                    ReversLengthCursorDirection();
                                }
                                else
                                {
                                    increaseDirection = 0;
                                    changeLengthCursorSpeed = maxLengthCursorSpeed;
                                    aiActiveState = AiState.searching_target;
                                }
                            }
                            else
                            {
                                aiActiveState = AiState.cursor_increase;
                            }
                        }
                    }

                    bool obstacleInCursor = CheckForObstacle();
                    
                    if (newDistanceToTarget <= distanceForShoot)
                    {
                        if (!obstacleInCursor)
                        {
                            Shot?.Invoke();
                        }
                    }
                    else
                    {
                        if (Random.Range(0, 100) > 90)
                        {
                            if (!obstacleInCursor)
                            {
                                Shot?.Invoke();
                            }
                        }

                        if (newDistanceToTarget > minDistance)
                        {
                            rotateDirection = 0;
                            aiActiveState = AiState.searching_target;
                        }
                    }

                    prevDistanceToTarget = newDistanceToTarget;
                }

                yield return null;
            } while (true);
        }

        private bool CheckForObstacle()
        {
            int cursorPoints = cursorManager.CountPoint();
            
            foreach (var variable in obstacleList)
            {
                for (int i = 1; i < cursorPoints - 1; i++)
                {
                    if (Vector3.Distance(variable.position, cursorManager.GetPointPosition(i)) <= distanceForShoot)
                    {
                        return true;
                    }             
                }
            }

            return false;
        }

        private bool IncreaseRotateSpeed()
        {
            if (rotateSpeed < maxRotateSpeed)
            {
                rotateSpeed += stepChangeRotateSpeed;
                return true;
            }

            return false;
        }
        
        private bool ReduceRotateSpeed()
        {
            if (rotateSpeed > minRotateSpeed)
            {
                rotateSpeed -= stepChangeRotateSpeed;
                return true;
            }

            return false;
        }
        
        private void ReversRotateDirection()
        {
            if (aiActiveState == AiState.turning_left)
            {
                aiActiveState = AiState.turning_right;
            }
            else
            {
                aiActiveState = AiState.turning_left;
            }
        }
        
        private bool IncreaseLengthCursorSpeed()
        {
            if (changeLengthCursorSpeed < maxLengthCursorSpeed)
            {
                changeLengthCursorSpeed += stepChangeLengthCursorSpeed;
                return true;
            }

            return false;
        }
        
        private bool ReduceLengthCursorSpeed()
        {
            if (changeLengthCursorSpeed > minLengthCursorSpeed)
            {
                changeLengthCursorSpeed -= stepChangeLengthCursorSpeed;
                return true;
            }

            return false;
        }
        
        private void ReversLengthCursorDirection()
        {
            if (aiActiveState == AiState.cursore_decrease)
            {
                aiActiveState = AiState.cursor_increase;
            }
            else
            {
                aiActiveState = AiState.cursore_decrease;
            }
        }

        private float DistanceFromObjectToTarget()
        {
            return (myTransform.position - target.position).magnitude;
        }
        
        private float DistanceFromCursorToTarget()
        {
            return (cursorManager.LastPointPosition - target.position).magnitude;
        }
    }

    public enum AiState
    {
        pouse_no_target,
        searching_target,
        turning_left,
        turning_right,
        cursor_increase,
        cursore_decrease
    }
}