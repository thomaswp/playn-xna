using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Storage;
using playn.core;
using playn.core.json;
using System.IO;
using System.Collections;

namespace PlayNXNA
{
    public class XNAStorage : Storage, java.lang.Iterable
    {
        private const string STORAGE_NAME = "storage";
        private const string FILE_NAME = "storage.dat";
        private const int TIMEOUT = 10000000;

        private XNAPlatform platform;
        private StorageDevice device;
        private StorageContainer container;
        private Dictionary<string, string> map = new Dictionary<string, string>();

        public XNAStorage(XNAPlatform platform)
        {
            this.platform = platform;
        }

        public void init()
        {
            StorageDevice.BeginShowSelector((IAsyncResult target) =>
                {
                    device = StorageDevice.EndShowSelector(target);
                    device.BeginOpenContainer(STORAGE_NAME, (IAsyncResult callback) =>
                        {
                            container = device.EndOpenContainer(callback);
                        }, null);
                }, null);
            long time = DateTime.Now.Ticks;
            while (container == null && DateTime.Now.Ticks - time < TIMEOUT) ;

            if (container == null)
            {
                PlayN.log().warn("Failed to load storage!");
            }
            else
            {
                LoadStorage();
            }

        }

        private void LoadStorage()
        {
            if (container != null && container.FileExists(FILE_NAME))
            {
                map.Clear();
                try
                {
                    StreamReader sr = new StreamReader(container.OpenFile(FILE_NAME, FileMode.Open));
                    Json.Object json = PlayN.json().parse(sr.ReadToEnd());
                    java.util.Iterator iterator = json.keys().iterator();
                    while (iterator.hasNext())
                    {
                        string key = (string) iterator.next();
                        map.Add(key, json.getString(key));
                    }
                }
                catch (Exception e)
                {
                    PlayN.log().warn("Error reading storage", e);
                    try
                    {
                        container.DeleteFile(FILE_NAME);
                    }
                    catch { }
                }
            }
        }

        private void SaveStorage()
        {
            if (container != null)
            {
                try
                {
                    Stream stream = container.OpenFile(FILE_NAME, System.IO.FileMode.Create);
                    StreamWriter sw = new StreamWriter(stream);
                    Json.Object json = PlayN.json().createObject();
                    foreach (string key in map.Keys)
                    {
                        json.put(key, map[key]);
                    }
                    Json.Writer writer = PlayN.json().newWriter();
                    writer.@object(json);
                    sw.Write(writer.write());
                    sw.Close();
                }
                catch (Exception e)
                {
                    PlayN.log().warn("Error writing storage", e);
                }
            }
        }

        public string getItem(string str)
        {
            if (!map.ContainsKey(str)) return null;
            return map[str];
        }

        public bool isPersisted()
        {
            return container != null;
        }

        public java.util.Iterator iterator()
        {
            return new MapIterable(map);
        }


        class MapIterable : java.util.Iterator
        {
            private List<string> keys;
            private int index;

            public MapIterable(Dictionary<string, string> map)
            {
                keys = map.Keys.ToList();
            }

            public bool hasNext()
            {
                return index < keys.Count;
            }

            public object next()
            {
                return keys[index++];
            }

            public void remove()
            {
                throw new NotImplementedException();
            }
        }

        public java.lang.Iterable keys()
        {
            return this;
        }

        public void removeItem(string str)
        {
            removeItem(str, true);
        }

        private void removeItem(string str, bool push)
        {
            map.Remove(str);
            if (push) SaveStorage();
        }

        public void setItem(string key, string value)
        {
            setItem(key, value, true);
        }

        private void setItem(string key, string value, bool push)
        {
            map[key] = value;
            if (push) SaveStorage();
        }

        public Storage.Batch startBatch()
        {
            return new XNABatch(this);
        }

        public class XNABatch : Storage.Batch
        {
            XNAStorage storage;

            public XNABatch(XNAStorage storage)
            {
                this.storage = storage;
            }

            public void commit()
            {
                storage.SaveStorage();
            }

            public void removeItem(string key)
            {
                storage.removeItem(key, false);
            }

            public void setItem(string key, string value)
            {
                storage.setItem(key, value, false);
            }
        }
    }
}
