using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace WorldGeneration
{
    public class GameManager : MonoBehaviour
    {
        public GameObject playerPrefab;

        public int cellToCreate;
        private int cellCreated;

        private static GameManager _instance;

        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    new GameManager();
                }
                return _instance;
            }
        }

        public GameManager()
        {
            _instance = this;
        }


        private void OnEnable()
        {
            EventSystem.OnCellGenerationComplete += OnCellGenerationComplete;
        }

        private void OnDisable()
        {
            EventSystem.OnCellGenerationComplete -= OnCellGenerationComplete;
        }

        private void OnCellGenerationComplete()
        {
            cellCreated++;

            if (cellCreated == cellToCreate)
            {
                SpawnPlayer();
            }
        }

        private void SpawnPlayer()
        {
            GameObject _player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);

            EventSystem.CallPlayerSpawned(_player.transform);
        }
    }

}