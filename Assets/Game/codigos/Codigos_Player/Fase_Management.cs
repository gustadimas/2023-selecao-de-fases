using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fase_Management : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Fase> faseInfo = new List<Fase>(4);
    public int level;

    void Awake()
    {
        GameObject[] loaders = GameObject.FindGameObjectsWithTag("Loader");
        if(loaders.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        level = 1;
        BeginProgress();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SceneLoadButton(string buttonName)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        //controller = GameObject.Find("Controller");

        switch (currentScene.name)
        {
            case "Menu_Title":
                if (buttonName == "Options_Button")
                {
                    SceneManager.LoadScene("Menu_Options");
                }
                if (buttonName == "Play_Button")
                {
                    // antes disso, se for a primeira vez jogando carregar cutscene
                    SceneManager.LoadScene("Menu_Levels");
                }
                break;
            case "Menu_Options":
            case "Menu_Levels":
                if (buttonName == "Back_Button")
                {
                    SceneManager.LoadScene("Menu_Title");
                }
                // colocar fases como botoes? por enquanto sim
                if (buttonName == "Fase_Lagoa_Button")
                {
                    //level = 1;
                    SceneManager.LoadScene("SampleScene" /* mudar nome depois!!!!! */);
                    
                }
                if (buttonName == "Fase_Pelourinho_Button")
                {
                    //level = 2;
                    SceneManager.LoadScene("Scene_Pelourinho");
                    
                }
                if (buttonName == "Fase_Feira_Button")
                {
                    //level = 3;
                    SceneManager.LoadScene("Scene_FeiraJoaquim");
                    
                }
                if (buttonName == "Fase_Dique_Button")
                {
                    //level = 4;
                    SceneManager.LoadScene("Scene_DiqueTororo");
                    
                }
                break;
            case "SampleScene": // mudar nome da cena depois
            case "Scene_CaveLagoa":
            case "Scene_Pelourinho":
            case "Scene_FeiraSaoJoaquim":
            case "Scene_DiqueTororo":
                // if(controller != GameObject.Find("Controller"))
                //     controller = GameObject.Find("Controller");

                if (buttonName == "Pause_Button")
                {
                    if (!SceneManager.GetSceneByName("Menu_Options_Fase").isLoaded)
                    {
                        PlayerControler player = FindObjectOfType<PlayerControler>();
                        player.currentState = PlayerControler.PlayerState.Event;
                        SceneManager.LoadScene("Menu_Options_Fase", LoadSceneMode.Additive);
                    }
                        
                        //controller.SetActive(false);
                }
                if (buttonName == "Next_Button")
                {
                    //levelSelected.levelInfo.completed = true;
                    SceneManager.LoadScene("Menu_Levels");
                }
                if (buttonName == "Menu_Button")
                {
                    SceneManager.LoadScene("Menu_Title");
                }
                if (buttonName == "Back_Button")
                {
                    //controller.SetActive(true);
                    if(SceneManager.GetSceneByName("Menu_Options_Fase").isLoaded)
                    {
                        PlayerControler player = FindObjectOfType<PlayerControler>();
                        player.currentState = PlayerControler.PlayerState.Walking;
                        SceneManager.UnloadSceneAsync("Menu_Options_Fase");
                    }
                    
                    
                }
                break;
            default:
                Debug.LogWarning("SceneLoad() was called from an unrecognized scene: " + currentScene.name);
                break;
        }
    }
    public void SceneLoadObject(string objectName)
    {
        // por enquanto na personagem mas pode criar um script separado depois
        Scene currentScene = SceneManager.GetActiveScene();
        switch (currentScene.name)
        {
            case "SampleScene": // mudar nome
                if (objectName == "CaveEntrance")
                {
                    Quest_Manager quest_Manager = FindObjectOfType<Quest_Manager>();
                    UpdateFase(quest_Manager.sideQuestsDone.Count);
                    SceneManager.LoadScene("Scene_CaveLagoa");
                }
                break;
            case "Menu_Levels":
                if (objectName == "Lagoa do Abaeté")
                {
                    SceneManager.LoadScene("SampleScene"); //mudar nome da cena
                }
                if (objectName == "Pelourinho")
                {
                    SceneManager.LoadScene("Scene_Pelourinho"); 
                }
                if (objectName == "Feira de São Joaquim")
                {
                    SceneManager.LoadScene("Scene_FeiraSaoJoaquim"); 
                }
                if (objectName == "Dique do Tororó")
                {
                    SceneManager.LoadScene("Scene_DiqueTororo"); 
                }
                break;
            default:
                Debug.LogWarning("SceneLoad() was called from an unrecognized scene: " + currentScene.name);
                break;
        }
    }
    public void SceneLoadEvent(string sceneToLoad)
    {
        switch (sceneToLoad)
        {
            case "FinishLevel":
                SceneManager.LoadScene("Finish_LevelScene", LoadSceneMode.Additive);
                break;
            case "MinigameDique":
                SceneManager.LoadScene("MinigamePedalinhoDique");
                break;
            default:
                Debug.LogWarning("No");
                break;
        }
    }
    public void UpdateFase(int objectivesCompleted)
    {
        switch (level)
        {
            case 1:
            faseInfo[0].objetivos = objectivesCompleted;
            //faseInfo[0].completed = true;
            break;
            case 2:
            faseInfo[1].objetivos = objectivesCompleted;
            //faseInfo[1].completed = true;
            break;
            case 3:
            faseInfo[2].objetivos = objectivesCompleted;
            //faseInfo[2].completed = true;
            break;
            case 4:
            faseInfo[3].objetivos = objectivesCompleted;
            //faseInfo[3].completed = true;
            break;
            default:
                print("vixe");
            break;
        }
    }
    void BeginProgress()
    {
        print("oi");
        for(int i = 0; i < 3; i++)
        {
            print(i.ToString());
            faseInfo.Add(new Fase{objetivos = 0, completou = false});
        }
        print(faseInfo.Count.ToString());
    }
    public IEnumerator LevelComplete()
    {
        yield return new WaitForEndOfFrame();
        switch(level)
        {
            case 1:
            faseInfo[0].completou = true;
            //faseInfo[0].completed = true;
            break;
            case 2:
            faseInfo[1].completou = true;
            //faseInfo[1].completed = true;
            break;
            case 3:
            faseInfo[2].completou = true;
            //faseInfo[2].completed = true;
            break;
            case 4:
            faseInfo[3].completou = true;
            //faseInfo[3].completed = true;
            break;
            default:
                print("vixe2");
            break;
        }
        SceneLoadEvent("FinishLevel"); //animaçao e componentes da fase aparecem, elemento de hud com objetivos concluidos em sequência (outro script)
    }
}
public class Fase
{
    public int objetivos;
    public bool completou;
}
