using System.Collections;
using UnityEngine;
public class FirstCinematic
{
    private Transform _waterDrop;
    private Vector3 _initPos;
    private Vector3 _finalPos;
    private MonoBehaviour _monoBehaviour;
    private Animator _bossAnimator;
    private Animator[] _soldiersAnimator;
    private Enemy[] _activateEnemies;
    private GameObject _bossDoorRef;
    public FirstCinematic(Transform waterDrop,MonoBehaviour monoBehaviour, Animator bossAnimator, Animator[] soldiersAnimator, Enemy[] activateEnemies,GameObject bossDoorRef)
    {
        _monoBehaviour = monoBehaviour;
        _waterDrop = waterDrop;
        _initPos = _waterDrop.position;
        _finalPos = new Vector3(_initPos.x, _initPos.y - 10f, _initPos.z);
        _bossAnimator = bossAnimator;
        _soldiersAnimator = soldiersAnimator;
        _activateEnemies = activateEnemies;
        _bossDoorRef = bossDoorRef;
    }
    public void StartCinematic()
    {
        _monoBehaviour.StartCoroutine(WaterDrop());
    }
    public void AcitvateBoss()
    {
        _bossAnimator.SetBool("StartCinematic", true);
    }
    public void ActivateSoldiers()
    {
        foreach (Animator animator in _soldiersAnimator)
        {
            animator.SetBool("StartCinematic", true);
        }
    }
    public void ActivateEnemies()
    {
        foreach (Enemy enemy in _activateEnemies)
        {
            enemy.gameObject.SetActive(true);
        }
    }
    public void DesactivateEnemies()
    {
        foreach (var enemy in _soldiersAnimator)
        {
            enemy.gameObject.SetActive(false);
        }
    }
    public void CloseBossDoor()
    {
        _bossDoorRef.GetComponent<Animator>()?.SetTrigger("CloseDoor");
    }
    IEnumerator WaterDrop()
    {
        SoundManager.Instance?.PlayCinematicClip(SoundManager.Instance.GetAudioClip("UnderWater"), 1f, false);
        float elapsedTime = 0f;
        float duration =6f;
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
