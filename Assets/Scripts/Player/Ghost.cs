using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] private float _ghostDelay;
    private float _ghostDelaySeconds;
    [SerializeField] private GameObject _ghost;
    [SerializeField] private bool _isMakeGhost;
    public bool MakeGhost
    {
        get => _isMakeGhost;
        set
        {
            _isMakeGhost = value;

        }
    }

    private void Start()
    {
        _ghostDelaySeconds = _ghostDelay;
    }

    void Update()
    {
        if (_isMakeGhost)
        {
            if (_ghostDelaySeconds > 0)
            {
                _ghostDelaySeconds -= Time.deltaTime;
            }
            else
            {
                GameObject currentGhost = Instantiate(_ghost, transform.position, transform.rotation);
                Sprite currentSprite = GetComponent<SpriteRenderer>().sprite;
                currentGhost.transform.localScale = transform.localScale;
                currentGhost.GetComponent<SpriteRenderer>().sprite = currentSprite;

                _ghostDelaySeconds = _ghostDelay;
                Destroy(currentGhost, 1f);
            }
        }
    }

}
