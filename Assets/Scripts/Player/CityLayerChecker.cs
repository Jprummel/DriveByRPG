/*
	CityLayerChecker.cs
	Created 11/9/2017 1:27:09 PM
	Project DriveBy RPG by DefaultCompany
*/
using Environment;
using Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Player
{
	public class CityLayerChecker : MonoBehaviour 
	{
        [SerializeField] private LayerMask _fadeLayer;
        [SerializeField] private SpriteRenderer _playerSR;

        [SerializeField] private Vector2 _checkPoint;

        private GameObject _lastFadeGameObject = null;

        private float _offSet = 0.05f;

        private void Start()
        {
            _playerSR = transform.parent.GetComponent<SpriteRenderer>();
            _offSet = (_playerSR.size.y/4) - 0.05f;
        }

        private void FixedUpdate()
        {
            _checkPoint = new Vector2(transform.parent.position.x, transform.parent.position.y - _offSet);

            Collider2D coll = Physics2D.OverlapPoint(_checkPoint, _fadeLayer);

            if (coll)
            {
                _lastFadeGameObject = coll.gameObject;
                ExecuteEvents.Execute<IFadeable>(coll.gameObject, null, (x, y) => x.FadeAlpha());
                _playerSR.sortingOrder = -1;
            }
            else if (_lastFadeGameObject != null)
            {
                ExecuteEvents.Execute<IFadeable>(_lastFadeGameObject.gameObject, null, (x, y) => x.ResetAlpha());
                _lastFadeGameObject = null;

                _playerSR.sortingOrder = 1;
            }   
        }
    }
}