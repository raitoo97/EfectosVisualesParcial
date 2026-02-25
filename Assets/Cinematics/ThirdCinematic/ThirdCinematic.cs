using System.Collections;
using UnityEngine;
public class ThirdCinematic
{
    private Transform _starshipPos;
    private Transform _starshipArrivePos;
    private MonoBehaviour _corutineHost;
    public ThirdCinematic(Transform starshipPos, Transform starshipArrivePos, MonoBehaviour corutineHost)
    {
        _starshipPos = starshipPos;
        _starshipArrivePos = starshipArrivePos;
        _corutineHost = corutineHost;
    }
    public void StartCinematic()
    {
        SoundManager.Instance?.PlayClip(SoundManager.Instance.GetAudioClip("AirSpace"), 1f, true);
        SoundManager.Instance?.PlayClip(SoundManager.Instance.GetAudioClip("StarShip"), 1f, true);
        _corutineHost.StartCoroutine(MoveStarship());
    }
    IEnumerator MoveStarship()
    {
        float elapsedTime = 0f;
        float duration = 20f;
        Vector3 startingPos = _starshipPos.position;
        Vector3 targetPos = _starshipArrivePos.position;
        while (elapsedTime < duration)
        {
            float t = Mathf.Clamp01(elapsedTime / duration);
            _starshipPos.position = Vector3.Lerp(startingPos, targetPos, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _starshipPos.position = targetPos;
    }
}
