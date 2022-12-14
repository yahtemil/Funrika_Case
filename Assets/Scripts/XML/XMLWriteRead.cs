using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class XMLWriteRead : ReadWrite
{
    string path;

    private void Start()
    {
        //path = Path.Combine(Application.streamingAssetsPath + "/data.xml");
#if UNITY_ANDROID
        path = System.IO.Path.Combine(Application.persistentDataPath, "data.xml");
#endif
#if UNITY_EDITOR
        path = Application.dataPath + "/XML/Data/data.xml";
#endif

        if (PlayerPrefs.GetInt("Write",0) == 0)
        {
            PlayerPrefs.SetInt("Write", 1);
            Write();
        }
        else
        {
            Read();
        }

    }

    public override void Read()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<string>));
        FileStream stream = new FileStream(path, FileMode.Open);
        ReadLetterList = serializer.Deserialize(stream) as List<string>;
        stream.Close();

        CubeAreaCreate.instance.letters = ReadLetterList;
    }

    public override void Write()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<string>));
        FileStream stream = new FileStream(path, FileMode.Create);
        serializer.Serialize(stream, SaveStringList);
        stream.Close();
        Read();
    }
}
