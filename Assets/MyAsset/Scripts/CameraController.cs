using UnityEngine;

namespace RollABollGame
{
    public sealed class CameraController : IExecute
    {
		private Transform _player;
		private Transform _mainCamera;
		private Vector3 _offset;
		private float _y;

		private float sensitivity = 5f;

		private float _zoom = 1.0f;
		private float _zoomMax = 40;
		private float _zoomMin = 20;

		private bool _pause = false;

		public CameraController(Transform player, Transform mainCamera)
		{
			_player = player;
			_mainCamera = mainCamera;
			_mainCamera.LookAt(_player);
			_offset = new Vector3(0, 30, 0);
			//_offset = _mainCamera.position - _player.position;
		}
		public void GamePause(bool value)
		{
			_pause = value;
		}
		public void SensChanged(float value)
		{
			sensitivity = value;
		}
		public float GetSens()
		{
			return sensitivity;
		}
		public void Execute()
		{
			if (!_pause)
			{
				if (Input.GetAxis("Mouse ScrollWheel") > 0)
				{
					if (_offset.y < _zoomMax)
					{
						_offset.y += _zoom;
					}
				}
				else if (Input.GetAxis("Mouse ScrollWheel") < 0)
				{
					if (_offset.y > _zoomMin)
					{
						_offset.y -= _zoom;
					}
				}

				_y = _mainCamera.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
				_mainCamera.transform.localEulerAngles = new Vector3(90f, _y, 0);
				_mainCamera.transform.position = _player.position + _offset;
			}
		}
	}
}