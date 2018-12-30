using System;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;

namespace CookingLib.Objects
{
    public abstract class ObjectBase : INotifyPropertyChanged
    {
        #region Member     

        #endregion

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Informiert über eine Propertyänderung
        /// </summary>
        /// <param name="property"></param>
        protected virtual void OnPropertyChanged(string property)
        {
            if ((PropertyChanged != null))
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        #endregion

        #region Properties

        public long ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                OnPropertyChanged("ID");
            }
        }
        private long _id;

        #endregion

        #region Methods

        public static T Load<T>(string name)
        {
            T retVal = default(T);

            var directory = typeof(T).Name;

            try
            {
                string fileName = Path.Combine(Environment.CurrentDirectory, directory, name);
                FileInfo file = new FileInfo(fileName);

                if (file.Exists)
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    FileStream stream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    retVal = (T)serializer.Deserialize(stream);
                    stream.Close();
                }
            }
            catch (Exception ex)
            {

            }

            return retVal;
        }

        /// <summary>
        /// Einstellungen speichern
        /// </summary>
        public static bool Save(object value, long id)
        {
            bool retVal = false;

            try
            {
                var type = value.GetType();
                var directory = type.Name;

                if (!Directory.Exists(Path.Combine(Environment.CurrentDirectory, directory)))
                {
                    Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, directory));
                }

                string file = string.Format(@"{0}\{1}", Path.Combine(Environment.CurrentDirectory, directory), id + ".srf");

                XmlSerializer serializer = new XmlSerializer(type);
                FileStream stream = new FileStream(file, FileMode.Create);
                serializer.Serialize(stream, value);
                stream.Close();

                retVal = true;
            }
            catch (Exception ex)
            {
                retVal = false;
            }

            return retVal;
        }

        public static bool Delete(ObjectBase value)
        {
            bool retVal = false;

            try
            {
                var type = value.GetType();
                var directory = type.Name;

                var path = Path.Combine(directory, string.Format("{0}.srf", value.ID));

                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                retVal = true;
            }
            catch (Exception ex)
            {
                retVal = false;
            }

            return retVal;
        }

        #endregion
    }
}
