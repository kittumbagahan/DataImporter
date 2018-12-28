using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using BeardedManStudios;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System;

public sealed class Launcher : CachedAssetBundleLoader
{
    [SerializeField]
    Button btnUpdate;
    [SerializeField]
    Button btnStart;
    [SerializeField]
    Button btnCancel;
    [SerializeField]
    UnityEngine.UI.Text txtAppVersion;
    [SerializeField]
    GameObject pnlLoading;

    [SerializeField]
    LauncherNetworking lNet;
    [SerializeField]
    ProgressBar pb;

    int findingServerTime = 0;
    private int retryCount;


    [SerializeField]
    string testUrl; //include extension name
    [SerializeField]
    int testVersion; 

    void Start()
    {
        lNet.Initialize();
        lNet.OnFindingServer += FindingServer;
        lNet.OnAssetBundleDataReceived += DownloadAssetBundle;

        txtAppVersion.text = "version " + PlayerPrefs.GetInt("productVersion") + "." + PlayerPrefs.GetInt("releaseVersion") + "." + PlayerPrefs.GetInt("bundleVersion");
        btnCancel.gameObject.SetActive(false);
        pnlLoading.SetActive (false);

    }

    public void CheckForUpdate()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            MessageBox.ins.ShowOk("No connection.", MessageBox.MsgIcon.msgError, null);
        }
        else
        {
            pb.gameObject.SetActive(true);
            btnUpdate.interactable = false;
            btnStart.interactable = false;
            btnCancel.gameObject.SetActive(true);
            //check for open network
            //if there is open network connect
            lNet.FindServer();
            OnDownload += pb.SetProgress;

            bundleURL = testUrl;
            bundleVersion = testVersion;
            //Caching.ClearCache();
            //PlayerPrefs.SetInt("bundleVersion", 0);
            
           
        }
    }

    public void CancelCheckUpdate()
    {
        //cancel will be disabled once the client succesfully connected to the server
        Debug.Log("Check for update cancelled.");
        btnCancel.gameObject.SetActive(false);
        btnUpdate.interactable = true;
        btnStart.interactable = true;
        pb.gameObject.SetActive(false);

    }

    private void DownloadAssetBundle(AssetBundleData assetBundleData)
    {
        Debug.Log("I am connected");

        MainThreadManager.Run (() =>
         {
            
             pb.TextTitle.text = "Connection success!";
             //wait for the server to send download url
             //automatically accept
             //check bundle version

             if (CheckBundleVersion(assetBundleData.version))
             {
                 //download assetbundle from url
                 StartCoroutine(IEDownload(assetBundleData));
                 //on download completed load the bundle
             }
             else
             {
                 pb.SetProgress(1);
                 MessageBox.ins.ShowOk("Version is up to date.", MessageBox.MsgIcon.msgInformation, 
                     () => {
                         StartGame();
                     });
             }
         });         
       

    }

    public void StartGame()
    {
     
        //PlayerPrefs.SetString ("bundleUrl", "https://www.dropbox.com/s/9inhox6owsspn40/assetbundlebookshelf.1?dl=1");
        //PlayerPrefs.SetInt ("bundleVersion", 1);
        //PlayerPrefs.SetString ("bundleUrl", "");
        //PlayerPrefs.SetInt ("bundleVersion", 0);
        LoadSceneFromAssetBundle loader = new LoadSceneFromAssetBundle (PlayerPrefs.GetString ("bundleUrl"), PlayerPrefs.GetInt ("bundleVersion"));
        loader.OnLoadSceneFail += FailLoadBookShelf;
        loader.OnLoadSceneSuccess += SuccessLoadBookShelf;
        print (PlayerPrefs.GetString ("bundleUrl"));
        StartCoroutine (loader.IEStreamAssetBundle ());
   
     
    }

    void FailLoadBookShelf ()
    {
        pnlLoading.SetActive(true);
        SceneManager.LoadSceneAsync ("BookShelf");
    }
    void SuccessLoadBookShelf ()
    {
        pnlLoading.SetActive (true);

    }

    IEnumerator IEDownload(AssetBundleData assetBundleData)
    {
        //*NOTE when we have downloaded versions bundle you can switch to previous version by setting the bundleVersion without changing bundleURL
        //*always download assetbundle with together with its url and version number
        yield return StartCoroutine(IEGetFromCacheOrDownload(assetBundleData.url, assetBundleData.version));
        if (success)
        {
            PlayerPrefs.SetInt("bundleVersion", assetBundleData.version); // reference importanteu for loading the bundle from cache this serves as its key
            PlayerPrefs.SetString("bundleUrl", assetBundleData.url); // reference importanteu for loading the bundle from cache this serves as its key
            pb.TextTitle.text = "Download Complete";
            OnDownload -= pb.SetProgress;
            print("Complete download");
            if (bundle.isStreamedSceneAssetBundle)
            {
                string[] scenePaths = bundle.GetAllScenePaths();
                string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePaths[0]);
                pnlLoading.SetActive (true);
                SceneManager.LoadSceneAsync(sceneName);
            }
        }
       
        {
            if (retryCount < 20)
            {
                pb.TextTitle.text = "Connection error... Retrying download...";
                yield return new WaitForSeconds(1f);
                //stop exisiting download coroutine here
                retryCount++;
                StartCoroutine(IEDownload(assetBundleData));
            }
            else
            {
                MessageBox.ins.ShowOk("error:9000\nINTERNET CONNECTION FAILED.", MessageBox.MsgIcon.msgError, ()=> { ConnectionErrMsgRetry (assetBundleData); });
            }
        }
    }


    private void FindingServer()
    {
        //Debug.Log(string.Format("finding server counting={0}", findingServerTime));
        pb.TextTitle.text = "Finding server " + findingServerTime.ToString() +"s";
        findingServerTime += 1;
        if (findingServerTime >= 60)
        {
            Debug.Log("What to do?");
        }
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            MessageBox.ins.ShowOk("No connection. Do something here.", MessageBox.MsgIcon.msgError, null);
            //stop lNet.FindServer();
        }
    }

    bool CheckBundleVersion(int serverBundleVersion)
    {
        if (serverBundleVersion > PlayerPrefs.GetInt("bundleVersion"))
        {
            return true;
        }
        return false;
    }

    #region Messages

    void ConnectionErrMsgRetry(AssetBundleData assetBundleData)
    {
        MessageBox.ins.ShowQuestion("Retry download?", MessageBox.MsgIcon.msgInformation, ()=>{ RetryDownload (assetBundleData); }, new UnityAction(BeforeCloseMsg));
    }

    void RetryDownload(AssetBundleData assetBundleData)
    {
        retryCount = 0;
        StartCoroutine(IEDownload(assetBundleData));
    }

    void BeforeCloseMsg()
    {
        MessageBox.ins.ShowOk("Try downloading your books later.", MessageBox.MsgIcon.msgInformation, null);
    }
    #endregion
}
