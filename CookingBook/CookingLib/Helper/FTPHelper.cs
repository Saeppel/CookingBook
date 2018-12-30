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

        private string _adress = "FTP://saeppelnet.ddns.net/Kochbuch/";

        private string _user = "KochbuchMaster";

        private string _password = "CookingIsAwesome";

        private DirectoryInfo _localDirectory = new DirectoryInfo(Path.Combine(System.Environment.CurrentDirectory, "FTP"));

        #endregion

        #region Constructor

        public FTPHelper()
        {
            if (!_localDirectory.Exists)
            {
                _localDirectory.Create();
            }
        }

        #endregion        

        #region Properties

        public FTPHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new FTPHelper();
                }

                return _instance;
            }
        }

        #endregion

        #region Methods

        public string WriteLocalFile(string content, string fileName)
        {
            var path = Path.Combine(_localDirectory.FullName, fileName);

            using (StreamWriter writer = new StreamWriter(path, false, Encoding.Default))
            {
                writer.WriteLine(content);

                writer.Close();
            };

            return path;
        }

        public string Download(string targetDirectory, string targetFile)
        {
            string content = string.Empty;
            string file = string.Empty;

            if (string.IsNullOrEmpty(targetDirectory))
            {
                file = Path.Combine(_adress, targetFile);
            }
            else
            {
                file = Path.Combine(_adress, targetDirectory, targetFile);
            }

            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(file);
                request.Method = WebRequestMethods.Ftp.DownloadFile;

                request.Credentials = new NetworkCredential(_user, _password);

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);
                content = reader.ReadToEnd();

                reader.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                content = null;
            }

            return content;
        }

        public bool UploadFile(string sourceFile, string targetDirectory, string targetFile)
        {
            bool upload = false;
            string file = string.Empty;

            if (string.IsNullOrEmpty(targetDirectory))
            {
                file = Path.Combine(_adress, targetFile);
            }
            else
            {
                file = Path.Combine(_adress, targetDirectory, targetFile);
            }

            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(file);
                request.Method = WebRequestMethods.Ftp.UploadFile;

                request.Credentials = new NetworkCredential(_user, _password);

                // Copy the contents of the file to the request stream.
                StreamReader sourceStream = new StreamReader(sourceFile);
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

        public bool CreateDirectory(string folder)
        {
            bool created = false;

            try
            {
                FtpWebRequest ftpWebRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri(Path.Combine(_adress, folder)));
                ftpWebRequest.UseBinary = true;
                ftpWebRequest.Credentials = new NetworkCredential(_user, _password);
                ftpWebRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
                ftpWebRequest.Proxy = null;
                ftpWebRequest.KeepAlive = false;
                ftpWebRequest.UsePassive = false;
                ftpWebRequest.GetResponse();

                created = true;
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

            try
            {
                delete = true;
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

        #endregion
    }
}
