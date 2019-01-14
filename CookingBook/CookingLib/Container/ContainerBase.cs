using CookingLib.Helper;
using CookingLib.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingLib.Container
{
    public abstract class ContainerBase<T> where T : ObjectBase
    {
        #region Member

        private string _directory;

        #endregion

        #region Properties

        public List<T> Entities
        {
            get
            {
                return _entities;
            }
        }
        protected List<T> _entities = new List<T>();

        #endregion

        #region Events

        public event EventHandler<EventArgs> Updated;

        #endregion

        #region Methods

        public void LoadAllEntities()
        {
            var directory = new DirectoryInfo(Path.Combine(System.Environment.CurrentDirectory, typeof(T).Name));
            if (directory.Exists)
            {
                foreach (var file in directory.GetFiles("*.srf"))
                {
                    var entity = ObjectBase.Load<T>(file.Name);

                    if (entity != null)
                    {
                        _entities.Add(entity);
                    }
                }

                if (Updated != null)
                {
                    Updated(this, new EventArgs());
                }
            }
        }

        public void AddNewEntity(T entity)
        {
            var now = DateTime.Now;

            if (entity.ID == 0)
            {
                var tempID = string.Format("{0}{1}{2}{4}{5}{6}", now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second, now.Millisecond);

                long id = Convert.ToInt64(tempID);
                entity.ID = id;
            }

            ObjectBase.Save(entity, entity.ID);

            if (!_entities.Any(x => x.ID == entity.ID))
            {
                _entities.Add(entity);
            }

            if (Updated != null)
            {
                Updated(this, new EventArgs());
            }
        }

        public void UpdateEntity(T entity)
        {
            if (_entities.Any(x => x.ID == entity.ID))
            {
                var update = GetEntity(entity.ID);
                Entities.Remove(update);
            }

            AddNewEntity(entity);
        }

        public void DeleteEntity(T entity)
        {
            ObjectBase.Delete(entity);

            _entities.RemoveAll(x => x.ID == entity.ID);

            if (Updated != null)
            {
                Updated(this, new EventArgs());
            }
        }

        public T GetEntity(long id)
        {
            return _entities.FirstOrDefault(x => x.ID == id);
        }

        public List<T> GetRecipes(List<long> enitityIDs)
        {
            return enitityIDs != null ? _entities.Where(x => enitityIDs.Contains(x.ID)).ToList() : new List<T>();
        }

        public void UploadData(List<long> ids = null)
        {
            if (FTPHelper.HasInstance)
            {
                var directory = new DirectoryInfo(Path.Combine(System.Environment.CurrentDirectory, typeof(T).Name));

                var files = directory.GetFiles("*srf").Where(x => ids == null 
                                                                  ||ids.Any(id => x.Name.Contains(id.ToString())))
                                                      .ToList();

                foreach (var file in files)
                {
                    FTPHelper.Instance.UploadFile(file.FullName, typeof(T).Name, file.Name);
                }
            }
        }

        public List<T> DownloadData()
        {
            var directory = typeof(T).Name;
            var data = new List<T>();

            try
            {
                if (FTPHelper.HasInstance)
                {
                    var files = FTPHelper.Instance.GetFilesInDirectory(directory);

                    foreach (var file in files)
                    {
                        FTPHelper.Instance.Download(directory, file);
                    }

                    var directoryInfo = new DirectoryInfo(Path.Combine(FTPHelper.LocalDirectory.FullName, directory));

                    foreach (var file in directoryInfo.GetFiles("*srf"))
                    {
                        var recipe = ObjectBase.Load<T>(file.FullName, true);
                        data.Add(recipe);

                        File.Delete(file.FullName);
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return data;
        }

        #endregion
    }
}
