using UnityEngine;

[RequireComponent(typeof(Character))]
public class CharacterCollisionHandler : MonoBehaviour
{
    private Character _character;

    private void Awake()
    {
        _character = GetComponent<Character>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _character.Die();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _character.Die();
    }
}
