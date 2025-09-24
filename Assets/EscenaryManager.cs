using UnityEngine;
public class EscenaryManager : MonoBehaviour
{
    private GetStaticObjectsHandler _getStaticObjectsHandler;
    private void OnEnable()
    {
        _getStaticObjectsHandler = new GetStaticObjectsHandler();
    }
    private void OnDisable()
    {
        _getStaticObjectsHandler = null;
    }
}
