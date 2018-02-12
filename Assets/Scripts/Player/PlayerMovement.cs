/*
	PlayerMovement.cs
	Created 11/8/2017 3:45:09 PM
	Project DriveBy RPG by Basegames
*/
using UnityEngine;
using Utility;

namespace Player
{
	public class PlayerMovement : MonoBehaviour 
	{
        public static Vector3 PlayerPosition;
        private float _x, _y;
        [SerializeField]private float _speed = 100f;

        private Rigidbody2D _rb2D;

        private void Start()
        {
            _rb2D = GetComponent<Rigidbody2D>();
            if(PlayerLocation.PlayerPosition != Vector3.zero)
            {
                this.transform.position = PlayerLocation.PlayerPosition;
            }
        }

        private void FixedUpdate()
        {
            _x = Input.GetAxis("Horizontal");
            _y = Input.GetAxis("Vertical");
            if (GameStateManager.GameState == GamesStates.Playing)
            {
                _rb2D.velocity = new Vector2(_x, _y) * _speed * Time.deltaTime;
                if (_x != 0 || _y != 0)
                {
                    PlayerPosition = this.transform.position;
                    RandomBattleEncounter.OnEncounter();
                }
            }
            else
            {
                _rb2D.velocity = Vector2.zero;
            }
            
        }
    }
}