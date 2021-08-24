using Cinemachine;
using System.Collections;
using UnityEngine;

namespace Between
{
    public class CameraViewChanger : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _camera;

        private CinemachineTransposer _transposer;
        private Vector3 _defaultDepth;

        private bool _isChanging = false;
        private void Start()
        {
            _transposer = _camera.GetCinemachineComponent<CinemachineTransposer>();
            _defaultDepth = _transposer.m_FollowOffset;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<CameraViewTrigger>(out var trigger))
            {
                if (_isChanging)
                    StopAllCoroutines();

                StartCoroutine(ChangeDepth(trigger.ChangeTo, trigger.ChangeTime));
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<CameraViewTrigger>(out var trigger))
            {
                if (_isChanging)
                    StopAllCoroutines();

                StartCoroutine(ChangeDepth(_defaultDepth, trigger.RestoreTime));
            }
        }

        private IEnumerator ChangeDepth(Vector3 to, float changeTime)
        {
            _isChanging = true;
            float time = 0f;
            Vector3 from = _transposer.m_FollowOffset;
            
            while (time <= changeTime)
            {
                _transposer.m_FollowOffset = Vector3.Lerp(from, to, time / changeTime);
                time += Time.deltaTime;

                yield return null;
            }

            _transposer.m_FollowOffset = to;
            _isChanging = false;

            yield break;
        }
    }
}