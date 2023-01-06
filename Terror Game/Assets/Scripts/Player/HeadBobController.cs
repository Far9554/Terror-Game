using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobController : MonoBehaviour
{

    [SerializeField, Range(0, 0.1f)] private float _amplitude = 0.015f;
    [SerializeField, Range(0, 30)] private float _frequency = 10.0f;
    float frequency;

    [SerializeField] private Transform _camera = null;
    [SerializeField] private Transform _cameraHolder = null;

    public float speed;
    private float _toggleSpeed = 1.0f;
    private Vector3 _startPos;
    [SerializeField] private CharacterController _controller;
    [SerializeField] private PlayerMovement _playerController;

    // Start is called before the first frame update
    void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _playerController = GetComponent<PlayerMovement>();
        _startPos = _camera.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        CheckMotion();
        _camera.LookAt(FocusTarget());
    }

    private void CheckMotion()
    {
        speed = _playerController.moveSpeed;

        ResetPosition();

        if (speed < _toggleSpeed) return;
        if (!_playerController.isGrounded) return;

        PlayMotion(FootStepMotion());
    }

    private Vector3 FootStepMotion()
    {
        frequency = _frequency + (speed / 2);

        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * frequency) * _amplitude;
        pos.x += Mathf.Cos(Time.time * frequency / 2) * _amplitude * 2;
        return pos;
    }

    private void ResetPosition()
    {
        if (_camera.localPosition == _startPos) return;
        _camera.localPosition = Vector3.Lerp(_camera.localPosition, _startPos, 1 * Time.deltaTime);
    }

    void PlayMotion(Vector3 motion)
    {
        _camera.localPosition += motion;
    }

    private Vector3 FocusTarget()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + _cameraHolder.localPosition.y, transform.position.z);
        pos += _cameraHolder.forward * 15;
        return pos;
    }
}
