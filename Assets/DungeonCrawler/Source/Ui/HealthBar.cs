using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Util;

namespace DungeonCrawler
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private HealthBarSegment segmentPrefab;
        [SerializeField] private int maxHealthPoints;
        [SerializeField] private float delayBeforeAutoHide;
        [Range(0.0001f, 1f)]
        [SerializeField] private float fadeOutSpeed;

        [Header("Events")]
        [SerializeField] private UnityEvent onHealthLost;
        [SerializeField] private UnityEvent onHealthGained;
        
        
        
        [Header("Layout")] 
        [SerializeField] private Vector2 centerOffset;
        [SerializeField] private float segmentWidth;
        [SerializeField] private float segmentPadding;
        
        
        private int _currentHealthPoints;
        private ConsistentCoroutine _showAndThenFadeOutCoroutine;
        private readonly List<HealthBarSegment> _segments = new List<HealthBarSegment>();

        public int HealthPoints {
            get => _currentHealthPoints;
            set {
                var delta = value - _currentHealthPoints;
                _currentHealthPoints = Mathf.Min(Mathf.Max(0, value), maxHealthPoints);
                OnHealthPointsValueChanged();
                if (delta < 0) onHealthLost?.Invoke();
                else if (delta > 0) onHealthGained?.Invoke();
            }
        }

        public void SetFullHealth () {
            HealthPoints = maxHealthPoints;
        }
        
        private void Awake () {
            GenerateLayout();
            SetFullHealth();
            _showAndThenFadeOutCoroutine = new ConsistentCoroutine(this);
            SetAllSegmentsSpriteAlpha(0.0f);
        }

        private void GenerateLayout () {
            _segments.ForEach(segment => Destroy(segment.gameObject));
            _segments.Clear();
            
            var totalHealthBarWidth = maxHealthPoints * segmentWidth 
                                      + (maxHealthPoints - 1) * segmentPadding;
            var currentLeftPosition = centerOffset + Vector2.left * totalHealthBarWidth / 2f;
            for (var i = 0; i < maxHealthPoints; i++) {
                var segment = Instantiate(segmentPrefab, transform);
                segment.transform.localPosition = currentLeftPosition + Vector2.right * segmentWidth / 2f;
                segment.GetComponent<SpriteRenderer>().size = new Vector2(segmentWidth, 0.25f);
                currentLeftPosition += Vector2.right * (segmentWidth + segmentPadding);
                _segments.Add(segment);
                
                if (i < HealthPoints)
                    segment.SetFull();
                else
                    segment.SetEmpty();
            }
        }

        private void OnHealthPointsValueChanged () {
            for (var i = 0; i < _segments.Count; i++) {
                if (i < HealthPoints)
                    _segments[i].SetFull();
                else
                    _segments[i].SetEmpty();
            }

            _showAndThenFadeOutCoroutine?.StartAndInterruptIfRunning(ShowAndThenFadeOutCoroutine());
        }

        private IEnumerator ShowAndThenFadeOutCoroutine () {
            SetAllSegmentsSpriteAlpha(1.0f);
            yield return new WaitForSeconds(delayBeforeAutoHide);
            var alpha = 1f;
            while (alpha > Mathf.Epsilon) {
                alpha = Mathf.Lerp(alpha, 0f, fadeOutSpeed);
                SetAllSegmentsSpriteAlpha(alpha);
                yield return new WaitForEndOfFrame();
            }
        }

        private void SetAllSegmentsSpriteAlpha (float alpha) => _segments.ForEach(segment => segment.SetSpriteAlpha(alpha));
        
#if UNITY_EDITOR

        private void OnDrawGizmosSelected () {
            var centerOffsetPosition = transform.position;
            centerOffsetPosition.x += centerOffset.x;
            centerOffsetPosition.y += centerOffset.y;
            centerOffsetPosition.z -= 0.1f;
            Gizmos.DrawSphere(centerOffsetPosition, 0.03f);

            var totalHealthBarWidth = maxHealthPoints * segmentWidth 
                                      + (maxHealthPoints - 1) * segmentPadding;
            var segmentStartPoint = centerOffsetPosition + Vector3.left * totalHealthBarWidth / 2f;
            var padding = 0f;
            for (var i = 0; i < maxHealthPoints; i++) {
                var lineStart = segmentStartPoint + Vector3.right * (segmentWidth * i + padding);
                var lineEnd = lineStart + Vector3.right * segmentWidth;
                Gizmos.DrawLine(lineStart, lineEnd);
                padding += segmentPadding;
            }
        }

#endif
        
    }
}
