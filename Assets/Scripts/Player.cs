using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Player : SingletonBase<Player>
    {

        [SerializeField] private int m_NumLives;
        [SerializeField] private SpaceShip m_Ship;
        [SerializeField] private GameObject m_PlayerShipPrefab;
        public SpaceShip ActiveShip => m_Ship;


        [SerializeField] private CameraController m_CameraController;
        [SerializeField] private MovementController m_MovementController;

        bool freezing = false;
        [SerializeField] private float m_Timer;
        [SerializeField] private float m_LifeTime;




        private void Start()
        {

            m_Ship.EventOnDeath.AddListener(OnShipDeath);
        }



        


        private void OnShipDeath()
        {





            m_NumLives--;

            if (m_NumLives > 0)
            {
                freezing = true;
            }

        }

        private void Update()
        {
            if (freezing == true)
            {
                m_Timer += Time.deltaTime;
                if (m_Timer>m_LifeTime)
                {
                    freezing= false;
                    m_Timer = 0;
                    Respawn();
                }
            }
        }

        private void Respawn()
        {
            var newPlayerShip = Instantiate(m_PlayerShipPrefab);
            m_Ship = newPlayerShip.GetComponent<SpaceShip>();
            m_Ship.EventOnDeath.AddListener(OnShipDeath);



            m_CameraController.SetTarget(m_Ship.transform);
            m_MovementController.SetTargetShip(m_Ship);

        }
    }
}

