using System.Collections;
using UnityEngine;
public class FirstCinematic
{
    private Transform _waterDrop;
    private Vector3 _initPos;
    private Vector3 _finalPos;
    private MonoBehaviour _monoBehaviour;
    public FirstCinematic(Transform waterDrop,MonoBehaviour monoBehaviour)
    {
        _monoBehaviour = monoBehaviour;
        _waterDrop = waterDrop;
        _initPos = _waterDrop.position;
        _finalPos = new Vector3(_initPos.x, _initPos.y - 10f, _initPos.z);
    }
    public void StartCinematic()
    {
        _monoBehaviour.StartCoroutine(WaterDrop());
    }
    IEnumerator WaterDrop()
    {
        float elapsedTime = 0f;
        float duration =3f;
        yield return new WaitForSeconds(2f);
        while (elapsedTime < duration)
        {
            _waterDrop.position = Vector3.Lerp(_initPos, _finalPos, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        GameObject.Destroy(_waterDrop.gameObject);
    }
}
