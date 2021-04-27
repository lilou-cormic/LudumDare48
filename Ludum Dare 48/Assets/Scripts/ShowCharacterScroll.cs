using PurpleCable;
using UnityEngine;

public class ShowCharacterScroll : MonoBehaviour
{
    [SerializeField] MainMenu MainMenu = null;

    private float _idleTime = 0;

    private bool _isLoading = false;

    private void Update()
    {
        if (_isLoading)
            return;

        if (Input.anyKeyDown)
        {
            _idleTime = 0;
        }
        else
        {
            _idleTime += Time.deltaTime;
        }

        if (_idleTime > 10)
        {
            _isLoading = true;
            MainMenu.LoadScene("Characters");
        }
    }
}
