using UnityEngine;
//using System.Collections;

namespace RollABollGame
{
	public sealed class MiniMap : MonoBehaviour
	{
		private Transform _player;
		private void Start()
		{
			_player = Camera.main.transform;
			transform.parent = null;
			transform.rotation = Quaternion.Euler(90.0f, 0, 0);
			transform.position = _player.position + new Vector3(0, 5.0f, 0);

			var rt = Resources.Load<RenderTexture>("MiniMap/MiniMapTexture");

			GetComponent<Camera>().targetTexture = rt;
		}

		private void LateUpdate()
		{
			var newPosition = _player.position;
			newPosition.y = transform.position.y;
			transform.position = newPosition;
			transform.rotation = Quaternion.Euler(90, _player.eulerAngles.y, 0);
		}
	}

}


	//	public Transform target;
	//	public float zoom = 10;

	//	private Vector2 XRot = Vector2.right;
	//	private Vector2 YRot = Vector2.up;

	//	public Vector2 TransformPosition(Vector3 position)
	//	{
	//		Vector3 offset = position - target.position;
	//		Vector2 pos = offset.x * XRot;
	//		pos += offset.z * YRot;
	//		pos *= zoom;
	//		return pos;
	//	}

	//	public Vector3 TransformRotation(Vector3 rotation)
	//	{
	//		return new Vector3(0, 0, target.eulerAngles.y - rotation.y);
	//	}

	//	public Vector2 MoveInside(Vector2 point)
	//	{
	//		Rect rect = GetComponent<RectTransform>().rect;
	//		point = Vector2.Max(point, rect.min);
	//		point = Vector2.Min(point, rect.max);
	//		return point;
	//	}

	//	void LateUpdate()
	//	{
	//		XRot = new Vector2(target.right.x, -target.right.z);
	//		YRot = new Vector2(-target.forward.x, target.forward.z);
	//	}
	//}
//}



//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//namespace RollABollGame
//{
//    public sealed class MiniMap : MonoBehaviour, IExecute
//    {
//        private Transform _playerPos; // Позиция главного героя
//        private readonly float _mapScale = 2;
//        public static List<IRadar> RadObjects = new List<IRadar>();

//        private void Start()
//        {
//            _playerPos = Camera.main.transform;
//        }

//        public void RegisterRadarObject(IRadar radarObject)
//        {
//            Debug.Log("RegisterRadarObject");
//            Image image = Instantiate(radarObject.GetIcon());
//            RadObjects.Add(radarObject);
//        }

//        public void RemoveRadarObject(IRadar radarObject)
//        {
//            Debug.Log("RemoveRadarObject");
//            List<IRadar> newList = new List<IRadar>();
//            foreach (IRadar obj in RadObjects)
//            {
//                if (obj.GetOwner() == radarObject.GetOwner())
//                {
//                    Destroy(obj.GetIcon());
//                    continue;
//                }
//                newList.Add(obj);
//            }
//            RadObjects.RemoveRange(0, RadObjects.Count);
//            RadObjects.AddRange(newList);
//        }

//        private void DrawRadarDots() // Синхронизирует значки на миникарте с реальными объектами
//        {
//            foreach (IRadar radObject in RadObjects)
//            {
//                Vector3 radarPos = (radObject.GetOwner().transform.position - _playerPos.position);
//                float distToObject = Vector3.Distance(_playerPos.position, radObject.GetOwner().transform.position) * _mapScale;
//                float deltay = Mathf.Atan2(radarPos.x, radarPos.z) * Mathf.Rad2Deg - 270 - _playerPos.eulerAngles.y;
//                radarPos.x = distToObject * Mathf.Cos(deltay * Mathf.Deg2Rad) * -1;
//                radarPos.z = distToObject * Mathf.Sin(deltay * Mathf.Deg2Rad);
//                //radObject.GetIcon().transform.SetParent(transform);
//                radObject.GetIcon().transform.position = new Vector3(radarPos.x, radarPos.z, 0) + transform.position;
//            }
//        }

//        public void Execute()
//        {
//            if (Time.frameCount % 2 == 0)
//            {
//                DrawRadarDots();
//            }
//        }
//    }
//}
