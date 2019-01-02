using CookingLib.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CookingLib.Helper
{
    public class FTPHelper
    {
        #region Member

        private static FTPHelper _instance;

        private static string _address = ConfigHelper.Instance.FTPServer;

        private static string _user = string.Empty;

        private static string _password = string.Empty;

        public static DirectoryInfo LocalDirectory = new DirectoryInfo(Path.Combine(System.Environment.CurrentDirectory, "FTP"));

        #endregion

        #region Constructor

        public FTPHelper(string user, string password)
        {
            _user = user;
            _password = password;

            if (!LocalDirectory.Exists)
            {
                LocalDirectory.Create();
            }
        }

        #endregion        

        #region Properties

        public static FTPHelper Instance
        {
            get
            {
                return _instance;
            }
        }

        public static bool HasInstance
        {
            get
            {
                return _instance != null;
            }
        }

        public static void CreateInstance(string user, string password)
        {
            if (!HasInstance)
            {
                var validLogin = CheckLogin(user, password);

                if (validLogin)
                {
                    _instance = new FTPHelper(user, password);
                    _instance.CreateDirectories();
                }
            }
        }

        public static void KillInstance()
        {
            _instance = null;
            _user = null;
            _password = null;
        }

        #endregion

        #region Methods

        public string WriteLocalFile(string content, string targetDirectory, string fileName)
        {
            var directory = Path.Combine(LocalDirectory.FullName, targetDirectory);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var path = Path.Combine(directory, fileName);

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            using (StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8))
            {
                writer.WriteLine(content);

                writer.Close();
            };

            return path;
        }

        public void Download(string targetDirectory, string targetFile)
        {
            string file = string.Empty;

            if (string.IsNullOrEmpty(targetDirectory))
            {
                file = Path.Combine(_address, targetFile);
            }
            else
            {
                file = Path.Combine(_address, targetDirectory, targetFile);
            }

            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(file);
                request.Method = WebRequestMethods.Ftp.DownloadFile;

                request.Credentials = new NetworkCredential(_user, _password);

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                var content = reader.ReadToEnd();

                reader.Close();
                response.Close();

                WriteLocalFile(content, targetDirectory, targetFile);
            }
            catch (Exception ex)
            {
            }
        }

        public bool UploadFile(string sourceFile, string targetDirectory, string targetFile)
        {
            bool upload = false;
            string file = string.Empty;

            if (string.IsNullOrEmpty(targetDirectory))
            {
                file = Path.Combine(_address, targetFile);
            }
            else
            {
                file = Path.Combine(_address, targetDirectory, targetFile);
            }

            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(file);
                request.Method = WebRequestMethods.Ftp.UploadFile;

                request.Credentials = new NetworkCredential(_user, _password);

                // Copy the contents of the file to the request stream.
                StreamReader sourceStream = new StreamReader(sourceFile, Encoding.UTF8);
                byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
                sourceStream.Close();
                request.ContentLength = fileContents.Length;

                Stream requestStream = request.GetRequestStream();
                requestStream.Write(fileContents, 0, fileContents.Length);
                requestStream.Close();

                upload = true;
            }
            catch (Exception ex)
            {
                upload = false;
            }

            return upload;
        }

        public bool DirectoryExists(string targetPath)
        {
            bool exists = false;

            return exists;
        }

        public bool CreateDirectories()
        {
            bool created = false;

            try
            {
                var requestRecipe = SendWebRequest(Path.Combine(_address, nameof(Recipe)), WebRequestMethods.Ftp.MakeDirectory);
                var requestCategory = SendWebRequest(Path.Combine(_address, nameof(Category)), WebRequestMethods.Ftp.MakeDirectory);

                created = requestRecipe != null && requestCategory != null;
            }
            catch (Exception ex)
            {
                created = false;
            }

            return created;
        }

        public List<string> GetFilesInDirectory(string directory)
        {
            var files = new List<string>();

            try
            {
                var response = SendWebRequest(Path.Combine(_address, directory), WebRequestMethods.Ftp.ListDirectory);

                StreamReader reader = new StreamReader(response.GetResponseStream());

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        // Recipe/20181231521042.srf
                        if (line.Contains("/"))
                        {
                            line = line.Split('/').LastOrDefault();
                        }

                        files.Add(line);
                    }
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                files.Clear();
            }

            return files;
        }

        public bool DeleteFiles(string directory)
        {
            bool delete = true;

            try
            {
                var files = GetFilesInDirectory(directory);

                foreach (var file in files)
                {
                    if (delete)
                    {
                        delete = DeleteFile(file);
                    }
                }
            }
            catch (Exception ex)
            {
                delete = false;
            }

            return delete;
        }

        public bool DeleteFile(string file)
        {
            bool delete = true;

            file = Path.Combine(_address, file);

            try
            {
                var request = SendWebRequest(file, WebRequestMethods.Ftp.DeleteFile);

                delete = request != null;
            }
            catch (Exception ex)
            {
                delete = false;
            }

            return delete;
        }

        public bool RemoveDirectory(string folder)
        {
            bool removed = true;

            DeleteFiles(folder);

            return removed;
        }

        public static bool CheckLogin(string user, string password)
        {
            var success = false;

            try
            {
                var request = SendWebRequest(_address, WebRequestMethods.Ftp.ListDirectory, user, password);

                success = request != null;
            }
            catch (Exception ex)
            {
                success = false;
            }

            return success;
        }

        private static WebResponse SendWebRequest(string path, string request, string user = null, string password = null)
        {
            WebResponse response = null;

            try
            {
                var uri = new Uri(path);

                user = user ?? _user;
                password = password ?? _password;

                FtpWebRequest ftpWebRequest = (FtpWebRequest)FtpWebRequest.Create(uri);
                ftpWebRequest.UseBinary = true;
                ftpWebRequest.Credentials = new NetworkCredential(user, password);
                ftpWebRequest.Method = request;
                ftpWebRequest.Proxy = null;
                ftpWebRequest.KeepAlive = false;
                ftpWebRequest.UsePassive = false;
                response = ftpWebRequest.GetResponse();
            }
            catch (Exception ex)
            {
                response = null;
            }

            return response;
        }

        #endregion
    }
}
