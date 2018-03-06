using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CaptureScreen : MonoBehaviour
{

    public float waitTime = 1f;
    float currTime = 0f;
    bool wait;

    private void Update()
    {
        currTime += Time.deltaTime;

        if(currTime > waitTime)
        {
            wait = false;
        }
    }

    public void Capture()
    {
        if (SingleTon.instance.dollList.isWindow)
            return;
        if (wait)
            return;
        currTime = 0f;
        wait = true;
        StartCoroutine(CaptureCor(false));
    }
    public void Share()
    {
        if (SingleTon.instance.dollList.isWindow)
            return;
        if (wait)
            return;
        currTime = 0f;
        wait = true;
        StartCoroutine(CaptureCor(true));
    }
    IEnumerator CaptureCor(bool share)
    {
        yield return new WaitForEndOfFrame();

        byte[] imageByte;
        Texture2D tex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, true);
        //RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 24);

        //foreach (Camera cam in cams)
        //{
        //    RenderTexture prev = cam.targetTexture;
        //    cam.targetTexture = rt;
        //    cam.Render();
        //    cam.targetTexture = prev;
        //}
        //RenderTexture.active = rt;

        tex.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);

        tex.Apply();

        imageByte = tex.EncodeToPNG();

        DestroyImmediate(tex);

#if UNITY_ANDROID

        if (share)
        {
            try
            {
                string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
                File.WriteAllBytes(filePath, imageByte);

                new NativeShare().AddFile(filePath).SetSubject("소녀전선 제대 시뮬레이터").SetText("- Made By Cosmos0").Share();
                SingleTon.instance.msg.SetMsg("공유");
            }
            catch
            {
                SingleTon.instance.msg.SetMsg("공유 에러");
            }

        }
        else
        {
            try
            {
                var permission = NativeGallery.SaveImageToGallery(imageByte, "GFSIM", "HOXY {0}.png");
                

                if (permission == NativeGallery.Permission.ShouldAsk)
                {
                    NativeGallery.RequestPermission();
                    SingleTon.instance.msg.SetMsg("캡쳐 실패!");
                }
                else if (permission == NativeGallery.Permission.Denied)
                {
                    SingleTon.instance.msg.SetMsg("캡쳐 실패!");
                }
                else
                    SingleTon.instance.msg.SetMsg("캡쳐 됨!");


            }
            catch
            {
                SingleTon.instance.msg.SetMsg("캡쳐 에러");
            }
        }
#elif UNITY_EDITOR
        if (share)
            SingleTon.instance.msg.SetMsg("PC버전에서 공유기능은 지원되지 않습니다.(캡쳐됨)");

        var path = Application.persistentDataPath + "/screenshot";

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        File.WriteAllBytes(path + "/HOXY" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".PNG", imageByte);
        SingleTon.instance.msg.SetMsg("screenshot 폴더에 캡쳐 됨!");
#elif UNITY_STANDALONE
        if(share)
            SingleTon.instance.msg.SetMsg("PC버전에서 공유기능은 지원되지 않습니다.(캡쳐됨)");

        var path = Application.dataPath + "/screenshot";

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        File.WriteAllBytes(path + "/HOXY" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".PNG", imageByte);
        SingleTon.instance.msg.SetMsg("screenshot 폴더에 캡쳐 됨!");

#endif
    }
    //public void GalleryRefresh(string fileToRefresh)
    //{
    //    if (Application.platform == RuntimePlatform.Android)
    //    {
    //        try
    //        {
    //            Debug.Log("file://" + fileToRefresh);
    //            AndroidJavaClass classPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    //            AndroidJavaObject objActivity = classPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    //            AndroidJavaClass classUri = new AndroidJavaClass("android.net.Uri");
    //            AndroidJavaObject objIntent = new AndroidJavaObject("android.content.Intent", new object[2] {
    //                "android.intent.action.MEDIA_SCANNER_SCAN_FILE",
    //                classUri.CallStatic<AndroidJavaObject> ("parse", "file://" + fileToRefresh)
    //            });

    //            objActivity.Call("sendBroadcast", objIntent);
    //        }
    //        catch
    //        {

    //            //GameObject.Find("console").GetComponent<Text>().text = "Exception: ";
    //            //GameObject.Find("console").GetComponent<Text>().text += e.Message;

    //        }
    //    }
    //}

}
