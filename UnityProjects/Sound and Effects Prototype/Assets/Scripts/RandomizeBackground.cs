using System.Collections.Generic;
using UnityEngine;

public class RandomizeBackground : MonoBehaviour
{
    public List<Sprite> listOfPossibleSprites;
    private SpriteRenderer _spriteRender;

    private void Start()
    {
        listOfPossibleSprites = listOfPossibleSprites ?? new List<Sprite>();
        _spriteRender = GetComponent<SpriteRenderer>();
        _spriteRender.sprite = listOfPossibleSprites[Random.Range(0, listOfPossibleSprites.Count - 1)];
    }
}
