using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Utilities : Singleton<UI_Utilities>
{
    [Header("Editable text sprites")]
    public List<string> TextSpritesKeys;
    public List<TextMeshProUGUI> TextSpritesValues;

    [Header("\nNext page change on button click")]
    [SerializeField] private List<Button> buttonsForNextPage;
    [SerializeField] private List<GameObject> nextPageToActivateValues;

    [Header("\nNext scene to load on button click")]
    [SerializeField] private List<Button> buttonsForNextScene;
    [SerializeField] private List<string> nextSceneNameValues;

    public Dictionary<string, TextMeshProUGUI> TextSprites;
    private Dictionary<GameObject, GameObject> nextPageToActivate;

    public void GoToNextPage(GameObject currentPage)
    {
        currentPage.SetActive(false);
        nextPageToActivate[currentPage].SetActive(true);
    }

    public void LoadNextScene(string sceneName)
    {
        if (sceneName == "Quit")
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;

#else
            Application.Quit();
#endif

            return;
        }

        else if (sceneName == "Resume")
        {
            PlayerController playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            playerController.TogglePauseMenu(true);

            return;
        }

        SceneManager.LoadScene(sceneName);
    }

    // Start is called before the first frame update
    void Start()
    {
        TextSprites = new Dictionary<string, TextMeshProUGUI>();
        nextPageToActivate = new Dictionary<GameObject, GameObject>();

        for (int i = 0; i < TextSpritesKeys.Count; i++)
        {
            TextSprites[TextSpritesKeys[i]] = TextSpritesValues[i];
        }

        for (int i = 0; i < buttonsForNextPage.Count; i++)
        {
            GameObject buttonParent = buttonsForNextPage[i].transform.parent.gameObject;

            nextPageToActivate[buttonsForNextPage[i].transform.parent.gameObject] = nextPageToActivateValues[i];
            buttonsForNextPage[i].onClick.AddListener(delegate { GoToNextPage(buttonParent); });
        }

        for (int i = 0; i < buttonsForNextScene.Count; i++)
        {
            string nextSceneName = nextSceneNameValues[i];

            buttonsForNextScene[i].onClick.AddListener(delegate { LoadNextScene(nextSceneName); });
        }
    }
}
