using UnityEngine;
using System.Collections;
using System;
using System.Net;

public class DownloadProgress{

    public DownloadProgress(){
    }
    public DownloadProgress(String downloadURL) {
        //StartCoroutine(IEGetAssetBundleSize(downloadURL));
    }

    private float progress;
    private float contentLen;

    public float Progress {
        set {
            progress = value;
        }
        get {
            return progress;
        }
    }

    public float ContentLength {
        get { return contentLen; }
    }


    //public IEnumerator IEGetAssetBundleSize(String URL)
    //{
    //    WebRequest req = WebRequest.Create(URL);
    //    req.Method = "HEAD";
    //    yield return req;
    //    using (System.Net.WebResponse resp = req.GetResponse())
    //    {
    //        float.TryParse(resp.ContentLength.ToString(), out contentLen);
    //        yield return null;
    //    }

    //    //print("Asset bundle size: " + contentLen);
    //}

   

}
