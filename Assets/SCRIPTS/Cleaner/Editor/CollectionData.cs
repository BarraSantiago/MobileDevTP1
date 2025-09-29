using System;
using System.Collections.Generic;
using System.Linq;

namespace Cleaner.Editor
{
    [Serializable]
    public class CollectionData
    {
        public string fileGuid;
        public string fileName;
        public List<string> referenceGids = new();
        public DateTime timeStamp;
    }

    [Serializable]
    public class TypeDate
    {
        public string guid;
        public string fileName;
        public List<string> typeFullName = new();
        public string assemblly;
        public DateTime timeStamp;

        public Type[] types
        {
            get { return typeFullName.Select(c => Type.GetType(c)).ToArray(); }
        }

        public void Add(Type addtype)
        {
            assemblly = addtype.Assembly.FullName;
            var typeName = addtype.FullName;
            if (!typeFullName.Contains(typeName)) typeFullName.Add(typeName);
        }
    }

    public interface IReferenceCollection
    {
        void CollectionFiles();
        void Init(List<CollectionData> refs);
    }
}