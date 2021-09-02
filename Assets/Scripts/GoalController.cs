using UnityEngine;

public class GoalController : MonoBehaviour
{
    private bool _playerInside;
    private float _timeInside;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
            _playerInside = true;
    }

    private void FixedUpdate()
    {
        if (_playerInside)
            _timeInside += Time.fixedDeltaTime;
        if (_timeInside >= 3)
            Quit();
    }

    private static void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInside = false;
            _timeInside = 0;
        }
    }
}
