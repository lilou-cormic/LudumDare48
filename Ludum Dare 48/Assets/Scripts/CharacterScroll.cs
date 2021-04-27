using PurpleCable;
using UnityEngine;

public class CharacterScroll : MonoBehaviour
{
    [SerializeField] MainMenu MainMenu = null;

    private Camera Camera;

    private bool _isDone = false;

    private void Awake()
    {
        Camera = Camera.main;
    }

    private void Update()
    {
        if (_isDone)
            return;

        Camera.transform.position += (Vector3.down * 0.3f) * Time.deltaTime;

        if ((Input.anyKeyDown || Camera.transform.position.y < -19.5))
        {
            _isDone = true;
            MainMenu.GoToMenu();
        }
    }
}
