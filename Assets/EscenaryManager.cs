using UnityEngine;
public class EscenaryManager : MonoBehaviour
{
    private WallAnimation _wallAnimation;
    private void OnEnable()
    {
        _wallAnimation = new WallAnimation(this);
    }
    void Start()
    {
        _wallAnimation.Onstart();
    }
    private void Update()
    {
        _wallAnimation.OnUpdate();
    }
}
