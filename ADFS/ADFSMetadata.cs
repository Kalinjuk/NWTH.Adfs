using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace nwth.ADFS
{
    public class ADFSMetadata
    {
        private static List<ADFSMetadata> Instances = new List<ADFSMetadata>();
        private String _server;
        private ADFSKey[] Keys;
        private ILogger Logger;

        public ADFSMetadata(string Server,ILogger logger)
        {
            _server = Server;
            Logger = logger;
        }


        public IEnumerable<SecurityKey> SigningKeys
        {
            get
            {
                Logger?.LogDebug("Get Signing Keys collection");
                CheckKeys();

                foreach (var key in Keys)
                {
                    X509Certificate2 res = new X509Certificate2(Convert.FromBase64String(key.x5c[0]));
                    yield return new X509SecurityKey(res);
                }

            }
        }

        public X509Certificate2 GetCertificate(string ID)
        {
            Logger?.LogDebug("GetCertificate");
            try
            {
                CheckKeys();
                ADFSKey key = Keys.Where(p => p.x5t == ID ).FirstOrDefault();
                if (key == null) throw new Exception("Bad server certificate");
                X509Certificate2 res = new X509Certificate2(Convert.FromBase64String(key.x5c[0]));

                return res;
            } catch (Exception ex)
            {
                Logger?.LogError($"Error creating certificate from ADFS key {ex.Message}");
                throw ex;
            }
        }



        private void CheckKeys()
        {
            if (Keys == null) GetKeys();
        }

        private void GetKeys()
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    String S = wc.DownloadString(new Uri($"https://{_server}/adfs/discovery/keys"));

                    ADFSReply reply = JsonConvert.DeserializeObject<ADFSReply>(S);
                    Keys = reply?.keys;
                }
            } catch (Exception ex)
            {
                Keys = null;
                Logger?.LogError($"Error getting keys from ADFS server {ex.Message}");
                throw new Exception("Can't retrive metdatada from ADFS server.", ex);
            }
        }


        public static ADFSMetadata Instance(string Server,ILogger logger)
        {
            ADFSMetadata res = Instances.Where(p => p._server == Server).FirstOrDefault();
            if (res == null)
            {
                res = new ADFSMetadata(Server,logger);
                Instances.Add(res);
            }
            return res;
        }
    }

    
    public class ADFSReply
    {
        public ADFSKey[] keys { get; set; }
    }


    public class ADFSKey
    {
        public string kty { get; set; }
        public string use { get; set; }
        public string alg { get; set; }
        public string kid { get; set; }
        public string x5t { get; set; }
        public string n { get; set; }
        public string e { get; set; }
        public string[] x5c { get; set; }
    }
}

