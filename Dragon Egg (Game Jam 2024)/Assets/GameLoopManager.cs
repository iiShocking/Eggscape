using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoopManager : MonoBehaviour
{
    [SerializeField] public bool hasKey;

    [SerializeField] public bool hasWings;

    [SerializeField] public bool hasEscaped;

    [SerializeField] GameObject winUI;
    [SerializeField] private TMP_Text text;

    [SerializeField] private GameObject player;
    [SerializeField] private float timeTaken;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasEscaped)
        {
            timeTaken += Time.deltaTime;
            return;
        }

        StartCoroutine(GameOver());
    }

    public void ObtainedKey() => hasKey = true;

    private void UsedKey() => hasKey = false;

    private IEnumerator GameOver()
    {
        //END GAME.. RESET?..
        winUI.SetActive(true);
        player.GetComponent<Rigidbody>().isKinematic = true;
        yield return new WaitForSeconds(2);
        text.text = "You escaped in " + Mathf.Round(timeTaken) + " seconds!";
        yield return new WaitForSeconds(3);
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
