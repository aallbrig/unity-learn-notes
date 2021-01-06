using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CongratScript : MonoBehaviour
{
    public TextMesh text;
    public ParticleSystem sparksParticles;
    private List<string> _textToDisplay = new List<string>();
    private float _rotatingSpeed;
    private float _timeToNextText;
    private int _currentTextIndex;

    private void Start()
    {
        _timeToNextText = 0.0f;
        _currentTextIndex = 0;

        _rotatingSpeed = 1.0f;

        _textToDisplay.Add("Congratulations");
        _textToDisplay.Add("All Errors Fixed");
        _textToDisplay.Add("Hooray!");

        text.text = _textToDisplay[_currentTextIndex];
        
        sparksParticles.Play();
    }

    private void Update()
    {
        _timeToNextText += Time.deltaTime;
        transform.RotateAround(Vector3.up, _rotatingSpeed * Time.deltaTime);
        if (!(_timeToNextText > 1.5f)) return;

        _timeToNextText = 0.0f;
        _currentTextIndex++;
        if (_currentTextIndex >= _textToDisplay.Count)
            _currentTextIndex = 0;

        text.text = _textToDisplay[_currentTextIndex];
    }
}